using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float moveSpeed;
    private bool KeyRight;
    private bool KeyLeft;
    private bool KeyrightMouse;
    public float _leftLimit;
    public float _rightLimit;
    public delegate void SomeAction();
    public event SomeAction pressButtonKey;
    void Awake()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        GetInput();
        if(KeyLeft && KeyRight)
        {
            return;
        }
        if (KeyLeft)
        {         
            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x - moveSpeed, _leftLimit, _rightLimit), this.transform.position.y, this.transform.position.z);
        }else if (KeyRight)
        {
            this.transform.position = new Vector3(Mathf.Clamp(this.transform.position.x + moveSpeed, _leftLimit,_rightLimit) , this.transform.position.y, this.transform.position.z);
        }
        if (KeyrightMouse) {

            PlayerStats.singleton.selectedUnit = null;
            BuildManager.instance.buildMode = true;
            BuildManager.instance.OffNodesForBuild();
            CurrentUnitStats.instance.CloseUI();
        } 
    }

    void GetInput()
    {
        KeyLeft = Input.GetKey(KeyCode.A);
        KeyRight = Input.GetKey(KeyCode.D);
        KeyrightMouse = Input.GetMouseButton(1);
        /*if (Input.GetKey(KeyCode.S))
        {
            pressButtonKey?.Invoke();
        }*/
    }
}
