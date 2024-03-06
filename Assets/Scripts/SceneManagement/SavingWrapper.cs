using System;
using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Saving;
using UnityEngine;
namespace BOARDSGATE.SceneMnagement{
    public class SavingWrapper : MonoBehaviour
    {
        const string savingFileName="Save";

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
            GetComponent<SavingSystem>().Save(savingFileName);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(savingFileName);
        }
    }
}

