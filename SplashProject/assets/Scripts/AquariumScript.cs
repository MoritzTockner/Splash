using UnityEngine;

public class AquariumScript : MonoBehaviour {

	private bool ignoreFollowing = false;	// true if fish is swimming
	private bool following = false;			// true if aquarium follows mouse
	private bool followingBuffer = false;	// true if aquarium was clicked while fish was swimming
	private Vector2 deltaAquariumMouse;	// vector from aquarium to mouse
	private Vector2 newPosition;	// next position of aquarium
	private Rigidbody2D aquariumRb;
	private SpriteRenderer spriteRenderer;
	private const int fishLayer = 8;
	private const int aquariumLayer = 9;

	public Sprite transparent; 	// Indicates that aquarium is draggable --> moves through objects and fish
	public Sprite highlighted;	// Indicates that aquarium is clickable
	public Sprite normal;		// Indicates that fish is in aquarium --> not clickable


	public bool IgnoreFollowing { get; set; }

	void Start () {
		aquariumRb = transform.GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void Update() {
		if (Input.GetMouseButtonUp (0)) {
			following = false;
			followingBuffer = false;
		}
		if (ignoreFollowing == false && following == false) {	// Aquarium is clickable
			spriteRenderer.sprite = highlighted;
		}
	}

	void FixedUpdate () {
		if (following == true) {	// aquarium is draggable
			newPosition = new Vector2 (Camera.main.ScreenToWorldPoint (Input.mousePosition).x, 
									   Camera.main.ScreenToWorldPoint (Input.mousePosition).y) - deltaAquariumMouse;	
			aquariumRb.MovePosition (newPosition);
		}
		if (followingBuffer == true && Input.GetMouseButton(0) && ignoreFollowing == false) {		// Can drag aquarium if mouse button is still held down after fish exits
			following = true;
			followingBuffer = false;
		}
	}

	void OnMouseOver() {
		if (Input.GetMouseButtonDown (0)) {
			if (ignoreFollowing == false) {	// Object is klickable
				following = true;
				spriteRenderer.sprite = transparent;			
			} else {	// Object isnt clickable at the moment, click is saved 
				followingBuffer = true;			
			}
			deltaAquariumMouse = Camera.main.ScreenToWorldPoint (Input.mousePosition) - transform.position; 
		}
	}
		
	void OnCollisionStay2D (Collision2D col) {
		if (col.gameObject.transform.tag == "Ground" && following == false) {		// cant collide if aquarium is still dragged
			Physics2D.IgnoreLayerCollision (fishLayer, aquariumLayer, false);
		}
		if (col.gameObject.transform.tag == "Ground" && following == true) {		
			Physics2D.IgnoreLayerCollision (fishLayer, aquariumLayer, true);
		}
	}
		
	void OnCollisionExit2D (Collision2D col) {
		if (col.gameObject.transform.tag == "Ground") {	// can only collide if aquarium is on ground
			Physics2D.IgnoreLayerCollision (fishLayer, aquariumLayer, true);
		}
	}
}
