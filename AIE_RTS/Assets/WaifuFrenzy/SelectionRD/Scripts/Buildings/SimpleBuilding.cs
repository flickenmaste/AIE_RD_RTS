using UnityEngine;
using System.Collections;

public class SimpleBuilding : MonoBehaviour {

    // Player camera
    private Camera camera;
    
    // Bool to show that building is placed
    [SerializeField]
    private bool isPlaced;
    public bool IsPlaced
    {
        get
        {
            return isPlaced;
        }
        set
        {
            isPlaced = value;
            CheckBool(isPlaced);
        }
    }

    // Nav mesh obstacle
    public NavMeshObstacle Obstacle;

    // Units building spawns
    public GameObject Unit;
    
    // Use this for initialization
	void Start () 
    {
        camera = Camera.main;
        
        // Disable buildings nav obstacle so to not move already in game units
        //Obstacle.enabled = false;
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (isPlaced)
        {
            Obstacle.enabled = true;
        }
	}

    // Check placed bool
    void CheckBool(bool placed)
    {
        if (placed)
        {
            // Enable nav obstacle once placed
            //Obstacle.enabled = true;
        }
    }

    // Spawn unit
    public void SpawnUnit()
    {
        GameObject clone;
        clone = Instantiate(Unit, new Vector3(this.transform.position.x, this.transform.position.y, this.transform.position.z - 8), Quaternion.identity) as GameObject;
    }
}
