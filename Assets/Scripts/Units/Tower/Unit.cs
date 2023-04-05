using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	protected float health { set; get; }

	protected GameObject _node;
	public void TakeDamage(float damage)
	{
		health -= damage;
		if (health <= 0)
		{

			Die();
		}
	}
	// Умереть
    public virtual void Die()
	{
		BuffManager.instance.RemoveUnit(this);
		Destroy(gameObject);
	}
	public virtual void Buff(Buff buff)
	{
		health += buff.GetHealthBuff();
	}
	public void SetNode(GameObject node)
	{
		_node = node;
	}
}
