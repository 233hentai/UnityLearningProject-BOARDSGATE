using System;
using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Combat;
using BOARDSGATE.Core;
using BOARDSGATE.Movement;
using UnityEngine;
using UnityEngine.AI;

namespace BOARDSGATE.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] float checkDistance=10f;
        [SerializeField] float suspicionTime=5f;
        [SerializeField] float patrolWaitTime=3f;
        [SerializeField] float toleranceToPathPoint=2f;

        [SerializeField][Range(0,1)] float patrolSpeedRate=0.3f;
        GameObject player;
        Assaulter assaulter;
        Health health;
        Vector3 guardPosition;
        Mover mover;
        [SerializeField]PatrolPath patrolPath;

        float timeSinceLastSawPlayer=Mathf.Infinity;
        float timeSinceLastArrivePoint=Mathf.Infinity;
        int pathPointIndex=0;


        private void Start() {
            assaulter=GetComponent<Assaulter>();
            player=GameObject.FindWithTag("Player");
            health=GetComponent<Health>();
            guardPosition=transform.position;
            mover=GetComponent<Mover>();
        }

        void Update()
        {
            if (health.IsDead()) return;
            if (PlayerCanBeCheck() && assaulter.CanAssault(player))
            {
                assaulter.Assault(player);
                timeSinceLastSawPlayer = 0;
            }
            else if (timeSinceLastSawPlayer < suspicionTime)
            {
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimer();
        }

        private void UpdateTimer()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceLastArrivePoint += Time.deltaTime;
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition=guardPosition;
            if(patrolPath!=null){
                if(AtPathPoint()){
                    CyclePathPoint();
                    timeSinceLastArrivePoint=0;
                }
                //print(timeSinceLastArrivePoint);
                nextPosition=GetCurrentPointPosition();
            }
            if(timeSinceLastArrivePoint>patrolWaitTime){
                mover.StartMoveAction(nextPosition,patrolSpeedRate);
            }
            
        }

        private void CyclePathPoint()
        {
            pathPointIndex=patrolPath.NextIndex(pathPointIndex);
        }

        private Vector3 GetCurrentPointPosition()
        {
            return patrolPath.GetPointPosition(pathPointIndex);
        }

        private bool AtPathPoint()
        {
            float distanceToPathPoint=Vector3.Distance(transform.position,GetCurrentPointPosition());
            return distanceToPathPoint<toleranceToPathPoint;
        }

        bool PlayerCanBeCheck(){
            return Vector3.Distance(player.transform.position,transform.position)<checkDistance;
        }

        private void OnDrawGizmosSelected() {
            Gizmos.color=Color.blue;
            Gizmos.DrawWireSphere(transform.position,checkDistance);
        }
    }   
}

