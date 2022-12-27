using System;
using UnityEngine;

public class InputControler : MonoBehaviour
{
    float horizontal;
    public static event Action<float> OnMove;
    public static event Action<float> OnFIRE_1;
    public static event Action OnJump;
    public static event Action OnFall;

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

        if (Input.GetButtonUp(ConstValueInputOptions.JUMP))
        {
            OnFall?.Invoke();
        }

        if (Input.GetButtonDown(ConstValueInputOptions.FIRE_1))
        {
            OnFIRE_1?.Invoke(horizontal);
        }
    }

    private void FixedUpdate()
    {
        if (horizontal != 0)
        {
            OnMove?.Invoke(horizontal);
        }
    }
}
