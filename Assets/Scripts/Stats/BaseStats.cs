using UnityEngine;

namespace BOARDSGATE.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1,20)][SerializeField] int level=1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression;
        [SerializeField] float EXPReward=0;

        public float GetStats(Stats stats){
            return progression.GetStats(characterClass,stats,level);
        }
    }
}

