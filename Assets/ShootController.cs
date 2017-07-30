using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootController : MonoBehaviour {

	public float speed = 20f;
	public bool direction;
	private float lifeTime = 0f;
	private const float maxLifeTime = 2f;

	// Use this for initialization
	void Start () {
		this.transform.position += new Vector3(this.direction ? +1 : -1, 0f, 0f);
	}
	
	// Update is called once per frame
	void Update () {
		this.GetComponent<Rigidbody>().velocity = new Vector3((direction ? 1 : -1) * speed, 0f, 0f);
		if((lifeTime += Time.deltaTime) > maxLifeTime)
			Destroy(this.gameObject);
	}

	void OnCollisionEnter(Collision collision) {
		if(!collision.gameObject.CompareTag("Player")) {
			if(collision.gameObject.CompareTag("Target")) {
				bool alreadyHit = collision.gameObject.GetComponent<TargetController>().hit;
				if(!alreadyHit) {
					GameObject.FindGameObjectsWithTag("Player")[0].GetComponent<PlayerController>().score++;
				}
				collision.gameObject.GetComponent<TargetController>().hit = true;
			}
			Destroy(this.gameObject);
		}
    }
}
