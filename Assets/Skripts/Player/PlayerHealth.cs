using System;

public class PlayerHealth : Health
{
    public static event Action<float> OnDamage;
    public static event Action<float> OnHeal;
    public static event Action OnDead;

    public override void TakeDamage(float value)
    {
        OnDamage?.Invoke(value);
        currentHealth -= value;
        CheckAlife();
    }

    public void TakeHeal(float value)
    {
        if (currentHealth + value > _maxHealth)
        {
            currentHealth = _maxHealth;
        }
        else
        {
            currentHealth += value;
        }
        OnHeal?.Invoke(value);
    }

    protected override void Die()
    {
        OnDead?.Invoke();
    }
}
