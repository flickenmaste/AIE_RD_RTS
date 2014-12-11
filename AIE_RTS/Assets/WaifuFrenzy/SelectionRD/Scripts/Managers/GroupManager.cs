using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GroupManager : MonoBehaviour {

    // Lists for Deactive or Active units
    public List<GameObject> ActiveSelected;
    public List<GameObject> DeactiveSelected;

    // Lists for control groups
    public List<GameObject> ControlGroup1;

    // TODO Add lists for individual control groups
    
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        FormControlGroup();
        SelectControlGroup();
	}

    void FormControlGroup()
    {
        if (Input.GetKey(KeyCode.Q) && Input.GetKey(KeyCode.Alpha1))
        {
            ControlGroup1.Clear();
            foreach (var element in ActiveSelected)
            {
                ControlGroup1.Add(element.gameObject);
            }
        }
    }

    void SelectControlGroup()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            foreach (var element in ControlGroup1)
            {
                element.gameObject.GetComponent<UnitManager>().Selected = true;
            }
        }
    }
}
