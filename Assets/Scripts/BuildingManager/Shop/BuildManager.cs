using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    // Start is called before the first frame update

    TurretBlueprint _turretToBuild;

    public GameObject allTowerNodes;
    //public GameObject allMovingUnitNodes;
    public static BuildManager instance;
    public bool buildMode;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in scene!");
            return;
        }
        instance = this;
    }
    private void Start()
    {
        buildMode = true;
    }
    public void SelectTurretToBuild(TurretBlueprint turret)
    {
        allTowerNodes.SetActive(true);
        _turretToBuild = turret;
        buildMode = true;
    }
    public void SelectMovingUnitToBuild(TurretBlueprint movingUnit)
    {
        ActiveAllNode();
        _turretToBuild = movingUnit;
    }
    public void ActiveAllNode()
    {
        allTowerNodes.SetActive(true);
    }
    public TurretBlueprint GetTurretToBuild()
    {
        return _turretToBuild;
    }
 

    public bool CanBuild { get { return _turretToBuild != null; } }
    /*public bool HasMoney() 
    { 
        return (PlayerStats.singleton.GetAmmunition() >= _turretToBuild.costAmmunition && PlayerStats.singleton.GetMaterials() >= _turretToBuild.costMaterials && PlayerStats.singleton.GetRecruits() >= _turretToBuild.costRecruts); 
    }*/
    public void OffNodesForBuild()
    {
        allTowerNodes.SetActive(false);
        _turretToBuild = null;
    }
    public void OffUnitNodesForBuild()
    {
        PlayerStats.singleton.selectedUnit = null;
        allTowerNodes.SetActive(false);
        //allMovingUnitNodes.SetActive(false);
        _turretToBuild = null;
        buildMode = true;
    }
}
