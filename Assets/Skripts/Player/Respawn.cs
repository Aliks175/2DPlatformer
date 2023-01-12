using UnityEngine;

public class Respawn : Die
{
    [SerializeField] private Animator _animator;

    private void OnEnable()
    {
        PlayerHealth.OnDead += OnDead;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDead -= OnDead;
    }

    public override void OnDead()
    {
        _animator.SetTrigger(ConstAnimation.Die);
    }
}
