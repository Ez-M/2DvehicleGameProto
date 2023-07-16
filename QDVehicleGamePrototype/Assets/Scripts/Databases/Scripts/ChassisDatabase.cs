using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "ChassisDatabase", menuName = "Databases/new ChassisDatabase")]
public class ChassisDatabase: ScriptableObject
{
    public List<GameObject> chassisPrefabs = new List<GameObject>();

    public GameObject GetChassisFromID(int _chassisID)
    {
        return chassisPrefabs[_chassisID]; 
    }
    private static ChassisDatabase instance;
    public static ChassisDatabase Instance{get => instance;}

    private void VerifySingleton()
    {

        if(Instance != null && Instance != this)
        {
            Destroy(this);

        } else
        {
            instance = this;

        }
    }
}