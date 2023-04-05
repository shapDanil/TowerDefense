using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour
{
    public static BuffManager instance;
    private Dictionary< string, HashSet<Unit>> allUnit;
    private Dictionary< string, Queue<Buff>> allBuff;
    public GameObject BuffUI;
    public GameObject timeMilitaryTextUI;
    
    private void Awake()
    {
        instance = this;
        
    }
    private void Start()
    {
        allUnit = new Dictionary<string, HashSet<Unit>>();
        allBuff = new Dictionary<string, Queue<Buff>>();
    }

    public void AddUnit(Unit someUnit)
    {
        if (!allUnit.ContainsKey(someUnit.tag))
        {
            allUnit.Add(someUnit.tag, new HashSet<Unit>());
        }
        allUnit[someUnit.tag].Add(someUnit);
        BuffThisUnit(someUnit);
    }
    public void AddBuff(Buff buff)
    {
        if (!allBuff.ContainsKey(buff.GetTag()))
        {
            allBuff.Add(buff.GetTag(), new Queue<Buff>());
        }
        allBuff[buff.GetTag()].Enqueue(buff);
    }
    public void RemoveUnit(Unit someUnit)
    {
        if (allUnit.ContainsKey(someUnit.tag))
        {
            if(!allUnit[someUnit.tag].Remove(someUnit))
                Debug.LogError("In Buffmanager not found" + someUnit  + "unit");
        }
        else
        {
            Debug.LogError("In Buffmanager not found " + someUnit + " with this " + someUnit.tag + " tag");
        }
    }
   
    public void StartMilitaryBuff(Buff buff)
    {
        timeMilitaryTextUI.SetActive(true);
        BuffUI.SetActive(false);
        StartCoroutine(StartResearch(buff));
    }
   

    IEnumerator StartResearch(Buff buff)
    {
        float timeRate = buff.GetTimeRate();
        float time = timeRate;
        while (time > 0)
        {
            time -= Time.deltaTime;
            timeMilitaryTextUI.GetComponent<Text>().text = ((int)((1 - (time / timeRate)) * 100)).ToString() + "%";
            yield return null;
        }
        timeMilitaryTextUI.SetActive(false);
        BuffUI.SetActive(true);
        BuffAllUnitOnScene(buff);
        buff.gameObject.SetActive(false);
        AudioManager.instance.Play("reserche");
    }
    void BuffAllUnitOnScene(Buff buff)
    {
        if (allUnit.ContainsKey(buff.GetTag()))
        {
            foreach(var unit in allUnit[buff.GetTag()])
            {
                Debug.Log(unit);
                unit.Buff(buff);
            }
        }
        else
        {
            Debug.Log("Not unit with this the buff's tag! - BuffManager");
        }
    }
    void BuffThisUnit(Unit unit)
    {
        if (allBuff.ContainsKey(unit.tag))
        {
            foreach (var buff in allBuff[unit.tag])
            {
                unit.Buff(buff);
            }
        }
        else
        {
            Debug.Log(unit.tag);
            Debug.Log("Not buff with this the unit's tag! - BuffManager");
        }
    }
}
