using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public float speed = 20f;
	public bool direction;

	// Use this for initialization
	void Start () {
		this.transform.position += new Vector3(this.direction ? +1 : -1, 0f, 0f);
		Debug.Log("Shoot x = " + this.transform.position.x);
		Debug.Log("Shoot y = " + this.transform.position.y);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody>().velocity = new Vector3((direction ? 1 : -1) * speed, 0f, 0f);
	}

	void OnCollisionEnter(Collision collision) {
		if(!collision.gameObject.CompareTag("Player")) {
			if(collision.gameObject.CompareTag("Target")) {
				bool hit = collision.gameObject.GetComponent<TargetController>().hit;
				if(!hit) {
					ans++;
				}
				collision.gameObject.GetComponent<TargetController>().hit = true;
			}
			Destroy(this.gameObject);
		}
    }
}
