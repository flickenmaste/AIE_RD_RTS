using UnityEngine;
using System.Collections;

public class CameraManager : MonoBehaviour {

    // Texture for select box
    public Texture2D SelectionHighlight = null;

    // Box for selection
    public static Rect Selection = new Rect(0, 0, 0, 0);

    // Start of box
    private Vector3 StartClick = -Vector3.one;

    // Cam Zoom min max
    public float MinZoom = 1;
    public float MaxZoom = 5;
    
    // Use this for initialization
	void Start () 
    {
	
	}
	
	// Update is called once per frame
	void Update () 
    {
        CheckCamera();
        CheckCamMovement();
	}

    void CheckCamera()
    {
        if(Input.GetMouseButtonDown(0))
        {
            StartClick = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StartClick = -Vector3.one;
        }

        if (Input.GetMouseButton(0))
        {
            Selection = new Rect(StartClick.x, InvertMouseY(StartClick.y), Input.mousePosition.x - StartClick.x,
                                InvertMouseY(Input.mousePosition.y) - InvertMouseY(StartClick.y));
            
            if (Selection.width < 0)
            {
                Selection.x += Selection.width;
                Selection.width = -Selection.width;
            }
            if (Selection.height < 0)
            {
                Selection.y += Selection.height;
                Selection.height = -Selection.height;
            }
        }
    }

    void CheckCamMovement()
    {
        if(Input.GetKey(KeyCode.UpArrow))
        {
            this.transform.position += new Vector3(0, 0, 10) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            this.transform.position += new Vector3(0, 0, -10) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.position += new Vector3(-10, 0, 0) * Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.position += new Vector3(10, 0, 0) * Time.deltaTime;
        }

        // Scroll mouse down
        if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            this.transform.position += new Vector3(0, 30, 0) * Time.deltaTime;
        }
        // Scroll mouse up
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            this.transform.position += new Vector3(0, -30, 0) * Time.deltaTime;
        }
    }

    private void OnGUI()
    {
        if (StartClick != -Vector3.one)
        {
            // Draw select box
            GUI.color = new Color(1, 1, 1, 0.5f);
            GUI.DrawTexture(Selection, SelectionHighlight);
        }
    }

    // Make it unity screen space
    public static float InvertMouseY(float y)
    {
        return Screen.height - y;
    }
}
