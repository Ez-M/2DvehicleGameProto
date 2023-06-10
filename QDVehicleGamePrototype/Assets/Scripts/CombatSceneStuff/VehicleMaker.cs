using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMaker : MonoBehaviour
{


    // this will produce a Vehicle component attached to a gameObject under which will also be spawned the actual various gameobjects that make up the functional part of a vehicle entity
    // I want to use an empty parent entity as the root object so that the "type" of entity could be shared between eneity types such as a human or monster instead of a vehicle. 


    // shared elements of an entity will be a Health component, IAttackable, IMovement(optional), Icollission? 


    private static GameObject NewBlankEntity()
    {
        var newObject = new GameObject();
        return newObject;
    }

    public Vehicle VehicleComponentFromData(GameObject _blankEntity, VehicleData _vehicleData)
    {
        Vehicle newVehicle = _blankEntity.AddComponent<Vehicle>();
        newVehicle.InitializeFromData(_vehicleData);
        return newVehicle;



    }

    public Vehicle NewVehicleEntity(VehicleData _vehicleData)
    {
        GameObject _newEntity = NewBlankEntity();
        Vehicle newVehicle = VehicleComponentFromData(_blankEntity: _newEntity, _vehicleData: _vehicleData);
        return newVehicle;
    }

}

public class ChassisDatabase: ScriptableObject
{
    public static List<GameObject> chassisPrefabs;

    public static GameObject GetChassisFromID(int _chassisID)
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
