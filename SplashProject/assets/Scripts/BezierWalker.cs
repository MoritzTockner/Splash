using UnityEngine;

public class BezierWalker : MonoBehaviour {

	public BezierCurve curve;
	public float duration;
	private float progress;
	public bool lookForward;
	public bool trigger = false;

	void Start() {
		progress = 0f;
	}

	void Update () {
		progress += Time.deltaTime / duration;
		if (progress > 1f) {
			progress = 1f;
			return;
		}
		Vector2 position = curve.GetPoint (progress);
		transform.localPosition = position;
		if (lookForward) {
			transform.LookAt (position + curve.GetDirection (progress));
		}
	}
}
