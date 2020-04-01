using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneratorForLand : MonoBehaviour
{
    public Transform [] target; //target objects i.e. push, terminals
    public float speed; // speed of our plane

    bool holdPosition = true;

    int startPos;
    int endPos;

    bool nextPos = false;
    bool gateButtonClicked = false;


    // Update is called once per frame
    void Update()
    {
        
        if(holdPosition){
             parkThePlane();
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
                    transform.Rotate(0, 90, 0);
                    nextPos = true;
                } 

                if(nextPos){
                    print("condition second");
                    Vector3 pos =  Vector3.MoveTowards(transform.position, target[endPos].position, speed * Time.deltaTime);
                    GetComponent<Rigidbody>().MovePosition(pos);

                    if(transform.position == target[endPos].position){
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

    }
    public void GateB(){
        print("gateB");
        gateButtonClicked = true;
        startPos = 2;
        endPos = 3;
    }
    public void GateC(){
        print("gateC");
        gateButtonClicked = true;
        startPos = 4;
        endPos = 5;
    }
    public void GateD(){
                print("gateD");
        gateButtonClicked = true;
        startPos = 6;
        endPos = 7;
    }
    public void holPositioButton(){
        holdPosition = !holdPosition;
    }
}
