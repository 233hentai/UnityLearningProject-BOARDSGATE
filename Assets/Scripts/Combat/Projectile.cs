using System.Collections;
using System.Collections.Generic;
using BOARDSGATE.Core;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] float speed=1f;
    Health target;
    float damage;

    void Update()
    {   
        if(target==null) return;
        transform.LookAt(GetAimPosition());
        transform.Translate(Vector3.forward*Time.deltaTime*speed);
    }

    private void OnTriggerEnter(Collider other) {
        if(other.GetComponent<Health>()==target){
            target.TakeDamage(damage);
        }
        Destroy(gameObject);
    }

    public void SetTarget(Health target,float damage){
        this.target=target;
        this.damage=damage;
    }

    Vector3 GetAimPosition(){
        CapsuleCollider collider=target.GetComponent<CapsuleCollider>();
        if(collider==null) return target.transform.position;
        return target.transform.position+Vector3.up*collider.height*0.7f;
    }
}
