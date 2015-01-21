using UnityEngine;
using System.Collections;

public class ResourceManager : MonoBehaviour {

    public float Gold = 3000;
    public float Income = 0;
    public string GoldString;



	// Use this for initialization
	void Start () 
    {

	}
	
	// Update is called once per frame
	void Update () 
    {

        GetGold();
	}
    void GetGold()
    {
        Gold += Time.deltaTime * Income;
    }
}
