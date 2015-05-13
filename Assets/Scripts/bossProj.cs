using UnityEngine;
using System.Collections;

public class bossProj : MonoBehaviour {
	private float dirX;
	private float dirY;

	public void SetDirection(float _dirX, float _dirY)
	{
		dirX = _dirX;
		dirY = _dirY;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(dirX, dirY, 0.0f);

		//Debug.Log(transform.position.x + " " + transform.position.y);
		if (transform.position.x < -12.0f || transform.position.x > 12.0f ||
		    transform.position.y < -6.0f || transform.position.y > 6.0f)
		{
			//Debug.Log("DestROYED");
			Destroy(gameObject);
		}
	}
}
