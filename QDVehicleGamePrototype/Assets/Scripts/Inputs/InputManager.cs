using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public Player Player {get => player;}
    [SerializeField]
    private static InputManager instance;
    public static InputManager Instance{get => instance;}
    [SerializeField]
    private MouseTracker mouseTracker;
    public MouseTracker MouseTracker{get => mouseTracker;}
    public bool fireInputHeld;

    public Vector3 mouseWorldPosition;
    public Vector2 mouseScreenPosition;
    public Inputs Inputs;


    public event Action PlayerShoot;
    // public class OnShootEventArgs : EventArgs
    // {
    //     public Vector3 gunEndPosition;
    //     public Vector3 shootPosition;
    // }

    void Awake()
    {
        Inputs = new Inputs();
        Inputs.Enable();
        Inputs.Combat.Click.performed += HandleShoot;
        Inputs.Combat.Click.canceled += HandleShoot;
    }

    public void Update()
    {
        mouseScreenPosition = MouseTracker.GetScreenPosition();
        mouseWorldPosition = MouseTracker.GetWorldPosition();
    }

    public void HandleShoot(InputAction.CallbackContext ctx)
    {
        if(ctx.performed)
        {
            Debug.Log("performed");
        fireInputHeld = true;
        PlayerShoot.Invoke();
        }

        if (ctx.canceled)
        {
            Debug.Log("canceled");

            fireInputHeld = false;
        }
    }


}
