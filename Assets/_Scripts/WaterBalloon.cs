using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class WaterBalloon : NetworkBehaviour
{
    public int capacity;

    void OnTriggerEnter2D(Collider2D other)
    {
		var player = other.GetComponent<Player> ();
		if (player != null) {
			player.WaterPool.Add (5);
			NetworkBehaviour.Destroy (this.gameObject);
		}
    }
}
