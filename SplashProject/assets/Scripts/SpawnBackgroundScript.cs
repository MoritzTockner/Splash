using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBackgroundScript : MonoBehaviour {

	public GameObject obj;
	private GameObject fishObj;
	private float relativeFishPos;
	private Vector3 StartPos;
	public float TraveledDistance = 0f;
	private bool Spawned = false;
	public float SpawnDistance = 1f;

	void Start () {
		StartPos = transform.position;
		fishObj = GameObject.Find ("Fish");
		relativeFishPos = transform.position.x - fishObj.transform.position.x;
	}

	// Use this for initialization
	void FixedUpdate() {
		TraveledDistance = (StartPos - transform.position).magnitude;
		transform.position += Vector3.right * (fishObj.transform.position.x + relativeFishPos - transform.position.x);

		if (TraveledDistance % SpawnDistance <= SpawnDistance / 2 && Spawned == false) {
			Spawn ();
			Spawned = true;
		}
		if (TraveledDistance % SpawnDistance >= SpawnDistance / 2) {
			Spawned = false;
		}
	}

	void Spawn() {
		Instantiate (obj, transform.position, Quaternion.identity);
	}

}
