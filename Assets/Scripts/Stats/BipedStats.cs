using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

public class BipedStats : CoreStats
{

    #region Variables

    #region Modifiers

    public float jumpStrength;
    public float jumpLength;
    public float fastFallSpeed;

    public float acceleration;

    #endregion

    #region Components

    public CoreWeapon weapon;

    public CoreAccessory accessory;

    #endregion

    #region Transforms

    [HideInInspector]
    public Transform weaponHolder;
    Transform weaponTransform;

    [HideInInspector]
    public Transform accessoryHolder;
    Transform accessoryTransform;

    #endregion

    #region Properties

    public bool hasWeapon
    { get { return weapon != null || GetComponent<CoreWeapon>() != null; } }

    public bool hasAccessory
    { get { return accessory != null || GetComponent<CoreAccessory>() != null; } }

    private bool _isDead;
    public bool isDead
    {
        get
        {
            return _isDead;
        }
        set
        {
            if (_isDead && !value)
            {
                CoreController controller = GetComponent<CoreController>();
                if (controller != null) controller.enabled = true;

                NetworkStartPosition[] spawnPoints = FindObjectsOfType<NetworkStartPosition>();

                transform.position = spawnPoints[Random.Range(0, spawnPoints.Length - 1)].transform.position;
            }
            else if (!_isDead && value)
            {
                CoreController controller = GetComponent<CoreController>();
                if (controller != null) controller.enabled = false;

                Debug.Log("RIP");
            }
            _isDead = value;
        }
    }

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    // Use this for initialization
    protected override void Start () {
        base.Start();


        weaponHolder = transform.FindDeepChild("_weaponHolder");
        accessoryHolder = transform.FindDeepChild("_accessoryHolder");
	}
	
	// Update is called once per frame
	protected override void Update () {
        base.Update();
	}

    #endregion

    #region Public
    
    public void GiveWeapon(GameObject weaponToGive)
    {
        if (weaponTransform != null)
        {
            NetworkServer.Destroy(weaponTransform.gameObject);
        }

        weaponTransform = Instantiate(weaponToGive).transform;
        weaponTransform.parent = weaponHolder;
        weaponTransform.localPosition = Vector3.zero;
        weaponTransform.localRotation = Quaternion.identity;
        weapon = weaponTransform.GetComponent<CoreWeapon>();
        weapon.owner = (BipedMotor)motor;

        NetworkServer.Spawn(weaponTransform.gameObject);
    }

    
    public void GiveAccessory(GameObject accessoryToGive)
    {
        if (accessoryTransform != null)
        {
            NetworkServer.Destroy(accessoryTransform.gameObject);
        }

        accessoryTransform = Instantiate(accessoryToGive).transform;
        accessoryTransform.parent = accessoryHolder;
        accessoryTransform.localPosition = Vector3.zero;
        accessoryTransform.localRotation = Quaternion.identity;
        accessory = accessoryTransform.GetComponent<CoreAccessory>();
        accessory.owner = (BipedMotor)motor;

        NetworkServer.Spawn(accessoryTransform.gameObject);
    }

    #endregion

    #endregion

}
