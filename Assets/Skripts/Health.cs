using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] protected float _maxHealth;
    [SerializeField] private Die _die;
    protected float currentHealth;

    private void Start()
    {
        currentHealth = _maxHealth;
    }

    public virtual void TakeDamage(float enterDamage)
    {
        currentHealth += enterDamage;
        CheckAlife(enterDamage);
    }

    protected void CheckAlife(float enterDamage)
    {
        if (currentHealth + enterDamage > 0)
        {
        }
        else
        {
            currentHealth = 0;
            Die();
        }
    }

    protected virtual void Die()
    {
        _die.OnDead();
    }
}
