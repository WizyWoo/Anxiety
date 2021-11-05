using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestingCam : MonoBehaviour
{

    public float CamSpeed, ZoomChangeSpeed;
    private float zoomLevel;
    private Camera cameraComponent;

    private void Start()
    {

        cameraComponent = gameObject.GetComponent<Camera>();
        zoomLevel = cameraComponent.orthographicSize;

    }

    private void Update()
    {

        if(Input.GetAxisRaw("Horizontal") != 0 || Input.GetAxisRaw("Vertical") != 0)
        {

            transform.position += new Vector3(Input.GetAxisRaw("Horizontal") * (zoomLevel * CamSpeed), Input.GetAxisRaw("Vertical") * (zoomLevel * CamSpeed), 0) * Time.deltaTime;

        }

        if(Input.mouseScrollDelta != Vector2.zero)
        {

            zoomLevel -= Input.mouseScrollDelta.y * Time.deltaTime * ZoomChangeSpeed;

            cameraComponent.orthographicSize = zoomLevel;

        }

    }

    public void MoveTo(Vector3 targetDestination)
    {

        transform.position = new Vector3(targetDestination.x, targetDestination.y, transform.position.z);

    }

}
