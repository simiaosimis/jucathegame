using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

	public float offset = 1f;
	public GameObject player;
	private float rightBound;
	private float leftBound;
	private float topBound;
	private float bottomBound;
	private SpriteRenderer spriteBounds;
	private GameObject[] shoots;
	private float minx, maxx;
	private Vector3 playerPosition;

	void Update () {
		playerPosition = this.player.transform.position;
		minx = playerPosition.x;
		maxx = playerPosition.x;
		shoots = GameObject.FindGameObjectsWithTag("Shoot");
		foreach(GameObject shoot in shoots){
			minx = Mathf.Min(minx, shoot.transform.position.x);
			maxx = Mathf.Max(maxx, shoot.transform.position.x);
		}
		UpdateZoom();
		UpdatePosition();
	}

	void UpdateZoom () {
		spriteBounds = GameObject.Find("background").GetComponent<SpriteRenderer>();
		float cameraSize = Camera.main.orthographicSize;
		float nextCameraize = Mathf.Min((maxx - minx)/2f + 5f, spriteBounds.bounds.size.y / 2.0f);
		float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		if(shoots.Length == 0 || minx < (this.transform.position.x - horzExtent) + offset || maxx + offset >  (this.transform.position.x + horzExtent) ){
			Camera.main.orthographicSize = Mathf.Lerp(cameraSize, nextCameraize, Time.deltaTime);
		}
	}

	void GetBounds () {
		float vectExtent = Camera.main.orthographicSize;
		float horzExtent = Camera.main.orthographicSize * Screen.width / Screen.height;
		leftBound = (float)(horzExtent - spriteBounds.bounds.size.x / 2.0f);
	    rightBound = (float)(spriteBounds.bounds.size.x / 2.0f - horzExtent);
	    bottomBound = (float)(vectExtent - spriteBounds.bounds.size.y / 2.0f);
	    topBound = (float)(spriteBounds.bounds.size.y  / 2.0f - vectExtent);
	}

	void UpdatePosition () {
		GetBounds();
		Vector3 nextPosition = new Vector3((minx + maxx)/2, playerPosition.y, -10f);
		nextPosition.x = Mathf.Clamp(nextPosition.x, leftBound, rightBound);
		nextPosition.y = Mathf.Clamp(nextPosition.y, bottomBound, topBound);
		float distance = Vector3.Distance(this.transform.position, nextPosition);
		float delta = 1f/distance;
		this.transform.position = Vector3.Lerp(this.transform.position, nextPosition, delta/5f);
	}
}
