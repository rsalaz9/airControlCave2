using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class FlockAgent : MonoBehaviour
{
    SphereCollider agentCollider;
    public SphereCollider AgentCollider{ get {return agentCollider;}}
    // Start is called before the first frame update
    void Start()
    {
        agentCollider = GetComponent<SphereCollider>();
    }

    public void Move(Vector3 velocity){
        transform.up = velocity;
        transform.position += (Vector3)velocity * Time.deltaTime;
    }
}
