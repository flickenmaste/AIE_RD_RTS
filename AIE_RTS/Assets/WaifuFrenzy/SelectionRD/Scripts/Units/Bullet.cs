using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {

    public Vector3 targetPosition;

    public float journeyLength;
    public float startTime;
    public float speed = 0.5f;
    
    // Use this for initialization
	void Start () 
    {
        Destroy(this.gameObject, 1.5f);
	}
	
	// Update is called once per frame
	void Update () 
    {
        Move();
	}

    void Move()
    {
        // Lerp bullet to target
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector3.Lerp(transform.position, targetPosition, fracJourney);
    }

    public void LerpToTarget(Collider c)
    {
        // Launch bullet out
        targetPosition = c.transform.position;
        startTime = Time.time;
        journeyLength = Vector3.Distance(transform.position, targetPosition);
    }
}
