
using UnityEngine;

[CreateAssetMenu(fileName = "turretData", menuName = "MyObjectsInfo/TurretInfo")]
public class TurretData : ScriptableObject
{
    public float health;
    public float fireRate;
    public float range;
    public float damage;
    public float scaleX;
    public float scaleY;
    public float colSizeX;
    public float colSizeY;
    public float colOffsetX;
    public float colOffsetY;
    public float firePointX;
    public float firePointY;
    public string Tag;
    public RuntimeAnimatorController controller;
    public BulletData bulletInfo;
   // public Transform firePoint;
}
