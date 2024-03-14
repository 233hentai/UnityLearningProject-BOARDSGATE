using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BOARDSGATE.Combat{
    public class WeaponPickup : MonoBehaviour
    {
        [SerializeField] Weapon weapon;
        private void OnTriggerEnter(Collider other) {
            if(other.tag=="Player"){
                other.GetComponent<Assaulter>().EquipWeapon(weapon);
                Destroy(gameObject);
            }
        }
    }
}

