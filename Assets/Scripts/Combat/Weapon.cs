using System;
using BOARDSGATE.Core;
using UnityEngine;

namespace BOARDSGATE.Combat{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject {
        [SerializeField] GameObject weapon;
        [SerializeField] AnimatorOverrideController animatorOverrideController;

        [SerializeField] float assaultRange;
        [SerializeField] float timeBetweenAssault;
        [SerializeField] float damage;
        [SerializeField] bool isRightHand=true;
        [SerializeField] Projectile projectile;
        const string weaponName="Weapon";

        public void Spawn(Transform leftHandTransform,Transform rightHandTransform,Animator animator){
            DestroyOldWeapon(leftHandTransform,rightHandTransform);
            if(weapon!=null)
            {
                GameObject newWeapon=Instantiate(weapon, GetHandTransform(leftHandTransform, rightHandTransform));
                newWeapon.name=weaponName;
            }
            if (animatorOverrideController!=null){
                animator.runtimeAnimatorController=animatorOverrideController;
            }
        }

        private void DestroyOldWeapon(Transform leftHandTransform,Transform rightHandTransform)
        {
            Transform oldWeapon=leftHandTransform.Find("Weapon");
            if(oldWeapon==null){
                oldWeapon=rightHandTransform.Find("Weapon");
            }
            if(oldWeapon==null) return;
            oldWeapon.name="Weapon_Destroy";//防止出现混淆的bug
            Destroy(oldWeapon.gameObject);
        }

        public void LaunchProjectile(Transform leftHandTransform,Transform rightHandTransform,Health target){
            Projectile projectileInstance=Instantiate(projectile,GetHandTransform(leftHandTransform,rightHandTransform).position,Quaternion.identity);
            projectileInstance.SetTarget(target,damage);
        }
        private Transform GetHandTransform(Transform leftHandTransform, Transform rightHandTransform)
        {
            return isRightHand ? rightHandTransform : leftHandTransform;
        }

        public float GetAssaultRange(){
            return assaultRange;
        }

        public float GetTimeBetweenAssault(){
            return timeBetweenAssault;
        }
        public float GetDamage(){
            return damage;
        }

        public bool hasProjectile(){
            return projectile!=null;
        }
    }   
}
