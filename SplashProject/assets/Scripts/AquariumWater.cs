using UnityEngine;

public class AquariumWater : MonoBehaviour {

	AquariumScript aquariumScript;
	FishScript fishScript;
	SpriteRenderer spriteRenderer;

	public Sprite normal;
	public Sprite highlighted;

	void Start() {
		aquariumScript = GameObject.Find ("Aquarium").GetComponent<AquariumScript> ();
		spriteRenderer = GameObject.Find ("Aquarium").GetComponent<SpriteRenderer> ();
		fishScript = GameObject.Find ("Fish").GetComponent<FishScript> ();
	}

	void OnTriggerEnter2D (Collider2D col) {
		if (col.gameObject.name == "Fish") {
			aquariumScript.IgnoreFollowing = true;
			fishScript.EnterAquarium (col.gameObject.transform.position);
			spriteRenderer.sprite = normal;
		}
	}

	void OnTriggerExit2D (Collider2D col) {
		if (col.gameObject.name == "Fish") {
			spriteRenderer.sprite = highlighted;
			aquariumScript.IgnoreFollowing = false;
		}
	}

}
