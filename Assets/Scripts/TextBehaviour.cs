﻿using UnityEngine;
using System.Collections;

public class TextBehaviour : MonoBehaviour {
    private TextMesh text;
    public float fadeSpeed = 1.5f;
    public float textSpeed = 5f;

    void Awake() {
        text = GetComponent<TextMesh>();
    }
	// Update is called once per frame
	void Update () {
        transform.Translate(transform.up * Time.deltaTime * textSpeed);
        text.color = Color.Lerp(text.color, Color.clear, fadeSpeed * Time.deltaTime);
	}
}
