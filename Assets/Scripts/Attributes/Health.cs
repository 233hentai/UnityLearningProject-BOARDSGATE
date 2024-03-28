using UnityEngine;
using BOARDSGATE.Saving;
using BOARDSGATE.Core;
using BOARDSGATE.Stats;

namespace BOARDSGATE.Attributes
{
    public class Health : MonoBehaviour,ISaveable{
        float health=-1f;
        bool isDead=false;
        BaseStats baseStats;
        private void Start() {
            baseStats=GetComponent<BaseStats>();
            baseStats.onLevelUp+=UpdateHealthWhenLevelUp;
            if(health<0){
                health=baseStats.GetStats(Stats.Stats.Health);
            }
            if(tag=="Player"){
                print("HP:"+health);
            }
        }
        public void TakeDamage(float damage,GameObject instigator){
            health=health-damage>=0?health-damage:0;
            if(health==0)
            {
                Die();
                ProvideEXP(instigator);
            }
        }

        private void ProvideEXP(GameObject instigator)
        {
            Experience experience=instigator.GetComponent<Experience>();
            if(experience==null) return;
            experience.AcquireEXP(baseStats.GetStats(Stats.Stats.EXPReward));
        }

        public void UpdateHealthWhenLevelUp(){
            float percent=GetPercentage()/100;
            health=baseStats.GetStats(Stats.Stats.Health,baseStats.GetLevel()+1)*percent;
            // if(tag=="Player"){
            //     print("HP:"+health);
            // }
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

        public float GetPercentage(){
            return 100*health/baseStats.GetStats(Stats.Stats.Health);
        }

        public bool IsDead(){
            return isDead;
        }

        public object GetStates()
        {
            return health;
        }

        public void RestoreStates(object state)
        {
            health=(float)state;
            if(health<=0){
                Die();
            }
        }
    }

}
