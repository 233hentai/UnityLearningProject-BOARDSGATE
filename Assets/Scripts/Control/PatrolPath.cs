using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOARDSGATE.Control{
    public class PatrolPath : MonoBehaviour
    {
        private void OnDrawGizmos() {
            float radius=0.3f;
            for(int i=0;i<transform.childCount;++i)
            {
                Gizmos.DrawSphere(GetPointPosition(i), radius);
                Gizmos.DrawLine(GetPointPosition(i),GetPointPosition(NextIndex(i)));

            }
        }

        public Vector3 GetPointPosition(int i)
        {
            return transform.GetChild(i).position;
        }

        public int NextIndex(int index){
            if(index==transform.childCount-1){
                return 0;
            }
            return index+1;
        }
    }

}
