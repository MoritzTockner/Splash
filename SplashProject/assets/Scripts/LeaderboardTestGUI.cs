using UnityEngine;
using UnityEngine.SceneManagement;
public class LeaderboardTestGUI : MonoBehaviour {



	private void OnGUI() {
		GUILayout.BeginArea(new Rect(Screen.width / 2 - 40, 0, Screen.width, Screen.height));
		GUILayout.Space (50f);
		// Display high scores!
		GUILayout.Label("HIGHSCORES");
		for (int i = 0; i < Leaderboard.EntryCount; ++i) {
			var entry = Leaderboard.GetEntry(i);
			GUILayout.Label(entry.name + "\t" + entry.score + "cm");
		}

		GUILayout.EndArea();
		if(GUI.Button(new Rect(Screen.width / 2 - 30, 350, 60, 30), "Retry?")) {
			SceneManager.LoadScene(0, LoadSceneMode.Single);
		}

//		if(GUI.Button(new Rect(Screen.width / 2 - 60, 400, 120, 30), "Clear Highscores")) {
//			Leaderboard.Clear ();
//		}

	}
}