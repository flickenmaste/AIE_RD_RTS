using UnityEngine;
using System.Collections;

public class SimpleSpawner : MonoBehaviour {

    // Location of spawner
    public Vector3 StartLocation;

    public GameObject EndLocation;

    // Objects that can be spawned
    public GameObject Monster;
    
    // Use this for initialization
	void Start () 
    {
        // Set location of spawner
        this.transform.position = StartLocation;
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckInput();
	}

    void CheckInput()
    {
        // Check input to spawn object
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameObject clone;
            clone = Instantiate(Monster, transform.position, Quaternion.identity) as GameObject;
        }
    }
}
