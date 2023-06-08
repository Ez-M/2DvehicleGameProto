using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class MissionSelect : MonoBehaviour
{
    [SerializeField]
    public List<MissionData> missions;
    public List<GameObject> missionCards;
    public MissionDetailsPanel missionDetailsPanel;
    public GameObject cardPrefab;
    public GameObject cardUIArea;
    // Start is called before the first frame update
    void Start()
    {
        if(missionCards.Count>0)
        {RemoveMissionCards();}
        if(missions.Count > 0)
        {AllMissionCards();}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    [ContextMenu("Gen Mission Cards")]
    public void AllMissionCards()
    {
        foreach (MissionData _missionData in missions)
        {
            GenerateMissionCard(_missionData);
        }
    }

    public void GenerateMissionCard(MissionData _missionData)
    {
        GameObject newCard = GameObject.Instantiate(cardPrefab, parent: cardUIArea.transform);
        newCard.GetComponent<MissionCard>().Initialize(_missionData);
        missionCards.Add(newCard);
        
    }
    public void RemoveMissionCards()
    {
        foreach(GameObject _card in missionCards)
        {
            Destroy(_card);
        }
    }


    public void ViewMissionDetails(MissionData _missionData)
    {
        missionDetailsPanel.UpdateMissionDetails(_missionData);
        missionDetailsPanel.gameObject.SetActive(true);
    }

    public void CloseMissionDetails()
    {
        missionDetailsPanel.gameObject.SetActive(false);
    }

    
}



[Serializable]
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