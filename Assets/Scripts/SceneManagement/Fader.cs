using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOARDSGATE.SceneMnagement
{
    public class Fader : MonoBehaviour
    {      
        CanvasGroup canvasGroup;
        private void Awake() {
            canvasGroup=GetComponent<CanvasGroup>();
        }

        public IEnumerator FadeOut(float time){
            while(canvasGroup.alpha<1){
                canvasGroup.alpha+=Time.deltaTime/time;
                yield return null;
            }
        }

        public IEnumerator FadeIn(float time){
            while(canvasGroup.alpha>0){
                canvasGroup.alpha-=Time.deltaTime/time;
                yield return null;
            }
        }

        public void FadeOutDirectly(){
            canvasGroup.alpha=1;
        }
    }
}

