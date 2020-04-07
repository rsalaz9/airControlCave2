using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    public Score scoreManager;
    public GameObject warningPrefab;
    public GameObject warningInstance;
    GameObject score;
    public GameObject gameover;
    public GameObject fire;
    public GameObject redLight;


    void Start(){
            score = GameObject.Find("Score");
            scoreManager = score.GetComponent<Score>();
            gameover = null;
            //GameObject.Find("") doesn't work for finding objects that are disabled so this is a way to find the inactive objects
            Transform[] trans1 = GameObject.Find("UI").GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trans1) {
                if (t.gameObject.name == "GameOver") {
                    gameover = t.gameObject;
                }
            }
            redLight = null;
            Transform[] trans2 = GameObject.Find("UI").GetComponentsInChildren<Transform>(true);
            foreach (Transform t in trans2) {
                if (t.gameObject.name == "FlickeringLight") {
                    redLight = t.gameObject;
                }
            }
            gameover.active = false;
            fire.active = false;
            redLight.active =false;

    }

    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "TakeOffATarget" || theCollision.gameObject.tag == "TakeOffBTarget" ){
                Debug.Log("collision detected");
                scoreManager.AddPoint();
                Debug.Log(gameObject);
                //also have to destroy menu that was instantiated for that plane
                Destroy(gameObject.GetComponent<SelectObject>().menu);
                Destroy(gameObject);  
        }

        if(theCollision.gameObject.tag == "GroundPlane" || theCollision.gameObject.tag == "InAirPlane" ) {
                Debug.Log("collision detected");
                Invoke("Restart",5f);
                fire.active = true;
                redLight.active = true;
                gameover.active = true;
        }

    }

    void Restart(){
        SceneManager.LoadScene("Predefined_Path_with_menu");
    }

    public void GenerateDepartureWarning(){
        Debug.Log(transform.position);
        warningInstance = Instantiate(warningPrefab, transform.position, transform.rotation);
        warningInstance.transform.parent = gameObject.transform;
    }
}
