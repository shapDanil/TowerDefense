using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStartStats", menuName = "MyObjectsInfo/PlayerStartStats")]
public class PlayerStartStats : ScriptableObject
{
	// Start is called before the first frame update
	public int lives;
	public float rateMining;
	public int incomAmmunition;
	public int incomMaterial;
	public int incomRecruts;
	public int capacityAmmunition;
	public int capacityMaterial;
	public int capacityRecruts;
}
