using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseTracker : ScriptableObject
{
    public float Xpos;
    public float Ypos;

    // Update is called once per frame
    void Update()
    {
        Xpos = Mouse.current.position.ReadValue().x;
        Ypos = Mouse.current.position.ReadValue().y;
    }

    public Vector2 GetPosition()
    {
        Vector2 Pos = new Vector2(Xpos, Ypos);
        return Pos; 
    }
}
