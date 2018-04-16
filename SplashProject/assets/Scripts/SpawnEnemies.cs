using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemies : MonoBehaviour {

	public GameObject[] obj;
	private SpawnGround spawnGround;
	private GameObject fishObj;
	private float relativeFishPos;
	public float SpwnLevelMin = -2f;
	public float SpwnLevelMax = 2f;
	public float SpwnLevelCat = -3f;
	public float SpwnLevelHand = 3f;
	private Vector3 StartPos;
	public float TraveledDistance = 0f;
	public float SpawnDistanceMin = 1f;
	public float SpawnDistanceMax = 10f;
	private float SpawnDistance = 0f;
	private float LastSpawn = 0f;
	public float handycap = 100f;
	private bool handycapChanged = false;

	void Start () {
		StartPos = transform.position;
		fishObj = GameObject.Find ("Fish");
		spawnGround = GameObject.Find ("SpawnGround").GetComponent<SpawnGround> ();
		relativeFishPos = transform.position.x - fishObj.transform.position.x;
	}

	void FixedUpdate() {
		TraveledDistance = (StartPos - transform.position).magnitude;
		transform.position += Vector3.right * (fishObj.transform.position.x + relativeFishPos - transform.position.x);

		if (TraveledDistance - LastSpawn >= SpawnDistance) {
			Spawn ();
			LastSpawn = TraveledDistance;
			SpawnDistance = Random.Range (SpawnDistanceMin, SpawnDistanceMax);
		}

		if (TraveledDistance % handycap >= handycap / 2f && handycapChanged == false && SpawnDistanceMin > 2f && SpawnDistanceMax > 7f) {
			SpawnDistanceMin -= 0.5f;
			SpawnDistanceMax -= 1f;
			handycapChanged = true;
		}
		if (TraveledDistance % handycap < handycap / 2f && handycapChanged == true) {
			SpawnDistanceMin -= 0.5f;
			SpawnDistanceMax -= 1f;
			handycapChanged = false;
		}
	}


	void Spawn() {
		Vector3 SpwnPosition;
		Object Enemy = obj[Random.Range(0, obj.GetLength(0))];
		if (Enemy.name == obj[0].name || Enemy.name == obj[1].name || Enemy.name == obj[2].name) {
			SpwnPosition = new Vector3 (transform.position.x, transform.position.y + Random.Range (SpwnLevelMin, SpwnLevelMax) + spawnGround.groundLevel * 3f, transform.position.z);		
		} else if (Enemy.name == obj[3].name) {
			SpwnPosition = new Vector3 (transform.position.x, transform.position.y + SpwnLevelCat + spawnGround.groundLevel * 3f, transform.position.z);		
		} else {
			SpwnPosition = new Vector3 (transform.position.x, transform.position.y + SpwnLevelHand + spawnGround.groundLevel * 3f, transform.position.z);		
		}

		Instantiate(Enemy, SpwnPosition, Quaternion.identity);
	}

}
