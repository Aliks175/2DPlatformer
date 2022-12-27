using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject _goPref;
    [SerializeField] private float _force;
    [SerializeField] private Transform _centreShot;
    private GameObject go;
    private Rigidbody2D goRig;

    private void OnEnable()
    {
        InputControler.OnFIRE_1 += Fire;
    }

    private void OnDisable()
    {
        InputControler.OnFIRE_1 -= Fire;
    }

    private void Fire(float direction)
    {
        go = Instantiate(_goPref, _centreShot.position, Quaternion.identity);
        goRig = go.GetComponent<Rigidbody2D>();
        if (direction >= 0)
        {
            goRig.velocity = new Vector2(_force * 1, goRig.velocity.y);
        }
        else
        {
            goRig.velocity = new Vector2(_force * -1, goRig.velocity.y);
        }
    }
}
