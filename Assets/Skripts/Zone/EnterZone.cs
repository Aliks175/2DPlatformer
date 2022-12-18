using UnityEngine;

public class EnterZone : MonoBehaviour
{
    [SerializeField] private GameObject _targetGo;
    [SerializeField] private ActiveInterObj activeInterObj;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject == _targetGo)
        {
            activeInterObj.Active();
        }
    }
}
