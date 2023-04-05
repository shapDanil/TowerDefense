using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainBuilding : Unit
{
    public static MainBuilding singelton;

    public GameObject CityUi;
    public GameObject ShopUi;
    
    void Start()
    {
        singelton = this;
        health = 500;
    }
    
     void OnMouseDown()
    {
        AudioManager.instance.Play("base");
        CityUi.SetActive(true);
        CurrentUnitStats.instance.CloseUI();
    }
    public override void Die()
    {
        AudioManager.instance.Play("sos");
        OffUI();
        Tooltip.singelton.HideToolTip();
        ShopUi.SetActive(false);
        TooltipReserche.singelton.HideToolTip();
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.GetComponent<Animator>().SetBool("IsDeath",true);
    }
    void vanish()
    {
        Destroy(gameObject);
    }
    public void OffUI()
    {
        CityUi.SetActive(false);
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.C))
        {
            //AudioManager.instance.Play("base");
            CityUi.SetActive(true);
            CurrentUnitStats.instance.CloseUI();
        }
    }
}
