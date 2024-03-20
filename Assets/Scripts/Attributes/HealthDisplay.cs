using TMPro;
using UnityEngine;

namespace BOARDSGATE.Attributes{
    public class HealthDisplay : MonoBehaviour
    {
        Health health;
        private void Awake() {
            health=GameObject.FindWithTag("Player").GetComponent<Health>();
        }
        void Update()
        {
            GetComponent<TextMeshProUGUI>().text=health.GetPercentage().ToString( "#0")+"%";
        }
    }
}
