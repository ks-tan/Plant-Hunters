using UnityEngine;
using System.Collections;

public class FruitSpawnScript : MonoBehaviour {
    public Transform[] spwnpts;
    public GameObject fruit;
    public float spwnInterval = 60f;
    private float time;
    private GameObject[] spwnedFruits;
	// Use this for initialization
	void Start () {
        spwnedFruits = new GameObject[4];
        SpawnFruits();
        time = spwnInterval;
	}
	
	// Update is called once per frame
	void Update () {
        time = time - Time.deltaTime;
        if(time <=0) {
            SpawnFruits();
            time = spwnInterval;
        }
	}

    void SpawnFruits()
    {
        for (int i = 0; i < spwnpts.Length; i++ )
        {
            GameObject obj = Instantiate(fruit, spwnpts[i].position, Quaternion.identity) as GameObject;
            Destroy(obj, spwnInterval);
            spwnedFruits[i] = obj;
        }
    }

    public bool AllDestroyed()
    {

        bool res = true;
        for (int i = 0; i < spwnpts.Length; i++)
        {
            res = res && spwnedFruits[i] == null;
        }
        return res;
    }
}
