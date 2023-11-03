using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(menuName = "InputManager")]
public class InputManager : ScriptableObject, GameControls.ICommonActions, GameControls.ISideActions, GameControls.ITopdownActions
{

    public event Action<Vector2> TopdownMoveEvent;
    public event Action<float> SideMoveEvent;
    public event Action JumpBeginEvent;
    public event Action JumpEndEvent;
    public event Action InteractEvent;

    private GameControls gameControls;

    private void OnEnable()
    {
        if( gameControls == null )
        {
            this.gameControls = new GameControls();

            gameControls.Common.SetCallbacks(this);
            gameControls.Topdown.SetCallbacks(this);
            gameControls.Side.SetCallbacks(this);
        }

    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            InteractEvent?.Invoke();
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            JumpBeginEvent?.Invoke();
        }
        if(context.phase == InputActionPhase.Canceled)
        {
            JumpEndEvent?.Invoke();
        }
    }

    public void OnSideMovement(InputAction.CallbackContext context)
    {
        SideMoveEvent.Invoke(context.ReadValue<float>());
    }

    public void OnTopdownMovement(InputAction.CallbackContext context)
    {
        TopdownMoveEvent?.Invoke(context.ReadValue<Vector2>());
    }
}
