using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {
    public bool isShielding = false;
    public float depletionSpd = 0.5f;
    public float regenSpd = 0.3f;
    private Collider2D shieldCheck;
    private ComponentEnergy energy;
    private Animator shieldAnim;
    private SpriteRenderer shieldSprite;

    void Awake()
    {
        shieldSprite = GameObject.Find("ladybugshield_open").GetComponent<SpriteRenderer>();
        shieldAnim = GameObject.Find("ladybugshield_open").GetComponent<Animator>();
        energy = GetComponent<ComponentEnergy>();
        shieldCheck = GameObject.Find("ShieldCheck").GetComponent<Collider2D>();
    }
	// Use this for initialization
	void Start () {
        shieldCheck.enabled = false;
        shieldSprite.enabled = false;
	}

    void OnTriggerStay2D(Collider2D other)
    {
        if(other.tag == "EnemyProjectile") {
            GameObject.Destroy(other.gameObject);
        }
    }

	// Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("Fire2") && energy.CurrEnergy > 10) {
            isShielding = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            isShielding = false;
        }
        if (isShielding)
        {
            shieldSprite.enabled = true;
            //shieldAnim.Play("Ladybug_shield", -1, 0);
            shieldCheck.enabled = true;
            energy.Modify(-(depletionSpd ));
            if (energy.CurrEnergy <= 1)
            {
                isShielding = false;
            }
        }
        else
        {

            shieldAnim.Play("ShieldClose", -1, 0 );
            shieldSprite.enabled = false;
            shieldCheck.enabled = false;
            energy.Modify((regenSpd));
        }
    }
}
