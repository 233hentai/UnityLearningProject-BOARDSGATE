using BOARDSGATE.Attributes;
using UnityEngine;
namespace BOARDSGATE.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] float speed=1f;
        [SerializeField] bool canTrackTarget=false;
        [SerializeField] float maxLifeTime=10f;
        [SerializeField] GameObject impactEffect;
        [SerializeField] GameObject[] objectsDestroyOnHit;
        [SerializeField] float objectLifeAfterImpact=0.5f;
        Health target;
        float damage;
        GameObject instigator;

        void Update()
        {   
            if(target==null) return;
            if(canTrackTarget&&!target.IsDead()){
                transform.LookAt(GetAimPosition());
            }
            transform.Translate(Vector3.forward*Time.deltaTime*speed);
        }

        private void OnTriggerEnter(Collider other) {
            Health targetHealth=other.GetComponent<Health>();
            if(targetHealth==target){
                if(targetHealth.IsDead()) return;
                target.TakeDamage(damage,instigator);
            }
            if(impactEffect!=null){
                Instantiate(impactEffect,transform.position,Quaternion.identity);
            }
            foreach(GameObject ob in objectsDestroyOnHit){
                Destroy(ob);//碰撞后并不立即销毁全部，留下需要留下的(如粒子特效)
            }
            Destroy(gameObject,objectLifeAfterImpact);
        }



        public void SetTarget(Health target,GameObject instigator,float damage){
            this.target=target;
            this.damage=damage;
            this.instigator=instigator;
            transform.LookAt(GetAimPosition());
            Destroy(gameObject,maxLifeTime);
        }

        Vector3 GetAimPosition(){
            CapsuleCollider collider=target.GetComponent<CapsuleCollider>();
            if(collider==null) return target.transform.position;
            return target.transform.position+Vector3.up*collider.height*0.7f;
        }
    }

}
