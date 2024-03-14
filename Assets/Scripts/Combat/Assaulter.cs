using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Core;
using BOARDSGATE.Movement;
using Unity.VisualScripting;
using UnityEngine;

namespace BOARDSGATE.Combat{
    public class Assaulter : MonoBehaviour,IAction
    {
        Health target;
        Mover mover;
        // [SerializeField] float assaultRange=2;
        // [SerializeField] float timeBetweenAssault=1f;

        // [SerializeField] float damage=30;

        float timeToLastAssault=0;
        [SerializeField] Transform rightHandTransform;
        [SerializeField] Transform leftHandTransform;

        [SerializeField] Weapon defaultWeapon;

        Weapon currentWeapon;
        
        
        private void Start() {
            mover=GetComponent<Mover>();
            EquipWeapon(defaultWeapon);
        }
        private void Update() {
            timeToLastAssault+=Time.deltaTime;
            if(target==null) return;

            if(target.IsDead()) {
                return;
            }

            if(!InAssaultRange()){
                mover.MoveTo(target.transform.position,1f);
            }
            else
            {
                mover.Cancel();
                AssaltBehaviour();
                
            }
        }

        private void AssaltBehaviour()
        {
            if(timeToLastAssault>=currentWeapon.GetTimeBetweenAssault()){
                transform.LookAt(target.transform);
                GetComponent<Animator>().ResetTrigger("StopAssault");
                GetComponent<Animator>().SetTrigger("Assault");
                timeToLastAssault=0;
            }
            
        }

        private bool InAssaultRange(){
            return Vector3.Distance(transform.position,target.transform.position)<=currentWeapon.GetAssaultRange();
        }
        public void Assault(GameObject assaultTarget){
            target=assaultTarget.GetComponent<Health>();
            //Debug.Log("Assaulting");
            GetComponent<ActionScheduler>().StartAction(this);
            
        }

        public bool CanAssault(GameObject assaultTarget){
            if(assaultTarget==null) return false;
            Health targetHealth=assaultTarget.GetComponent<Health>();
            return !targetHealth.IsDead()&&targetHealth!=null;
        }

        public void Cancel(){
            GetComponent<Animator>().ResetTrigger("Assault");
            GetComponent<Animator>().SetTrigger("StopAssault");
            target=null;
        }

        public void EquipWeapon(Weapon weaponToEquip){
            Animator animator=GetComponent<Animator>();      
            weaponToEquip.Spawn(leftHandTransform,rightHandTransform,animator);   
            currentWeapon=weaponToEquip;
        }


        //动画事件
        void Hit(){
            if(target==null) return;
            if(currentWeapon.hasProjectile()){
                currentWeapon.LaunchProjectile(leftHandTransform,rightHandTransform,target);
            }
            else{
                target.TakeDamage(currentWeapon.GetDamage());
            }
        }
        void Shoot(){
            Hit();
        }

        
    }
}

