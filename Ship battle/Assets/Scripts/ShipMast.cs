using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMast : MonoBehaviour
{
    public enum ShipMastDirection
    {
        Forward,
        Left,
        Right
    }

    public SailScript[] sails;
    public float rotationForward = 0f;
    public float rotiationLeft = 0f;
    public float rotationRight = 0f;

    public float mastRotateSpeed = 10f;

    public float toRotation;
    private ShipMastDirection currentDirection = ShipMastDirection.Forward;

	void Start () {
        toRotation = rotationForward;
	}
	
	void Update () {
        float dt = Time.deltaTime;

        Vector3 currentRot = transform.localRotation.eulerAngles;
        if (currentRot.z != toRotation)
        {
            currentRot.z = Utility.NormalizeRotation(currentRot.z);
            float diff = toRotation - currentRot.z;
            float add = dt * mastRotateSpeed;
            if (Mathf.Abs(diff) <= add)
                currentRot.z = toRotation;
            else
                currentRot.z = currentRot.z + add * Mathf.Sign(diff);
            transform.localRotation = Quaternion.Euler(currentRot);

            foreach (SailScript ss in sails)
                ss.SetSailOffset(-3f * Mathf.Min(1f, Mathf.Max(-1f, Utility.NormalizeRotation(currentRot.z)/60f)));
        }
	}

    public void SetDirection(ShipMastDirection dir)
    {
        if (currentDirection == dir)
            return;
        switch(dir)
        {
            case ShipMastDirection.Forward:
                {
                    float forwarRight = rotationForward;
                    float forwardLeft = rotiationLeft + (rotationRight - forwarRight);
                    float currentRot = Utility.NormalizeRotation(transform.rotation.eulerAngles.z);
                    if (Mathf.Abs(currentRot - forwardLeft) > Mathf.Abs(currentRot - forwarRight))
                        toRotation = forwarRight;
                    else
                        toRotation = forwardLeft;
                }
                break;
            case ShipMastDirection.Left:
                toRotation = rotiationLeft;
                break;
            case ShipMastDirection.Right:
                toRotation = rotationRight;
                break;
            default:
                break;
        }
        currentDirection = dir;
    }

    public void SetSailSkin(SailSkin skin)
    {
        foreach (SailScript ss in sails)
            ss.SetSkin(skin);
    }
    public void SetSailLevel(float level)
    {
        foreach (SailScript ss in sails)
            ss.SetSailLevel(level);
    }

    public void SetSailDamageLevel(float dmg)
    {
        foreach (SailScript ss in sails)
            ss.SetDamageLevel(dmg);
    }
}
