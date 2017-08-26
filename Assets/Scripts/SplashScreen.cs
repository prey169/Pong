using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class SplashScreen : MonoBehaviour {

    public string sceneToLoad;

    public int secTillSceneLoad;

	// Use this for initialization
	void Start () {

        Invoke("OpenNextScene", secTillSceneLoad);

	}
	
	// Update is called once per frame
	void OpenNextScene () {
        SceneManager.LoadScene(sceneToLoad);
	}
}
