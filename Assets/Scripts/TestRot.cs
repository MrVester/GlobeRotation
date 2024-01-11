using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class TestRot : MonoBehaviour
{
    public GameObject Camera;
    public GameObject Globe;
    public GameObject Mark;
    public Quaternion currentGlobeRot;

    public Vector3 markPos;
    // Movement speed in units per second.
    public float speed = 1.0F;

    // Time when the movement started.
    private float startTime;

    // Total distance between the markers.
    private float journeyLength;
    void Start()
    {
        currentGlobeRot = Globe.transform.rotation;
        markPos = Mark.transform.position;
        startTime = Time.time;

        // Calculate the journey length.
        journeyLength = Vector3.Distance(Mark.transform.position, Camera.transform.position);
    }
    void Update()
    {
        //if (Input.GetKeyDown("space"))


        // angleX =;
        //angleY = Vector3.Angle(Mark.transform.position - Globe.transform.position, Camera.transform.position - Globe.transform.position);
        // angleZ =;


        float distCovered = (Time.time - startTime) * speed;
        float fractionOfJourney = distCovered / journeyLength;

        //Globe.transform.LookAt(Camera.transform, Vector3.up);

        Globe.transform.rotation = Quaternion.Slerp(currentGlobeRot, Quaternion.Euler((Camera.transform.position - Globe.transform.position).normalized + markPos + currentGlobeRot.eulerAngles), fractionOfJourney);

    }

}
