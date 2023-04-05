using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "UnitMoveState", menuName = "MyObjectsInfo/UnitMoveState")]
public class UnitMoveState : UnitState
{
    private Transform currentWaytarget;
    private Vector3 _pos;
    public UnitState nextState;
    [SerializeField]
    private UnitState _stopAndShotState;
    [SerializeField]
    private UnitState _turretState;
    float tempScale;
    public override void Init(Transform currentWaytarget, Vector3 pos)
    {
        this.currentWaytarget = currentWaytarget;
        _pos = pos;
        IsFinished = false;
        unit.animator.SetBool("IsMove", true);
        
        Vector3 dir = currentWaytarget.position - pos;
        Debug.Log(dir);
        tempScale = unit.transform.localScale.x;
        //unit.transform.localScale = new Vector3(unit.direction * unit.transform.localScale.x, unit.transform.localScale.y, unit.transform.localScale.z);
       // Debug.Log(unit.transform.localScale.x *(dir.x < 0 ? -1f : 1f) + " " + unit.transform.localScale.x);
       // unit.transform.localScale = new Vector3(unit.transform.localScale.x * unit.direction, unit.transform.localScale.y, unit.transform.localScale.z);
        unit.transform.localScale = new Vector3(unit.transform.localScale.x * ((unit.transform.localScale.x * unit.direction < 0)? (dir.x < 0 ? 1f : -1f) : (dir.x < 0 ? -1f : 1f)), unit.transform.localScale.y, unit.transform.localScale.z);
        //unit.transform.localScale.Set(unit.transform.localScale.x * (dir.x < 0? -1f:1f), unit.transform.localScale.y, unit.transform.localScale.z);
       // Debug.Log(unit.transform.localScale.x * (dir.x < 0 ? -1f : 1f) + " " + unit.transform.localScale.x);
        
    }
    private void SendNextPosition()
    {
        float dirX = currentWaytarget.position.x - unit.getPos().x;
        //Vector3 dir = currentWaytarget.position - unit.getPos();
        
        if (Math.Abs(dirX) < 0.1f)
        {
            IsFinished = true;
            nextState = _turretState;
        }
        Vector3 dir = new Vector3(dirX, 0, 0); 
        _pos = unit.MoveTo(dir);
    }
    public override void Run()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)(_pos), Vector2.right, unit.range, LayerMask.GetMask("Enemy"));
        if (hit.collider != null)
        {
            unit.animator.SetBool("IsMove", false);
            IsFinished = true;
            nextState = _stopAndShotState;
            return;
        }
        SendNextPosition();

    }
    public override UnitState GetNextState()
    {
        unit.transform.localScale = new Vector3(tempScale , unit.transform.localScale.y, unit.transform.localScale.z);
        return nextState;
    }
}
