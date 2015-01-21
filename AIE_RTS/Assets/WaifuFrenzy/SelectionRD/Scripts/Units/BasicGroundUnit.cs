using UnityEngine;
using System.Collections;

public class BasicGroundUnit : UnitManager {

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

    // Animator
    public Animator anim;

    //Shooting
    float fireRate = 1.0f;
    private float lastShot = 0.0f;
    public GameObject Bullet;
    
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
        DoAnims();
	}

    void DoAnims()
    {
        if (Agent.velocity == Vector3.zero)
        {
            anim.SetBool("Moving", false);
        }
        else
        {
            anim.SetBool("Moving", true);
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

        // Detect unit and shoot if enemy
        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "EnemyUnit")
            {
                NextFire = Time.time + FireRate;
                //hits[i].gameObject.GetComponent<EnemyBasicUnit>().TakeDamage(5);
                Shoot(hits[i]);
            }
        }
    }

    void Shoot(Collider c)
    {
        // If they can shoot, launch out bullet and take damage away from target
        if (Time.time > fireRate + lastShot)
        {
            GameObject clone = Instantiate(Bullet, this.transform.position, Quaternion.identity) as GameObject;
            clone.gameObject.GetComponent<Bullet>().LerpToTarget(c);
            c.gameObject.GetComponent<EnemyBasicUnit>().TakeDamage(50);
            lastShot = Time.time + fireRate;
        }
    }
}
