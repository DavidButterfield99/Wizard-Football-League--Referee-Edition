using UnityEngine;

public class CameraController : MonoBehaviour {
    public GameObject referee;
    public Transform soccerField;
    Camera cam;


    public float maxOffset = 50f;
    float xOffset = 0;
    float yOffset = 0;

    void Start()
    {
        cam = GetComponent<Camera>();
        
    }

    private void Update() {
        UpdateOffsets();
        Vector3 camPos = new Vector3(0 ,900, -320);
        // Vector3 camOffset = (new Vector3(mousePos.x, 0 ,mousePos.y) - new Vector3(referee.transform.position.x,0,referee.transform.position.y)).normalized;
        Vector3 camOffset = new Vector3(xOffset, 0 ,yOffset);
        Vector3 newPos = referee.transform.position + camPos + camOffset;


        // Debug.Log($"mousePos: {mousePos}");
        // Debug.Log($"newPos: {newPos}");
        transform.position = newPos;

    }

    private void UpdateOffsets()
    {       
        xOffset += Input.GetAxis("Mouse X");
        yOffset += Input.GetAxis("Mouse Y");

        if (xOffset > maxOffset) xOffset = maxOffset;
        if (xOffset < -maxOffset) xOffset = -maxOffset;
        if (yOffset > maxOffset) yOffset = maxOffset;
        if (yOffset < -maxOffset) yOffset = -maxOffset;
    }
}