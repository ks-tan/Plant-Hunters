using UnityEngine;
using System.Collections;

public class bossProj : MonoBehaviour {
	private float dirX;
	private float dirY;
    private Animator anim;
    public int projDmg = 2;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }

    void Start()
    {
        anim.Play("BossProj_leaf",-1,0);
    }
	public void SetDirection(float _dirX, float _dirY)
	{
		dirX = _dirX;
		dirY = _dirY;
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player") {
            ComponentHealth hp = other.GetComponent<ComponentHealth>();
            hp.Modify(-projDmg);
            Destroy(gameObject);
        }
    }
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(dirX, dirY, 0.0f);

		//Debug.Log(transform.position.x + " " + transform.position.y);
		if (transform.position.x < -50.0f || transform.position.x > 50.0f ||
		    transform.position.y < -50.0f || transform.position.y > 50.0f)
		{
			//Debug.Log("DestROYED");
			Destroy(gameObject);
		}
	}
}
