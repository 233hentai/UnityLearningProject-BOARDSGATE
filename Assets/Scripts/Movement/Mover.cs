using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using BOARDSGATE.Core;
using BOARDSGATE.Saving;

namespace BOARDSGATE.Movement{
    public class Mover : MonoBehaviour,IAction,ISaveable
    {
        NavMeshAgent navMeshAgent;
        Health health;

        [SerializeField]float maxSpeed=5.66f;
        private void Start() {
            navMeshAgent=GetComponent<NavMeshAgent>();
            health=GetComponent<Health>();
        }
        private void Update() {
            navMeshAgent.enabled=!health.IsDead();
            UpdateAnimator();
        }

        public void MoveTo(Vector3 pos,float speedRate)
        {
            navMeshAgent.destination = pos;
            navMeshAgent.speed=maxSpeed*Mathf.Clamp01(speedRate);
            navMeshAgent.isStopped=false;
        }

        public void StartMoveAction(Vector3 pos,float speedRate){
            GetComponent<ActionScheduler>().StartAction(this);
            MoveTo(pos,speedRate);
        }

        public void UpdateAnimator(){
            Vector3 velocity=navMeshAgent.velocity;
            Vector3 localVelocity=transform.InverseTransformDirection(velocity);
            float speed=localVelocity.z;
            GetComponent<Animator>().SetFloat("ForwardSpeed",speed);
        }

        public void Cancel(){
            navMeshAgent.isStopped=true;
        }

        public object GetStates()
        {
            return new Vector3Serializable(transform.position);
        }

        public void RestoreStates(object state)
        {
            Vector3Serializable position=(Vector3Serializable)state;
            print(gameObject+": "+position.ToVector3());
            GetComponent<NavMeshAgent>().enabled=false;//启用navmesh时改变位置可能会引起Bug
            transform.position=position.ToVector3();
            GetComponent<NavMeshAgent>().enabled=true;
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }
    }
}


