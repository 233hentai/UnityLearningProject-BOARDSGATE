using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Saving;
using UnityEngine;

namespace BOARDSGATE.SceneMnagement{
    public class SLWrapper : MonoBehaviour
    {
        const string savingFileName="save";
        [SerializeField]float fadeInTime=0.5f;

        private void Awake() {
            StartCoroutine(LoadLastScene());
        }
        private IEnumerator LoadLastScene() {
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
            if(Input.GetKeyDown(KeyCode.Delete)){
                Delete();
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

        public void Delete()
        {
            GetComponent<SLSystem>().Delete(savingFileName);
        }
    }
}

