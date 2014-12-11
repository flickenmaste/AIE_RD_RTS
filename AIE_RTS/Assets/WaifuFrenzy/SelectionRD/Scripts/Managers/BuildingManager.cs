using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {

    // Main camera
    private Camera camera;

    // Building to spawn
    public GameObject Building;
    public GameObject BuildingPrimary;
    [SerializeField]
    public bool BuildingPrimBool = false;
    public List<GameObject> BuildingList;

    // Clone and bools to manage placing
    public GameObject Clone;
    public bool CloneSpawned = false;
    
    // Use this for initialization
	void Start () 
    {
        camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () 
    {
        FollowCursor();
        PlaceBuilding();
	}

    void OnGUI()
    {
        // Button to create building clone
        if (GUI.Button(new Rect(Screen.width / 2, Screen.height - 50, 100, 50), "Spawn Building"))
        {
            Clone = Instantiate(Building) as GameObject;
            CloneSpawned = true;
            if(!BuildingPrimBool)
            {
                BuildingPrimary = Clone;
                BuildingPrimBool = true;
            }
        }

        // Button to spawn unit on primary building
        if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height - 50, 100, 50), "Spawn Units"))
        {
            if (BuildingPrimBool)
                BuildingPrimary.gameObject.GetComponent<SimpleBuilding>().SpawnUnit();
        }
    }

    void FollowCursor()
    {
        // Raycast to keep building clone on cursor
        if (CloneSpawned)
        {
            // Raycast
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.tag == "Ground")
                {
                    Clone.transform.position = new Vector3(hit.point.x, 5.0f, hit.point.z);
                }
            }
        }
    }

    void PlaceBuilding()
    {
        // When left clicked, building is placed at location
        if (Input.GetMouseButtonDown(0))
        {
            if (CloneSpawned)
            {
                CloneSpawned = false;
                Clone.transform.position = new Vector3(Clone.transform.position.x, 1.0f, Clone.transform.position.z);
                BuildingList.Add(Clone);
                Building.gameObject.GetComponent<SimpleBuilding>().IsPlaced = true;
            }
        }
    }
}
