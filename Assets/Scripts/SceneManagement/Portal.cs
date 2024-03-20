using System;
using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Saving;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace BOARDSGATE.SceneMnagement
{
    public class Portal : MonoBehaviour
    {   
        enum PortalNo{A,B,C,D,E};
        [SerializeField] int sceneIndex=-1;
        [SerializeField] Transform spwanPoint;
        [SerializeField] PortalNo portalID;
        [SerializeField] float fadeOutTime=1f;
        [SerializeField] float fadeInTime=1f;
        [SerializeField] float fadeWaitTime=0.5f;
        private void OnTriggerEnter(Collider other) {
            if(other.tag=="Player"){
                StartCoroutine(Transition());
            }
        }
         private void Start() {
            spwanPoint=transform.GetChild(0);

         }
        IEnumerator Transition(){
            if(sceneIndex<0){
                yield break;
            }
            DontDestroyOnLoad(gameObject);
            Fader fader=FindObjectOfType<Fader>();
            //yield return fader.FadeOut(fadeOutTime);
            SLWrapper Wrapper=FindObjectOfType<SLWrapper>();
            Wrapper.Save();
            print("start load scene");
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            print("start load data");
            Wrapper.Load();
            print("Loaded");

            Portal anotherPortal=GetAnotherPortal();
            UpdatePlayer(anotherPortal);
            Wrapper.Save();//到一个新场景后自动存档

            print("start fade in");
            yield return fader.FadeIn(fadeInTime);
            print("faded in");
            yield return new WaitForSeconds(fadeWaitTime);

            Destroy(gameObject);
        }

        private void UpdatePlayer(Portal anotherPortal)
        {
            GameObject player=GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().enabled=false;
            player.GetComponent<NavMeshAgent>().Warp(anotherPortal.spwanPoint.position);
            player.transform.rotation=anotherPortal.spwanPoint.rotation;
            player.GetComponent<NavMeshAgent>().enabled=true;
        }

        private Portal GetAnotherPortal()
        {
            foreach(Portal portal in FindObjectsOfType<Portal>()){
                if(portal!=this&&portal.portalID==portalID) return portal;
            }
            return null;
        }
    }
}

