using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionScript : MonoBehaviour
{
    public float radius = 5.0f;
    public float power = 10.0f;
    public float upForce = 20.0f;
    // Start is called before the first frame update
    void Start()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach(Collider hit in colliders){
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if(rb != null){
                rb.AddExplosionForce(power, explosionPos, radius, upForce);
            }
            if(hit.CompareTag("Player")){
                carScript s = hit.GetComponent<carScript>();
                s.hp -= 2;
            }

            if(hit.CompareTag("enemy")){
                enemyCarScript ecs = hit.GetComponent<enemyCarScript>();
                ecs.hp -= 2;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
