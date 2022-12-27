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
    [Header("Delay Settings")]
    [SerializeField] private float _timeDelayToContact;
    [SerializeField] private float _timeDelayAfterContact;
    [SerializeField] private float _coefficientFalls = 1.5f;
    [Header("Layer Settings")]
    [SerializeField] private Collider2D _playerCollider;
    [SerializeField] private LayerMask _layerGraund;
    private Rigidbody2D rig;
    private byte valueRemainingJump;
    private bool isgroundContact;
    private bool isnoUseJump;
    private bool isjumpAfterContact;
    private bool inair;

    private void Awake()
    {
        InputControler.OnMove += ChangePosPlayer;
        InputControler.OnJump += CheckGround;
        InputControler.OnFall += FallButtonClick;
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
                JumpPlayer();
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
        rig.velocity = new Vector2(_animationCurve.Evaluate(moveLine), rig.velocity.y);
    }

    private void RecoverValueJump()
    {
        valueRemainingJump = _valueJump;
    }

    private void JumpPlayer()
    {
        if (valueRemainingJump > 0)
        {
            isnoUseJump = false;
            --valueRemainingJump;
            rig.velocity = new Vector2(rig.velocity.x, _maxHeightJump);
        }
    }

    private void FallButtonClick()
    {
        if (rig.velocity.y > 0)
        {
            rig.velocity = new Vector2(rig.velocity.x, rig.velocity.y / _coefficientFalls);
        }
    }

    private void CheckGround()
    {
        if (_valueJump > 1)
        {
            JumpPlayer();
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
            JumpPlayer();
        }
        else
        {
            if (isjumpAfterContact && isnoUseJump)
            {
                JumpPlayer();
            }
            StartCoroutine(TimeToContact());
        }
    }
}
