using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Dictionary<GameObject, Faction> allVehicles; 

    void Awake()
    {
        allVehicles = new Dictionary<GameObject, Faction>();
    }

    void OnEnable()
    {
        if(Instance != null && Instance !=this)
        {Destroy(this.gameObject);}
        else {Instance = this;} 
    }

    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    internal Dictionary<GameObject, Faction> getAllVehicles()
    {
        throw new NotImplementedException();
    }
}
