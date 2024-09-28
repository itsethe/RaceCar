using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class carScript : MonoBehaviour
{
    Rigidbody rb;
    public float speed = 5;
    public float acceleration = 0;
    public float rotationSpeed = 5;
    float maxAcceleration = 100;
    public GameObject itemLocation;
    GameObject item;
    bool hasShield = false;
    int shieldHits = 0;
    GameObject shield;
    float timer = 0;
    public int hp = 10;
    public Slider healthBar;
    int lapCounter = 0;
    int checkPointCounter = 0;
    public Text lapText;
    
    

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float ws = Input.GetAxis("Vertical");
        RaycastHit touchGround;
        RaycastHit checkBottom;
        Debug.DrawRay(rb.transform.position+transform.up,Vector3.down*1.2f, Color.yellow);
        bool grounded = (Physics.Raycast(rb.transform.position+transform.up, Vector3.down, out touchGround, 1.2f));
        bool bottom = (Physics.Raycast(rb.transform.position+transform.up, -transform.up, out checkBottom, 1.2f));
        
        if(bottom == false){
            Quaternion targetRotation = Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime*15);
        }
        if(grounded == true){
            acceleration += ws;
            Vector3 movement = transform.forward*acceleration*speed*Time.deltaTime;
            rb.MovePosition(transform.position+movement);
        
            rb.mass = 20;
        }else{
            rb.mass = 200;
        }
        if(acceleration >= maxAcceleration){
            acceleration = maxAcceleration;
        }
        
        
        
        

        if(ws == 0 || !grounded){
            acceleration-=Time.deltaTime;
            if(acceleration<0){
                acceleration=0;
            }
        }
        float ad = Input.GetAxis("Horizontal");
        Vector3 rotation = transform.up*ad*Time.deltaTime*rotationSpeed;
        Quaternion bodyRotation = Quaternion.Euler(rotation);
        rb.MoveRotation(transform.rotation*bodyRotation);
    }

    public void giveItem(GameObject item){
        if(this.item == null){
            this.item = item;
            item.transform.position = itemLocation.transform.position;
            item.transform.rotation = itemLocation.transform.rotation;
            item.transform.SetParent(itemLocation.transform);
        }
        
    }
    void Update(){
        lapText.text = "Laps: " + lapCounter;
        healthBar.value = hp/10.0f;
        if(Input.GetKeyDown(KeyCode.E)){
            if(item != null){
                itemScript itsc = item.GetComponent<itemScript>();
                if(itsc.type == "shield"){
                    shield = itsc.useItem();
                    shield.transform.SetParent(transform);
                    Destroy(item);
                    shieldHits = 3;
                    hasShield = true;
                }else if(itsc.type == "speed"){
                    speed = 1;
                    timer = 0;
                    Destroy(item);
                }else if(itsc.type == "health"){
                    hp += 2;
                    Destroy(item);
                }else if(itsc.type == "shell"){
                    itsc.setShellSpeed(rb.velocity.magnitude*2);
                    itsc.useItem();
                    Destroy(item);
                }else{
                    itsc.useItem();
                    Destroy(item);
                }
                
            }

        }

        if(shieldHits <= 0){
            Destroy(shield);
        }

        timer += Time.deltaTime;
        if(speed == 1 && timer >= 8){
            speed = 0.5f;
        }

        if(hp <= 0){
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter(Collider c){
        if(c.CompareTag("shell")){
            Destroy(c.gameObject);
            if(shieldHits>0){
                shieldHits -= 1;
            }else{
                hp -= 1;
            }
            
        }
        if(c.CompareTag("explosion")){
            if(shieldHits > 0){
                shieldHits -= 1;
            }else{
                hp -= 1;
            }
            
        }
        if(c.CompareTag("checkpoint")){
            checkPointCounter += 1;
        }

        if(c.CompareTag("endLap") && checkPointCounter >= 4){
            lapCounter += 1;
            checkPointCounter = 0;
        }
    }

    
}
