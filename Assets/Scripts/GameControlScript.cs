using UnityEngine;
using System.Collections;

public class GameControlScript : MonoBehaviour {
    public ComponentHealth bugHp;
    public ComponentHealth mantisHp;
    public bossAI boss;
    public int rainDmg = 1;
    public static GameControlScript current;
    private FruitSpawnScript fruitSpwn;


    void Awake()
    {
        fruitSpwn = GetComponent<FruitSpawnScript>();
        current = this;
    }
	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        if(boss.GetIsRaining()){
            if(bugHp != null) {
                bugHp.Modify(-rainDmg * Time.deltaTime);
            }

            if(mantisHp != null) {
                mantisHp.Modify((-rainDmg * Time.deltaTime));
            }
            if(fruitSpwn.AllDestroyed()) {
                boss.SetStopRain(true);
            }
            else
            {
                boss.SetStopRain(false);
            }
            
        }
	}
}
