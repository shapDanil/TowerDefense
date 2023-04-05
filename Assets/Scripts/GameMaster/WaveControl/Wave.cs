using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Wave
{
	public EnemyData[] typesEnemy;
	public int[] countOfTypes;
	public bool isOrder = true;
	public int count;
	public float rate;
}

