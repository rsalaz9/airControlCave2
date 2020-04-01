using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    public Score scoreManager;
    public GameObject gameover;
    void Start(){
            gameover = GameObject.Find("GameOver");
            gameover.active = false;
    }

        void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "TakeOffATarget" || theCollision.gameObject.tag == "TakeOffBTarget" ){
                Debug.Log("collision detected");
                scoreManager.AddPoint();
                Destroy(gameObject);
                
        }

        if(theCollision.gameObject.tag == "airplane") {
                Debug.Log("collision detected");
                Invoke("Restart",5f);
            
                gameover.active = true;
        }

    }

    void Restart(){
        SceneManager.LoadScene("Predefined_Path_with_menu");
    }
}
