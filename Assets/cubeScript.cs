using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubeScript : MonoBehaviour
{
    public GameObject[] powerups;
    //make it so when it collides it disappears
    //also make it rotate
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider c){
        if(c.CompareTag("Player")){
            GameObject newPowerUp = Instantiate(powerups[Random.Range(0,5)]);
            carScript cs = c.GetComponent<carScript>();
            cs.giveItem(newPowerUp);
            Destroy(this.gameObject);
        }
        if(c.CompareTag("enemy")){
            GameObject newPowerUp = Instantiate(powerups[Random.Range(0,5)]);
            enemyCarScript cs = c.GetComponent<enemyCarScript>();
            cs.giveItem(newPowerUp);
            Destroy(this.gameObject);
        }
    }
}
