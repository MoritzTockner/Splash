using UnityEngine;
using UnityEngine.SceneManagement;

public class EntryHighscoreGUI : MonoBehaviour {
	private string _nameInput = "";
	private string _scoreInput = "0";
	int Distance = 0;

	private void Start(){
		Distance = PlayerPrefs.GetInt ("Distance");
	}

	private void OnGUI() {

		// Interface for reporting test scores.

		GUI.Label(new Rect (Screen.width / 2 - 100, Screen.height / 2 - 100, 200, 30), "Your Score: " + Distance.ToString() + "cm");
		_nameInput = GUI.TextField(new Rect (Screen.width / 2 - 100, Screen.height / 2 - 60, 200, 30), _nameInput);
		_scoreInput = Distance.ToString();

		if (GUI.Button(new Rect (Screen.width / 2 - 100, Screen.height / 2, 200, 30) ,"Submit Highscore")) {
			int score;
			int.TryParse(_scoreInput, out score);

			Leaderboard.Record(_nameInput, score);

			// Reset for next input.
			_nameInput = "";
			_scoreInput = "0";
			SceneManager.LoadScene(2, LoadSceneMode.Single);

		}

	}
}
