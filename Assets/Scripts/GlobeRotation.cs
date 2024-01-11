using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security;
using UnityEngine;
using UnityEngine.UIElements;

public class GlobeRotation : MonoBehaviour
{
    public Transform camera;
    public Transform globe;
    public Transform mark;
    private Vector3 globeToCam;
    private Vector3 globeToMark;
    private float cosFi;
    private float angle;
    private Vector3 R;

    Quaternion startingRotation;
    Quaternion targetRotation;

    private float timeToRot = 2;


    // Start is called before the first frame update
    void Start()
    {

        globeToCam = camera.position - globe.position;
        globeToMark = mark.position - globe.position;

    }
    void StartRotation()
    {
        startingRotation = globe.rotation;
        globeToCam = camera.position - globe.position;
        globeToMark = mark.position - globe.position;

        cosFi = ScalarProductV3(globeToCam, globeToMark) / MagnitudeProductsV3(globeToCam, globeToMark);
        angle = Mathf.Acos(cosFi);
        R = DivideV3(Vector3.Cross(globeToCam, globeToMark), MagnitudeProductsV3(globeToCam, globeToMark));
        targetRotation = GetQuaternion(R * Mathf.Sin(angle / 2), Mathf.Cos(angle / 2));
        Debug.Log("globeToCam " + globeToCam);
        Debug.Log("globeToMark " + globeToMark);
        Debug.Log("angle " + angle);
        Debug.Log("Cross " + Vector3.Cross(globeToCam, globeToMark));
        Debug.Log("Magnitude " + MagnitudeProductsV3(globeToCam, globeToMark));
        Debug.Log("R " + R);
        Debug.Log(targetRotation.eulerAngles);
        StartCoroutine(Rotate(startingRotation, targetRotation, timeToRot));
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
    private float ScalarProductV3(Vector3 vector1, Vector3 vector2)
    {
        float scalar = vector1.x * vector2.x + vector1.y * vector2.y + vector1.z * vector2.z;
        return scalar;
    }
    private float MagnitudeProductsV3(Vector3 vector1, Vector3 vector2)
    {
        float prod = vector1.magnitude * vector2.magnitude;
        return prod;
    }
    private Vector3 DivideV3(Vector3 vector, float num)
    {
        Vector3 division = new Vector3(vector.x / num, vector.y / num, vector.z / num);
        return division;
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
    private Quaternion GetQuaternion(Vector3 R, float w)
    {
        return new Quaternion(R.x, R.y, R.z, w);
    }
}
