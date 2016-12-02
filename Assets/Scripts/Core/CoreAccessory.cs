using UnityEngine;
using System.Collections;

public class CoreAccessory : MonoBehaviour
{

    #region Variables

    #region Modifiers

    public float jumpStrengthMod;
    public float jumpStrengthMultiplier;

    public float jumpTimerMod;
    public float jumpTimerMultiplier;

    public float fastFallMod;
    public float fastFallMultiplier;

    public float gravityStrengthMod;
    public float gravityStrengthMultiplier;

    public float moveSpeedMod;
    public float moveSpeedMultiplier;

    public float accelerationMod;
    public float accelerationMultiplier;

    public bool replaceRequestJump;
    public bool replaceResetJump;
    public bool replaceMove;
    public bool replaceHit;
    public bool replaceFastFall;

    #endregion

    #region Components

    [HideInInspector]
    public BipedMotor owner;

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    // Use this for initialization
    protected virtual void Start ()
    {
	
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
	
	}

    public virtual void RequestJumpFunction()
    {

    }

    public virtual void ResetJumpFunction()
    {

    }

    public virtual void FastFallFunction()
    {

    }

    public virtual void MoveFunction()
    {

    }

    public virtual void HitFunction()
    {

    }

    public virtual void GetActionDown()
    {

    }

    public virtual void GetActionUp()
    {

    }

    public virtual void GetAction()
    {

    }

    #endregion

    #endregion

}
