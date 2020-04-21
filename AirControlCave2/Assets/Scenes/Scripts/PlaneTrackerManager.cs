using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 using System.Linq;

public class PlaneTrackerManager : MonoBehaviour
{
    public GameObject PlaneAtGround;
    public GameObject PlaneAtAir;
    Vector3 GateA;
    Vector3 GateB;
    Vector3 GateC;
    Vector3 GateD;
    Vector3 AirPositionA;
    Vector3 AirPositionB;
    public GameObject[] getGroundCount;
    public GameObject[] getAirCount;
    public GameObject [] PlanesAtGate;
    public GameObject PlaneLongestAtGate = null;
    public GameObject plane = null;    
    private float nextActionTime = 0f;
    public float nextActionTimeGen = 10f;
    float period = 30f;
    int whichRun;
    float periodgen = 40f;
    public float currentTime;
    public int PlanesAtGateCount =0;
    private int GroundPlanesCnt =-1;
    public int PlanesAtAirCount =0;
    private int PlanesAtAirCnt =-1;
    int count =0;
    public System.Random random;

    // Start is called before the first frame update
    void Start()
    {   
        GateA = GameObject.Find("Gate A").transform.position;
        GateB = GameObject.Find("Gate B").transform.position;
        GateC = GameObject.Find("Gate C").transform.position;
        GateD = GameObject.Find("Gate D").transform.position;
        AirPositionA = GameObject.Find("Air Position A").transform.position;
        AirPositionB = GameObject.Find("Air Position B").transform.position;

        Instantiate(PlaneAtGround, GateB, PlaneAtGround.transform.rotation);
        Instantiate(PlaneAtGround, GateC, PlaneAtGround.transform.rotation);
        Instantiate(PlaneAtGround, GateD, PlaneAtGround.transform.rotation);
        // GameObject planeAir = Instantiate(PlaneAtAir, AirPositionB, PlaneAtAir.transform.rotation);
        // planeAir.GetComponent<PathGeneratorForLand>().landB = true;

        getGroundCount = GameObject.FindGameObjectsWithTag("GroundPlane");
        getAirCount = GameObject.FindGameObjectsWithTag("InAirPlane");
        random = new System.Random();
    }

    // Update is called once per frame
    void Update()
    {   
        currentTime = Time.time;
        getGroundCount = GameObject.FindGameObjectsWithTag("GroundPlane");
        getAirCount = GameObject.FindGameObjectsWithTag("InAirPlane"); 
        AlertGroundPlaneLogic(getGroundCount, currentTime);
        GenerateLandingPlanes(getGroundCount, getAirCount);
       
    }

    void AlertGroundPlaneLogic(GameObject [] getGroundCount, float currentTime){
        PlanesAtGate = getGroundCount.OrderBy(GPlane => GPlane.GetComponent<PathGenerateAuto>().timeAtGate).ToArray();
        PlaneLongestAtGate = PlanesAtGate[PlanesAtGate.Length-1];
        if (PlaneLongestAtGate != plane){
            if (PlaneLongestAtGate.GetComponent<PathGenerateAuto>().inGateForLongTime){
            // Debug.Log(PlaneLongestAtGate);
                if (count == 0){
                    nextActionTime = 25f;
                    GenerateWarningForReadyToDepart(PlaneLongestAtGate);
                    count ++;
                }
                if (currentTime > nextActionTime){
                    period = random.Next(20, 40);
                    nextActionTime = Time.time + period;
                    GenerateWarningForReadyToDepart(PlaneLongestAtGate);
                    plane = PlaneLongestAtGate;
                }                
            }
        }
    }


    void GenerateWarningForReadyToDepart(GameObject PlaneLongestAtGate){
        PlaneLongestAtGate.GetComponent<PlaneController>().GenerateDepartureWarning();
    }

    void GenerateLandingPlanes(GameObject [] getGroundCount, GameObject [] getAirCount) {
        if (currentTime > nextActionTimeGen){
            nextActionTimeGen = Time.time + periodgen;
            PlanesAtGateCount = CountPlanesAtGates(getGroundCount);
            PlanesAtAirCount = CountPlanesInAir(getAirCount);
            if (PlanesAtGateCount != GroundPlanesCnt) {
                if (PlanesAtGateCount >= 0 && PlanesAtGateCount <= 2) {
                    if (getAirCount.Length ==0 || PlanesAtAirCount ==0 ){
                        Debug.Log("is it hitting this condition");
                        ChooseRunwayToLand();
                    }    
                }
                if (PlanesAtGateCount >=  3 && getAirCount.Length ==0){
                   ChooseRunwayToLand();
                }
                GroundPlanesCnt = PlanesAtGateCount;
            }
        }
    }

    void ChooseRunwayToLand(){
        GameObject planeAir;
        whichRun = random.Next(0, 2);
        if (whichRun > 0){
            Debug.Log("make one plane land at B");
            planeAir = Instantiate(PlaneAtAir, AirPositionB, PlaneAtAir.transform.rotation);
            planeAir.GetComponent<PathGeneratorForLand>().landB = true;
        }else {
            Debug.Log("make one plane land at A");
            planeAir = Instantiate(PlaneAtAir, AirPositionA, PlaneAtAir.transform.rotation);
            planeAir.GetComponent<PathGeneratorForLand>().landA = true;
        }
    }

    int CountPlanesInAir(GameObject [] getAirCount) {
        int AirPlaneCount = 0;
        foreach (GameObject PlaneInAir in getAirCount){
            if (PlaneInAir.transform.position.y > -1){
                Debug.Log("plane in air");
                AirPlaneCount++;
            }
        }
        return AirPlaneCount;
    }

    int CountPlanesAtGates(GameObject [] getGroundCount) {
        int GroundPlaneCount = 0;
        foreach (GameObject groundPlane in getGroundCount){
            if (groundPlane.transform.position == GateA){
                Debug.Log("at gate A");
                GroundPlaneCount++;
            }
            else if (groundPlane.transform.position == GateB){
                Debug.Log("at gate B");
                GroundPlaneCount++;
            }
            else if (groundPlane.transform.position == GateC){
                Debug.Log("at gate C");
                GroundPlaneCount++;
            }
            else if (groundPlane.transform.position == GateD){
                Debug.Log("at gate D");
                GroundPlaneCount++;
            }
        }
        return GroundPlaneCount;
    }

}



