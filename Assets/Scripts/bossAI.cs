using UnityEngine;
using System.Collections;

public class bossAI : MonoBehaviour {
	GameObject GOprojectile;
	GameObject GOrain;
	GameObject GOspike;

	// Shooting
	bool isShooting;
	public float maxShootCD;
	float shootCD;
	public float maxShootInterval;
	float shootInterval;
	public float speed;
	float angle;
	float randomAngle;
	bool generateRandom;
    bool stopRain = false;

	// Rain
	public float maxRainTime; // 30 seconds
	float rainTime;
	public float maxRainBreakTime; // 15 seconds
	float rainBreakTime;

	public float maxRainInterval; // 4 seconds
	float rainInterval;
	public float maxRainBreakInterval; // 6 seconds
	float rainBreakInterval;
	int fruitCount;
	bool isRainTime;
	bool isRain;

	// Spike
	public float maxSpikeInterval;
	float spikeInterval;
	public float maxSpikeDuration;
	float spikeDuration;
	bool isSpike;

	// Use this for initialization
	void Start () {
		shootCD = maxShootCD;
		shootInterval = maxShootInterval;
		angle = 0;

		spikeInterval = maxSpikeInterval;
		spikeDuration = maxSpikeDuration;

		rainTime = maxRainTime; // Total rain time
		rainBreakTime = maxRainBreakTime; // Break time after the long rain time
		rainInterval = maxRainInterval; // Rain intervals within the rain time
		rainBreakInterval = maxRainBreakInterval; // Break intervals within the rain time
	}

	void RangeAttack() 
	{
		//GOprojectile.GetComponent<Animator>().SetBool("isShooting", true);
		//Debug.Log (shootInterval);
		shootCD -= Time.deltaTime;
		if (shootCD <= 0.0f)
		{
			if (!generateRandom) 
			{
				randomAngle = Random.Range(0, 30);
				generateRandom = true;
			}
			isShooting = true;
		}
		//Debug.Log (angle);
		if (isShooting) //if (Input.GetKeyUp("a"))	
		{
			shootInterval -= Time.deltaTime;
			if (shootInterval <= 0.0f)
			{
				GOprojectile = Instantiate(Resources.Load("projectiles/Prefab/bossProj"), new Vector3(transform.position.x, 
				             		       transform.position.y + 5.0f, -0.1f), Quaternion.identity) as GameObject;
				
				GOprojectile.transform.position = new Vector3(GOprojectile.transform.position.x, GOprojectile.transform.position.y, -0.1f);
				
				GOprojectile.GetComponent<bossProj>().SetDirection(Mathf.Cos(Mathf.PI / 180 * angle * 30 + randomAngle) * speed * Time.deltaTime, 
				                                                   Mathf.Sin(Mathf.PI / 180 * angle * 30 + randomAngle) * speed * Time.deltaTime);
				
				shootInterval = maxShootInterval;
				angle++;
			}
		}
		
		if (angle > 11)
		{
			angle = 0;
			isShooting = false;
			shootCD = maxShootCD;
			generateRandom = false;
		}
	}

	void RainEvent()
	{
		rainBreakTime -= Time.deltaTime; // 15 --> 0
		if (rainBreakTime <= 0.0f && !isRainTime)
		{
			// Start raining
			isRainTime = true;
			rainTime = maxRainTime;
		}
		if (isRainTime)
		{
			if (!isRain)
			{
				rainBreakInterval -= Time.deltaTime; // 6 --> 0
				if (rainBreakInterval <= 0.0f) // Start raining again
				{	
					//Debug.Log("START RAINING");
					isRain = true;
					GOrain = Instantiate(Resources.Load("env/Prefab/rain")) as GameObject;
					rainInterval = maxRainInterval;
				}
			}
			else
			{
				rainInterval -= Time.deltaTime; // 4 --> 0
				if (rainInterval <= 0.0f) 
				{	// Interval stop rain

					//Debug.Log("rainInterval: " + rainInterval);
					isRain = false;
					rainBreakInterval = maxRainBreakInterval;
					if (GOrain != null)
					{
						Destroy(GOrain);
					}
				}
			}

			rainTime -= Time.deltaTime; //30 ---> 0
			if (rainTime <= 0.0f) // Stop raining
			{
				rainBreakTime = maxRainBreakTime;
				isRainTime = false;
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
				int rand = Random.Range(0, 3);
				float rand2 = 0.0f;

				switch (rand){
				case 0:
					rand2 = Random.Range(-6.76f, 6.17f);
					GOspike.transform.position = new Vector3(rand2, -9.62f, 0.0f);
					break;
				case 1:
					rand2 = Random.Range(8.86f, 11.39f);
					GOspike.transform.position = new Vector3(rand2, -3.69f, 0.0f);
					break;
				case 2:
					rand2 = Random.Range(-11.27f, -8.57f);
					GOspike.transform.position = new Vector3(rand2, -3.69f, 0.0f);
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
    public void SetStopRain(bool rain) {
        stopRain = rain;
        if(rain && GOrain != null) {
            Destroy(GOrain);
        }
    }

	// Update is called once per frame
	void Update () {
		RangeAttack();
        if (!stopRain)
        {
            RainEvent();
        }
		SpawnSpikes();
	}

    public bool GetIsRaining()
    {
        return isRain;
    }
}
