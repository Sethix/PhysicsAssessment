/*
 *      Class
 *          Transform Sync
 *      
 *      Purpose
 *          Syncs position, rotation and scale data over the network smoothly.
 *      
 *      Created by
 *          Justin Hamm
 *          
 *      Modified by
 *          None
 */

using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class TransformSync : NetworkBehaviour
{

    #region Variables

    #region SyncVars

    // Sync server transform data to clients.
    [SyncVar]
    Vector3     position;

    [SyncVar]
    Vector3     scale;

    [SyncVar]
    Quaternion  rotation;

    #endregion

    #region Modifiers

    // Time to wait before sending transform data
    public float syncRate = 0.11f;

    // Speed to lerp and slerp at.
    public float lerpSpeed = 3.5f;
    public float slerpSpeed = 7;

    // Do we lerp scale or set directly?
    public bool  lerpScale;

    #endregion

    #region Checks

    // Current sync timer
    private float syncTimer = 0;

    #endregion

    #endregion

    #region Functions

    #region UnityFunctions

    // Update is called once per frame
    void Update ()
    {
        // If the player is running locally...
        if (isLocalPlayer)
        {
            // And it's time to sync...
            if (syncTimer >= syncRate)
            {
                // Set the sync vars locally and send them to the server.
                position    = transform.position;
                scale       = transform.localScale;
                rotation    = transform.rotation;

                // If we're not the server, we must send the command.
                if(!isServer)
                    CmdSyncTransform(position, scale, rotation);

                // Reset the timer.
                syncTimer = 0;
            }

            // Otherwise...
            else

                // Increment the timer.
                syncTimer += Time.deltaTime;
        }

        // Otherwise, if not local...
        else
        {
            // Update the transform info based on lerp/slerp speed and server data.
            transform.position = Vector3.Lerp     (transform.position, position, lerpSpeed  * Time.deltaTime);
            transform.rotation = Quaternion.Slerp (transform.rotation, rotation, slerpSpeed * Time.deltaTime);

            // If we're lerping the scale, lerp it. Otherwise, set directly.
            if (lerpScale)  transform.localScale = Vector3.Lerp(transform.localScale, scale, lerpSpeed * Time.deltaTime);

            else            transform.localScale = scale;
        }
    }

    #endregion

    #region Commands

    // Command to update the server transform data using what the client sends.
    [Command]
    void CmdSyncTransform(Vector3 pos, Vector3 scl, Quaternion rot)
    {
        position = pos;
        scale    = scl;
        rotation = rot;
    }

    #endregion

    #endregion

}
