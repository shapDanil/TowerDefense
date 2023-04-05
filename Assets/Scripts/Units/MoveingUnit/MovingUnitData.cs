using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MovingUnitInfo", menuName = "MyObjectsInfo/MovingUnit")]
public class MovingUnitData : ScriptableObject
{
    public float health;
    public float fireRate;
    public float range;
    public float speed;
    public float damage;
    public int diraction;
    public float scaleX;
    public float scaleY;
    public float colSizeX;
    public float colSizeY;
    public float colOffsetX;
    public float colOffsetY;
    public float firePointX;
    public float firePointY;
    public float Z;
    public float spawnOffsetY;
    public string Tag;
    public RuntimeAnimatorController controller;
    public Sprite icon;
    public BulletData bulletInfo;
}
