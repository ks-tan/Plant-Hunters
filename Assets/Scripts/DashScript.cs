using UnityEngine;
using System.Collections;

public class DashScript : MonoBehaviour {
    public int dashForce = 20;
    public float depletionSpd = 0.5f;
    public float regenSpd = 0.3f;
    private bool isDashing = false;
    private ComponentEnergy energy;
    private Player2Control playerCont;
    private Rigidbody2D rb;

    void Awake()
    {
        energy = GetComponent<ComponentEnergy>();
        rb = GetComponent<Rigidbody2D>();
        playerCont = GetComponent<Player2Control>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2P2") && energy.CurrEnergy > 10)
        {
            isDashing = true;
        }
        if (Input.GetButtonUp("Fire2P2"))
        {
            isDashing = false;
        }

        if (isDashing)
        {
            energy.Modify(-(depletionSpd));
            if(energy.CurrEnergy <= 1) {
                isDashing = false;
            }
        }
        else
        {
            energy.Modify((regenSpd));
        }
	}
    void FixedUpdate()
    {
        if (isDashing)
        {
            if(playerCont.IsFacingRight()) {
                rb.AddForce(new Vector2(dashForce,0));
            }
            else
            {
                rb.AddForce(new Vector2(-dashForce, 0));
            }
        }
    }

}
