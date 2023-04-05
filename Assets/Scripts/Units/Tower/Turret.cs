using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Turret : Unit
{
	
	private Transform target;

	private TurretData _properties;
	[Header("Use Bullets (default)")]
	public GameObject bulletPrefab;
	private BulletData bulletData;
	[HideInInspector]
	public float range = 0;
	[HideInInspector]
	public float fireRate = 1f;
	[HideInInspector]
	public float damage;
	[HideInInspector]
	private float fireCountdown = 0f;
	public Transform firePoint;
	private Animator _animator;
	public void Init(TurretData data)
	{
		_properties = data;
		health = data.health;
		range = data.range;
		fireRate = data.fireRate;
		damage = data.damage;
		_animator = gameObject.GetComponent<Animator>();
		_animator.runtimeAnimatorController = data.controller;
		Transform trans = gameObject.GetComponent<Transform>();
		trans.localScale = new Vector3(data.scaleX, data.scaleY, 0);
		BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
		col.size = new Vector2(data.colSizeX, data.colSizeY);
		col.offset = new Vector2(data.colOffsetX, data.colOffsetY);
		gameObject.transform.Find("firePoint").transform.localPosition = new Vector3(data.firePointX, data.firePointY, gameObject.transform.position.z);
		gameObject.tag = data.Tag;
		bulletData = data.bulletInfo;
	}
	void UpdateTargetAndFire()
	{
		RaycastHit2D hit = Physics2D.Raycast((Vector2)(this.transform.position), Vector2.right, range, LayerMask.GetMask("Enemy"));
		if (hit.collider != null)
		{
			target = hit.transform;
			_animator.SetBool("IsAttack", true);
		}
		
	}
	private void EndShooting()
    {
		_animator.SetBool("IsAttack", false);
	}
	private void Shoot()
	{
		GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
		Bullet bullet = bulletGO.GetComponent<Bullet>();
		bullet.Seek(target, bulletData, damage);
	}
    public override void Die()
    {
		BuffManager.instance.RemoveUnit(this);
		gameObject.GetComponent<BoxCollider2D>().enabled = false;
		_animator.SetBool("IsDead", true);
		if(gameObject.tag == "Tower")
			Invoke("vanish", 3f);
		this.enabled = false;
	}
	void FixedUpdate()
    {
		if (fireCountdown <= 0f)
        {
			UpdateTargetAndFire();
			fireCountdown = fireRate;
		}
		fireCountdown = Mathf.Clamp(fireCountdown - Time.fixedDeltaTime, 0f, float.MaxValue);
	}

	void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.red;
		Gizmos.DrawWireSphere(transform.position, range);
	}
	void vanish()
	{
		_node.SetActive(true);
		Destroy(gameObject);
	}
    public override void Buff(Buff buff)
    {
		health += health * buff.GetHealthBuff() * 0.01f; 
		range += range * buff.GetRangeBuff() * 0.01f; 
		fireRate -= fireRate * buff.GetTimeRecharge() * 0.01f;
		damage += damage * buff.GetDamageBuff() * 0.01f;
	}
	void OnMouseDown()
	{
		PlayerStats.singleton.selectedUnit = null;
		BuildManager.instance.OffUnitNodesForBuild();
		CurrentUnitStats.instance.SelectCurentUnit(gameObject.tag, fireRate.ToString().ToString(), range.ToString(), damage.ToString(), "0", health.ToString());
	}

}
