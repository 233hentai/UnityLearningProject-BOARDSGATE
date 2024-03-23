using System;
using System.Collections.Generic;
using BOARDSGATE.Attributes;
using UnityEngine;

namespace BOARDSGATE.Stats{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject {
        [SerializeField] CharacterProgressionClass[] characterProgression;
        Dictionary<CharacterClass,Dictionary<Stats,float[]>> lookupTable;

        public float GetStats(CharacterClass character,Stats stats,int level){
            BuildLookup();
            if(lookupTable[character][stats].Length>=level){
                return lookupTable[character][stats][level-1];
            }
            
            // foreach(CharacterProgressionClass progression in characterProgression){
            //     if(character==progression.characterClass){
            //         foreach(ProgressionStats progressionStats in progression.statsList)
            //         {
            //             if(stats==progressionStats.stats&&progressionStats.levels.Length>=level){
            //                 return progressionStats.levels[level-1];
            //             }                       
            //         }
                    
            //     }
            // }
            return 0f;
        }

        private void BuildLookup()
        {
            if(lookupTable!=null) return;
            lookupTable=new Dictionary<CharacterClass, Dictionary<Stats, float[]>>();
            foreach(CharacterProgressionClass progression in characterProgression){
                Dictionary<Stats,float[]> dic=new Dictionary<Stats, float[]>();
                foreach(ProgressionStats progressionStats in progression.statsList)
                {
                    dic[progressionStats.stats]=progressionStats.levels;                    
                }
                lookupTable[progression.characterClass]=dic;
            }
        }

        public int GetMaxLevel(CharacterClass character,Stats stats){
            BuildLookup();
            return lookupTable[character][stats].Length;
        } 

        [System.Serializable] class CharacterProgressionClass{
            [SerializeField] public CharacterClass characterClass;
            [SerializeField] public ProgressionStats[] statsList;
        }

        [System.Serializable] class ProgressionStats{
            [SerializeField] public Stats stats;
            [SerializeField] public float[] levels;
        }
    }
}
