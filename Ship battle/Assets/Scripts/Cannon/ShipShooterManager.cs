using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class ShipShooterManager : NetworkBehaviour {

    public float reloadTime = 5f;
    public Ship ship;
    public GameObject HUDContainer;

    public GameObject LeftCannons;
    public GameObject RightCannons;
    public GameObject TopCannons;
    public GameObject BottomCannons;

    private float leftTime;
    private float rightTime;
    private float topTime;
    private float bottomTime;

    private CannonsHUD cHUD;

    void Start () {
        leftTime = reloadTime;
        rightTime = reloadTime;
        topTime = reloadTime;
        bottomTime = reloadTime;

        if (isLocalPlayer)
        {
            HUDContainer.SetActive(true);
            cHUD = HUDContainer.GetComponent<CannonsHUD>();
        }
        else
        {
            GameObject.Destroy(HUDContainer);
        }
    }
	
	// Update is called once per frame
	void Update () {
        float dt = Time.deltaTime;
        leftTime = Mathf.Min(leftTime + dt, reloadTime);
        rightTime = Mathf.Min(rightTime + dt, reloadTime);
        topTime = Mathf.Min(topTime + dt, reloadTime);
        bottomTime = Mathf.Min(bottomTime + dt, reloadTime);

        if (cHUD != null)
        {
            cHUD.SetLeftLevel(leftTime / reloadTime);
            cHUD.SetRightLevel(rightTime / reloadTime);
            cHUD.SetTopLevel(topTime / reloadTime);
            cHUD.SetBottomLevel(bottomTime / reloadTime);
        }

        if (isLocalPlayer && !ship.isDrowing && !WaitingScreen.screen.gameObject.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1) && leftTime == reloadTime)
            {
                Fire("left");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha2) && rightTime == reloadTime)
            {
                Fire("right");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha3) && topTime == reloadTime)
            {
                Fire("top");
            }
            else if (Input.GetKeyDown(KeyCode.Alpha4) && bottomTime == reloadTime)
            {
                Fire("bottom");
            }
        }
    }

    void Fire(string side)
    {
        if (isServer)
        {
            RpcFire(side);
            DoFire(side);
        }
        else
        {
            CmdFire(side);
        }
    }

    [Command]
    void CmdFire(string side)
    {
        RpcFire(side);
        DoFire(side);
    }

    [ClientRpc]
    void RpcFire(string side)
    {
        DoFire(side);
    }

    void DoFire(string side)
    {
        switch(side)
        {
            case "left":
                leftTime = 0f;
                DoSideFire(LeftCannons);
                break;
            case "right":
                rightTime = 0f;
                DoSideFire(RightCannons);
                break;
            case "top":
                topTime = 0f;
                DoSideFire(TopCannons);
                break;
            case "bottom":
                bottomTime = 0f;
                DoSideFire(BottomCannons);
                break;
            default:
                break;
        }
    }
    void DoSideFire(GameObject side)
    {
        foreach (CannonShoot cannon in side.GetComponentsInChildren<CannonShoot>())
            cannon.ShootCannonLatent();
    }
}
