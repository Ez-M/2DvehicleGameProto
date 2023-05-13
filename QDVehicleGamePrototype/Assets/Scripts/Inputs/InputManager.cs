using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : ScriptableObject
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


}
