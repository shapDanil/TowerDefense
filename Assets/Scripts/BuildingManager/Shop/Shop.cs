using UnityEngine;

public class Shop : MonoBehaviour
{
	public TurretBlueprint[] allBlueprints;
	BuildManager _buildManager;

	void Start()
	{
		BuildManager.instance = GameObject.Find("GameMaster").GetComponent<BuildManager>();
		
	}
	public void SelectSomeUnit(int index)
    {
		BuildManager.instance.SelectTurretToBuild(allBlueprints[index]);		
    }
	public void SelectSomeMovingUnit(int index)
	{
		BuildManager.instance.SelectMovingUnitToBuild(allBlueprints[index]);
	}
/*	public TurretBlueprint GetCostUnit()
    {

    }*/
}
