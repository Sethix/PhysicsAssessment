using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class CoreWeapon : NetworkBehaviour {

    public GameObject projectile;

    [HideInInspector]
    public BipedMotor owner;

    protected virtual void Start()
    {

    }

    protected virtual void Update()
    {

    }

    public virtual void OnAttackPressed()
    {

    }

    public virtual void OnAttackUnpressed()
    {

    }

    public virtual void OnAttack()
    {

    }
}
