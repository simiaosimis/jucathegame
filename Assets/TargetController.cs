using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour {

	public bool hit;
	public GameObject bagulho;
	private bool lastHit;
	private SpriteRenderer sprite;

	void Update() {
		if(hit != lastHit) {
			bagulho.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("snowman_1");
		}
		lastHit = hit;
	}
}
