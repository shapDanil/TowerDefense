using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CostUp : MonoBehaviour
{
	public int costAmmunition;
	public int costMaterials;
	public int costRecruts;
	public float runeTime;

	public void TryBuffIncomAmmunition()
	{
		if(!PlayerStats.singleton.BuffIncomAmmunition(costAmmunition, costMaterials, costRecruts))
        {
			gameObject.SetActive(false);
			Tooltip.singelton.HideToolTip();
		}
			
	}
	public void TryBuffIncomMaterials()
	{
		if (!PlayerStats.singleton.BuffIncomMaterials(costAmmunition, costMaterials, costRecruts))
        {
			gameObject.SetActive(false);
			Tooltip.singelton.HideToolTip();
        }

    }
    public void TryBuffIncomRecruits()
    {
        if (!PlayerStats.singleton.BuffIncomRecruits(costAmmunition, costMaterials, costRecruts))
        {
            gameObject.SetActive(false);
            Tooltip.singelton.HideToolTip();
        }
    }

    public void TryBuffCapacityAmmunition()
    {
        if (!PlayerStats.singleton.BuffCapacityAmmunition(costAmmunition, costMaterials, costRecruts))
        {
            gameObject.SetActive(false);
            Tooltip.singelton.HideToolTip();
        }
    }

    public void TryBuffCapacityMaterials()
    {
        if (!PlayerStats.singleton.BuffCapacityMaterials(costAmmunition, costMaterials, costRecruts))
        {
            gameObject.SetActive(false);
            Tooltip.singelton.HideToolTip();
        }
    }

    public void TryBuffCapacityRecruits()
    {
        if (!PlayerStats.singleton.BuffCapacityRecruits(costAmmunition, costMaterials, costRecruts))
        {
            gameObject.SetActive(false);
            Tooltip.singelton.HideToolTip();
        }
    }


}
