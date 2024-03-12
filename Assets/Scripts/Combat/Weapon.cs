using UnityEngine;

namespace BOARDSGATE.Combat{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] GameObject weapon;
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        public void Spawn(Transform handTransform,Animator animator){
            Instantiate(weapon,handTransform);
            animator.runtimeAnimatorController=animatorOverrideController;
        }
    }   
}
