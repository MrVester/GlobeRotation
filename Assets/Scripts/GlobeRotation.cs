using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobeRotation : MonoBehaviour
{
    new public Transform camera;
    public Transform globe;
    public Transform mark;

    Quaternion startingRotation;
    Quaternion targetRotation;

    private float timeToRot = 0.5f;

    /**
     * https://gamedev.stackexchange.com/questions/155406/rotating-a-sphere-so-point-lines-up-with-the-camera
     */
    void StartRotation() {
        Quaternion toCamera = Quaternion.LookRotation(camera.position - globe.position);
        Quaternion toSite = Quaternion.LookRotation(mark.localPosition);
        Quaternion fromSite = Quaternion.Inverse(toSite);

        startingRotation = globe.rotation;
        targetRotation = toCamera * fromSite;
        StartCoroutine(Rotate(globe.rotation, targetRotation, timeToRot));
    }
    // Update is called once per frame
    void Update()
    {
        //On Space pressed start rotation
        if (Input.GetKey(KeyCode.Space))
        {
            StartRotation();
        }
        
    }
    IEnumerator Rotate(Quaternion startRot, Quaternion endRot, float time)
    {
        float elapsedTime = 0;
        while (elapsedTime < time)
        {
            elapsedTime += Time.deltaTime;

            globe.rotation = Quaternion.Slerp(startingRotation, targetRotation, (elapsedTime / time));
            yield return new WaitForEndOfFrame();
        }
    }
}
