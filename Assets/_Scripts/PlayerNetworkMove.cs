﻿using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerNetworkMove : NetworkBehaviour {

	public float moveSpeed = 5f;
	public GameObject bullet;

	Collider2D bodyCollider;
	Rigidbody2D body;
	float hInput;
	float vInput;

	[Command]
	void CmdFire(Vector3 shootPosition)
	{
		RpcFire(shootPosition);
	}

	[ClientRpc]
	void RpcFire(Vector3 shootPosition)
	{
		if(isLocalPlayer) return;

		SpawnBullet (this.transform.position, shootPosition);
	}

	void Fire(Vector3 shootPosition)
	{
		if(isLocalPlayer == false) return;

		SpawnBullet (this.transform.position, shootPosition);
	}
		
	void SpawnBullet(Vector3 pos, Vector3 shootPosition)
	{
		GameObject bulletGO = NetworkBehaviour.Instantiate (bullet, pos, Quaternion.identity) as GameObject;
		var forward = (shootPosition - pos).normalized;
		//bulletGO.transform.forward = forward;
		bulletGO.GetComponent<Rigidbody2D> ().velocity = forward * 10.0f;
		Physics2D.IgnoreCollision (bodyCollider, bulletGO.GetComponent<Collider2D> ());
	}

	void Awake()
	{
		bodyCollider = GetComponent<Collider2D> ();
		body = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if(isLocalPlayer == false) return; // Don't check input if this isn't
		// running on the local player object
		hInput = Input.GetAxis("Horizontal");
		vInput = Input.GetAxis("Vertical");

		if (Input.GetMouseButton (0)) {
			var worldMousePosition = Input.mousePosition;
			worldMousePosition.z = 10.0f;
			worldMousePosition = Camera.main.ScreenToWorldPoint(worldMousePosition);
			Fire (worldMousePosition);
			CmdFire (worldMousePosition);
		}
	}

	void FixedUpdate()
	{
		if(isLocalPlayer == false) return; // Don't try to move the player
		// if this isn't running on the local player object

		// Remove unwanted forces resulting from collisions
		body.velocity = Vector3.zero;
		body.angularVelocity = 0;

		var mov = new Vector2 (hInput, vInput);

        Vector2 playerPosition = Camera.main.WorldToViewportPoint(body.position);
        playerPosition.x = Mathf.Clamp(playerPosition.x, 0f, 1f);
        playerPosition.y = Mathf.Clamp(playerPosition.y, 0f, 1f);
        playerPosition = Camera.main.ViewportToWorldPoint(playerPosition);
        // Handle forward movement
        body.MovePosition(playerPosition + mov * moveSpeed * Time.deltaTime);
	}
}
