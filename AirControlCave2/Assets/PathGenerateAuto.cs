using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerateAuto : MonoBehaviour
{
    public Transform [] target;
    public float speed;

    public int current;

    // Update is called once per frame
    void Update()
    {
        if(transform.position != target[current].position){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
         }else{ //} if(current < (target.Length - 1)){
            current = (current + 1) % target.Length;
        }
        // }else if(current = (target.Length - 1)){
        //     continue;
        // }
        
    }
}
