using UnityEngine;
using System.Collections;

public class MantisSlashScript : MonoBehaviour {
    private Collider2D slashBox;
    public int slashDmg = 5;
    public float m_cooldownTimer = 0.2f;
    private bool onCooldown = false;
    private float m_timeStamp;
    void Awake()
    {
        slashBox = GameObject.Find("MantisSlash").GetComponent<Collider2D>();
    }

    void Start()
    {
        m_timeStamp = Time.time + m_cooldownTimer;
    }

    void Update()
    {
        if (onCooldown && Time.time >= m_timeStamp + m_cooldownTimer)
        {
            onCooldown = false;
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
            ComponentHealth enemyHp = other.gameObject.GetComponent<ComponentHealth>();
            if (enemyHp != null)
            {
                enemyHp.Modify(-slashDmg);
            }
            StartCooldown();
        }
	}
}
