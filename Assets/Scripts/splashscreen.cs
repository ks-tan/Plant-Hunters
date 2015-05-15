using UnityEngine;
using System.Collections;

public class splashscreen : MonoBehaviour {
    public AudioClip[] sfx;

	// Use this for initialization
	void Start () {
        audio.clip = sfx[0];
        audio.Play();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetMouseButtonDown(0))
        {
            Application.LoadLevel("Main Scene2");
        }
	}
}
