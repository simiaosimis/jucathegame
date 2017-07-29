using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightController : MonoBehaviour {
	private float currentColor = 0f;
	void Update () {
		currentColor = Mathf.Max(0f, 1f - Time.time/3f);
		RenderSettings.ambientLight = new Color(currentColor, currentColor, currentColor);
	}
}
