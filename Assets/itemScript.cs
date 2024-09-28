using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemScript : MonoBehaviour
{
    public GameObject bomb;
    public GameObject speed;
    public GameObject shield;
    public GameObject health;
    public GameObject triple;
    public string type;
    float shellSpeed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setShellSpeed(float num){
        shellSpeed = num;
        Debug.Log(shellSpeed);
    }

    public GameObject useItem(){
        if(type == "bomb"){
            Instantiate(bomb, transform.position, transform.rotation);
        }
        if(type == "triple"){
           
            
            GameObject firstShell = Instantiate(triple, transform.position+transform.right*2+transform.forward*2, transform.rotation*Quaternion.Euler(0, -15, 0));
            GameObject secondShell = Instantiate(triple, transform.position+transform.forward*2, transform.rotation);
            GameObject thirdShell = Instantiate(triple, transform.position-transform.right*2+transform.forward*2, transform.rotation*Quaternion.Euler(0, 15, 0));
            shellScript sScript1 = firstShell.GetComponent<shellScript>();
            shellScript sScript2 = secondShell.GetComponent<shellScript>();
            shellScript sScript3 = thirdShell.GetComponent<shellScript>();
            Debug.Log("shellspeed: " + shellSpeed);
            sScript1.speed = shellSpeed;
            sScript2.speed = shellSpeed;
            sScript3.speed = shellSpeed;
        }
        if(type == "health"){
            
        }
        if(type == "shield"){
            return Instantiate(shield, transform.position-transform.up*2, transform.rotation);
        }
        if(type == "speed"){
            
        }
        return gameObject;
    }
}
