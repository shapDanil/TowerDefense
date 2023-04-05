using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "bulletData", menuName = "MyObjectsInfo/bulletData")]
public class BulletData : ScriptableObject
{
    public float speed;
    public float scaleX;
    public float scaleY;
    public float colSizeX;
    public float colSizeY;
    public float colOffsetX;
    public float colOffsetY;
    public Sprite sprite;
    public RuntimeAnimatorController controller;
}
