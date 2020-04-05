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
    // public int p; //for now acting as the menu button
    public float speed; // speed of our plane

    int currentRunway; //index of the target runway

    // public Button Aterminal;
    // public Button Bterminal;

    //boolean to make sure update runs once only
    bool PushpathGen = false; 
    bool taxiPath = false;
    bool terminalA = false;
    bool terminalB = false;
    bool TerminalpathGen = false;
    bool takeoff = false;

    bool pushFromGate = true;
    int pushCurrent;

    //boolean to keep everything in place
    bool push = true;
    bool taxi = true;
    bool runway = true;

    //run way air checker
    bool toTheAirA = false;
    bool toTheAirB = false;

    bool playedSound;
    AudioSource audioSource;
   [SerializeField] AudioClip audioTakeOff;

    void Start(){
        audioSource = GetComponent<AudioSource>();
        playedSound = true;
    }

    // Update is called once per frame
    void Update()
    {
        
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
            print("object position" + transform.position);
            print("gate A" + target[11].position);
            print("gate B" + target[12].position);
            print("gate C" + target[13].position);
            print("Gate D" + target[14].position);
            print("name " + objectSelect.gameObjectName);
            // print(Enumerable.Range(target[12].position + 5, target[12].position - 5).Contains(transform.position));
            if(objectSelect.gameObjectName == "Aeroplane (2)"){ //gate A
                print("Gate A");
                pushCurrent = 8 ; //push A
            }
            else if(objectSelect.gameObjectName == "Aeroplane"){ //gate B
                print("Gate B");
                pushCurrent = 0; //push B
            }
            else if(objectSelect.gameObjectName == "GroundPlane"){ //gate C
                print("gate C");
                pushCurrent = 9; //push C
            }
            else if(objectSelect.gameObjectName == "Aeroplane (3)"){ //gate D
                print("Gate D");
                pushCurrent = 10 ; //push D
            }

            pushFromGate = false;

        }
        if(transform.position != target[pushCurrent].position){
            Vector3 pos =  Vector3.MoveTowards(transform.position, target[pushCurrent].position, speed * Time.deltaTime);
            GetComponent<Rigidbody>().MovePosition(pos);
        }else if(transform.position == target[pushCurrent].position){
            //let's rotate the object
            transform.Rotate(0, 90, 0);
            // checkTargetVariable();
            PushpathGen = false;
            push = false;
        }

    }

    public void PushToggleButton(){
        print("pushtoggle was called");
        PushpathGen = !PushpathGen;
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
                transform.Rotate(0, 90, 0);
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
                transform.Rotate(0, 90, 0);
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