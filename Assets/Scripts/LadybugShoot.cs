using UnityEngine;
using System.Collections;

public class LadybugShoot : MonoBehaviour {

    private bool onCooldown = false;
    private ComponentCooldown cd;
    public ObjectPoolerScript bulletPool;
    [SerializeField]
    protected int m_dmg = 1;

    void Awake()
    {
        cd = GetComponent<ComponentCooldown>();
    }
    // Update is called once per frame
    void Update()
    {
        // FIRE!
        if (Input.GetButton("Fire1") )
        {
            FireBullet();

        }

    }

    public void FireBullet()
    {
        if (!onCooldown)
        {
            Rigidbody2D bulletRb;
            GameObject bulletObj = bulletPool.GetPooledObject();
            if (bulletObj != null)
            {
                bulletRb = (Rigidbody2D)bulletObj.GetComponent<Rigidbody2D>();
                bulletRb.transform.position = transform.position;
                bulletObj.SetActive(true);
                SetCooldown(true);
                cd.StartCooldown();
            }
        }
    }

    public void SetCooldown(bool cd)
    {
        onCooldown = cd;
    }


    public void increaseBulletDamage(int amt)
    {
        m_dmg += amt;
    }

    public int GetBulletDmg() { return m_dmg; }
}
