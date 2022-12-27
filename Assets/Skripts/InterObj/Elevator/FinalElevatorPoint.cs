using UnityEngine;

public class FinalElevatorPoint : ActiveInterObj
{
    [SerializeField] private Elevator _elevator;

    public override void Active()
    {
        _elevator.ChangeMoveLine();
    }
}
