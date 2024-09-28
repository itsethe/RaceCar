using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shellScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        rb=GetComponent<Rigidbody>();
       
        Destroy(gameObject, 20);
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.forward*50*speed, ForceMode.Impulse);
    }
}
