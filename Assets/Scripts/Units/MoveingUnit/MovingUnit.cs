using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingUnit : Unit
{
    public UnitState currentState;
    public UnitState moveState;
    public GameObject currentWayTarget;
    //[HideInInspector]
    public GameObject tempWayTarget;
    public Transform hitTarget;
    public GameObject bulletPrefab;
    public BulletData bulletData;
    public Transform firePoint;
    private Transform _target;
    [HideInInspector]
    public float direction = 1;
    [HideInInspector]
    public Animator animator;
    public MovingUnitData _properties { get; private set; }
    public float speed { get; private set; }
    public float range{ get; private set; }
    public float fireRate { get; private set; }
    public float damage { get; private set; }
    public void Init(MovingUnitData data)
    {
        _properties = data;
        health = data.health;
        speed = data.speed;
        range = data.range;
        damage = data.damage;
        fireRate = data.fireRate;
        direction = data.diraction;
        bulletData = data.bulletInfo;
        animator = gameObject.GetComponent<Animator>();
        animator.runtimeAnimatorController = data.controller;
        Transform trans = gameObject.GetComponent<Transform>();
        trans.localScale = new Vector3(data.scaleX, data.scaleY, 0);
        BoxCollider2D col = gameObject.GetComponent<BoxCollider2D>();
        col.size = new Vector2(data.colSizeX, data.colSizeY);
        col.offset = new Vector2(data.colOffsetX, data.colOffsetY);
        gameObject.transform.Find("FirePoint").transform.localPosition = new Vector3(data.firePointX,data.firePointY,gameObject.transform.position.z);
        gameObject.tag = data.Tag;
    }
    private void Start()
    {
        SetStay(currentState);
        tempWayTarget = null;
    }
    void FixedUpdate()
    {
        
        if (!currentState.IsFinished)
        {
            currentState.Run();
        }
        else
        {
            SetStay(currentState.GetNextState()); 
        }
    }
    public Vector3 MoveTo(Vector3 pos)
    {

        transform.Translate(pos.normalized * speed, Space.World);
        return gameObject.transform.position;
    }
    public Vector3 getPos()
    {
        return gameObject.transform.position;
    }
    public void SetStay(UnitState state)
    {
        animator.SetBool("IsMove", false);
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsShoot", false);
        currentState = Instantiate(state);
        currentState.unit = this;
        currentState.Init(currentWayTarget.transform, gameObject.transform.position);
        currentState.Init(transform.position);
    }
    public void Shoot()
    {
        GameObject bulletGO = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        /*if (bullet != null && _target != null)
        {
            bullet.Init(range, bulletData);
        }*/
        // bullet.Init(range, bulletData);
        bullet.Seek(_target, bulletData, damage);
    }
    private void EndShooting()
    {
        animator.SetBool("IsShoot", false);
    }
  
    void vanish()
    {
        currentWayTarget.SetActive(true);
        Destroy(gameObject);
    }
    public void SetTarget(Transform target)
    {
        _target = target;
    }
    void OnMouseDown()
    {
        AudioManager.instance.Play("select");
        PlayerStats.singleton.StartFlashingSelectionUnit(this);
        BuildManager.instance.buildMode = false;
        BuildManager.instance.ActiveAllNode();
        CurrentUnitStats.instance.SelectCurentUnit(gameObject.tag,fireRate.ToString().ToString(),range.ToString(),damage.ToString(),(speed * 100).ToString(),health.ToString());
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "WayPoint" && tempWayTarget != null)
        {
            //currentState.GetNextState();
            gameObject.transform.position = tempWayTarget.GetComponent<Node>().spawnPoint.transform.position + _properties.spawnOffsetY * Vector3.up;
            transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
            currentWayTarget = tempWayTarget;
            tempWayTarget = null;
            SetStay(moveState);
        }
    }
    public override void Buff(Buff buff)
    {
        health += health * buff.GetHealthBuff() * 0.01f;
        range += range * buff.GetRangeBuff() * 0.01f;
        fireRate -= fireRate * buff.GetTimeRecharge() * 0.01f;
        damage += damage * buff.GetDamageBuff() * 0.01f;
        speed += speed * buff.GetSpeedBuff() * 0.01f;
    }
    public override void Die()
    {
        if(PlayerStats.singleton.selectedUnit == this)
        {
            PlayerStats.singleton.selectedUnit = null;
            BuildManager.instance.OffUnitNodesForBuild();
        }
        BuffManager.instance.RemoveUnit(this);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        animator.SetBool("IsIdle", false);
        animator.SetBool("IsShoot", false);
        animator.SetBool("IsMove", false);
        animator.SetBool("IsDead", true);
        Invoke("vanish", 3f);
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        this.enabled = false;
    }
}
