using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class HUDScript : MonoBehaviour {

	private Vector3 StartPos;
	private float TraveledDistance = 0f;

	void Start () {
		StartPos = transform.position;
	}
	
	void Update () {
		TraveledDistance = (StartPos - transform.position).magnitude;
	}

	void OnDisable(){
		PlayerPrefs.SetInt ("Distance", (int)TraveledDistance);
	}

	void OnGUI ()
	{
		GUI.Label (new Rect (10, 10, 150, 60), "Distance: " + (int)(TraveledDistance) + " cm");
	}

	public void DecreaseLife() {
		SceneManager.LoadScene (1, LoadSceneMode.Single);
		return;

	}
}
