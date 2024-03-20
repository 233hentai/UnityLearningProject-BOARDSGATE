using UnityEngine;
using UnityEngine.Playables;

namespace BOARDSGATE.Cinematic
{
    public class CinematicTrigger : MonoBehaviour{
        bool hasBeenPlayed=false;
        private void OnTriggerEnter(Collider other) {
            if(other.tag=="Player"&&!hasBeenPlayed){
                
                GetComponent<PlayableDirector>().Play();
                hasBeenPlayed=true;
            }
        }
    }
}

