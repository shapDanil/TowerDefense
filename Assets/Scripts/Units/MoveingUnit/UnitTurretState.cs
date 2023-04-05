using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "UnitTurretState", menuName = "MyObjectsInfo/UnitTurretState")]
public class UnitTurretState : UnitState
{
    [SerializeField]
    private UnitState _moveState;
    private Vector3 _pos;
    float contdown;
    public override void Init(Vector3 pos)
    {
        this._pos = pos;
        contdown = 0f;
        unit.animator.SetBool("IsIdle", true);
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
                contdown =  unit.fireRate;
            }
            else
            {
                unit.animator.SetBool("IsIdle", true);
                unit.animator.SetBool("IsShoot", false);
            }
        }
        else
        {
            unit.animator.SetBool("IsIdle", true);
            unit.animator.SetBool("IsShoot", false);
        }
        contdown = Mathf.Clamp(contdown - Time.fixedDeltaTime, 0f, float.MaxValue);
    }
    public override UnitState GetNextState()
    {
        return _moveState;
    }

}

