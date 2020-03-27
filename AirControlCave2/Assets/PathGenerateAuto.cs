using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGenerateAuto : MonoBehaviour
{
    public Transform [] target; //target objects i.e. push, terminals
    public int p; //for now acting as the menu button
    public float speed; // speed of our plane

    public int current; //index of the target array

    //boolean to make sure update runs once only
    bool PushpathGen = true; 
    bool TerminalpathGen = false;


    // Update is called once per frame
    void Update()
    {
        //push will work first 
        if(PushpathGen){
            PushpathGenerator(); //method for push
        }else{
            //then we will move to the terminal
            if(TerminalpathGen){
                TerminalpathGenerator(p); //method for terminal
            }
        }

    }

    public void PushpathGenerator(){
            
                if(transform.position != target[current].position){
                    Vector3 pos =  Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                    GetComponent<Rigidbody>().MovePosition(pos);
                }else if(transform.position == target[current].position){
                    PushpathGen = false;
                }

    }

    public void TerminalpathGenerator(int targetTerminal){
            current = targetTerminal;            
            if(transform.position != target[current].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[current].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[current].position){
                TerminalpathGen = false;
            }

    }
        //make a loop of path
        // else{ 
        //     current = (current + 1) % target.Length;
        // }

    public void PushToggleButton(){
        PushpathGen = !PushpathGen;
        // TerminalpathGen = !TerminalpathGen;
        print(PushpathGen);
    }

    public void TerminalToggleButton(){
        TerminalpathGen = !TerminalpathGen;
        // TerminalpathGen = !TerminalpathGen;
        print(TerminalpathGen);
    }

        
}
