using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneManager : MonoBehaviour {

	public void LoadScene(string levelName){
		Application.LoadLevel(levelName);
	}

	public void quit() {
		Application.Quit();
	}
}
