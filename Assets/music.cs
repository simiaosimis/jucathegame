using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music : MonoBehaviour {

	// Use this for initialization
	void Start () {
		DontDestroyOnLoad(this.transform.gameObject);
		AudioSource audiosource = GetComponent<AudioSource>();
		if(!audiosource.isPlaying){
			audiosource.Play();
		}
		 if (FindObjectsOfType(GetType()).Length > 1)
         {
             Destroy(gameObject);
         }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
