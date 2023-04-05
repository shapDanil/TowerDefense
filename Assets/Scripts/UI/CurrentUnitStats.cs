using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CurrentUnitStats : MonoBehaviour
{
    private Text _name;
    private Text _reload;
    private Text _range;
    private Text _damage;
    private Text _health;
    private Text _speed;
    public static CurrentUnitStats instance;
    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        _name = transform.Find("Name").GetComponent<Text>();
        _health = transform.Find("Endurance").GetComponent<Text>();
        _speed = transform.Find("Speed").GetComponent<Text>();
        _damage = transform.Find("Damage").GetComponent<Text>();
        _reload = transform.Find("Reload").GetComponent<Text>();
        _range = transform.Find("Range").GetComponent<Text>();
        gameObject.SetActive(false);
    }
    public void SelectCurentUnit(string name, string reload, string range, string damage, string speed, string health)
    {
        _name.text = name;
        _health.text = "Endurance:" + health;
        _damage.text = "Damage:" + damage;
        _range.text = "Range:" + range;
        _reload.text = "Reload:" + reload;
        _speed.text = "Speed:" + speed;
        gameObject.SetActive(true);
    }
    public void CloseUI()
    {
        gameObject.SetActive(false);
    }
}