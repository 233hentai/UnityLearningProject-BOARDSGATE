using System;
using BOARDSGATE.Attributes;
using UnityEngine;

namespace BOARDSGATE.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,20)][SerializeField] int startlevel=1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;
        [SerializeField] GameObject levelUpEffect;
        public event Action onLevelUp;
        Experience experience;
        int currentLevel=0;

        private void Start() {
            currentLevel=CalculateLevel();
            experience=GetComponent<Experience>();
            if(experience!=null){
                experience.onEXPAquired+=UpdateLevel;
            }
        }

        private void UpdateLevel() {
            int level=CalculateLevel();
            if(level>currentLevel){
                onLevelUp();
                currentLevel=level;
                if(levelUpEffect!=null){
                    Instantiate(levelUpEffect,transform);
                }               
            }
        }

        public float GetStats(Stats stats){
            return progression.GetStats(characterClass,stats,GetLevel());
        }

        public float GetStats(Stats stats,int level){
            return progression.GetStats(characterClass,stats,level);
        }

        public int CalculateLevel(){
            experience=GetComponent<Experience>();
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

        public int GetLevel(){
            if(currentLevel<1){
                currentLevel=CalculateLevel();
            }
            return currentLevel;
        }
    }
}

