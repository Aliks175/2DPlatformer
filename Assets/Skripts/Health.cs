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

    public virtual void TakeDamage(float value)
    {
        currentHealth -= value;
        CheckAlife();
    }

    protected void CheckAlife()
    {
        if (currentHealth > 0)
        {
           
        }
        else
        {
            
            Die();
        }
    }

    protected virtual void Die()
    {
        _die.OnDead();
    }
}
