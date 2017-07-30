using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RefreshTimer : MonoBehaviour {
	private const float interval = 3f;
	private float currentTime = 0f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if(currentTime > interval) {
			currentTime = 0f;
			SceneManager.LoadScene("gameplay");
		} else {
			currentTime += Time.deltaTime;
		}
	}
}
