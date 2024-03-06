using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

namespace BOARDSGATE.SceneMnagement{
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
            yield return fader.FadeOut(fadeOutTime);
            FindObjectOfType<SavingWrapper>().Save();
            yield return SceneManager.LoadSceneAsync(sceneIndex);
            FindObjectOfType<SavingWrapper>().Load();
            Portal anotherPortal=GetAnotherPortal();
            UpdatePlayer(anotherPortal);

            yield return fader.FadeIn(fadeInTime);
            yield return new WaitForSeconds(0.5f);

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

