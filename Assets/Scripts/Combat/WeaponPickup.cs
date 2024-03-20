using System.Collections;
using UnityEngine;

namespace BOARDSGATE.Combat
{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        [SerializeField] float respawnTime=5f;
        private void OnTriggerEnter(Collider other) {
            if(other.tag=="Player"){
                other.GetComponent<Assaulter>().EquipWeapon(weapon);
                //Destroy(gameObject);
                StartCoroutine(Hide(respawnTime));
            }
        }

        IEnumerator Hide(float hideTime){
            ShowPickup(false);
            yield return new WaitForSeconds(respawnTime);
            ShowPickup(true);
        }

        private void ShowPickup(bool shouldShow)
        {
            GetComponent<SphereCollider>().enabled=shouldShow;
            for(int i=0;i<transform.childCount;i++){
                transform.GetChild(i).gameObject.SetActive(shouldShow);
            }
        }
        
    }
}

