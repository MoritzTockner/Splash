using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BezierCurve : MonoBehaviour {

	public Vector2[] points;

	public void MakeCurve(Vector2 p0, Vector2 p1, Vector2 p2, Vector2 p3) {
		points = new Vector2[] { p0, p1, p2, p3 };
	}

	public Vector2 GetPoint (float t) {
		return transform.TransformPoint(Bezier.GetPoint(points[0], points[1], points[2], points[3], t));
	}

	public Vector2 GetVelocity (float t) {
		return transform.TransformPoint (Bezier.GetFirstDerivative (points [0], points [1], points [2], points[3], t)) - transform.position;
	}

	public Vector2 GetDirection(float t) {
		return GetVelocity (t).normalized;
	}
	
}
