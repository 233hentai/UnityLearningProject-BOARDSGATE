using UnityEngine;

namespace BOARDSGATE.Saving{
    [System.Serializable]
    public class Vector3Serializable{
        float x,y,z;
        public Vector3Serializable(Vector3 vector){
            x=vector.x;
            y=vector.y;
            z=vector.z;
        }

        public Vector3 ToVector3(){
            // Vector3 vector;
            // vector.x=x;
            // vector.y=y;
            // vector.z=z;
            // return vector;
            return new Vector3(x,y,z);
        }
    }
}