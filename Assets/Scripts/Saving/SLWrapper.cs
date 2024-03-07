using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOARDSGATE.Saving{
    public class SLWrapper : MonoBehaviour
    {
        const string savingFileName="save";
        void Update()
        {
            if(Input.GetKeyDown(KeyCode.L)){
                Load();
            }
            if(Input.GetKeyDown(KeyCode.S)){
                Save();
            }
        }

        public void Save()
        {
            GetComponent<SLSystem>().Save(savingFileName);
        }

        public void Load()
        {
            GetComponent<SLSystem>().Load(savingFileName);
        }
    }
}

