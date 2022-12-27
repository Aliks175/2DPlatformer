using Unity.VisualScripting;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] private GameObject _go;
    [Serialize] public SliderJoint2D sliderJoint;
    private JointMotor2D joint;
    private const int directionModifier = -1;

    void Start()
    {
        sliderJoint = _go.GetComponent<SliderJoint2D>();
    }

    public void ChangeMoveLine()
    {
        joint = sliderJoint.motor;
        joint.motorSpeed = sliderJoint.motor.motorSpeed * directionModifier;
        sliderJoint.motor = joint;
    }
}
