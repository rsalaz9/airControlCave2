using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    public Score scoreManager;


        void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "TakeOffATarget" || theCollision.gameObject.tag == "TakeOffBTarget" ){
                Debug.Log("collision detected");
                scoreManager.AddPoint();
                Destroy(gameObject);
                
        }
    }
}
