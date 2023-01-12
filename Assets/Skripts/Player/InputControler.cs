using System;
using UnityEngine;

public class InputControler : MonoBehaviour
{
    float horizontal;
    public static event Action<float> OnMove;
    public static event Action OnFIRE_1;
    public static event Action OnJump;

    private void Update()
    {
        ChangeValueAxis();
        ChekPress();
    }

    private void ChangeValueAxis()
    {
        horizontal = Input.GetAxis(ConstValueInputOptions.HORIZONTAL_AXIS);
    }

    private void ChekPress()
    {
        if (Input.GetButtonDown(ConstValueInputOptions.JUMP))
        {
            OnJump?.Invoke();
        }
        if (Input.GetButtonDown(ConstValueInputOptions.FIRE_1))
        {
            OnFIRE_1?.Invoke();
        }
    }

    private void FixedUpdate()
    {
        OnMove?.Invoke(horizontal);
    }
}
