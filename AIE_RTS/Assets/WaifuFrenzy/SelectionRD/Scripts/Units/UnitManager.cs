using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {

    // Player camera
    private Camera camera;
    
    // Check if user selected
    private bool selected;
    public bool Selected
    {
        get
        {
            return selected;
        }
        set
        {
            selected = value;
            CheckBool(selected);
        }
    }

    // Navmesh
    public NavMeshAgent Agent;

    public GroupManager Group;

    // Health I guess
    [SerializeField]
    private int health;
    public int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
        }
    }
    public int MaxHealth = 100;

    // Fire rate
    public float FireRate = 1.0f;
    private float NextFire = 0.0f;
    
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
	}

    void CheckSelected()
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

    void CheckMovement()
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

    // Check if object is selected or not
    void CheckBool(bool select)
    {
        // If object is selected
        if (select)
        {
            // If its not already in the list, add to active remove from deactive
            if (!Group.ActiveSelected.Contains(this.gameObject))
            {
                Group.ActiveSelected.Add(this.gameObject);
                Group.DeactiveSelected.Remove(this.gameObject);
            }
        }
        else if (!select)
        {
            // If is in the active list remove and add to deactive
            if (Group.ActiveSelected.Contains(this.gameObject))
            {
                Group.ActiveSelected.Remove(this.gameObject);
                Group.DeactiveSelected.Add(this.gameObject);
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

    public void TakeDamage(int val)
    {
        if (health > 0)
        {
            health -= val;
        }
        else if (health <= 0)
        {
            if(Group.ActiveSelected.Contains(this.gameObject))
            {
                Group.ActiveSelected.Remove(this.gameObject);
            }
            if (Group.DeactiveSelected.Contains(this.gameObject))
            {
                Group.DeactiveSelected.Remove(this.gameObject);
            }
            
            Destroy(this.gameObject, 0);
        }
    }

    IEnumerator WaitInTime(float valTime)
    {
        yield return new WaitForSeconds(valTime);
    }
}
