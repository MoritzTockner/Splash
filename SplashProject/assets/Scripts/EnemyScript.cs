using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {

	HUDScript hud;

	public Sprite[] mSprites;
	private float switchTime = 0.005f;
	private int counter;
	private SpriteRenderer spriteRenderer;

	void Start() {
		counter = 0;
		spriteRenderer = GetComponent<SpriteRenderer> ();
		StartCoroutine ("SwitchSprite");
	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.name == "Fish") {
			hud = GameObject.Find ("Main Camera").GetComponent<HUDScript> ();
			hud.DecreaseLife ();
		}
	}

	private IEnumerator SwitchSprite() {
		spriteRenderer.sprite = mSprites [counter];
		if (counter < mSprites.Length - 1) {
			counter++;
		} else {
			counter = 0;
		}

		yield return new WaitForSeconds (switchTime);
		StartCoroutine ("SwitchSprite");
	}
}
