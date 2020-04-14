using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PathGenerateAuto : MonoBehaviour
{
    public SelectObject objectSelect = new SelectObject();

    public Transform [] target; //target objects i.e. push, terminals
    // Transform testTarget;
    GameObject [] pathGameobject = new GameObject[15];
    bool objectAdded = true;
    // public int p; //for now acting as the menu button
    public float speed; // speed of our plane

    int currentRunway; //index of the target runway

    // public Button Aterminal;
    // public Button Bterminal;

    //boolean to make sure update runs once only
    bool PushpathGen = false; 
    bool pushed = false;
    bool taxiPath = false;
    bool terminalA = false;
    bool terminalB = false;
    bool TerminalpathGen = false;
    bool takeoff = false;
    public bool inGateForLongTime =false;

    //boolean to control the rotation
    bool pushRotate = true;
    bool taxiRotate = true;
    bool terminalRotate = true;

    bool pushFromGate = true;
    int pushCurrent;

    //boolean to keep everything in place
    bool push = true;
    bool taxi = true;
    bool runway = true;

    public float startTime;
    public float currentTime;
    public float timeAtGate;

    //run way air checker
    bool toTheAirA = false;
    bool toTheAirB = false;

    bool playedSound;
    AudioSource audioSource;
   [SerializeField] AudioClip audioTakeOff;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        playedSound = true;
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {   
        currentTime = Time.time;
        if (pushed == false){
            timeAtGate = currentTime - startTime;
            if (timeAtGate > 5){
                inGateForLongTime = true;
            }
        }
        else {
            startTime = currentTime;
            inGateForLongTime = false;
        }

        if(objectAdded){
            populateTarget();
        }
        
        //plane will push back 
        if(PushpathGen == true){
            // print("push path called");
            PushpathGenerator(); //method for push
            //it will taxi then
            // print("push flag" + push);
        }    
        if(push == false && taxiPath == true){
            // print("terminal called");
            // print("i am called");
            TaxiPathGenerator(1); //method for taxi
        }
        //then we will move to the terminal
        if(push == false && taxi == false && TerminalpathGen == true){
            // print("runway called");
            TerminalpathGenerator(currentRunway); //method for terminal
        }  

        //then let's take off our plane
        if(push == false && taxi == false && runway == false && takeoff == true){
            Invoke("playTakeOffSound",10f);
            TakeOffPathGenerator();
        }      

    }

    public void populateTarget(){
                //make the references of the objects

        pathGameobject[0] = GameObject.Find("Push B");
        // target[0] = pathGameobject[0].transform;

        pathGameobject[1] = GameObject.Find("Taxi");
        // target[1] = pathGameobject[1].transform;

        pathGameobject[2] = GameObject.Find("RunwayA");
        // target[2] = pathGameobject[2].transform;

        pathGameobject[3] = GameObject.Find("RunwayB");
        // target[3] = pathGameobject[3].transform;

        pathGameobject[4] = GameObject.Find("Takeoff to ground A");
        // target[4] = pathGameobject[4].transform;

        pathGameobject[5] = GameObject.Find("ground to air A");
        // target[5] = pathGameobject[5].transform;

        pathGameobject[6] = GameObject.Find("Takeoff to ground B");
        // target[6] = pathGameobject[6].transform;

        pathGameobject[7] = GameObject.Find("ground to air B");
        // target[7] = pathGameobject[7].transform;

        pathGameobject[8] = GameObject.Find("Push A");
        // target[8] = pathGameobject[8].transform;

        pathGameobject[9] = GameObject.Find("Push C");
        // target[9] = pathGameobject[9].transform;

        pathGameobject[10] = GameObject.Find("Push D");
        // target[10] = pathGameobject[10].transform;

        pathGameobject[11] = GameObject.Find("Gate A");
        // target[11] = pathGameobject[11].transform;

        pathGameobject[12] = GameObject.Find("Gate B");
        // target[12] = pathGameobject[12].transform;

        pathGameobject[13] = GameObject.Find("Gate C");
        // target[13] = pathGameobject[13].transform;

        pathGameobject[14] = GameObject.Find("Gate D");
        // target[14] = pathGameobject[14].transform;
        // populateTarget(pathGameobject);

        print(pathGameobject);
        for(int i = 0 ; i < pathGameobject.Length ; i++ ){
            target[i] = pathGameobject[i].transform;
            //print(i);
        }
        objectAdded = false;

    }

    public void playTakeOffSound(){
        if (!audioSource.isPlaying && playedSound ==  true ){
            audioSource.PlayOneShot(audioTakeOff);
            playedSound = false;
        }
        
    }

     //hold position
    public void holdPosition(){
        if(push == true){
            PushToggleButton();
        }else if(push == false && taxi == true){
            TaxiToggleButton();
        }else if(push == false && taxi == false && runway == true){
            TerminalpathGen = !TerminalpathGen;
        }
    }

    //it will generate push back path
    public void PushpathGenerator(){

        // print("object position" + transform.position);
        // print("push B position" + target[12].position);
        if(pushFromGate){
            // print("object position" + transform.position);
            // print("push A" + target[8].position);
            // print("push B" + target[0].position);
            // print("push C" + target[9].position);
            // print("push D" + target[10].position);
            // print("name " + objectSelect.selecetedGameObject);

            generatePushPosition(objectSelect.selecetedGameObject.transform.position, target[8].position, target[0].position, target[9].position, target[10].position);
            // print(Enumerable.Range(target[12].position + 5, target[12].position - 5).Contains(transform.position));
            // if(objectSelect.gameObjectName == "Aeroplane (2)"){ //gate A
            //     print("Gate A");
            //     pushCurrent = 8 ; //push A
            // }
            // else if(objectSelect.gameObjectName == "Aeroplane"){ //gate B
            //     print("Gate B");
            //     pushCurrent = 0; //push B
            // }
            // else if(objectSelect.gameObjectName == "GroundPlane"){ //gate C
            //     print("gate C");
            //     pushCurrent = 9; //push C
            // }
            // else if(objectSelect.gameObjectName == "Aeroplane (3)"){ //gate D
            //     print("Gate D");
            //     pushCurrent = 10 ; //push D
            // }

            pushFromGate = false;

        }
        if(transform.position != target[pushCurrent].position){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[pushCurrent].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }else if(transform.position == target[pushCurrent].position){
            //let's rotate the object
            if(pushRotate){
                transform.Rotate(0, 90, 0);
                pushRotate = false;

            }
            // checkTargetVariable();
            PushpathGen = false;
            push = false;
        }

    }

    public void generatePushPosition(Vector3 plane, Vector3 pushToA, Vector3 pushToB, Vector3 pushToC, Vector3 pushToD){
        // distance between two points = ( (x1 - x2)^2 + (y1 - y2)^2 + (z1 - z2)^2 )^1/2
        double distanceA = Math.Sqrt( (plane.x - pushToA.x) * (plane.x - pushToA.x) + (plane.y - pushToA.y) * (plane.y - pushToA.y) + (plane.z - pushToA.z) * (plane.z - pushToA.z));
        double distanceB = Math.Sqrt( (plane.x - pushToB.x) * (plane.x - pushToB.x) + (plane.y - pushToB.y) * (plane.y - pushToB.y) + (plane.z - pushToB.z) * (plane.z - pushToB.z));
        double distanceC = Math.Sqrt( (plane.x - pushToC.x) * (plane.x - pushToC.x) + (plane.y - pushToC.y) * (plane.y - pushToC.y) + (plane.z - pushToC.z) * (plane.z - pushToC.z));
        double distanceD = Math.Sqrt( (plane.x - pushToD.x) * (plane.x - pushToD.x) + (plane.y - pushToD.y) * (plane.y - pushToD.y) + (plane.z - pushToD.z) * (plane.z - pushToD.z));

        // double [] alldistance = {distanceA, distanceB, distanceC,distanceD};

        // double minDistance = alldistance.Min();
        //8  0  9  10
        print("A " + distanceA + " B " + distanceB + " C " + distanceC + " D " + distanceD);
        
        if(distanceA < distanceB && distanceA < distanceC && distanceA < distanceD){
            //pushA
            pushCurrent = 8;
        }else if(distanceB < distanceA && distanceB < distanceC && distanceB < distanceD){
            //push b
            pushCurrent = 0;
        }else if(distanceC < distanceA && distanceC < distanceB && distanceC < distanceD){
            //push c
            pushCurrent = 9;
        }else if(distanceD < distanceA && distanceD < distanceB && distanceD < distanceC){
            pushCurrent = 10;
        }
    }

    public void PushToggleButton(){
        print("pushtoggle was calledul");
        PushpathGen = !PushpathGen;
        pushed = true;
        Transform childWarning = gameObject.transform.Find("Warning(Clone)");
        Debug.Log("childWarnign");
        Debug.Log(childWarning);
        if (childWarning){
            Destroy(childWarning.gameObject);
        }
        // TerminalpathGen = !TerminalpathGen;
        // print("Push path" + PushpathGen);
    }




    // it will generate taxi path
    public void TaxiPathGenerator(int current){
            // current = current;            
            if(transform.position != target[current].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[current].position){
                //rotate again
                if(taxiRotate){
                    transform.Rotate(0, 90, 0);
                    taxiRotate = false;
                }
                taxiPath = false;
                taxi = false;
            }

    }

    public void TaxiToggleButton(){
        if(push == false){
            taxiPath = !taxiPath;
        }
        // print("taxi path" + taxiPath);
    }




    // it will generate terminal path
    public void TerminalpathGenerator(int current){
            // current = current;            
            if(transform.position != target[current].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[current].position){
                //rotate again
                if(terminalRotate){
                    transform.Rotate(0, 90, 0);
                }
                TerminalpathGen = false;
                runway = false;
            } 

    }     
    
    public void RunwayAToggleButton(){
        currentRunway = 2;
        if(push == false && taxi == false){
            TerminalpathGen = !TerminalpathGen;
        }
    }
    public void RunwayBToggleButton(){
        currentRunway = 3;
        if(push == false && taxi == false){
            TerminalpathGen = !TerminalpathGen;
        }
        
    }

    public void TakeOffPathGenerator(){
       
        print("i am taking off");
        print(currentRunway);
        if(currentRunway == 2){ // we will go to 4,5
            int runToGroundA = 4;
            int groundToAirA = 5;

            if(toTheAirA == false &&  transform.position != target[runToGroundA].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[runToGroundA].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[runToGroundA].position){
                toTheAirA = true;
            } 

            if(toTheAirA){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[groundToAirA].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);

                if(transform.position == target[groundToAirA].position){
                    Destroy(gameObject);
                }

            }

        }else if(currentRunway == 3){ // we will go to 6,7
            int runToGroundB = 6;
            int groundToAirB = 7;

            if(toTheAirB == false && transform.position != target[runToGroundB].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[runToGroundB].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[runToGroundB].position){
                toTheAirB = true;
            } 

            if(toTheAirB){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[groundToAirB].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);

                if(transform.position == target[groundToAirB].position){
                    Destroy(gameObject);
                }
            }

        }
    }
   

    public void TakeOfPermission(){
        takeoff = true;
    }
        
}