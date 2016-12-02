using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class BulletMotor : CoreMotor {

    public CoreMotor owner;

    public float speed;

    public float lifetime;

    public bool destroyOnHit;

    public float dropoff;

    public bool directionSet;

    protected override void Start()
    {
        base.Start();
        //facingRight = owner.GetComponent<CoreMotor>().facingRight;
        if (lifetime > 0) Destroy(gameObject, lifetime);
    }

    // Update is called once per frame
    protected override void FixedUpdate()
    {
        if(owner != null && !directionSet)
        {
            facingRight = owner.facingRight;
            directionSet = true;
        }
        base.FixedUpdate();
        Move();
    }

    void Move()
    {
        Vector2 direction;

        if (facingRight)
            direction = Vector3.right * speed;

        else
            direction = Vector3.left * speed;

        Vector2 velocity = transform.InverseTransformDirection(rigidbody.velocity);
        Vector2 velocityChange = direction - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -speed, speed);
        velocityChange.y = -dropoff * Time.deltaTime;

        velocityChange = transform.TransformDirection(velocityChange);
        rigidbody.velocity += velocityChange;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (destroyOnHit)
        {
            if (isLocalPlayer)
            {
                BipedStats player = col.transform.GetComponent<BipedStats>();
                if (player != null) CmdKillPlayer(player.gameObject);
            }
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (destroyOnHit && col.GetComponent<ItemPickupObject>() == null)
        {
            if (isLocalPlayer)
            {
                BipedStats player = col.transform.GetComponent<BipedStats>();
                if (player != null) CmdKillPlayer(player.gameObject);
            }
            Destroy(gameObject);
        }
    }

    [Command]
    void CmdKillPlayer(GameObject plr)
    {
        plr.GetComponent<BipedStats>().isDead = true;
    }
}
