using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField]
    private GameObject playerObject;
    public GameObject PlayerObject{get=> playerObject;}

    public Health health; 

}
