using UnityEngine;

[CreateAssetMenu(fileName = "EnemyData" , menuName = "MyObjectsInfo/EnemyInfo")]
public class EnemyData : ScriptableObject
{
    public float health;
    public float speed;
    public float unitDamage;
    public int cityDamage;
    public int diraction;
    public float scaleX;
    public float scaleY;
    public float colSizeX;
    public float colSizeY;
    public float colOffsetX;
    public float colOffsetY;
    public RuntimeAnimatorController controller;
}
