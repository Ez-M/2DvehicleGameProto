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

    public void Initialize(MissionData _missionData)
    {
        titleText.text = _missionData.nameText;
        descriptionText.text = _missionData.descriptionText;
    }
}
