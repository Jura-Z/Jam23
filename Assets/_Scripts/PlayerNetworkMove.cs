using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkMove : NetworkBehaviour {

	public float moveSpeed = 5f;

	Rigidbody2D body; // Used to cache the RigidBody
	float hInput;
	float vInput;

	void Awake()
	{
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update ()
    {
		if(isLocalPlayer == false) return; // Don't check input if this isn't
		// running on the local player object
		hInput = Input.GetAxis("Horizontal");
		vInput = Input.GetAxis("Vertical");
	}

	void FixedUpdate()
	{
		if(isLocalPlayer == false) return; // Don't try to move the player
		// if this isn't running on the local player object

		// Remove unwanted forces resulting from collisions
		body.velocity = Vector3.zero;
		body.angularVelocity = 0f;

		var mov = new Vector2 (hInput, vInput);

		// Handle forward movement
		body.MovePosition(body.position + mov * moveSpeed * Time.deltaTime);
	}
}
