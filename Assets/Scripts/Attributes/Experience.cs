using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Saving;
using UnityEngine;

namespace BOARDSGATE.Attributes{
    public class Experience : MonoBehaviour,ISaveable
    {
        [SerializeField] float EXP=0;

        public float GetEXP(){
            return EXP;
        }

        public void AcquireEXP(float EXP){
            this.EXP+=EXP;
        }

        public object GetStates()
        {
            return EXP;
        }

        public void RestoreStates(object state)
        {
            EXP=(float)state;
        }
    }
}

