using UnityEngine;
using System.Collections;

public class ComponentHealth : MonoBehaviour
{
    public int maxHP = 10;
	
	protected int currHP;
	
	// Getter
	public int CurrHP {	get	{ return currHP; } }
	public int MaxHP {	get	{ return maxHP; } }
	public float FractionHP { get { return (float)(currHP)/(float)(maxHP); } }

	
	void Start()
	{
		currHP = maxHP;
	}

    
	
	public void Modify(int amount)
	{
        //UIFloatingText.current.Show(transform.position, amount+"");
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

    

	
	public void Set(int amount)
	{
		currHP = amount;
		Modify (0); // run thru the checks in Modify()
	}
		
	public void Die()
	{
		Destroy(this.gameObject);
	}

    public void SetMaxHp(int amt)
    {
        maxHP = amt;
    }

    public void setCurrHp(int amt)
    {
       currHP = amt;
    }
}
