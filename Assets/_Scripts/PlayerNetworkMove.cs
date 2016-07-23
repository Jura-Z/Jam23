using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkMove : NetworkBehaviour {

	Transform trans; // Used to cache the transform
	Rigidbody body; // Used to cache the RigidBody
	float hInput;
	float vInput;
	float rotationSpeed = 90f;
	float moveSpeed = 5f;

	void Awake()
	{
		trans = transform;
		body = GetComponent<Rigidbody>();
	}

	// Update is called once per frame
	void Update () {
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
		body.angularVelocity = Vector3.zero;

		var mov = new Vector3 (hInput, vInput, 0);

		// Handle forward movement
		body.MovePosition(body.position + mov * moveSpeed * Time.deltaTime);
	}
}
