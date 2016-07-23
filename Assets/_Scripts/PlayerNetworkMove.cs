using UnityEngine;
using System.Collections;
using UnityEngine.Networking;

public class PlayerNetworkMove : NetworkBehaviour {

	public float moveSpeed = 5f;
	public GameObject bullet;

	Collider bodyCollider;
	Rigidbody body; // Used to cache the RigidBody
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
		bulletGO.transform.forward = forward;
		bulletGO.GetComponent<Rigidbody> ().velocity = forward * 20.0f;
		Physics.IgnoreCollision (bodyCollider, bulletGO.GetComponent<Collider> ());
	}

	void Awake()
	{
		bodyCollider = GetComponent<Collider> ();
		body = GetComponent<Rigidbody>();
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
		body.angularVelocity = Vector3.zero;

		var mov = new Vector3 (hInput, vInput, 0);

		// Handle forward movement
		body.MovePosition(body.position + mov * moveSpeed * Time.deltaTime);
	}
}
