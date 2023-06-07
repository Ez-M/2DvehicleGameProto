using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionSelect : MonoBehaviour
{
    public List<MissionData> missions;
    public GameObject cardPrefab;
    public GameObject cardUIArea;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GenerateMissionCard(MissionData _missionData)
    {
        GameObject newCard = GameObject.Instantiate(cardPrefab, parent: cardUIArea.transform);
        newCard.GetComponent<MissionCard>().Initialize(_missionData);
        
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