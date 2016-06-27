using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour
{
    Vector3 currFramePosition, lastFramePosition;
    public static CameraController Instance { get; protected set; }

    void Start()
    {
        if (Instance != null)
        {
            Debug.LogError("Two Camera Controllers are present in the Scene!");
        }
        Instance = this;
    }

    void Update()
    {
        currFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        currFramePosition.z = 0;

        UpdateCameraMovement();

        lastFramePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        lastFramePosition.z = 0;
    }

    void UpdateCameraMovement()
    {
        // Handle screen panning
        if (Input.GetMouseButton(1) || Input.GetMouseButton(2))
        {   // Right or Middle Mouse Button
            Vector3 diff = lastFramePosition - currFramePosition;
            Camera.main.transform.Translate(diff);
        }
        //scroll to zoom
        Camera.main.orthographicSize -= Camera.main.orthographicSize * Input.GetAxis("Mouse ScrollWheel");
        Camera.main.orthographicSize = Mathf.Clamp(Camera.main.orthographicSize, 2f, 500f);
    }

    public void SnapToCoords(float x, float y)
    {
        transform.position = new Vector3(x,y,transform.position.z);
    }
}

