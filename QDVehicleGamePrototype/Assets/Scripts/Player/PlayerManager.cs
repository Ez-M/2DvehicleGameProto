using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private Player player;
    public Player Player {get => player;}
    [SerializeField]
    private InputManager inputManager;
    public InputManager InputManger{get => inputManager;}


}
