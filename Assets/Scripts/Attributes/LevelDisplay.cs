using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Stats;
using TMPro;
using UnityEngine;

namespace BOARDSGATE.Attributes{
    public class LevelDisplay : MonoBehaviour
    {
        BaseStats stats;
        private void Awake() {
            stats=GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text=stats.GetLevel().ToString();
        }
    }
}
