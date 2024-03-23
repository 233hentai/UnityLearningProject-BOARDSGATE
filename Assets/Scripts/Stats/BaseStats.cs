using BOARDSGATE.Attributes;
using UnityEngine;

namespace BOARDSGATE.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,20)][SerializeField] int startlevel=1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;

        public float GetStats(Stats stats){
            return progression.GetStats(characterClass,stats,GetLevel());
        }

        public int GetLevel(){
            Experience experience=GetComponent<Experience>();
            if(experience==null) return startlevel;
            float exp=experience.GetEXP();
            int maxLevel=progression.GetMaxLevel(characterClass,Stats.LevelUpEXP);
            for(int i=1;i<=maxLevel;++i){
                if(exp<progression.GetStats(characterClass,Stats.LevelUpEXP,i)){
                    return i;
                }
            }
            return maxLevel+1;
        }
    }
}

