using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundMusic : MonoBehaviour {

	// Use this for initialization
	private static SoundMusic instance;

	// Update is called once per frame
	private void Awake () {
		if (instance != null)
			Destroy (gameObject);
		else {
			instance = this;
			DontDestroyOnLoad(transform.gameObject);
		}
	}
}
