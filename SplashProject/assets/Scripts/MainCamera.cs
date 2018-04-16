using System;
using UnityEngine;

public class MainCamera : MonoBehaviour
{
	public Transform Fish;
	private Vector3 RelativeFishPos;
	private Transform ground;
	SpawnGround spawnGround;
	public float transitionSpeed = 1f;
	public bool isTransitioning = false;
	private float timeStartedTransitioning;
	private float startPosition;
	private float endPosition;
	public float timeTakenDuringTransition = 1f;
	public bool isTransitioned = true;
	public int groundLevel = 0;
	private float cameraStart = 0f;

	void Start() {
		ground = GameObject.Find ("SpawnGround").GetComponent<Transform> ();
		spawnGround = ground.GetComponent<SpawnGround> ();
		RelativeFishPos = new Vector3(Fish.transform.position.x - transform.position.x, 0f, 0f);
		cameraStart = transform.position.x + 8f;
	}

	void Update() {
		if((spawnGround.traveledDistance - cameraStart) % spawnGround.newLevelDistance < spawnGround.newLevelDistance / 2f && isTransitioned == true) {
			isTransitioned = false;
		}
		if ((spawnGround.traveledDistance - cameraStart) % spawnGround.newLevelDistance >= spawnGround.newLevelDistance / 2f && isTransitioned == false) {
			switch (groundLevel) {
			case 0:
				groundLevel = 1;
				StartTransition (3f);
				break;
			case 1:
				if (spawnGround.groundLevelDecision) {
					groundLevel = 0;
					StartTransition (-3f);
				} else {
					groundLevel = 2;
					StartTransition (3f);
				}
				break;
			case 2:
				groundLevel = 1;
				StartTransition (-3f);
				break;
			}
			isTransitioned = true;
		}
	}

	void FixedUpdate() {
		// Lerping along y axis to follow ground spawn
		if (isTransitioning) {
			float timeSinceStarted = Time.time - timeStartedTransitioning;
			float percentageComplete = timeSinceStarted / timeTakenDuringTransition;

			transform.position = new Vector3 (0f, Mathf.Lerp (startPosition, endPosition, percentageComplete), -5f);

			if (percentageComplete >= 1.0f) {
				isTransitioning = false;
			}
		}

		//Moving along x Axis with speed of the fish
		transform.position = transform.position + new Vector3(Fish.transform.position.x - transform.position.x, 0f, 0f) - RelativeFishPos;

	}

	public void StartTransition(float newPos) {
		isTransitioning = true;
		timeStartedTransitioning = Time.time;

		startPosition = transform.position.y;
		endPosition = transform.position.y + newPos;
	}

}

