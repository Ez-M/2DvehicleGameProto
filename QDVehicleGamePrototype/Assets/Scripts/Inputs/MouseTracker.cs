using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[CreateAssetMenu(fileName = "New MouseTracker", menuName = "Inputs/MouseTracker")]
public class MouseTracker : ScriptableObject
{
    public Vector2 ScreenPos;
    public Vector3 WorldPos;

    // Update is called once per frame
    void Update()
    {

    }

    public Vector2 GetScreenPosition()
    {

        ScreenPos = Mouse.current.position.ReadValue();
        return ScreenPos; 
    }

    public Vector3 GetWorldPosition()
    {
        WorldPos = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        return WorldPos;
    }
}
