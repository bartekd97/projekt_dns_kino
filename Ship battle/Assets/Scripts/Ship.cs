using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour {

    public ShipMast[] masts;
    public SailSkin sailSkin;
    public ShipSkin shipSkin;
    public ShipSkinSetup skinSetup;

    public float ForwardSpeed = 30f;
    public float TurnForce = 10f;

    public float sailCollapseSpeed = 0.25f;

    public float sailLevel = 1f;
    private float toSailLevel = 1f;

	void Start () {
        foreach (ShipMast m in masts)
            m.SetSailSkin(sailSkin);
        skinSetup.ApplySkin(shipSkin);
	}
	
	
	void Update () {
        float dt = Time.deltaTime;

        if (sailLevel != toSailLevel)
        {
            float diff = toSailLevel - sailLevel;
            float add = dt * sailCollapseSpeed;
            if (Mathf.Abs(diff) <= add)
                sailLevel = toSailLevel;
            else
                sailLevel = sailLevel + add * Mathf.Sign(diff);
            foreach (ShipMast m in masts)
                m.SetSailLevel(sailLevel);
        }
    }

    public void TurnLeftSails()
    {
        foreach (ShipMast m in masts)
            m.SetDirection(ShipMast.ShipMastDirection.Left);
    }
    public void TurnRightSails()
    {
        foreach (ShipMast m in masts)
            m.SetDirection(ShipMast.ShipMastDirection.Right);
    }
    public void TurnForwardSails()
    {
        foreach (ShipMast m in masts)
            m.SetDirection(ShipMast.ShipMastDirection.Forward);
    }

    public void CollapseSails()
    {
        toSailLevel = 0f;
    }
    public void ExpandSails()
    {
        toSailLevel = 1f;
    }
}
