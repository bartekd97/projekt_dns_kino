using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipPhysics : MonoBehaviour {

    public Ocean ocean;
    public Transform frontRightPoint;
    public Transform frontLeftPoint;
    public Transform backRightPoint;
    public Transform backLeftPoint;

    void Start () {
		
	}
	

	void FixedUpdate () {
        Vector3 fronRightVec = frontRightPoint.position;
        Vector3 frontLeftVec = frontLeftPoint.position;
        Vector3 backRightVec = backRightPoint.position;
        Vector3 backLeftVec = backLeftPoint.position;

        Vector3 frontVec = (fronRightVec + frontLeftVec) / 2;
        Vector3 backVec = (backRightVec + backLeftVec) / 2;

        float rotX, rotZ;
        
        // rotX - nachylenie przod/tyl
        float frontPos = ocean.GetWaterLevel(frontVec);
        float backPos = ocean.GetWaterLevel(backVec);

        float dist = (backVec - frontVec).magnitude;
        rotX = Mathf.Atan((backPos - frontPos) / dist);
        rotX = rotX * Mathf.Rad2Deg;

        // rotZ - nachylenie lewo/prawo
        float frPos = ocean.GetWaterLevel(fronRightVec);
        float flPos = ocean.GetWaterLevel(frontLeftVec);
        float brPos = ocean.GetWaterLevel(backRightVec);
        float blPos = ocean.GetWaterLevel(backLeftVec);

        float fDist = (fronRightVec - frontLeftVec).magnitude;
        float bDist = (backRightVec - backLeftVec).magnitude;
        float rotZf = Mathf.Atan((frPos - flPos) / fDist) * Mathf.Rad2Deg;
        float rotZb = Mathf.Atan((brPos - blPos) / bDist) * Mathf.Rad2Deg;
        rotZ = (rotZf + rotZb) / 2f;

        Vector3 currentRotationVec = transform.rotation.eulerAngles;
        Vector3 toRotationVec = new Vector3(rotX, currentRotationVec.y, rotZ);
        Vector3 finalRotationVec = new Vector3(
                Utility.InterpolateRotation(currentRotationVec.x, toRotationVec.x, 0.1f),
                Utility.InterpolateRotation(currentRotationVec.y, toRotationVec.y, 0.1f),
                Utility.InterpolateRotation(currentRotationVec.z, toRotationVec.z, 0.1f)
            );
        transform.rotation = Quaternion.Euler(finalRotationVec);


        // pozycja Y
        float middlePos = ocean.GetWaterLevel(transform.position);
        Vector3 shipPos = transform.position;
        shipPos.y = Mathf.Lerp(shipPos.y, middlePos, 0.1f);
        transform.position = shipPos;
    }
}
