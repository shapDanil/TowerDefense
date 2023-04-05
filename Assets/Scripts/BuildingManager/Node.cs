using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
	public Sprite defaultSprite;
	public GameObject spawnPoint;
	void Start()
	{

	}

	private Vector3 GetBuildPosition()
	{
		Vector3 pos = new Vector3(transform.position.x, transform.position.y, 5);
		return pos;
	}

	
	private bool SpawnUnit(TurretBlueprint blueprint)
	{
		if (BuildManager.instance.buildMode)
		{
			GameObject go = Instantiate(blueprint.prefab, spawnPoint.transform.position + Vector3.up * blueprint.prefabData.spawnOffsetY + blueprint.prefabData.Z * Vector3.forward, Quaternion.identity);
			
			MovingUnit mu = go.GetComponent<MovingUnit>();
			mu.Init(blueprint.prefabData);
			BuffManager.instance.AddUnit(go.GetComponent<MovingUnit>());
			mu.currentWayTarget = gameObject;
			gameObject.SetActive(false);
			BuildManager.instance.OffUnitNodesForBuild();
			//PlayerStats.singleton.SubtractResurs(blueprint.costAmmunition, blueprint.costMaterials,blueprint.costRecruts);
			Debug.Log("I");
		}
		return true;
	}
	private void CallUnit()
	{
		MovingUnit mu = PlayerStats.singleton.selectedUnit;
		if(Mathf.Abs(mu.gameObject.transform.position.y - gameObject.transform.position.y) > 0.7f)
        {

			if(mu.tempWayTarget == null)
            {
				mu.currentWayTarget.SetActive(true);
				mu.currentWayTarget = mu.currentWayTarget.GetComponent<Node>().spawnPoint;
			}
				
            else
            {
				mu.tempWayTarget.SetActive(true);
			}
			mu.tempWayTarget = gameObject;
		}
        else
		{
			mu.currentWayTarget.SetActive(true);
			mu.currentWayTarget = gameObject;
			mu.tempWayTarget = null;
		}
		mu.currentState.GetNextState();
		mu.currentState.IsFinished = true;
		mu.SetStay(mu.moveState);
		gameObject.SetActive(false);
		//BuildManager.instance.buildMode = true;
		BuildManager.instance.OffUnitNodesForBuild();
		CurrentUnitStats.instance.CloseUI();
		AudioManager.instance.Play("move");
	}
	private bool BuildTurret(TurretBlueprint blueprint)
	{
		//_turretBlueprint = blueprint;
		GameObject go = Instantiate(blueprint.prefab, GetBuildPosition(), Quaternion.identity);
		Turret turret = go.GetComponent<Turret>();
		Wall wall = go.GetComponent<Wall>();
		if (wall != null)
		{
			BuffManager.instance.AddUnit(wall);
		}
        else
        {
			
			turret.Init(blueprint.turretPrefabData);
			turret.SetNode(gameObject);
			BuffManager.instance.AddUnit(turret);
		}
		
		gameObject.SetActive(false);
		BuildManager.instance.OffNodesForBuild();
		Debug.Log("Turret build!");
		return true;
	}
	public void EnableNode()
	{
		gameObject.SetActive(true);
	}

	void OnMouseDown()
	{
		TurretBlueprint turretBlueprint = BuildManager.instance.GetTurretToBuild();
		if (BuildManager.instance.buildMode && BuildManager.instance.CanBuild && PlayerStats.singleton.SubtractResurs(turretBlueprint.costAmmunition, turretBlueprint.costMaterials, turretBlueprint.costRecruts))
        {
			if (BuildManager.instance.GetTurretToBuild().isMovingUnit)
				SpawnUnit(turretBlueprint);
			else
				BuildTurret(turretBlueprint);
		}	
		else
		{
			if (!BuildManager.instance.buildMode)
				CallUnit();
            else
            {
				OnMouseExit();
				BuildManager.instance.OffNodesForBuild();
			}
		}
		OnMouseExit();
	}
	private void OnMouseEnter()
	{
		if (BuildManager.instance.buildMode)
			gameObject.GetComponent<SpriteRenderer>().sprite = BuildManager.instance.GetTurretToBuild().sprite;
		else
			gameObject.GetComponent<SpriteRenderer>().sprite = PlayerStats.singleton.selectedUnit._properties.icon;

	}
	private void OnMouseExit()
	{
		gameObject.GetComponent<SpriteRenderer>().sprite = defaultSprite;
	}
}
