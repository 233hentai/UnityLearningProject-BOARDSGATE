using UnityEngine;
using BOARDSGATE.Movement;
using BOARDSGATE.Combat;
using BOARDSGATE.Attributes;
namespace BOARDSGATE.Control
{
    public class PlayerController : MonoBehaviour
    {
        Mover mover;
        Health health;
        [SerializeField][Range(0,1)] float speedRate=1f;

        // Start is called before the first frame update
        void Start()
        {
            mover=GetComponent<Mover>();
            health=GetComponent<Health>();
        }

        // Update is called once per frame
        void Update()
        {   
            if(health.IsDead()) return;
            if(CombatInteract()) return;
            if(MoveInteract()) return;
        }

        private bool MoveInteract()
        {

            RaycastHit hit;
            if (Physics.Raycast(GetRayCamToMouse(), out hit))
            {
                if (Input.GetMouseButton(1)){
                    mover.StartMoveAction(hit.point,speedRate);
                }
                return true;
            }
            return false;
        }

        private bool CombatInteract(){
            RaycastHit[] hits = Physics.RaycastAll(GetRayCamToMouse());
            foreach (RaycastHit hit in hits)
            {
                AssaultTarget target=hit.collider.GetComponent<AssaultTarget>();
                if(target==null) continue;
                if (GetComponent<Assaulter>().CanAssault(target.gameObject))
                {
                    if(Input.GetMouseButton(1))
                    {
                        GetComponent<Assaulter>().Assault(target.gameObject);
                    }   
                    return true;
                }
            }
            return false;
        }


        private static Ray GetRayCamToMouse()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }

    }

}
