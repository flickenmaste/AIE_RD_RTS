using UnityEngine;
using System.Collections;

public class EnemyBasicUnit : MonoBehaviour {

    // Navmesh
    public NavMeshAgent Agent;

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
    void Start()
    {
        Health = MaxHealth;
        renderer.material.color = Color.red;
        GameObject eek = GameObject.FindGameObjectWithTag("Finish");
        Agent.SetDestination(eek.gameObject.transform.position);
    }

    void FixedUpdate()
    {
        DetectOthers();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckMovement()
    {

    }

    // Check if object is selected or not
    void CheckBool(bool select)
    {

    }

    void DetectOthers()
    {
        int LayerMask = 1 << 10;
        
        Collider[] hits = Physics.OverlapSphere(this.transform.position, 30.0f, LayerMask);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.tag == "PlayerUnit")
            {
                NextFire = Time.time + FireRate;
                hits[i].gameObject.GetComponent<UnitManager>().TakeDamage(5);
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
            Destroy(this.gameObject, 0);
        }
    }

    IEnumerator WaitInTime(float valTime)
    {
        yield return new WaitForSeconds(valTime);
    }
}
