using UnityEngine;
using System.Collections;

public class BGM : MonoBehaviour {

    public AudioClip[] bgm;

	// Use this for initialization
	void Start () {
        
	}

    void PlayRandom(){
        if (audio.isPlaying) {
    	    return;
        }
	    audio.clip = bgm[Random.Range(0, bgm.Length)];
        audio.Play();
    }
	
	// Update is called once per frame
	void Update () {
	    PlayRandom();
	}
}