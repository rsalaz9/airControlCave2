using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class PlaneController : MonoBehaviour
{
    // Start is called before the first frame update
    public Score scoreManager;
    
    public TakeOffs takeOffsManager;
    public GameObject warningPrefab;
    public GameObject warningInstance;
    GameObject score;
    GameObject takeOffs;
    public GameObject gameover;
    public GameObject fire;
    public GameObject redLight;
    Vector3 GateA;
    Vector3 GateB;
    Vector3 GateC;
    Vector3 GateD;
    AudioSource audioSource;
   [SerializeField] AudioClip audioGateA;
   [SerializeField] AudioClip audioGateB;
   [SerializeField] AudioClip audioGateC;
   [SerializeField] AudioClip audioGateD;



    void Start(){
            score = GameObject.Find("Score");
            scoreManager = score.GetComponent<Score>();
            takeOffs = GameObject.Find("TakeOffs");
            takeOffsManager = takeOffs.GetComponent<TakeOffs>();
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
            GateA = GameObject.Find("Gate A").transform.position;
            GateB = GameObject.Find("Gate B").transform.position;
            GateC = GameObject.Find("Gate C").transform.position;
            GateD = GameObject.Find("Gate D").transform.position;
            audioSource = GetComponent<AudioSource>();

    }

    void OnCollisionEnter(Collision theCollision)
    {
        if(theCollision.gameObject.tag == "TakeOffATarget" || theCollision.gameObject.tag == "TakeOffBTarget" ){
                Debug.Log("collision detected");
                scoreManager.AddTakeOffScore();
                takeOffsManager.AddTakeOffsPoint();
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
        SceneManager.LoadScene("new_trial_scene");
    }

    public void GenerateDepartureWarning(){
        Debug.Log(transform.position);
        warningInstance = Instantiate(warningPrefab, transform.position, transform.rotation);
        warningInstance.transform.parent = gameObject.transform;
        playWarningSound();
    }

    public void playWarningSound(){
        if (!audioSource.isPlaying && transform.position == GateA){
            Debug.Log("generae warning at gate A");
            audioSource.PlayOneShot(audioGateA);
        }
        else if (!audioSource.isPlaying && transform.position == GateB){
            Debug.Log("generae warning at gate B");
            audioSource.PlayOneShot(audioGateB);
        }
        else if (!audioSource.isPlaying && transform.position == GateC){
            Debug.Log("generae warning at gate C");
            audioSource.PlayOneShot(audioGateC);
        }
        else if (!audioSource.isPlaying && transform.position == GateD){
            Debug.Log("generae warning at gate D");
            audioSource.PlayOneShot(audioGateD);
        }     
    }
}



