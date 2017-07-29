using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public float speed = 8f;
	public float cdShoot = 0.2f;
	public bool direction;
	public GameObject shoot;
	private float shootTime;
	private Rigidbody rigidBody;
	private float timeJump = 0f;
	bool isGround = false;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody>();
		 Physics.gravity = new Vector3(0, -20.0F, 0);
	}
	
	// Update is called once per frame
	void Update () {
		UpdateDirection();
		Shoot();
		CheckJump();
		Move();
		ResolveCollision();
	}

	void UpdateDirection () {
		if(Mathf.Abs(Input.GetAxis("Horizontal")) > 1e-9f) {
			this.direction = Input.GetAxis("Horizontal") > 0;
		}
	}

	void Shoot () {
		this.shootTime += Time.deltaTime;
		if(Input.GetKey("space") && this.shootTime > this.cdShoot) {
			this.shootTime = 0f;
			Instantiate(shoot, this.transform).GetComponent<ShootController>().direction = this.direction;
		}
	}

	void CheckJump () {
		if(Input.GetKey("up") && isGround) {
			isGround = false;
			timeJump = 0f;
			this.rigidBody.AddForce(new Vector3(0f, 2000f, 0f),  ForceMode.Force);
		}
	}

	void Move () {
		float y = this.rigidBody.velocity.y;
		float z = this.rigidBody.velocity.z;
		timeJump += Time.deltaTime;
		this.rigidBody.velocity = new Vector3((isGround ? 1f : Mathf.Max(1f - timeJump, 0.5f)) * this.speed * Input.GetAxis("Horizontal"), y, z);
	}

	void ResolveCollision() {
	    Vector3 horizontalMove = rigidBody.velocity;
	    horizontalMove.y = 0;
	    float distance =  horizontalMove.magnitude * Time.fixedDeltaTime;
	    horizontalMove.Normalize();
	    RaycastHit hit;
	 	if(rigidBody.SweepTest(horizontalMove, out hit, distance))
	        this.rigidBody.velocity = new Vector3(0, rigidBody.velocity.y, 0);
	}

	void OnCollisionEnter(Collision collision) {
		GameObject collidedObject = collision.gameObject;
		bool is_really_floor = true;
		float max_y = collision.contacts[0].otherCollider.bounds.max.y;
		foreach (ContactPoint contact in collision.contacts) {
            float y = contact.point.y;
            is_really_floor &= y >= max_y;
        }
        Debug.Log(is_really_floor);
		if(collidedObject.gameObject.CompareTag("Floor") && is_really_floor) {
			isGround = true;
		}
    }
}