using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class BipedMotor : CoreMotor {

    #region Variables

    #region Modifiers

    float jumpLength;
    float currentJumpTimer;
    float jumpStrength;
    float initialBoost = 10;

    bool velocityReset = true;

    #endregion

    #region Checks

    bool requestedJump;

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    // Use this for initialization
    protected override void Start () {
        base.Start();
	}

    // Update is called once per frame
    protected override void FixedUpdate () {
        if (isLocalPlayer)
        {
            base.FixedUpdate();
            ApplyJump();
        }
	}

    #endregion

    public void Move(Vector2 direction, float speed, float acceleration)
    {
        direction *= speed;
        Vector2 velocity = transform.InverseTransformDirection(rigidbody.velocity);
        Vector2 velocityChange = direction - velocity;
        velocityChange.x = Mathf.Clamp(velocityChange.x, -acceleration, acceleration);
        velocityChange.y = -acceleration * Time.deltaTime;

        velocityChange = transform.TransformDirection(velocityChange);
        rigidbody.velocity += velocityChange;
    }

    void ApplyJump()
    {
        if (requestedJump && currentJumpTimer <= jumpLength)
        {
            currentJumpTimer += Time.deltaTime;
            rigidbody.AddForce((Vector2)transform.up * jumpStrength);
        }

        else if(isGrounded && !velocityReset)
        {
            ResetVerticalVelocity();

            velocityReset = true;
            Debug.Log("RESET.");
        }
    }

    public void JumpReset()
    {
        requestedJump = false;
    }

    public void RequestJump(float strength, float timer)
    {
        jumpLength = timer;
        jumpStrength = strength;

        if (isGrounded && !requestedJump)
        {
            Debug.Log("jumpRequest");
            //ResetVerticalVelocity();
            requestedJump = true;
            velocityReset = false;
            currentJumpTimer = 0;
            float dist = Vector2.Distance(transform.position, closestPlanet.transform.position);
            rigidbody.AddForce((((Vector2)transform.up * closestPlanet.gravityStrength * rigidbody.mass * closestPlanet.rigidbody.mass) / (Mathf.Pow(dist, 2)) * Time.fixedDeltaTime) + (Vector2)transform.up * jumpStrength, ForceMode2D.Impulse);
            //rigidbody.velocity += (Vector2)transform.up * 20;
        }
    }

    public void FastFall(float strength)
    {
        if (!isGrounded)
            rigidbody.velocity += new Vector2(-transform.up.x, -transform.up.y) * strength;
    }

    void ResetVerticalVelocity()
    {
        rigidbody.velocity = transform.TransformVector(rigidbody.velocity);
        rigidbody.velocity = new Vector2(rigidbody.velocity.x, 0);
        rigidbody.velocity = transform.InverseTransformVector(rigidbody.velocity);
        Debug.Log("VReset");
    }

    #endregion

}
