using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(InputControler))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControler : MonoBehaviour
{
    [Header("Mover Options")]
    [SerializeField] private float _speed;
    [SerializeField] private float _maxHeightJump;
    [SerializeField] private byte _valueJump = 1;
    [SerializeField] private AnimationCurve _animationCurve;
    [Header("Animator Settings")]
    [SerializeField] private Animator _animator;
    [SerializeField] private SpriteRenderer _playerSprite;
    [Header("Delay Settings")]
    [SerializeField] private float _timeDelayToContact;
    [SerializeField] private float _timeDelayAfterContact;
    [Header("Layer Settings")]
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private LayerMask _layerGraund;
    private Rigidbody2D rig;
    private byte valueRemainingJump;
    private bool isgroundContact;
    private bool isnoUseJump;
    private bool isjumpAfterContact;
    private bool inair;
    public static event Action<bool> OnFlip;

    private void OnEnable()
    {
        InputControler.OnMove += ChangePosPlayer;
        InputControler.OnJump += CheckGround;
    }

    private void OnDisable()
    {
        InputControler.OnMove -= ChangePosPlayer;
        InputControler.OnJump -= CheckGround;
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        RecoverValueJump();
    }

    private void FixedUpdate()
    {
        isgroundContact = _playerCollider.IsTouchingLayers(_layerGraund);
        if (isgroundContact)
        {
            _animator.SetBool(ConstAnimation.Fall, false);
            if (!inair)
            {
                RecoverValueJump();
                isnoUseJump = true;
                inair = true;
            }
        }
        else
        {
            if (inair)
            {
                _animator.SetBool(ConstAnimation.Fall, true);
                inair = false;
                StartCoroutine(TimeAfterContact());
            }
        }
    }

    private IEnumerator TimeToContact()
    {
        for (int i = 0; i < 4; i++)
        {
            if (isgroundContact)
            {
                AnimJumpPlayer();
                StopCoroutine(TimeToContact());
            }
            yield return new WaitForSeconds(_timeDelayToContact);
        }
    }

    private IEnumerator TimeAfterContact()
    {
        for (int i = 0; i < 4; i++)
        {
            yield return new WaitForSeconds(_timeDelayAfterContact);
            if (isgroundContact)
            {
                isjumpAfterContact = false;
                StopCoroutine(TimeAfterContact());
            }
            else
            {
                isjumpAfterContact = true;
            }
        }
        isjumpAfterContact = false;
    }

    private void ChangePosPlayer(float moveLine)
    {
        if (Mathf.Abs(moveLine) > 0.01f)
        {
            _animator.SetBool(ConstAnimation.Mover, true);
            rig.velocity = new Vector2(_animationCurve.Evaluate(moveLine), rig.velocity.y);
            float changeX = rig.velocity.x;
            if (changeX > 0.1)
            {
                _playerSprite.flipX = false;
            }
            else if (changeX < -0.1)
            {
                _playerSprite.flipX = true;
            }
            OnFlip?.Invoke(_playerSprite.flipX);
        }
        else
        {
            _animator.SetBool(ConstAnimation.Mover, false);
        }
    }

    private void RecoverValueJump()
    {
        valueRemainingJump = _valueJump;
    }

    private void AnimJumpPlayer()
    {
        if (valueRemainingJump > 0)
        {
            _animator.SetTrigger(ConstAnimation.Jump);
            isnoUseJump = false;
            --valueRemainingJump;
        }
    }

    private void Jump()
    {
        rig.velocity = new Vector2(rig.velocity.x, _maxHeightJump);
    }



    private void CheckGround()
    {
        if (_valueJump > 1)
        {
            AnimJumpPlayer();
        }
        else
        {
            CheckContact();
        }
    }

    private void CheckContact()
    {
        if (isgroundContact)
        {
            AnimJumpPlayer();
        }
        else
        {
            if (isjumpAfterContact && isnoUseJump)
            {
                AnimJumpPlayer();
            }
            StartCoroutine(TimeToContact());
        }
    }
}
