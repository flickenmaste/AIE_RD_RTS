using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {

    // Main camera
    private Camera camera;

    // Building to spawn
    public GameObject BuildingMCV;
    public GameObject BuildingMCVPrimary;
    [SerializeField]
    public bool BuildingMCVPrimBool = false;
    public List<GameObject> BuildingMCVList;

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
        // Button to create MCV building clone
        if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2, 100, 50), "Spawn Building"))
        {
            Clone = Instantiate(BuildingMCV) as GameObject;
            CloneSpawned = true;
            if(!BuildingMCVPrimBool)
            {
                BuildingMCVPrimary = Clone;
                BuildingMCVPrimBool = true;
            }
        }

        // Button to spawn unit on primary building
        if (BuildingMCVPrimBool)
        {
            if (GUI.Button(new Rect(Screen.width - 100, Screen.height / 2, 100, 50), "Spawn Units"))
            {
                if (BuildingMCVPrimBool)
                    BuildingMCVPrimary.gameObject.GetComponent<SimpleBuilding>().SpawnUnit();
            }
        }

		// Button to spawn unit on primary building
		if (BuildingMCVPrimBool)
		{
			if (GUI.Button(new Rect(Screen.width - 100, Screen.height / 2 + 50, 100, 50), "Spawn Air Units"))
			{
				if (BuildingMCVPrimBool)
					BuildingMCVPrimary.gameObject.GetComponent<SimpleBuilding>().SpawnAirUnit();
			}
		}

        // Button to spawn resource building
        if (BuildingMCVPrimBool)
        {
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2 + 50, 100, 50), "Resource"))
            {
                Debug.Log("Spawn resource building here");
            }
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
                BuildingMCVList.Add(Clone);
                BuildingMCV.gameObject.GetComponent<SimpleBuilding>().IsPlaced = true;
            }
        }
    }
}
