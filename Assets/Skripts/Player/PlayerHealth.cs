using System;
using UnityEngine;

public class PlayerHealth : Health
{
    public static event Action<float> ChangerHp;
    public static event Action OnDead;

    public override void TakeDamage(float value)
    {
        if (value > 0)
        {
            TakeHeal(value);
        }
        else
        {
            currentHealth += value;
        }
        Debug.Log(currentHealth);
        SendHP();
        CheckAlife(value);
    }

    private void TakeHeal(float value)
    {
        if (currentHealth + value > _maxHealth)
        {
            currentHealth = _maxHealth;
        }
        else
        {
            currentHealth += value;
        }
    }

    private void SendHP()
    {
        ChangerHp?.Invoke(currentHealth / _maxHealth);
    }

    protected override void Die()
    {
        OnDead?.Invoke();
    }
}
