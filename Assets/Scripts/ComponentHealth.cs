using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour
{
    public float maxHP = 10;
    public bool isInvul = false;
    public float currHP;
	
	// Getter
    public float CurrHP { get { return currHP; } }
    public float MaxHP { get { return maxHP; } }
	public float FractionHP { get { return (float)(currHP)/(float)(maxHP); } }

	
	void Start()
	{
		currHP = maxHP;
	}



    public void Modify(float amount)
	{
        
        
        if(!isInvul) {
            if(Mathf.Abs(amount)>=1) {
                Color c = tag == "Enemy"? Color.red : Color.magenta;
                UIFloatingText.current.Show(transform.position, amount + "", c);

                if (name == "mantis")
                {
                    GetComponent<Animator>().Play("Mantis_kenahit", -1, 0);
                }

                if (name == "ladybug")
                {
                    GetComponent<Animator>().Play("Ladybug_kenahit", -1, 0);
                }
            }
            
		    currHP += amount;
		
		    if (currHP > maxHP)
		    {
			    currHP = maxHP;
		    }
		    else if (currHP <= 0)
		    {
			    Die();
		    }
        }
	}


    public void SetInvul(bool invul)
    {
        isInvul = invul;
    }

    public void Set(float amount)
	{
		currHP = amount;
		Modify (0); // run thru the checks in Modify()
	}
		
	public void Die()
	{
		Destroy(this.gameObject);
	}

    public void SetMaxHp(float amt)
    {
        maxHP = amt;
    }

    public void setCurrHp(float amt)
    {
       currHP = amt;
    }
}
