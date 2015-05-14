using UnityEngine;
using System.Collections;

public class MantisSlashScript : MonoBehaviour {
    public int slashDmg = 5;
    public float m_cooldownTimer = 0.2f;
    public float dmgMult = 2f;
    private bool onCooldown = false;
    private float m_timeStamp;
    private Animator anim;
    public AudioClip[] sfx;

    void Awake() {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        audio.clip = sfx[0];
        m_timeStamp = Time.time + m_cooldownTimer;
    }

    void Update()
    {
        if (onCooldown && Time.time >= m_timeStamp + m_cooldownTimer)
        {
            onCooldown = false;
        }
        if (Input.GetButtonDown("Fire1P2") && !onCooldown)
        {
            anim.Play("Mantis_attack", -1, 0);
            audio.Play();
            
        }
    }

    void StartCooldown()
    {
        onCooldown = true;
        m_timeStamp = Time.time;
    }

	// Update is called once per frame
	void OnTriggerStay2D (Collider2D other) {
	    if(Input.GetButtonDown("Fire1P2") && other.collider2D.tag != "Ground" && !onCooldown) {
            StartCooldown();

            ComponentHealth enemyHp = (other.name == "headshot") ? other.GetComponentInParent<ComponentHealth>() :
            other.gameObject.GetComponent<ComponentHealth>();
            if (enemyHp != null)
            {
                if (other.name == "headshot")
                {
                    enemyHp.Modify(-slashDmg * dmgMult);
                }
                else
                {
                    enemyHp.Modify(-slashDmg);

                }

            }
            
        }
	}
}
