using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Attributes;
using UnityEngine;
using TMPro;

public class ExperienceDisplay : MonoBehaviour
{
    Experience experience;
    private void Awake() {
        experience=GameObject.FindWithTag("Player").GetComponent<Experience>();
    }
    void Update()
    {
        GetComponent<TextMeshProUGUI>().text=experience.GetEXP().ToString();
    }
}
