using UnityEngine;
using System.Collections;

public class TurretBlueprint : MonoBehaviour
{
	[SerializeField]
	private int key;
	public GameObject prefab;
	public TurretData turretPrefabData;
	public MovingUnitData prefabData;
	public Sprite sprite;
	public int costAmmunition;
	public int costMaterials;
	public int costRecruts;
	public bool isMovingUnit;
	public void SelectSomeUnit()
	{
		BuildManager.instance.SelectTurretToBuild(this);
		PlayerStats.singleton.selectedUnit = null;

	}

/*	void Update()
	{
        if (Input.GetKey(Input.keypad))
        {
			SelectSomeUnit();
		}
    }*/

}
