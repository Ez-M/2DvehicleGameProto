using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionDetailsPanel : MonoBehaviour
{
    public TextMeshProUGUI titleText;
    public TextMeshProUGUI descriptionText;

    public void UpdateMissionDetails(MissionData _missionData)
    {
        titleText.text = _missionData.nameText;
        descriptionText.text = _missionData.descriptionText;
    }

    

}