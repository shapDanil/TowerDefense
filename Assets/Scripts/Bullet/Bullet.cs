using UnityEngine;

public class Bullet : MonoBehaviour {
	private Transform target;
	//private Vector3 target;
	private BulletData _bulletInfo;
	public float speed;

	public float damage;

	public float explosionRadius = 0f;
	public GameObject impactEffect;
	float _range;
	Vector3 startPosition;
	public void Seek(Transform _target, BulletData bulletInfo,float damage)
	{
		startPosition = transform.position;
		target = _target;
		this.damage = damage;
		speed = bulletInfo.speed;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletInfo.controller;
		Transform trans = gameObject.GetComponent<Transform>();
		trans.localScale = new Vector3(bulletInfo.scaleX, bulletInfo.scaleY, 0);
		BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
		col.size = new Vector2(bulletInfo.colSizeX, bulletInfo.colSizeY);
		col.offset = new Vector2(bulletInfo.colOffsetX, bulletInfo.colOffsetY);
	}
	/*public void Init (float range, BulletData bulletInfo)
	{
		_bulletInfo = bulletInfo;
		//target = _target;
		*//*target = transform.position;
		target  += Vector3.right * range;*//*
		damage = bulletInfo.damage;
		speed = bulletInfo.speed;
		gameObject.GetComponent<Animator>().runtimeAnimatorController = bulletInfo.controller;
		Transform trans = gameObject.GetComponent<Transform>();
		trans.localScale = new Vector3(bulletInfo.scaleX, bulletInfo.scaleY, 0);
		BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
		col.size = new Vector2(bulletInfo.colSizeX, bulletInfo.colSizeY);
		col.offset = new Vector2(bulletInfo.colOffsetX, bulletInfo.colOffsetY);
	}*/
	void Update()
	{
		if (target == null)
		{
			RaycastHit2D hit = Physics2D.Raycast((Vector2)(this.transform.position), Vector2.right, 2400f, LayerMask.GetMask("Enemy"));
			if (hit.collider != null)
			{
				target = hit.transform;
			}
			else
			{
				Destroy(gameObject);
				//target = transform.position + 5f * Vector3.right;
				return;
			}

		}

		Vector2 dir = target.position - transform.position;
		float distanceThisFrame = speed * Time.deltaTime;

		if (dir.x <= distanceThisFrame)
		{
			HitTarget();
			return;
		}

		transform.Translate(Vector2.right * distanceThisFrame, Space.World);
		transform.LookAt(target);
	}
	// Update is called once per frame
	/* void Update () {


		 Vector3 dir = target - transform.position;
		 if (dir.magnitude <= 0.1f)
		 {
			 vanish();
			 return;
		 }
		 transform.Translate(Vector2.right * speed * Time.deltaTime, Space.World);
		 //transform.LookAt(target);
	 }*/

	void HitTarget ()
	{
		Damage(target);
		Destroy(gameObject);
	}


	void Damage (Transform enemy)
	{
		Enemy e = enemy.GetComponent<Enemy>();

		if (e != null)
		{
			e.TakeDamage(damage);
		}
	}
	void vanish()
    {
		Destroy(gameObject);
	}
	/*private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Enemy")
		{
			Debug.Log("aaaaffff");
			collision.GetComponent<Enemy>().TakeDamage(damage);
			Destroy(gameObject);
		}
	}*/
}
