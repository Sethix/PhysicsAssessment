using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class SphereGun : CoreWeapon
{

    public Transform exitZone;

    private float currentCooldownTimer;
    public float cooldown;

    protected override void Start()
    {
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
        currentCooldownTimer += Time.deltaTime;
    }

    public override void OnAttackPressed()
    {
        base.OnAttackPressed();

        if (currentCooldownTimer >= cooldown)
        {
            currentCooldownTimer = 0;
            CmdShoot();
        }
    }

    [Command]
    void CmdShoot()
    {
        GameObject bullet = (GameObject)Instantiate(projectile, exitZone.position, transform.rotation);
        bullet.GetComponent<BulletMotor>().owner = owner;
        NetworkServer.Spawn(bullet);
    }
}

 /*
    Controller
        -> Inputs (Commands)
    Motor
        -> Interprets the Inputs (Synced Variables)
    Rigidbody 
        -> Application of Motor Actions
 */