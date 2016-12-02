/*
 *      Class
 *          CoreObject
 *          
 *      Purpose
 *          A class for all objects to inherit from. Contains collision info.
 *          Also contains virtual function to over-ride that are automatically called.
 * 
 *      Created by
 *          Justin Hamm
 *          
 *      Modified by
 *          None
 * 
 */

using UnityEngine;
using UnityEngine.Networking;
using System.Collections.Generic;

[RequireComponent(typeof(Collider2D))]
public class CoreObject : NetworkBehaviour
{

    #region Variables

    #region Components

    // Our objects collider
    new protected Collider2D collider;

    // List of objects currently colliding with this object.
    protected List<PlayerController>  collidingPlayers;
    protected List<Rigidbody2D>       collidingRigidbodies;
    protected List<BulletMotor>       collidingBullets;

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    // Use this for initialization
    protected virtual void Start ()
    {
        collider = GetComponent<Collider2D>();

        collidingPlayers     = new List<PlayerController>();
        collidingBullets     = new List<BulletMotor>();
        collidingRigidbodies = new List<Rigidbody2D>();
	}

    // Update is called once per frame
    protected virtual void Update ()
    {
        // Call the inheriting classes override functions on all objects we're colliding with
        BasePlayerUpdate();
        BaseBulletUpdate();
        BaseRigidbodyUpdate();
	}

    void OnCollisionEnter2D(Collision2D collision)
    {
        // When we collide with an object, check its components.
        // Then add it to the appropriate lists.
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BulletMotor      bm = collision.gameObject.GetComponent<BulletMotor>();
        Rigidbody2D      rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (pc != null) BaseOnPlayerCollision(pc);
        if (bm != null) BaseOnBulletCollision(bm);
        if (rb != null) BaseOnRigidbodyCollision(rb);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // When we stop colliding with an object, check its components.
        // Then remove it from the appropriate lists.
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BulletMotor bm = collision.gameObject.GetComponent<BulletMotor>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (pc != null) BaseOnPlayerExit(pc);
        if (bm != null) BaseOnBulletExit(bm);
        if (rb != null) BaseOnRigidbodyExit(rb);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // When we collide with an object, check its components.
        // Then add it to the appropriate lists.
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BulletMotor bm = collision.gameObject.GetComponent<BulletMotor>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (pc != null) BaseOnPlayerCollision(pc);
        if (bm != null) BaseOnBulletCollision(bm);
        if (rb != null) BaseOnRigidbodyCollision(rb);
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        // When we stop colliding with an object, check its components.
        // Then remove it from the appropriate lists.
        PlayerController pc = collision.gameObject.GetComponent<PlayerController>();
        BulletMotor bm = collision.gameObject.GetComponent<BulletMotor>();
        Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();

        if (pc != null) BaseOnPlayerExit(pc);
        if (bm != null) BaseOnBulletExit(bm);
        if (rb != null) BaseOnRigidbodyExit(rb);
    }

    #endregion

    #region Base

    #region Enter

    void BaseOnPlayerCollision(PlayerController player)
    {
        collidingPlayers.Add(player);
        OnPlayerCollision(player);
    }

    void BaseOnRigidbodyCollision(Rigidbody2D rbody)
    {
        collidingRigidbodies.Add(rbody);
        OnRigidbodyCollision(rbody);
    }

    void BaseOnBulletCollision(BulletMotor bullet)
    {
        collidingBullets.Add(bullet);
        OnBulletCollision(bullet);
    }

    #endregion

    #region Update

    void BasePlayerUpdate()
    {
        foreach (PlayerController pc in collidingPlayers)
            PlayerUpdate(pc);
    }

    void BaseRigidbodyUpdate()
    {
        foreach (Rigidbody2D rb in collidingRigidbodies)
            RigidbodyUpdate(rb);
    }

    void BaseBulletUpdate()
    {
        foreach (BulletMotor bm in collidingBullets)
            BulletUpdate(bm);
    }

    #endregion

    #region Exit

    void BaseOnPlayerExit(PlayerController player)
    {
        collidingPlayers.Remove(player);
        OnPlayerExit(player);
    }

    void BaseOnRigidbodyExit(Rigidbody2D rbody)
    {
        collidingRigidbodies.Remove(rbody);
        OnRigidbodyExit(rbody);
    }

    void BaseOnBulletExit(BulletMotor bullet)
    {
        collidingBullets.Remove(bullet);
        OnBulletExit(bullet);
    }

    #endregion

    #endregion

    #region Virtual

    #region Enter

    protected virtual void OnPlayerCollision(PlayerController player)
    {

    }

    protected virtual void OnRigidbodyCollision(Rigidbody2D rbody)
    {

    }

    protected virtual void OnBulletCollision(BulletMotor bullet)
    {

    }

    #endregion

    #region Update

    protected virtual void PlayerUpdate(PlayerController player)
    {

    }

    protected virtual void RigidbodyUpdate(Rigidbody2D rbody)
    {

    }

    protected virtual void BulletUpdate(BulletMotor bullet)
    {

    }

    #endregion

    #region Exit

    protected virtual void OnPlayerExit(PlayerController player)
    {

    }

    protected virtual void OnRigidbodyExit(Rigidbody2D rbody)
    {

    }

    protected virtual void OnBulletExit(BulletMotor bullet)
    {

    }

    #endregion

    #endregion

    #endregion

}
