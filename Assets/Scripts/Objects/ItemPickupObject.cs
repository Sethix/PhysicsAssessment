using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class ItemPickupObject : CoreObject
{

    private float currentRespawnTime;
    public float respawnTime;

    [SyncVar]
    public GameObject accessory;

    [SyncVar]
    public GameObject weapon;

	// Use this for initialization
	protected override void Start () {
        base.Start();
        currentRespawnTime = respawnTime;
	}
	
	// Update is called once per frame
	protected override void Update () {
        if (isLocalPlayer)
        {
            base.Update();
            currentRespawnTime += Time.deltaTime;
        }
	}

    protected override void OnPlayerCollision(PlayerController player)
    {
        base.OnPlayerCollision(player);

        if (isServer)
        {

            if (currentRespawnTime >= respawnTime && player.motor is BipedMotor)
            {
                if (accessory != null)
                {
                    RpcGiveAccessory(player.gameObject);
                }
                if (weapon != null)
                {
                    RpcGiveWeapon(player.gameObject);
                }

                respawnTime = 0;
            }

        }
    }

    [ClientRpc]
    void RpcGiveWeapon(GameObject plr)
    {
        plr.GetComponent<BipedStats>().GiveWeapon(weapon);
    }

    [ClientRpc]
    void RpcGiveAccessory(GameObject plr)
    {
        plr.GetComponent<BipedStats>().GiveAccessory(accessory);
    }
}
