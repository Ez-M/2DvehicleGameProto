using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelect : MonoBehaviour
{
    public List<MissionData> Missions;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

public struct MissionData
{
    public string nameText;
    public string descriptionText;
    public RegionType regionType;
    public int difficulty;
    public Dictionary<RewardType, int> rewards;
}

public enum RewardType
{
    cash,
    stuff
}

public enum RegionType
{
    desert,
    city
}