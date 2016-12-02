using UnityEngine;
using System.Collections;

[RequireComponent(typeof(CoreMotor))]
[RequireComponent(typeof(CoreStats))]
public class PlayerController : CoreController
{

    public CoreMotor motor;

    public CoreStats stats;

    protected bool isBiped
    { get { return motor is BipedMotor; } }

    // Use this for initialization
    protected override void Start ()
    {
        motor = GetComponent<CoreMotor>();
        stats = GetComponent<CoreStats>();
        transform.parent = GameObject.Find("_Players").transform;

        if (isLocalPlayer)
            base.Start();
	}

    // Update is called once per frame
    protected override void Update ()
    {
        if (isLocalPlayer)
        {
            base.Update();

            if (isBiped)
            {
                BipedMovement();

                BipedJump();

                BipedFastFall();

                if (((BipedStats)stats).hasWeapon)

                    FireWeapon();

                if (((BipedStats)stats).hasAccessory)

                    UseAccessory();
            }
        }
	}

    protected override void SetInput()
    {
        base.SetInput();
        SetMovementInput(new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")));
        SetActionOne(Input.GetButton("Fire1"));
        SetActionTwo(Input.GetButton("Fire2"));
        SetActionThree(Input.GetButton("Fire3"));
    }

    void BipedMovement()
    {
        if (Mathf.Abs(movementInput.x) > 0.05f)
        {
            if (movementInput.x > 0 && !motor.facingRight) motor.facingRight = true;
            else if (movementInput.x < 0 && motor.facingRight) motor.facingRight = false;

            if (((BipedStats)stats).hasAccessory) ((BipedStats)stats).accessory.MoveFunction();

            if (!(((BipedStats)stats).hasAccessory && ((BipedStats)stats).accessory.replaceMove))
                ((BipedMotor)motor).Move(new Vector2(movementInput.x, 0),
                                           ((BipedStats)stats).hasAccessory ?
                                                (((BipedStats)stats).moveSpeed + ((BipedStats)stats).accessory.moveSpeedMod) * ((BipedStats)stats).accessory.moveSpeedMultiplier
                                               : ((BipedStats)stats).moveSpeed,
                                           ((BipedStats)stats).hasAccessory ?
                                                (((BipedStats)stats).acceleration + ((BipedStats)stats).accessory.accelerationMod) * ((BipedStats)stats).accessory.accelerationMultiplier
                                               : ((BipedStats)stats).acceleration);
        }
    }

    void BipedFastFall()
    {
        if (movementInput.y < -0.05f)
        {
            if (((BipedStats)stats).hasAccessory) ((BipedStats)stats).accessory.FastFallFunction();

            if (!(((BipedStats)stats).hasAccessory && ((BipedStats)stats).accessory.replaceFastFall))
                ((BipedMotor)motor).FastFall(((BipedStats)stats).hasAccessory ?
                                                    (((BipedStats)stats).fastFallSpeed + ((BipedStats)stats).accessory.fastFallMod) * ((BipedStats)stats).accessory.fastFallMultiplier
                                                   : ((BipedStats)stats).fastFallSpeed);
        }
    }

    void BipedJump()
    {
        if (actionOnePressed)
        {
            if (((BipedStats)stats).hasAccessory) ((BipedStats)stats).accessory.RequestJumpFunction();

            if (!(((BipedStats)stats).hasAccessory && ((BipedStats)stats).accessory.replaceRequestJump))
                ((BipedMotor)motor).RequestJump(((BipedStats)stats).hasAccessory ?
                                                    (((BipedStats)stats).jumpStrength + ((BipedStats)stats).accessory.jumpStrengthMod) * ((BipedStats)stats).accessory.jumpStrengthMultiplier
                                                   : ((BipedStats)stats).jumpStrength,
                                                ((BipedStats)stats).hasAccessory ?
                                                     (((BipedStats)stats).jumpLength + ((BipedStats)stats).accessory.jumpTimerMod) * ((BipedStats)stats).accessory.jumpTimerMultiplier
                                                   : ((BipedStats)stats).jumpLength);
        }
        else if (actionOneUnpressed)
        {
            if (((BipedStats)stats).hasAccessory) ((BipedStats)stats).accessory.ResetJumpFunction();

            if (!(((BipedStats)stats).hasAccessory && ((BipedStats)stats).accessory.replaceResetJump))
                ((BipedMotor)motor).JumpReset();
        }
    }

    void FireWeapon()
    {
        if (actionTwoPressed) ((BipedStats)stats).weapon.OnAttackPressed();
        if (actionTwoUnpressed) ((BipedStats)stats).weapon.OnAttackUnpressed();
        if (actionTwo) ((BipedStats)stats).weapon.OnAttack();
    }

    void UseAccessory()
    {
        if (actionThreePressed) ((BipedStats)stats).accessory.GetActionDown();
        if (actionThreeUnpressed) ((BipedStats)stats).accessory.GetActionUp();
        if (actionThree) ((BipedStats)stats).accessory.GetAction();
    }
}
