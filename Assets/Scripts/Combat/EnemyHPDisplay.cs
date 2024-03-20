using BOARDSGATE.Attributes;
using TMPro;
using UnityEngine;

namespace BOARDSGATE.Combat{
    public class EnemyHPDisplay : MonoBehaviour
    {
        Assaulter assaulter;
        Health health;

        private void Awake() {
            assaulter=GameObject.FindWithTag("Player").GetComponent<Assaulter>();
        }
        void Update()
        {
            health=assaulter.GetTarget();
            if(health==null){
                GetComponent<TextMeshProUGUI>().text="";
            }
            else{
                GetComponent<TextMeshProUGUI>().text=health.GetPercentage().ToString( "#0")+"%";
            }
        }
    }
}
