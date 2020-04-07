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
    public GameObject [] PlaneLongestAtGate;

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
        GameObject planeAir = Instantiate(PlaneAtAir, AirPositionB, PlaneAtAir.transform.rotation);
        planeAir.GetComponent<PathGeneratorForLand>().landB = true;

        getGroundCount = GameObject.FindGameObjectsWithTag("GroundPlane");
        getAirCount = GameObject.FindGameObjectsWithTag("InAirPlane");
    }

    // Update is called once per frame
    void Update()
    {   
        getGroundCount = GameObject.FindGameObjectsWithTag("GroundPlane");
        getAirCount = GameObject.FindGameObjectsWithTag("InAirPlane"); 
        PlaneLongestAtGate = getGroundCount.OrderBy(GPlane => GPlane.GetComponent<PathGenerateAuto>().timeAtGate).ToArray();
        Debug.Log(PlaneLongestAtGate);
    }
}
