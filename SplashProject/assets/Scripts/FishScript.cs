using System.Collections;
using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;

public class FishScript : MonoBehaviour {

	GameObject aquariumEnd;
	GameObject curve;
	BezierCurve bezierCurve;
	Rigidbody2D fishRb;
	HUDScript hud;

	public Sprite[] mSprites;
	private float switchTime = 0.005f;
	private int counter = 0;
	private SpriteRenderer spriteRenderer;

	Vector2 ForceVector = new Vector2(0.3f, 0.7f);
	public float minForceValue;
	private float forceValue;
	public float forceWeight;

	public float fishDivingDistance = 1f;
	private float enterDist;
	public float duration = 1f;
	private float progress;
	private bool fishInAquarium = false;

	void Start() {
		fishRb = GetComponent<Rigidbody2D> ();
		aquariumEnd = GameObject.Find ("AquariumEnd");
		curve = GameObject.Find ("Curve");
		bezierCurve = curve.GetComponent<BezierCurve> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		StartCoroutine ("SwitchSprite");	// Swimming animation
	}

	void FixedUpdate () {
		Swim ();
		FaceDirection ();
	}

	void OnCollisionEnter2D(Collision2D col) {
		if (col.gameObject.transform.tag == "Ground") {		// Game over when fish hits ground
			hud = GameObject.Find ("Main Camera").GetComponent<HUDScript> ();
			hud.DecreaseLife ();
		}
	}

	void Swim() {
		if (fishInAquarium == true) {
			progress += Time.deltaTime / duration;
			if (progress > 0.66f) {		// need to follow only 2/3 of the bezier curve
				progress = 1f;
			} else {	// move along bezier curve
				Vector3 position = bezierCurve.GetPoint (progress);
				fishRb.MovePosition (position);
			}
		}

		if (progress >= 1f) {
			Jump ();
		}
	}

	void Jump() {
		fishRb.velocity = ForceVector * (minForceValue + forceValue * forceWeight);
		progress = 0f;
		fishInAquarium = false;
	}

	void FaceDirection() {
		if (fishInAquarium == true) {
			Vector3 position = bezierCurve.GetPoint (progress);

			// fish has no velocity while swimming, therefore faces in direction of next point on bezier curve
			transform.right = new Vector3(position.x - transform.position.x, position.y - transform.position.y, 0f);	

		} else { 
			// fish faces in direction of movement
			transform.right = new Vector3(transform.position.x + fishRb.velocity.x, transform.position.y + fishRb.velocity.y, 0f);
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

	// Calculates swimming curve (bezier curve) and force value for jumping out of aquarium
	public void EnterAquarium(Vector2 EntryPoint) {
		enterDist = aquariumEnd.transform.position.x - EntryPoint.x;
		forceValue = enterDist;
		Vector2 BezierEnd = new Vector2(aquariumEnd.transform.position.x, aquariumEnd.transform.position.y); // End of bezier curve
		// Calculates swimming curve between 3 different points: entry point, exit point and one point right between them with fishDivingDistance down on y axis
		bezierCurve.MakeCurve(EntryPoint, new Vector2(EntryPoint.x + (aquariumEnd.transform.position.x - EntryPoint.x) / 2, EntryPoint.y - fishDivingDistance), BezierEnd, BezierEnd);
		fishInAquarium = true;
	}
}
