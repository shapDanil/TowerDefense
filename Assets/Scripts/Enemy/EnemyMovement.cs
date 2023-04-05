using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
	public Transform target;
	public float _speed;
	public int _cityDamage;
	private int wavepointIndex = 0;
	public bool isMove = true;

	private Enemy enemy;

	void Start()
	{
		//target = GameObject.Find("EnemyWaypoint").transform.GetComponent<>;
	}
	void FixedUpdate()
	{
		Vector3 dir = target.position - transform.position;
		if(isMove)
			transform.Translate(dir.normalized * _speed, Space.World);
	}
	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.tag == "Finish")
		{
			PlayerStats.singleton.SubtractLives(_cityDamage);
			Destroy(gameObject);
		}
	}
	public void setTarget(Transform target)
    {
		this.target = target;
    }
	
}