using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{

    GameObject playerCamera;    // The camera
    Vector3 vector;             // A vector
    public float speed = 3f;    // Camera speed

    public float offsetX;
    public float offsetY;
    public float offsetZ;

    public float orthographicSize = 15f;

    // Use this for initialization
    void Start()
    {
        // Assign camera
        playerCamera = GameObject.FindGameObjectWithTag("MainCamera");
        playerCamera.GetComponent<Camera>().orthographicSize = orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        vector = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);

        // Lerp the camera's position
        playerCamera.transform.position = Vector3.Lerp(playerCamera.transform.position, vector, speed * Time.deltaTime);

    }

    public void InstantUpdate(float ortSize)
    {
        playerCamera.transform.position = new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z + offsetZ);
        playerCamera.GetComponent<Camera>().orthographicSize = ortSize;
    }
}
