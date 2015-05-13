using UnityEngine;
using System.Collections;

public class ComponentEnergy : MonoBehaviour {

    public float maxEnergy = 100;

    protected float currEnergy;

    // Getter
    public float CurrEnergy { get { return currEnergy; } }
    public float MaxEnergy { get { return maxEnergy; } }
    public float FractionEnergy { get { return currEnergy / maxEnergy; } }


    void Start()
    {
        currEnergy = maxEnergy;
    }



    public void Modify(float amount)
    {
        //UIFloatingText.current.Show(transform.position, amount+"");
        currEnergy += amount;

        if (currEnergy > maxEnergy)
        {
            currEnergy = maxEnergy;
        }
        else if (currEnergy <= 0)
        {
            currEnergy = 0;
        }
    }




    public void Set(float amount)
    {
        currEnergy = amount;
        Modify(0); // run thru the checks in Modify()
    }


}
