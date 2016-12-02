using UnityEngine;
using System.Collections;

public class FollowLocalPlayer : MonoBehaviour {

    Transform player;

    public float followSpeed;
    public float rotateSpeed;

    public bool followsRotation;

    bool playerSet;
	
	// Update is called once per frame
	void Update ()
    {
        if(playerSet)
        {
            if (followsRotation)
                transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotateSpeed * Time.deltaTime);

            transform.position = Vector3.Lerp(new Vector3(transform.position.x, transform.position.y, transform.position.z), 
                                              new Vector3(player.position.x, player.position.y, transform.position.z), 
                                              followSpeed * Time.deltaTime);
        }

        if(!playerSet)
            SetPlayer();
	}

    void SetPlayer()
    {
        foreach (PlayerController pc in FindObjectsOfType<PlayerController>())
            if (pc.isLocalPlayer)
            {
                player = pc.transform;

                playerSet = true;
            }
    }
}
