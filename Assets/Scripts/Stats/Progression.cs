using BOARDSGATE.Attributes;
using UnityEngine;

namespace BOARDSGATE.Stats{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 0)]
    public class Progression : ScriptableObject {
        [SerializeField] CharacterProgressionClass[] characterProgression;

        public float GetStats(CharacterClass character,Stats stats,int level){
            foreach(CharacterProgressionClass progression in characterProgression){
                if(character==progression.characterClass){
                    foreach(ProgressionStats progressionStats in progression.statsList)
                    {
                        if(stats==progressionStats.stats&&progressionStats.levels.Length>=level){
                            return progressionStats.levels[level-1];
                        }                       
                    }
                    
                }
            }
            return 0f;
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
