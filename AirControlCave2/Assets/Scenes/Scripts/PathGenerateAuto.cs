using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PathGenerateAuto : MonoBehaviour
{
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

    //boolean to keep everything in place
    bool push = true;
    bool taxi = true;
    bool runway = true;

    //run way air checker
    bool toTheAirA = false;
    bool toTheAirB = false;

    // Update is called once per frame
    void Update()
    {
        
        //plane will push back 
        if(PushpathGen == true){
            print("push path called");
            PushpathGenerator(0); //method for push
            //it will taxi then
            // print("push flag" + push);
        }    
        if(push == false && taxiPath == true){
            print("terminal called");
            // print("i am called");
            TaxiPathGenerator(1); //method for taxi
        }
        //then we will move to the terminal
        if(push == false && taxi == false && TerminalpathGen == true){
            print("runway called");
            TerminalpathGenerator(currentRunway); //method for terminal
        }  

        //then let's take off our plane
        if(push == false && taxi == false && runway == false && takeoff == true){
            TakeOffPathGenerator();
        }      

    }

    //it will generate push back path
    public void PushpathGenerator(int current){
            
                if(transform.position != target[current].position){
                    Vector3 pos =  Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                    GetComponent<Rigidbody>().MovePosition(pos);
                }else if(transform.position == target[current].position){
                    //let's rotate the object
                    transform.Rotate(0, 90, 0);
                    // checkTargetVariable();
                    PushpathGen = false;
                    push = false;
                }

    }

    public void PushToggleButton(){
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