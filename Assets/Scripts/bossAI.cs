using UnityEngine;
using System.Collections;

public class bossAI : MonoBehaviour {
	GameObject GOprojectile;
	GameObject GOrain;
	GameObject GOspike;

	bool isShooting;
	public float maxShootCD;
	float shootCD;
	public float maxShootInterval;
	float shootInterval;
	public float speed;
	float angle;

	public float maxRainInterval;
	float rainingTime;
	float rainInterval;
	bool isRain;

	public float maxSpikeInterval;
	float spikeInterval;
	public float maxSpikeDuration;
	float spikeDuration;
	bool isSpike;

	// Use this for initialization
	void Start () {
		shootCD = maxShootCD;
		shootInterval = maxShootInterval;
		rainInterval = maxRainInterval;
		rainingTime = maxRainInterval;
		spikeInterval = maxSpikeInterval;
		spikeDuration = maxSpikeDuration;
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
		rainInterval -= Time.deltaTime;
		if (isRain){
			rainingTime -= Time.deltaTime;
			if (rainingTime < 0.0f)
			{
				isRain = false;
				rainingTime = rainInterval = maxRainInterval;
				Destroy(GOrain);
			}
		}
		else { // Not raining
			if (rainInterval < 0.0f)
			{
				isRain = true;
				GOrain = Instantiate(Resources.Load("env/Prefab/rain")) as GameObject;
			}
		}
	}

	void SpawnSpikes()
	{
		// Boss HP > 67%, Trigger spike 
		spikeInterval -= Time.deltaTime;
		if (spikeInterval <= 0.0f)
		{
			if (!isSpike) 
			{
				GOspike = Instantiate(Resources.Load("env/Prefab/spikes")) as GameObject;
				int rand = Random.Range(0, 6);
				switch (rand){
				case 0:
					GOspike.transform.position = new Vector3(0.0f, 0.0f, 0.0f);
					break;
				case 1:
					GOspike.transform.position = new Vector3(1.0f, 0.0f, 0.0f);
					break;
				case 2:
					GOspike.transform.position = new Vector3(2.0f, 0.0f, 0.0f);
					break;
				case 3:
					GOspike.transform.position = new Vector3(3.0f, 0.0f, 0.0f);
					break;
				case 4:
					GOspike.transform.position = new Vector3(4.0f, 0.0f, 0.0f);
					break;
				case 5:
					GOspike.transform.position = new Vector3(5.0f, 0.0f, 0.0f);
					break;
				}
				isSpike = true;
			}
			else {
				spikeDuration -= Time.deltaTime;
				if (spikeDuration <= 0.0f)
				{
					isSpike = false;
					spikeDuration = maxSpikeDuration;
					Destroy(GOspike);
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
		RangeAttack();
		RainEvent();
		SpawnSpikes();
	}

    public bool GetIsRaining()
    {
        return isRain;
    }
}
