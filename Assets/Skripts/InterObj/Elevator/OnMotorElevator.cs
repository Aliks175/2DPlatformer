using UnityEngine;

public class OnMotorElevator : ActiveInterObj
{
    [SerializeField] private BoxCollider2D _coll2D;

    public override void Active()
    {
        _coll2D.enabled = true;
    }
}
