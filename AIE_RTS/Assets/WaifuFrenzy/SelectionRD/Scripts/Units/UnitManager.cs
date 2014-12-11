using UnityEngine;
using System.Collections;

public class UnitManager : MonoBehaviour {
    
    // Check if user selected
    protected bool selected;

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

    // Fire rate
    public float FireRate = 1.0f;
    protected float NextFire = 0.0f;
    
    // Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

	}

    // Check if object is selected or not
    protected void CheckBool(bool select, GroupManager Group)
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

    public void TakeDamage(int val, GroupManager Group)
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
