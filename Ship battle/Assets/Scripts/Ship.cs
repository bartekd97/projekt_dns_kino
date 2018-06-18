using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class Ship : NetworkBehaviour
{
    public static int PlayerCount = 0;

    private static Ship self;

    public ShipMast[] masts;
    [SyncVar(hook ="OnSetSailSkin")]
    public string sailSkin;
    [SyncVar(hook = "OnSetShipSkin")]
    public string shipSkin;
    private SailSkin _sailSkin;
    private ShipSkin _shipSkin;
    public ShipSkinSetup skinSetup;

    public float ForwardSpeed = 30f;
    public float TurnForce = 10f;

    public float sailCollapseSpeed = 0.25f;

    public float sailLevel = 1f;
    [SyncVar]
    public float toSailLevel = 1f;

    public float MaximumHealth = 100f;
    [SyncVar(hook ="OnUpdateHealth")]
    public float CurrentHealth;

    public bool isDrowing = false;

	void Start () {
        if (PlayerCount == 0)
            WaitingScreen.screen.gameObject.SetActive(true);
        else if (PlayerCount == 1)
            WaitingScreen.screen.gameObject.SetActive(false);
        PlayerCount++;

        if (isLocalPlayer)
        {
            if (_sailSkin == null)
            {
                _sailSkin = SailSkin.GetRandomSkin();
                sailSkin = _sailSkin.name;
            }
            foreach (ShipMast m in masts)
                m.SetSailSkin(_sailSkin);

            if (_shipSkin == null)
            {
                _shipSkin = ShipSkin.GetRandomSkin();
                shipSkin = _shipSkin.name;
            }
            skinSetup.ApplySkin(_shipSkin);

            if (!isServer)
            {
                CmdSetSailSkin(sailSkin);
                CmdSetShipSkin(shipSkin);
            }
            self = this;
        }
        else if (self != null)
        {
            self.InitSyncToOthers();
        }
        CurrentHealth = MaximumHealth;
    }

    private void OnDestroy()
    {
        if (isLocalPlayer)
            GameOverScreen.screen.gameObject.SetActive(false);
        if (PlayerCount == 1)
            WaitingScreen.screen.gameObject.SetActive(false);
        PlayerCount--;
    }

    [Command]
    void CmdSetSailSkin(string skin)
    {
        if (sailSkin == skin)
            sailSkin = "";
        OnSetSailSkin(skin);
    }
    [Command]
    void CmdSetShipSkin(string skin)
    {
        if (shipSkin == skin)
            shipSkin = "";
        OnSetShipSkin(skin);
    }
    void OnSetSailSkin(string skin)
    {
        sailSkin = skin;
        if (string.IsNullOrEmpty(skin))
            return;
        _sailSkin = SailSkin.RegisteredSkins[skin];
        foreach (ShipMast m in masts)
            m.SetSailSkin(_sailSkin);
    }
    void OnSetShipSkin(string skin)
    {
        shipSkin = skin;
        if (string.IsNullOrEmpty(skin))
            return;
        _shipSkin = ShipSkin.RegisteredSkins[skin];
        skinSetup.ApplySkin(_shipSkin);
    }

    public void InitSyncToOthers()
    {
        CmdSetSailSkin(sailSkin);
        CmdSetShipSkin(shipSkin);
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
        if (isServer)
            toSailLevel = 0f;
        else
            CmdCollapseSails();
    }
    [Command]
    void CmdCollapseSails()
    {
        toSailLevel = 0f;
    }
    public void ExpandSails()
    {
        if (isServer)
            toSailLevel = 1f;
        else
            CmdExpandSails();
    }
    [Command]
    void CmdExpandSails()
    {
        toSailLevel = 1f;
    }


    public void TakeDamage(float value)
    {
        if (!self.isServer)
            return;

        CurrentHealth = Mathf.Max(CurrentHealth - value, 0f);
    }

    public void OnUpdateHealth(float value)
    {
        CurrentHealth = value;

        float dmglevel = 1f - (CurrentHealth / MaximumHealth);
        foreach (ShipMast m in masts)
            m.SetSailDamageLevel(dmglevel);

        if (CurrentHealth == 0f)
        {
            isDrowing = true;
            if (isLocalPlayer)
                GameOverScreen.screen.gameObject.SetActive(true);
        }
    }
}
