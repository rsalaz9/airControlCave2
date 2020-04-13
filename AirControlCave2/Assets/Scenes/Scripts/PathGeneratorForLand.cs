using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneratorForLand : MonoBehaviour
{
    public Transform [] target; //target objects i.e. push, terminals
    public float speed = 50f; // speed of our plane

    GameObject [] pathGameobject = new GameObject[15];
    bool objectAdded = true;

    public GameObject prefab;

    bool holdPosition = true;

    int startPos;
    int endPos;

    bool nextPos = false;
    bool gateButtonClicked = false;

    //for animation
    public bool landB = false;
    bool moveForB = false;
    bool end = false;

    public bool landA = false;
    bool moveForA = false;

    bool animation = true;
    AudioSource audioSource;
   [SerializeField] AudioClip audioTakeOff;

    bool playedSound;
    public Score scoreManager;
    GameObject score;
    GameObject landings;
    public Landings landingsManager;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        playedSound = true;
        score = GameObject.Find("Score");
        scoreManager = score.GetComponent<Score>();

        landings = GameObject.Find("Landings");
        landingsManager = landings.GetComponent<Landings>();
    }

    // Update is called once per frame
    void Update()
    {
         if(objectAdded){
            populateTarget();
        }

        if(animation){
            holdPosition = false;
            makeThePlaneAnimation();
        }
        
        if(holdPosition){
             parkThePlane();
        }
       
        
    }

    public void populateTarget(){
                //make the references of the objects

        pathGameobject[0] = GameObject.Find("Push A");
        // target[0] = pathGameobject[0].transform;

        pathGameobject[1] = GameObject.Find("Gate A");
        // target[1] = pathGameobject[1].transform;

        pathGameobject[2] = GameObject.Find("Push B");
        // target[2] = pathGameobject[2].transform;

        pathGameobject[3] = GameObject.Find("Gate B");
        // target[3] = pathGameobject[3].transform;

        pathGameobject[4] = GameObject.Find("Push C");
        // target[4] = pathGameobject[4].transform;

        pathGameobject[5] = GameObject.Find("Gate C");
        // target[5] = pathGameobject[5].transform;

        pathGameobject[6] = GameObject.Find("Push D");
        // target[6] = pathGameobject[6].transform;

        pathGameobject[7] = GameObject.Find("Gate D");
        // target[7] = pathGameobject[7].transform;

        pathGameobject[8] = GameObject.Find("Air Position B");
        // target[8] = pathGameobject[8].transform;

        pathGameobject[9] = GameObject.Find("Landed On Ground B");
        // target[9] = pathGameobject[9].transform;

        pathGameobject[10] = GameObject.Find("Move Forward to the ground B");
        // target[10] = pathGameobject[10].transform;

        pathGameobject[11] = GameObject.Find("End of the Road");
        // target[11] = pathGameobject[11].transform;

        pathGameobject[12] = GameObject.Find("Air Position A");
        // target[8] = pathGameobject[8].transform;

        pathGameobject[13] = GameObject.Find("Landed On Ground A");
        // target[9] = pathGameobject[9].transform;

        pathGameobject[14] = GameObject.Find("Move Forward to the ground A");
        // target[10] = pathGameobject[10].transform;        

        print(pathGameobject);
        for(int i = 0 ; i < pathGameobject.Length ; i++ ){
            target[i] = pathGameobject[i].transform;
            //print(i);
        }
        objectAdded = false;
    }


    //let's animate the plane 
    public void makeThePlaneAnimation(){
        if (landB){
            LandOnRunwayB();
        }
        else if (landA){
            LandOnRunwayA();
        }           
    }

    public void LandOnRunwayB() {
        int airPosition = 8;
        int landedOnGroundB = 9;
        int moveForwardB = 10;
        int endOfTheRoad = 11;
        if(moveForB == false && transform.position != target[landedOnGroundB].position){
                
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[landedOnGroundB].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }else if(transform.position == target[landedOnGroundB].position){
            // transform.Rotate(0,-90,0);
            moveForB = true;
                playTakeOffSound();
        }

        if(moveForB == true && end == false && transform.position != target[moveForwardB].position){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[moveForwardB].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }else if(transform.position == target[moveForwardB].position){
            transform.Rotate(0,90,0);
            end = true;
        }

        if(moveForB && end){

            Vector3 pos =  Vector3.MoveTowards(transform.position, target[endOfTheRoad].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);

            if(transform.position == target[endOfTheRoad].position){
                transform.Rotate(0,90,0);
                holdPosition = true;
                animation = false;
                speed = 10f;
                }      
        }
    }

    public void LandOnRunwayA() {
        int airPosition = 12;
        int landedOnGroundA = 13;
        int moveForwardA = 14;
        int endOfTheRoad = 11;
        if(moveForA == false && transform.position != target[landedOnGroundA].position){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[landedOnGroundA].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }else if(transform.position == target[landedOnGroundA].position){
            moveForA = true;
            playTakeOffSound();
        }
        if(moveForA == true && end == false && transform.position != target[moveForwardA].position){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[moveForwardA].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }else if(transform.position == target[moveForwardA].position){
            transform.Rotate(0,90,0);
            end = true;
        }
        if(moveForA && end){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[endOfTheRoad].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);

            if(transform.position == target[endOfTheRoad].position){
                transform.Rotate(0,90,0);
                holdPosition = true;
                animation = false;
                speed = 10f;
                }      
        }
    }


    public void playTakeOffSound(){
        if (!audioSource.isPlaying && playedSound ==  true ){
            audioSource.PlayOneShot(audioTakeOff);
            playedSound = false;
        }
        
    }




    public void parkThePlane(){
            // print("called");
            // print("start" + startPos);
            // print("end" + endPos);
            if(gateButtonClicked){
                if(nextPos == false &&  transform.position != target[startPos].position){
                    // print("condition first");
                    // print("target position" + target[startPos].position);
                    // GameObject theCube = GameObject.Find("Aeroplane Land");
                    // Vector3 pos =  Vector3.MoveTowards(theCube.transform.localPosition, target[startPos].position, speed * Time.deltaTime);
                    Vector3 pos =  Vector3.MoveTowards(transform.position, target[startPos].position, speed * Time.deltaTime);
                    // transform.Rotate(0, 90, 0);
                    // print(pos);
                    
                    //  print(theCube.transform.position);
                    //  print("local position" + theCube.transform.localPosition);
                    // print(gameObject.GetComponent<Rigidbody>().position);
                    // theCube.GetComponent<Rigidbody>().MovePosition(pos);
                    GetComponent<Rigidbody>().MovePosition(pos);

                }else if(transform.position == target[startPos].position){
                    transform.Rotate(0, 270, 0);
                    nextPos = true;
                } 

                if(nextPos){
                    print("condition second");
                    Vector3 pos =  Vector3.MoveTowards(transform.position, target[endPos].position, speed * Time.deltaTime);
                    GetComponent<Rigidbody>().MovePosition(pos);

                    if(transform.position == target[endPos].position){
                        holdPosition = false;
                        Instantiate(prefab, pos, prefab.transform.rotation);//, new Vector3(target[endPos].position.x,target[endPos].position.y,target[endPos].position.z));//, Quaternion.identity);
                        //also have to destroy menu that was instantiated for that plane
                        Destroy(gameObject.GetComponent<SelectObjectAir>().menu);
                        Destroy(gameObject);
                    }

                }

            }
    }

    public void GateA(){
        print("gateA");
        gateButtonClicked = true;
        startPos = 0;
        endPos = 1;
        scoreManager.AddLandingScore();
        landingsManager.AddLandingsPoint();

    }
    public void GateB(){
        print("gateB");
        gateButtonClicked = true;
        startPos = 2;
        endPos = 3;
        scoreManager.AddLandingScore();
        landingsManager.AddLandingsPoint();
    }
    public void GateC(){
        print("gateC");
        gateButtonClicked = true;
        startPos = 4;
        endPos = 5;
        scoreManager.AddLandingScore();
        landingsManager.AddLandingsPoint();
    }
    public void GateD(){
        print("gateD");
        gateButtonClicked = true;
        startPos = 6;
        endPos = 7;
        scoreManager.AddLandingScore();
        landingsManager.AddLandingsPoint();
    }
    public void holPositioButton(){
        holdPosition = !holdPosition;
    }
}
