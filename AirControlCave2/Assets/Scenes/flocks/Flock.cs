using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flock : MonoBehaviour
{

    public FlockAgent agentPrefab;
    List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehavior behavior;

    [Range(10,50)]

    public int startingCount = 50;
     const float AgentDensity = 0.08f;

    [Range(1f, 100f)]
    public float driveFactor = 10f;

    [Range(1f, 100f)]
    public float maxSpeed = 5f;

    [Range(1f,10f)]
    public float neighborRadius = 1.5f;

    [Range(0f,1f)]
    public float avoidRadiusMultiplier =0.5f;

    float squareMaxSpeed;
    float squareNeighborRadius;
    float squareAvoidanceRadius;
    public float SquareAvoidanceRadius { get {return squareAvoidanceRadius;}}

    // Start is called before the first frame update
    void Start()
    {
        squareMaxSpeed = maxSpeed * maxSpeed;
        squareNeighborRadius = neighborRadius * neighborRadius;
        squareAvoidanceRadius =  squareNeighborRadius * avoidRadiusMultiplier * avoidRadiusMultiplier;
         Vector3 rotationVector = new Vector3(0,0, 1);
         Vector3 vect = new Vector3(0.7f,0.4f, 0.8f);
        for (int i = 0; i <startingCount; i++){
            FlockAgent newAgent = Instantiate(
                agentPrefab,
                Random.insideUnitCircle * startingCount * AgentDensity,
                Quaternion.Euler(rotationVector * Random.Range(0f,10f)),
                transform
            );
            newAgent.transform.Rotate(30.0f, 90.0f, 0.0f);
            newAgent.transform.Translate(vect * -100);
            //newAgent.transform.Rotate(0.0f, 90.0f, 180.0f);
            newAgent.name = "Agent" + i;
            agents.Add(newAgent);
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(FlockAgent agent in agents){
            List<Transform> context = GetNearbyObjects(agent);


            Vector3 move = behavior.CalculateMove(agent, context, this);
            move *= driveFactor;
            if(move.sqrMagnitude > squareMaxSpeed){
                move = move.normalized * maxSpeed;
            }
             //agent.transform.Rotate(-90.0f, -90.0f, 90.0f);
            agent.Move(move);
        }
    }

    List<Transform> GetNearbyObjects(FlockAgent agent){
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, neighborRadius);
        foreach(Collider c in contextColliders){
            if(c != agent.AgentCollider){
                context.Add(c.transform);
            }
        }
        return context;
    }
}
