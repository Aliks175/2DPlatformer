using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField] private Image _hpBar;
    [SerializeField] private Image _hpBackground;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage;
    [SerializeField] private float _timeDelayDamage = 0.2f;
    [SerializeField] private float _timeDelayPrintHp = 0.02f;
    private float newHp;

    private void OnEnable()
    {
        PlayerHealth.OnDamage += ChangeHp;
        PlayerHealth.OnDead += NullBar;
        PlayerHealth.OnHeal += ChangeHp;
    }

    private void OnDisable()
    {
        PlayerHealth.OnDamage -= ChangeHp;
        PlayerHealth.OnDead -= NullBar;
        PlayerHealth.OnHeal -= ChangeHp;
    }

    private void Start()
    {
        _hpBar.fillAmount = 1f;
        newHp = _hpBar.fillAmount;
    }

    private IEnumerator TimeToAnimation()
    {
        yield return new WaitForSeconds(_timeDelayDamage);
        while (_hpBackground.fillAmount != newHp)
        {
            _hpBackground.fillAmount = Mathf.Lerp(_hpBackground.fillAmount, newHp, _speed);
            if (Mathf.Approximately(_hpBackground.fillAmount, newHp))
            {
                Debug.Log("break");
                break;
            }
            yield return new WaitForSeconds(_timeDelayPrintHp);
        }
    }

    private void ResetHpbar()
    {
        _hpBar.fillAmount = 1f;
        newHp = _hpBar.fillAmount;
    }

    private void ChangeHp(float valueChange)
    {
        newHp += valueChange / 100;
        _hpBar.fillAmount = newHp;
        StartCoroutine(TimeToAnimation());
    }

    private void NullBar()
    {
        _hpBar.fillAmount = 0;
        _hpBackground.fillAmount = 0;
    }
}
