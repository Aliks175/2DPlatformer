using UnityEngine;

public class ChangerHp : MonoBehaviour
{
    [SerializeField] private float _damage;
    [SerializeField] private bool _destroy;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health health))
        {
            health.TakeDamage(_damage);
        }

        if (_destroy)
        {
            Destroy(gameObject);
        }
    }
}
