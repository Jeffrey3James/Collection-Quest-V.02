using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using static PlayerControls;

public interface IInputReader {
    Vector2 Direction { get; }
    void EnablePlayerActions();
}

[CreateAssetMenu(fileName = "InputReader", menuName = "InputReader")]
public class InputReader : ScriptableObject, IPlayerActions, IInputReader {

    public event UnityAction<Vector2> Move = delegate { };
    public event UnityAction<Vector2, bool> Look = delegate { };
    public event UnityAction EnableMouseControlCamera = delegate { };
    public event UnityAction DisableMouseControlCamera = delegate { };
    public event UnityAction<bool> Dash = delegate { };
  public event UnityAction Jump = delegate { };
    public event UnityAction LightAttack = delegate { };

    public event UnityAction HeavyAttackCharge = delegate { };
    public event UnityAction HeavyAttackRelease = delegate { };
    public event UnityAction<RaycastHit> Click = delegate { };

    public event UnityAction AbilitySlotOne = delegate { };
    public event UnityAction AbilitySlotTwo = delegate { };
    public event UnityAction AbilitySlotThree = delegate { };
    public event UnityAction AbilitySlotFour = delegate { };
    public event UnityAction AbilitySlotFive = delegate { };

    public PlayerControls controls;

    /*public bool IsJumpKeyPressed() => controls.Player.Jump.IsPressed();*/
    
    public Vector2 Direction => controls.Player.Move.ReadValue<Vector2>();
    public Vector2 LookDirection => controls.Player.Look.ReadValue<Vector2>();

    public void EnablePlayerActions() {
        if (controls == null) {
            controls = new PlayerControls();
            controls.Player.SetCallbacks(this);
      Debug.Log(" Player Action Enabled");
        }
        controls.Enable();
    }

    public void OnMove(InputAction.CallbackContext context) {
    Move.Invoke(context.ReadValue<Vector2>());
    }

    public void OnLook(InputAction.CallbackContext context) {
        Look.Invoke(context.ReadValue<Vector2>(), IsDeviceMouse(context));
    }

    bool IsDeviceMouse(InputAction.CallbackContext context) {
        return context.control.device is Mouse;
    }

    public void OnFire(InputAction.CallbackContext context) {
        if (context.phase == InputActionPhase.Started) {
            if (IsDeviceMouse(context)) {
                var ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
                if (Physics.Raycast(ray.origin, ray.direction, out var hit, 100)) {
                    Click.Invoke(hit);
                }
            }
        }
    }

    public void OnMouseControlCamera(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Started:
                EnableMouseControlCamera.Invoke();
                break;
            case InputActionPhase.Canceled:
                DisableMouseControlCamera.Invoke();
                break;
        }
    }

    public void OnRun(InputAction.CallbackContext context) {
        switch (context.phase) {
            case InputActionPhase.Started:
                Dash.Invoke(true);
                break;
            case InputActionPhase.Canceled:
                Dash.Invoke(false);
                break;
        }
    }

    public void OnLightAttack(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started)
        {
            
            LightAttack.Invoke();
        }
    }

    public void OnHeavyAttack(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Started:
                HeavyAttackCharge.Invoke();
                break;
            case InputActionPhase.Performed:
                HeavyAttackRelease.Invoke();
                break;
        }
    }

    public void OnAbilitySlotOne(InputAction.CallbackContext context) 
    {
        if (context.phase != InputActionPhase.Started)
        {
            Debug.Log("Used Ability Slot One");
            AbilitySlotOne.Invoke();
        }
    }
    public void OnAbilitySlotTwo(InputAction.CallbackContext context) 
    {
        if (context.phase != InputActionPhase.Started)
        {
            Debug.Log("Used Ability Slot Two");
            AbilitySlotTwo.Invoke();
        }
    }
    public void OnAbilitySlotThree(InputAction.CallbackContext context) 
    {
        /*if (context.phase != InputActionPhase.Started)
        {
            Debug.Log("Used Ability Slot Three");
            AbilitySlotThree.Invoke();
        }*/
    }
    public void OnAbilitySlotFour(InputAction.CallbackContext context) 
    {
        if (context.phase != InputActionPhase.Started)
        {
            Debug.Log("Used Ability Slot Four");
            AbilitySlotThree.Invoke();
        }
    }
    public void OnAbilitySlotFive(InputAction.CallbackContext context) 
    {
        if (context.phase != InputActionPhase.Started)
        {
            Debug.Log("Used Ability Slot Five");
            AbilitySlotFive.Invoke();
        }
    }

  public void OnJump ( InputAction.CallbackContext context ) {
    if(context.phase != InputActionPhase.Performed) {
      Jump.Invoke();
      Debug.Log("Jump Button Pressed");
    }
  }
}
