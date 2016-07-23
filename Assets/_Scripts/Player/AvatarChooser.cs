using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class AvatarChooser : NetworkBehaviour
{
    public SpriteRenderer target;
    public Sprite player1AvatarImage;
    public Sprite player2AvatarImage;

	// Use this for initialization
	void Start ()
    {
	
	}

    public override void OnStartClient()
    {
        Debug.LogFormat("{3} 1 - {0} {1} {2}", this.isClient, this.isServer, this.isLocalPlayer, this.GetInstanceID());
        if (isLocalPlayer == true)
        {
            target.sprite = player1AvatarImage;
        }
        else
        {
            target.sprite = player2AvatarImage;
        }
    }

    public override void OnStartLocalPlayer()
    {
        Debug.LogFormat("{3} 2 - {0} {1} {2}", this.isClient, this.isServer, this.isLocalPlayer, this.GetInstanceID());
        if (isLocalPlayer == true)
        {
            target.sprite = player1AvatarImage;
        }
        else
        {
            target.sprite = player2AvatarImage;
        }
    }
}
