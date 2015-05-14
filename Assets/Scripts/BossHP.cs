using UnityEngine;
using System.Collections;

public class BossHP : MonoBehaviour {
    GameObject HPBar;
	GameObject Boss;
    Vector3[] vertices;
    Vector2[] uv;
    GameObject Cam;
    float MAX_LEFT;
    float MAX_RIGHT;
    float width;

	// Use this for initialization
	void Start () {
        HPBar = GameObject.Find("BossHP");
		Boss = GameObject.Find("BossPlant");
        vertices = HPBar.GetComponent<MeshFilter>().mesh.vertices;
        uv = HPBar.GetComponent<MeshFilter>().mesh.uv;
        Cam = GameObject.FindGameObjectWithTag("UICam");

        transform.position = new Vector3(transform.position.x * Cam.camera.aspect, transform.position.y, transform.position.z);
        transform.localScale = new Vector3(transform.localScale.x * Cam.camera.aspect, transform.localScale.y, transform.localScale.z);

        MAX_LEFT = vertices[0].x;
        MAX_RIGHT = vertices[1].x;

        width = Mathf.Abs(MAX_RIGHT - MAX_LEFT);
	}
	
	// Update is called once per frame
	void Update () {
		vertices[1].x = MAX_LEFT - width * Boss.GetComponent<ComponentHealth>().FractionHP;
		vertices[2].x = MAX_LEFT - width * Boss.GetComponent<ComponentHealth>().FractionHP;
        
		uv[1].x = Boss.GetComponent<ComponentHealth>().FractionHP;
		uv[2].x = Boss.GetComponent<ComponentHealth>().FractionHP;

        GetComponent<MeshFilter>().mesh.vertices = vertices;
        GetComponent<MeshFilter>().mesh.uv = uv;
	}
}