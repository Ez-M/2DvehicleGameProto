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
    public InputManager InputManger{get{return inputManager;}}

    [SerializeField]
    private static PlayerManager instance;
    public static PlayerManager Instance{get => instance;}

    public void Awake()
    {
        VerifySingleton();
    }

    private void VerifySingleton()
    {

        if(Instance != null && Instance != this)
        {
            Destroy(this.gameObject);

        } else
        {
            instance = this;

        }
    }
}
