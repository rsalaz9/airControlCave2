using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PathGenerateAuto : MonoBehaviour
{
    public Transform [] target; //target objects i.e. push, terminals
    // public int p; //for now acting as the menu button
    public float speed; // speed of our plane

    int currentRunway; //index of the target runway

    // public button Aterminal;
    // public button Bterminal;

    //boolean to make sure update runs once only
    bool PushpathGen = false; 
    bool taxiPath = false;
    bool terminalA = false;
    bool terminalB = false;
    bool TerminalpathGen = false;

    //boolean to keep everything in place
    bool push = true;
    bool taxi = true;


    // Update is called once per frame
    void Update()
    {
        
        //plane will push back 
        if(PushpathGen == true){
            PushpathGenerator(0); //method for push
            //it will taxi then
            // print("push flag" + push);
        }    
        if(push == false && taxiPath == true){
            // print("i am called");
            TaxiPathGenerator(1); //method for taxi
        }
        //then we will move to the terminal
        if(push == false && taxi == false && TerminalpathGen == true){
            TerminalpathGenerator(currentRunway); //method for terminal
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
            } 

    }     

     public void RunWayA(){
        currentRunway = 2;
    }

    public void RunWayB() {
        currentRunway = 3;
    }
    
    public void TerminalToggleButton(){
        print(EventSystem.current.currentSelectedGameObject.name);
        if(EventSystem.current.currentSelectedGameObject.name == "Runway A"){
            currentRunway = 2;
        }else if(EventSystem.current.currentSelectedGameObject.name == "Runway B"){
            currentRunway = 3;
        }
        if(push == false && taxi == false){
            TerminalpathGen = !TerminalpathGen;
        }
        // TerminalpathGen = !TerminalpathGen;
        // print("terminal path" + TerminalpathGen);
    }

    // public void checkTargetVariable(){
    //     for(int i = 0 ; i < target.Length; i++){
    //         if(target[i] == Push){
    //             print(i);
    //         }
    //         // print(target[i]);
    //     }
    // }
   

        
}