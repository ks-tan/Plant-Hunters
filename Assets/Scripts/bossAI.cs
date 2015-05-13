using UnityEngine;
using System.Collections;

public class bossAI : MonoBehaviour {
	GameObject GOprojectile;
	GameObject GOrain;
	bool isShooting;
	public float maxShootCD;
	float shootCD;
	public float maxShootInterval;
	float shootInterval;
	public float speed;
	float angle;

	public float maxRainInterval;
	float rainInterval;
	bool isRain;

	
	// Use this for initialization
	void Start () {
		shootCD = maxShootCD;
		shootInterval = maxShootInterval;
		angle = 0;
	}

	void RangeAttack() 
	{
		//GOprojectile.GetComponent<Animator>().SetBool("isShooting", true);
		//Debug.Log (shootInterval);
		shootCD -= Time.deltaTime;
		if (shootCD <= 0.0f)
		{
			isShooting = true;
		}
		//Debug.Log (angle);
		if (isShooting) //if (Input.GetKeyUp("a"))
		{
			shootInterval -= Time.deltaTime;
			if (shootInterval <= 0.0f)
			{
				GOprojectile = Instantiate(Resources.Load("projectiles/Prefab/bossProj"), new Vector3(transform.position.x, 
				             		       transform.position.y + 2.0f, -0.1f), Quaternion.identity) as GameObject;
				
				GOprojectile.GetComponent<Animator>().speed = 0.3f;
				GOprojectile.transform.position = new Vector3(GOprojectile.transform.position.x, GOprojectile.transform.position.y, -0.1f);
				
				//int angle = Random.Range(0, 11) * 30;
				
				GOprojectile.GetComponent<bossProj>().SetDirection(Mathf.Cos(Mathf.PI / 180 * angle * 30) * speed * Time.deltaTime, 
				                                                 Mathf.Sin(Mathf.PI / 180 * angle * 30) * speed * Time.deltaTime);
				
				shootInterval = maxShootInterval;
				angle++;
			}
		}
		
		if (angle > 11)
		{
			angle = 0;
			isShooting = false;
			shootCD = maxShootCD;
		}
	}

	void RainEvent()
	{
		/*rainInterval -= Time.deltaTime;
		if (rainInterval < maxRainInterval)
		{
			isRain = true;
		}
		
		GOrain = Instantiate(Resources.Load("env/Prefab/rain")) as GameObject;*/
	}

	// Update is called once per frame
	void Update () {
		RangeAttack();
		RainEvent();
	}
}
