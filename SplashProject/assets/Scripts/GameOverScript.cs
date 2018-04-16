using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameOverScript : MonoBehaviour {

	int Distance = 0;

	void Start () {
		Distance = PlayerPrefs.GetInt ("Distance");
	}

	void OnGUI() {
		GUI.Label (new Rect (Screen.width / 2 - 40, 50, 80, 30), "GAME OVER");
		GUI.Label (new Rect (Screen.width / 2 - 40, 300, 120, 30), "Distance: " + Distance + " cm");
		if(GUI.Button(new Rect(Screen.width / 2 - 30, 350, 60, 30), "Retry?")) {
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		}
			
	}
	

}
