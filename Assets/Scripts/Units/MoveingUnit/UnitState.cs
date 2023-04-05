using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UnitState : ScriptableObject
{
    public bool IsFinished { get;  set; }
    public MovingUnit unit;
    public virtual void SwitchNextState() { }
    public virtual void Init(Transform currentWaytarget, Vector3 pos) { }
    public virtual void Init(Vector3 pos) { }
    public abstract UnitState GetNextState();
    public abstract void Run();
}
