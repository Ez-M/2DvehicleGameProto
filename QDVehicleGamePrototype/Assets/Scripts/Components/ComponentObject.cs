using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ComponentType
{
    SideMounted,
    FrontMounted,
    Engine
}

public abstract class ComponentObject : ScriptableObject
{
    public int purchasePrice;
    public int baseRepairCost;
    public int baseMaxDurability;
    public float baseRepairRate;
    public float baseWreckedThreshold;
    public ComponentType componentType;

}
