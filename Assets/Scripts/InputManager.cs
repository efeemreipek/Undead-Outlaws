using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get; private set; }
    public static PlayerInput PlayerInput;

    private Vector2 move;
    private Vector2 look;
    private bool isGamepad = false;
    private bool isShootPressed;
    private bool isReloadPressed;
    private bool isWeaponOnePressed;
    private bool isWeaponTwoPressed;
    private bool isWeaponThreePressed;
    private bool isPausePressed;
    private bool isPauseUIPressed;
    private bool isWeaponUpPressed;
    private bool isWeaponDownPressed;


    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(this);
            return;
        }
        Instance = this;

        PlayerInput = GetComponent<PlayerInput>();
    }



    public void OnMove(InputValue value)
    {
        move = value.Get<Vector2>();
    }

    public void OnMouseLook(InputValue value)
    {
        look = value.Get<Vector2>();
        isGamepad = false;

    }
    public void OnGamepadLook(InputValue value)
    {
        look = value.Get<Vector2>();
        isGamepad = true;
    }

    public void OnShoot(InputValue value)
    {
        isShootPressed = value.isPressed;
    }

    public void OnReload(InputValue value)
    {
        isReloadPressed = value.isPressed;
    }

    public void OnWeaponOne(InputValue value)
    {
        isWeaponOnePressed = value.isPressed;
    }

    public void OnWeaponTwo(InputValue value)
    {
        isWeaponTwoPressed = value.isPressed;
    }

    public void OnWeaponThree(InputValue value)
    {
        isWeaponThreePressed = value.isPressed;
    }
    public void OnPause(InputValue value)
    {
        isPausePressed = value.isPressed;
    }

    public void OnPauseUI(InputValue value)
    {
        isPauseUIPressed = value.isPressed;
    }

    public void OnWeaponUp(InputValue value)
    {
        isWeaponUpPressed = value.isPressed;
    }

    public void OnWeaponDown(InputValue value)
    {
        isWeaponDownPressed = value.isPressed;
    }

    public Vector2 GetMove() => move;
    public Vector2 GetLook() => look;
    public bool GetIsGamepad() => isGamepad;
    public bool GetIsShootPressed() => isShootPressed;
    public bool GetIsReloadKeyPressed()
    {
        bool wasReloadPressed = isReloadPressed;
        isReloadPressed = false;
        return wasReloadPressed;
    }
    public bool GetIsWeaponOnePressed()
    {
        bool wasWeaponOnePressed = isWeaponOnePressed;
        isWeaponOnePressed = false;
        return wasWeaponOnePressed;
    }
    public bool GetIsWeaponTwoPressed()
    {
        bool wasWeaponTwoPressed = isWeaponTwoPressed;
        isWeaponTwoPressed = false;
        return wasWeaponTwoPressed;
    }
    public bool GetIsWeaponThreePressed()
    {
        bool wasWeaponThreePressed = isWeaponThreePressed;
        isWeaponThreePressed = false;
        return wasWeaponThreePressed;
    }
    public bool GetIsPausePressed()
    {
        bool wasPausePressed = isPausePressed;
        isPausePressed = false;
        return wasPausePressed;
    }
    public bool GetIsPauseUIPressed()
    {
        bool wasPauseUIPressed = isPauseUIPressed;
        isPauseUIPressed = false;
        return wasPauseUIPressed;
    }
    public bool GetIsWeaponUpPressed()
    {
        bool wasWeaponUpPressed = isWeaponUpPressed;
        isWeaponUpPressed = false;
        return wasWeaponUpPressed;
    }
    public bool GetIsWeaponDownPressed()
    {
        bool wasWeaponDownPressed = isWeaponDownPressed;
        isWeaponDownPressed = false;
        return wasWeaponDownPressed;
    }
}
