using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
	public EnemyData _properties;
	[SerializeField]
	private float _health;
	[SerializeField]
	private float _unitDamage;
	
	private Unit _attackTarget;
	bool isEndAttack;
	private Animator _animator;
	public void Init(EnemyData data)
    {
		_properties = data;
		_health = _properties.health;
		 GetComponent<EnemyMovement>()._speed = _properties.speed;
		 GetComponent<EnemyMovement>()._cityDamage = _properties.cityDamage;
		_unitDamage = _properties.unitDamage;
		_animator = gameObject.GetComponent<Animator>();
		_animator.runtimeAnimatorController = _properties.controller;
		Transform trans = gameObject.GetComponent<Transform>();
		trans.localScale = new Vector3(_properties.scaleX, _properties.scaleY, 0);
		BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
		col.size = new Vector2(_properties.colSizeX,_properties.colSizeY);
		col.offset = new Vector2(_properties.colOffsetX, _properties.colOffsetY);
	}

	public void TakeDamage(float amount)
	{
		_health -= amount;
		if (_health <= 0)
		{
			Die();
		}
	}
	void vanish()
	{
		//GameObject effect = (GameObject)Instantiate(deathEffect, transform.position, Quaternion.identity);
		//Destroy(effect, 5f);
		WaveSpawner.singleton.enemyAlive--;
		Destroy(gameObject);

	}
	void vanishWithRate(float rate)
    {
		Invoke("vanish", rate);
		gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		gameObject.layer = 0;
	}
	void Die()
	{
		_animator.SetBool("isDead", true);
		EnemyMovement enemyMove = gameObject.GetComponent<EnemyMovement>();
		enemyMove._speed = 0;
		//Invoke("vanish", 3f);
	}
	private void damageSomeUnit()
	{
		if(_attackTarget !=null)
			_attackTarget.TakeDamage(_unitDamage);
	}
	private void endAttack()
    {
		bool temp = (true && isEndAttack);
		gameObject.GetComponent<EnemyMovement>().isMove = temp;
		_animator.SetBool("isAttack", !temp);

	}
	void OnCollisionEnter2D(Collision2D collision)
	{
		if (collision.gameObject.layer == 3 || collision.gameObject.layer == 7)
		{
			isEndAttack = false;
			_animator.SetBool("isAttack", true);
			_attackTarget = collision.gameObject.GetComponent<Unit>();
			gameObject.GetComponent<EnemyMovement>().isMove = false;
		}
	}
	void OnCollisionExit2D(Collision2D collision)
	{
		//Debug.Log("tower is dead!!!");
		if (collision.gameObject.layer == 3 || collision.gameObject.layer == 7)
		{
			_attackTarget = null;
			
			isEndAttack = true;
			//gameObject.GetComponent<EnemyMovement>().speed = this.startSpeed;
		}
	}


}
