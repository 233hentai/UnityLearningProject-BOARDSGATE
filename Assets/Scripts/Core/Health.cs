using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BOARDSGATE.Saving;
using System.Runtime.InteropServices.WindowsRuntime;

namespace BOARDSGATE.Core{
    public class Health : MonoBehaviour,ISaveable{
        [SerializeField]float health=100f;
        bool isDead=false;
        public void TakeDamage(float damage){
            health=health-damage>=0?health-damage:0;
            if(health==0){
                Die();
            }
        }

        void Die(){
            if(isDead) {
                return;
            }
            if(health<=0){
                isDead=true;
                GetComponent<Animator>().SetTrigger("Die");
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
        }

        public bool IsDead(){
            return isDead;
        }

        public object CaptureState()
        {
            return health;
        }

        public void RestoreState(object state)
        {
            health=(float)state;
            if(health<=0){
                Die();
            }
        }
    }

}
