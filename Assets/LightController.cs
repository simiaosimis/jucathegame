using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {

	private float currentColor = 0f;
	private float initialTime = 0f;
	public float timeToDarkness = 10f;


	void Start() {
		initialTime = Time.time;
	}

	void Update () {
		currentColor = Mathf.Max(0f, 1f - (Time.time - initialTime)/timeToDarkness);
		RenderSettings.ambientLight = new Color(currentColor, currentColor, currentColor);
	}
}
