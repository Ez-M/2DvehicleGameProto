using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance;
    public Dictionary<Vehicle, Faction> allVehiclesByFaction; 
    public Dictionary<Vehicle, int> allVehiclesByID;

    void Awake()
    {
        allVehiclesByFaction = new Dictionary<Vehicle, Faction>();
        allVehiclesByID = new Dictionary<Vehicle, int>();

        if(Instance != null && Instance !=this)
        {Destroy(this.gameObject);}
        else {Instance = this;} 
    }

    void OnEnable()
    {

    }

    // Start is called before the first frame update
    void Start()
    {


        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public Dictionary<Vehicle, Faction> getAllVehicles()
    {// clean this up by assigning UpdateVehiclesByFaction to an Event that is triggered whenever a vehicle is removed or added to the AllVehiclesByID dictionary

        allVehiclesByFaction = new Dictionary<Vehicle, Faction>();
        foreach (var item in allVehiclesByID)
        {
            allVehiclesByFaction.Add(item.Key, item.Key.GetComponent<Vehicle>().faction);
        }
        return allVehiclesByFaction;
    }

    public void AddToAllVehicles(Vehicle _vehicle)
    {
        if(allVehiclesByID.ContainsKey(_vehicle) != true)
        {
            int _ID = 0;
            while(allVehiclesByID.ContainsValue(_ID))
            {
                _ID++;
            }
            if(allVehiclesByID.ContainsValue(_ID) != true) 
            {
                allVehiclesByID.Add(_vehicle, _ID);
                Debug.Log(_vehicle.gameObject + " added ot allVehiclesByID as ID " + _ID);
            }

        }
        else Debug.LogWarning(_vehicle.gameObject + " tried to be added to all vehicles, but already exists within the lsit");
         
    }
}
