using UnityEngine;
using System.Collections;

public class BugBulletScript : MonoBehaviour {
    private string owner = "ladybug";
    private string player = "Player";
    private PlayerControl playerCont;
    public float bulletLifeTime = 2f;
    public int bulletDmg = 1;


    [SerializeField]
    protected float m_Speed = 20.0f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
        {
            OnCollidePlatform();
        }

        if (other.tag != player)
        {
            OnCollideEnemy(other);
        }

    }

    public void OnCollidePlatform()
    {
        Destroy();
    }

    public void OnCollideEnemy(Collider2D collider)
    {
        ComponentHealth enemyHp = collider.gameObject.GetComponent<ComponentHealth>();
        if (enemyHp != null)
        {
            enemyHp.Modify(-bulletDmg);
        }
        Destroy();
    }

    void Destroy()
    {
        gameObject.SetActive(false);
    }

    void OnEnable()
    {
        
        GameObject ownerObj = GameObject.Find(owner);
        playerCont = ownerObj.GetComponent<PlayerControl>();
        if (ownerObj != null)
        {
            LadybugShoot cs = ownerObj.GetComponent<LadybugShoot>();
            if (cs != null)
            {
                bulletDmg = cs.GetBulletDmg();
            }
            Vector2 bulletDirection = Vector2.right;
            if (!playerCont.IsFacingRight())
            {
                bulletDirection = -Vector2.right;
                Vector3 theScale = transform.localScale;
                theScale.x *= -1;
                transform.localScale = theScale;
            }
            InitBullet(m_Speed, bulletDirection);
            //Physics2D.IgnoreCollision(ownerObj.GetComponent<Collider2D>().collider2D, GetComponent<Collider2D>().collider2D);
            Invoke("Destroy", bulletLifeTime);
        }
    }

    public void InitBullet(float spd, Vector2 dir)
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(spd * dir.x, 0);
    }

    void OnDisable()
    {
        CancelInvoke();
    }
}
