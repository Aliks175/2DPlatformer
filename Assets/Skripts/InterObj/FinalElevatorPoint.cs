using UnityEngine;

public class FinalElevatorPoint : ActiveInterObj
{
    [SerializeField] private Elevator eliwate;

    public override void Active()
    {
        eliwate.ChangeMoveLine();
    }
}
