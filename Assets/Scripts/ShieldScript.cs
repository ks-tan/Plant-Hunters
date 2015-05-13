using UnityEngine;
using System.Collections;

public class ShieldScript : MonoBehaviour {
    public bool isShielding = false;
    public float depletionSpd = 0.5f;
    public float regenSpd = 0.3f;
    public int threshold = 30; 
    private ComponentHealth bugHp;
    private Collider2D shieldCheck;
    private Rigidbody2D rb;
    private ComponentEnergy energy;
    private Animator shieldAnim;
    private SpriteRenderer shieldSprite;


    void Awake()
    {
        bugHp = GetComponent<ComponentHealth>();
        rb = GetComponent<Rigidbody2D>();
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
        if (other.tag == "Player")
        {
            GameControlScript.current.mantisHp.SetInvul(true);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GameControlScript.current.mantisHp.SetInvul(false);
        }
    }

	// Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire2") && energy.CurrEnergy > threshold)
        {
            isShielding = true;
        }
        if (Input.GetButtonUp("Fire2"))
        {
            isShielding = false;
        }
        if (isShielding)
        {
            bugHp.SetInvul(true);
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
            GameControlScript.current.mantisHp.SetInvul(false);
            bugHp.SetInvul(false);
            shieldAnim.Play("ShieldClose", -1, 0 );
            shieldSprite.enabled = false;
            shieldCheck.enabled = false;
            energy.Modify((regenSpd));
        }
    }

    void FixedUpdate()
    {
        if(isShielding) {
            rb.velocity = new Vector2(0, 0);
        }
    }
}
