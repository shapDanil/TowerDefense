using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  class Buff : MonoBehaviour
{
    [SerializeField]
    private string _heder;
    [SerializeField]
    private string _description;
    [SerializeField]
    private int _costAmmunition;
    [SerializeField]
    private int _costMaterial;
    [SerializeField]
    private string _tag;
    [SerializeField]
    private float _damageBuff;
    [SerializeField]
    private float _healthBuff;
    [SerializeField]
    private float _speedBuff;
    [SerializeField]
    private float _rangeBuff;
    [SerializeField]
    private float _timeRecharge;
    [SerializeField]
    private float _timeRate;
   
    public string GetHeder()
    {
        return _heder;
    }
    public string GetDescription()
    {
        return _description;
    }
    public string GetTag()
    {
        return _tag;
    }
    public float GetDamageBuff()
    {
        return _damageBuff;
    }
    public float GetHealthBuff()
    {
        return _healthBuff;
    }
    public float GetSpeedBuff()
    {
        return _speedBuff;
    }
    public float GetRangeBuff()
    {
        return _rangeBuff;
    }
    public float GetTimeRate()
    {
        return _timeRate;
    }
    public float GetTimeRecharge()
    {
        return _timeRecharge;
    }
    public float GetCostMaterial()
    {
        return _costMaterial;
    }
    public float GetCostAmmunition()
    {
        return _costAmmunition;
    }
    public void TryAddThisBuffInBuffManager()
    {
        if(PlayerStats.singleton.SubtractResurs(_costAmmunition, _costMaterial, 0))
        {
            BuffManager.instance.StartMilitaryBuff(this);
            BuffManager.instance.AddBuff(this);
            TooltipReserche.singelton.HideToolTip();
           // gameObject.SetActive(false);
        }
           
    }
}
