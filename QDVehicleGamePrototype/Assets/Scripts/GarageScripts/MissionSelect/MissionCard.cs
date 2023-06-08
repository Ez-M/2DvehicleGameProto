using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class MissionCard : MonoBehaviour
{
    public Image thumbNail;
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;
    public GameObject inspectButton;
    public MissionData missionData;
    

    public void Initialize(MissionData _missionData)
    {
        missionData = _missionData;
        titleText.text = _missionData.nameText;
        descriptionText.text = _missionData.descriptionText;
    }

    public void ViewMissionDetailsPanel()
    {
        MissionSelect.Instance.ViewMissionDetails(missionData);
    }



}
