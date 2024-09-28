using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class enemyCarScript : MonoBehaviour
{
    Rigidbody rb;
    NavMeshAgent ai;
    public GameObject[] checkpoints;
    public GameObject itemLocation;
    GameObject item;
    public int index = 0;
    GameObject shield;
    float timer;
    public float maxSpeed = 50;
    public int hp = 10;
    int lapCounter;
    int checkPointCheck;
    public Slider healthBar;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        ai = GetComponent<NavMeshAgent>();
        ai.SetDestination(checkpoints[index].transform.position);
        
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.value = hp/10.0f;

        if(item != null){
            itemScript itsc = item.GetComponent<itemScript>();
            if(itsc.type == "shield"){
                shield = itsc.useItem();
                shield.transform.SetParent(transform);
                Destroy(item);
            }
            if(itsc.type == "speed"){
                ai.speed = maxSpeed*2;
                timer = 0;
                Destroy(item);
            }
            if(itsc.type == "bomb"){
                
                itsc.useItem();
                Destroy(item);
            }
            if(itsc.type == "triple"){
                itsc.useItem();
                Destroy(item);
            }
            if(itsc.type == "health"){
                hp += 2;
                Destroy(item);
            }
        }

        timer += Time.deltaTime;
        if(ai.speed == 18 && timer >= 8){
            ai.speed = maxSpeed;
        }
        if(hp <= 0){
            Destroy(gameObject);
        }
    }

     public void giveItem(GameObject item){
        this.item = item;
        item.transform.position = itemLocation.transform.position;
        item.transform.rotation = itemLocation.transform.rotation;
        item.transform.SetParent(itemLocation.transform);
     }

     void OnTriggerEnter(Collider c){
        if(c.CompareTag("checkpoint")){
            index += 1;
            checkPointCheck += 1;
            if(index == checkpoints.Length){
                index = 0;
            }
            ai.SetDestination(checkpoints[index].transform.position);
        }

        if(c.CompareTag("endLap") && checkPointCheck >= 4){
            lapCounter += 1;
            checkPointCheck = 0;
        }
     }

}
