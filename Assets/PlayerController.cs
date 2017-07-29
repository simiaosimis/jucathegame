using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public float speed = 8f;
	public bool direction;
	private float timeJump = 0f;
	
	public GameObject shoot;
	public float cdShoot = 0.2f;
	private float shootTime;
	
	private Rigidbody rigidBody;
	
	bool isGround = false;

	public int score = 0;

	public float batteryLife = 10f;
	public float initialBatteryLife = 10f;
	public bool isLanternOn = false;
	public float cdActiveLantern = 0.2f;
	private float activeLanternTime = 0f;
	private GameObject lantern;

	// Use this for initialization
	void Start () {
		rigidBody = this.GetComponent<Rigidbody>();
		Physics.gravity = new Vector3(0, -30.0F, 0);
		lantern = this.transform.Find("Lantern").gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		CheckWinningState();
		UpdateDirection();
		Shoot();
		CheckJump();
		Move();
		ResolveCollision();
		ResolveLantern();
	}

	void CheckWinningState() {
		if(this.score == 3) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
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
			this.rigidBody.AddForce(new Vector3(0f, 2500f, 0f),  ForceMode.Force);
		}
	}

	void Move () {
		float y = this.rigidBody.velocity.y;
		float z = this.rigidBody.velocity.z;
		timeJump += Time.deltaTime;
		this.rigidBody.velocity = new Vector3((isGround ? 1f : Mathf.Max(1f - timeJump, 0.5f)) * this.speed * Input.GetAxis("Horizontal"), y, z);
	}

	void Lantern(){
		if(isLanternOn){
			batteryLife -= Time.deltaTime;
			lantern.SetActive(true);
			lantern.GetComponent<Light>().intensity = 3f * (batteryLife/initialBatteryLife);
		}
		else{
			lantern.SetActive(false);
		}
	}
	
	void ResolveLantern(){
		activeLanternTime += Time.deltaTime;
		if(Input.GetKey(KeyCode.F) && activeLanternTime > cdActiveLantern){
 			isLanternOn = !isLanternOn;
 			activeLanternTime = 0f;
		}
		Lantern();
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
		if(collidedObject.gameObject.CompareTag("Floor") && is_really_floor) {
			isGround = true;
		}
		else if(collidedObject.gameObject.CompareTag("DeathFloor")) {
			SceneManager.LoadScene(SceneManager.GetActiveScene().name);
		}
    }
}