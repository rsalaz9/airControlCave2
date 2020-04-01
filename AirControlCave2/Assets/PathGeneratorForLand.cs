using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathGeneratorForLand : MonoBehaviour
{
    public Transform [] target; //target objects i.e. push, terminals
    public float speed = 50f; // speed of our plane

    public GameObject prefab;

    bool holdPosition = true;

    int startPos;
    int endPos;

    bool nextPos = false;
    bool gateButtonClicked = false;

    //for animation
    bool landB = true;
    bool moveForB = false;
    bool end = false;

    bool animation = true;


    // Update is called once per frame
    void Update()
    {
        if(animation){
            holdPosition = false;
            makeThePlaneAnimation();
        }
        
        if(holdPosition){
             parkThePlane();
        }
       
        
    }


    //let's animate the plane 
    public void makeThePlaneAnimation(){

        //later we will use these to decide the runways
      //  if(currentRunway == 2){ 
            int airPosition = 8;
            int landedOnGroundB = 9;
            int moveForwardB = 10;
            int endOfTheRoad = 11;

            // if(landB == false &&  transform.position != target[airPosition].position){
            //     Vector3 pos =  Vector3.MoveTowards(transform.position, target[airPosition].position, speed * Time.deltaTime);
            //     GetComponent<Rigidbody>().MovePosition(pos);
            // }else if(transform.position == target[airPosition].position){
            //     landB == true;
            // } 

            if(landB == true && moveForB == false && transform.position != target[landedOnGroundB].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[landedOnGroundB].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[landedOnGroundB].position){
                // transform.Rotate(0,-90,0);
                moveForB = true;
            }

            if(landB == true && moveForB == true && end == false && transform.position != target[moveForwardB].position){
                Vector3 pos =  Vector3.MoveTowards(transform.position, target[moveForwardB].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);
            }else if(transform.position == target[moveForwardB].position){
                transform.Rotate(0,90,0);
                end = true;
            }

            if(end){

                Vector3 pos =  Vector3.MoveTowards(transform.position, target[endOfTheRoad].position, speed * Time.deltaTime);
                GetComponent<Rigidbody>().MovePosition(pos);

                if(transform.position == target[endOfTheRoad].position){
                    transform.Rotate(0,90,0);
                    holdPosition = true;
                    animation = false;
                    speed = 10f;
                 }      
            }

        //}


        //we will need it later
        // else if(currentRunway == 3){ // we will go to 6,7
        //     int runToGroundB = 6;
        //     int groundToAirB = 7;

        //     if(toTheAirB == false && transform.position != target[runToGroundB].position){
        //         Vector3 pos =  Vector3.MoveTowards(transform.position, target[runToGroundB].position, speed * Time.deltaTime);
        //         GetComponent<Rigidbody>().MovePosition(pos);
        //     }else if(transform.position == target[runToGroundB].position){
        //         toTheAirB = true;
        //     } 

        //     if(toTheAirB){
        //         Vector3 pos =  Vector3.MoveTowards(transform.position, target[groundToAirB].position, speed * Time.deltaTime);
        //         GetComponent<Rigidbody>().MovePosition(pos);

        //         if(transform.position == target[groundToAirB].position){
        //             Destroy(gameObject);
        //         }
        //     }

        // }

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
                        // Instantiate(prefab);//, new Vector3(target[endPos].position.x,target[endPos].position.y,target[endPos].position.z));//, Quaternion.identity);
                        // Destroy(gameObject);
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
