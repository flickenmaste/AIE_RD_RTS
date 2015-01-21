using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BuildingManager : MonoBehaviour {

    // Main camera
    private Camera camera;

    // Building to spawn
    public GameObject BuildingMCV;
    public GameObject BuildingMCVPrimary;
    public GameObject BuildingResource;
    [SerializeField]
    public bool BuildingMCVPrimBool = false;
    public List<GameObject> BuildingMCVList;
    public List<GameObject> BuildingResourceList;
    public float Gold = 3000;
    public float Income = 0;
    public string GoldString;

    // Clone and bools to manage placing
    public GameObject Clone;
    public bool CloneSpawned = false;
    public bool CloneSpawnedR = false;
    public bool BuildingGhost = false;
    
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
        GetGold();
	}

    void OnGUI()
    {
        if (BuildingGhost == false && Time.timeScale != 0)
        {
            // Button to create MCV building clone
            if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2, 100, 50), "Spawn Building"))
            {
                if (Gold >= 1000)
                {
                    Clone = Instantiate(BuildingMCV) as GameObject;
                    CloneSpawned = true;
                    BuildingGhost = true;
                    if (!BuildingMCVPrimBool)
                    {
                        BuildingMCVPrimary = Clone;
                        BuildingMCVPrimBool = true;
                    }
                }
            }

            // Button to spawn unit on primary building
            if (BuildingMCVPrimBool)
            {
                if (GUI.Button(new Rect(Screen.width - 100, Screen.height / 2, 100, 50), "Spawn Units"))
                {
                    if (Gold >= 100)
                    {
                        if (BuildingMCVPrimBool)
                            BuildingMCVPrimary.gameObject.GetComponent<SimpleBuilding>().SpawnUnit();
                        Gold -= 100;
                    }
                }
            }

            // Button to spawn resource building
            if (BuildingMCVPrimBool)
            {
                if (GUI.Button(new Rect(Screen.width - 200, Screen.height / 2 + 50, 100, 50), "Resource"))
                {
                    if (Gold >= 500)
                    {
                        Clone = Instantiate(BuildingResource) as GameObject;
                        CloneSpawnedR = true;
                        BuildingGhost = true;
                        Debug.Log("Spawn resource building here");
                    }
                }
            }
        }
       GoldString = "Gold: " + (int)Gold;
       GUI.TextField(new Rect(Screen.width - 200, Screen.height / 2 - 20, 100, 20), GoldString);
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
        if (CloneSpawnedR)
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
            BuildingGhost = false;
            if (CloneSpawned)
            {
                CloneSpawned = false;
                Clone.transform.position = new Vector3(Clone.transform.position.x, 1.0f, Clone.transform.position.z);
                BuildingMCVList.Add(Clone);
                BuildingMCV.gameObject.GetComponent<SimpleBuilding>().IsPlaced = true;
                Gold -= 1000;
            }
            if (CloneSpawnedR)
            {
                CloneSpawnedR = false;
                Clone.transform.position = new Vector3(Clone.transform.position.x, 1.0f, Clone.transform.position.z);
                BuildingResourceList.Add(Clone);
                BuildingResource.gameObject.GetComponent<SimpleBuilding>().IsPlaced = true;
                Income += 1;
                Gold -= 500;
            }
        }
    }

    void GetGold()
    {
        Gold += Time.deltaTime  * Income;
    }
}
