using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGround : MonoBehaviour {

	public GameObject[] obj;
	private GameObject fishObj;
	private float relativeFishPos;
	Transform ground;
	private Vector3 StartPos;
	private bool spawned = false;

	public float traveledDistance = 0f;
	public float SpawnDistance = 1f;
	public float newLevelDistance = 5f;
	public bool levelChanged = false;
	public int groundLevel = 0;
	public bool groundLevelDecision = false;



	void Start () {
		StartPos = transform.position;
		ground = GameObject.Find ("Ground").GetComponent<Transform> ();
		fishObj = GameObject.Find ("Fish");
		relativeFishPos = transform.position.x - fishObj.transform.position.x;
	}

	// Use this for initialization
	void FixedUpdate() {
		traveledDistance = (StartPos - transform.position).magnitude;
		transform.position += Vector3.right * (fishObj.transform.position.x + relativeFishPos - transform.position.x);    // Scroll as fast as fish and maintain starting distance
		if (traveledDistance % newLevelDistance >= newLevelDistance / 2f && levelChanged == false) {
			switch (groundLevel) {	
			case 0:
				groundLevel = 1;
				break;
			case 1:
				if (groundLevelDecision = Random.Range (0, 1) > 0.5) {
					groundLevel = 0;
				} else {
					groundLevel = 2;
				}
				break;
			case 2:
				groundLevel = 1;
				break;
			}
			levelChanged = true;
		}
		if(traveledDistance % newLevelDistance < newLevelDistance / 2f && levelChanged == true) {
			levelChanged = false;
		}

		if (traveledDistance % SpawnDistance <= SpawnDistance / 2) {
			spawned = false;
		}
		if (traveledDistance % SpawnDistance >= SpawnDistance / 2 && !spawned) {
			Spawn (groundLevel);
			spawned = true;
		}

	}

 	// Instantiates ground object on given groundLevel
	void Spawn(int groundLevel) {
		switch (groundLevel) {
		case 0:
			Instantiate (obj [0], transform.position, Quaternion.identity, ground);
			break;
		case 1:
			Instantiate (obj [0], transform.position, Quaternion.identity, ground);	// still spawn other levels so aquarium cant fall out of screen
			Instantiate (obj [0], transform.position + Vector3.up * 3f, Quaternion.identity, ground);
			break;
		case 2:
			Instantiate (obj [0], transform.position, Quaternion.identity, ground);
			Instantiate (obj [0], transform.position + Vector3.up * 3f, Quaternion.identity, ground);
			Instantiate (obj [0], transform.position + Vector3.up * 6f, Quaternion.identity, ground);
			break;
		}
	}
}
