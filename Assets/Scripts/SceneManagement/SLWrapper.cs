using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Saving;
using UnityEngine;

namespace BOARDSGATE.SceneMnagement{
    public class SLWrapper : MonoBehaviour
    {
        const string savingFileName="save";
        [SerializeField]float fadeInTime=0.5f;

        private IEnumerator Start() {
            Fader fader=FindObjectOfType<Fader>();
            fader.FadeOutDirectly();
            yield return GetComponent<SLSystem>().LoadLastScene(savingFileName);
            yield return fader.FadeIn(fadeInTime);

        }
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
