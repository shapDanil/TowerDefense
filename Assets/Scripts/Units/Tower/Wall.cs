using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : Unit
{
	public float startHealth;

	void Start()
	{
		health = startHealth;
	}
	void OnMouseDown()
	{
		PlayerStats.singleton.selectedUnit = null;
		BuildManager.instance.OffUnitNodesForBuild();
		CurrentUnitStats.instance.SelectCurentUnit(gameObject.tag, "0", "0", "0", "0", health.ToString());
	}
    public override void Die()
    {
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetBool("IsDeath", true);
    }
    void vanish()
    {
        Destroy(gameObject);
    }
}
