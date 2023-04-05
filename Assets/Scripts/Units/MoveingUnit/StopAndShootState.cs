using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StopAndShootState", menuName = "MyObjectsInfo/StopAndShootState")]
public class StopAndShootState : UnitState
{
    private Vector3 _pos;
    public UnitState nextState;
    float contdown;
    public override void Init(Vector3 pos)
    {
        this._pos = pos;
        unit.animator.SetBool("IsShoot", true);
        contdown = 0f;
    }
    public override void Run()
    {
        RaycastHit2D hit = Physics2D.Raycast((Vector2)(_pos), Vector2.right, unit.range, LayerMask.GetMask("Enemy"));
      
        if (hit.collider != null)
        {
            if (contdown <= 0f)
            {
                unit.SetTarget(hit.transform);
                unit.animator.SetBool("IsShoot", true);
                unit.animator.SetBool("IsIdle", false);
                contdown = unit.fireRate;
            }
            else
            {
                unit.animator.SetBool("IsIdle", true);
                unit.animator.SetBool("IsShoot", false);
            }
            contdown = Mathf.Clamp(contdown - Time.fixedDeltaTime, 0f, float.MaxValue);
            //unit.Shoot();
        } 
        else
        {
            unit.animator.SetBool("IsIdle", false);
            unit.animator.SetBool("IsShoot", false);
            IsFinished = true;
        }
    }
    public override UnitState GetNextState()
    {
        return nextState;
    }
}
