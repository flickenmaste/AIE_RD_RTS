﻿using UnityEngine;
using System.Collections;

public class BasicAirUnit : UnitManager {

    // Player camera
    private Camera camera;
    
    public int MaxHealth = 100;

    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            CheckBool(selected, Group);
        }
    }

    // Navmesh
    public NavMeshAgent Agent;

    public GroupManager Group;
    
    // Use this for initialization
	void Start () 
    {
        camera = Camera.main;
        Group = FindObjectOfType<GroupManager>();
        Group.DeactiveSelected.Add(this.gameObject);
        Health = MaxHealth;
	}

    void FixedUpdate()
    {
        DetectOthers();
    }
	
	// Update is called once per frame
	void Update () 
    {
        CheckSelected();
        CheckMovement();

		//A hacky fix so air units "fly over" obsticles but then avoid themselves
		if (Agent.velocity == new Vector3(0,0,0)) 
		{
			Agent.obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
		}
		else 
		{
			Agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
		}
	}

    protected void CheckSelected()
    {
        // Only check if unit is on screen
        if (renderer.isVisible && Input.GetMouseButton(0))
        {
            // Get Camera view and use selection box
            Vector3 camPos = Camera.main.WorldToScreenPoint(transform.position);
            camPos.y = CameraManager.InvertMouseY(camPos.y);
            Selected = CameraManager.Selection.Contains(camPos);

            // Raycast to select invididual unit
            Ray ray = camera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider == this.collider)
                {
                    Selected = true;
                }
            }

            // Raycast to select all units of same types
            if (Input.GetMouseButton(0) && Input.GetKey(KeyCode.A))
            {
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "PlayerUnit")
                    {
                        Selected = true;
                    }
                }
            }
        }

        // Set visuals if unit is selected or not
        if (Selected)
        {
            renderer.material.color = Color.green;
        }
        else
        {
            renderer.material.color = Color.blue;
        }
    }

    protected void CheckMovement()
    {
        // Right click and move unit to where player clicked
        if (Input.GetMouseButtonDown(1))
        {
            if (Selected)
            {
                // Raycast
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.tag == "Ground")
                    {
                        // Move unit cooooooool
                        Agent.SetDestination(hit.point);
                    }
                }
            }
        }
    }

    void DetectOthers()
    {
        int LayerMask = 1 << 11;

        Collider[] hits = Physics.OverlapSphere(this.transform.position, 30.0f, LayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "EnemyUnit")
            {
                NextFire = Time.time + FireRate;
                hits[i].gameObject.GetComponent<EnemyBasicUnit>().TakeDamage(5);
            }
        }
    }
}