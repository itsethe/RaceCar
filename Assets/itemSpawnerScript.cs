using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemSpawnerScript : MonoBehaviour
{
    public GameObject[] items;
    public GameObject itemBox;
    GameObject[] boxes;
    float[] timers;
    
    // Start is called before the first frame update
    void Start()
    {
        boxes = new GameObject[items.Length];
        timers = new float[items.Length];
        for(int i = 0; i<items.Length;i++){
            boxes[i] = Instantiate(itemBox, items[i].transform.position, items[i].transform.rotation);
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        for(int i = 0; i<boxes.Length; i++){
            if(boxes[i] == null){
                timers[i] += Time.deltaTime;
                if(timers[i] >= 5){
                    boxes[i] = Instantiate(itemBox, items[i].transform.position, items[i].transform.rotation);
                    timers[i] = 0;
                }
                
            }
        }
    }
}
