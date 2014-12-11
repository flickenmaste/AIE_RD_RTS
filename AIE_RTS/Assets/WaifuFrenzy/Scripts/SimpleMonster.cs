using UnityEngine;
using System.Collections;

public class SimpleMonster : MonoBehaviour {

    // Start Location for Lerp
    public Vector3 StartLocation;

    // Var for which spawner spawned object
    public GameObject MySpawner;

    // Check if path has ended
    public bool Moving = false;

    // Speed of object
    public float Speed;

    // NavMesh
    private NavMeshPath Path;
    public NavMeshAgent Agent;

    // Use this for initialization
	void Start () 
    {
        Speed = 0.2f;
        StartLocation = this.transform.position;
        MySpawner = GameObject.Find("SimpleSpawner");
        Moving = true;
        Agent.SetDestination(MySpawner.GetComponent<SimpleSpawner>().EndLocation.transform.position);
    }
	
	// Update is called once per frame
	void Update () 
    {
        //GoToPosition();
	}

    void GoToPosition()
    {
        //Vector3 end = MySpawner.GetComponent<SimpleSpawner>().EndLocation.transform.position;
        //if (Moving == true)
       //{
            //this.transform.position = Vector3.Lerp(this.transform.position, end, Speed);
        //}
    }

    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject == MySpawner.GetComponent<SimpleSpawner>().EndLocation.gameObject)
        {
            DestroyObject(this.gameObject, 0);
        }
    }
}
