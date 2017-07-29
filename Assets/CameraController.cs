using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {
	public GameObject player;
	void Update () {
		Vector3 playerPosition = this.player.transform.position;
		float minx = playerPosition.x, maxx = playerPosition.x;
		GameObject[] shoots = GameObject.FindGameObjectsWithTag("Shoot");
		foreach(GameObject shoot in shoots){
			minx = Mathf.Min(minx, shoot.transform.position.x);
			maxx = Mathf.Max(maxx, shoot.transform.position.x);
		}
		//Debug.Log("min: " + minx + " max: " + maxx);
		Vector3 nextPosition = new Vector3((minx + maxx)/2, playerPosition.y, -10f - (maxx - minx)/2f);
		float distance = Vector3.Distance(this.transform.position, nextPosition);
		float delta = 1f/distance;
		this.transform.position = Vector3.Lerp(this.transform.position, nextPosition, delta/3f);
	}
}
