using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _goPref;
    [SerializeField] private float _force;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _centreShot;
    [SerializeField] private Collider2D _playerColl;
    private Collider2D _bulletColl;
    private GameObject go;
    private Rigidbody2D goRig;
    private bool directionFlipX;

    private void OnEnable()
    {
        PlayerControler.OnFlip += SaveFlip;
        InputControler.OnFIRE_1 += AnimThrow;
    }

    private void OnDisable()
    {
        PlayerControler.OnFlip -= SaveFlip;
        InputControler.OnFIRE_1 -= AnimThrow;
    }

    private void AnimThrow()
    {
        _animator.SetTrigger(ConstAnimation.Throw);
    }

    private void SaveFlip(bool flip)
    {
        directionFlipX = flip;
    }

    private void Fire()
    {
        go = Instantiate(_goPref, _centreShot.position, Quaternion.identity);
        _bulletColl = go.GetComponent<Collider2D>();
        Physics2D.IgnoreCollision(_bulletColl, _playerColl);
        goRig = go.GetComponent<Rigidbody2D>();
        if (directionFlipX)
        {
            goRig.velocity = new Vector2(_force * -1, goRig.velocity.y);
        }
        else
        {
            goRig.velocity = new Vector2(_force * 1, goRig.velocity.y);
        }
    }
}
