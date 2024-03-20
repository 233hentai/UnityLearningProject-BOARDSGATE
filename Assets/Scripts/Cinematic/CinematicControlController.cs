using BOARDSGATE.Control;
using BOARDSGATE.Core;
using UnityEngine;
using UnityEngine.Playables;

namespace BOARDSGATE.Cinematic
{
    public class CinematicControlController : MonoBehaviour
    {
        GameObject player;
        void Start()
        {
            GetComponent<PlayableDirector>().played+=DisablePlayerControl;
            GetComponent<PlayableDirector>().stopped+=EnablePlayerControl;
            player=GameObject.FindWithTag("Player");
        }

        // Update is called once per frame
        void DisablePlayerControl(PlayableDirector playableDirector){
            print("disable");
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled=false;
        }

        void EnablePlayerControl(PlayableDirector playableDirector){
            print("enable");
            player.GetComponent<PlayerController>().enabled=true;
        }
    }
}

