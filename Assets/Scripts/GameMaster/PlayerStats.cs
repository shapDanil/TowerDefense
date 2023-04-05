using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour {

	
	private delegate void CityAction();
	private event CityAction updateResurse;
	public delegate void SomeAction(bool resolteGame);
	public event SomeAction gameEnd;

	[HideInInspector]
	public static PlayerStats singleton;

	[SerializeField]
	private int _ammunition { get; set; }
	[SerializeField]
	private int _materials { get; set; }
	[SerializeField]
	private int _recruits { get; set; }

	[SerializeField]
	private int _lives;

	public PlayerStartStats startStats;

	private float rateMining;
	private int incomAmmunition;
	private int incomMaterial;
	private int incomRecruts;
	private int capacityAmmunition;
	private int capacityMaterial;
	private int capacityRecruts;

	[SerializeField]
	private float _miningCountdown { get; set; }

	public GameObject playerStatsUi;
	public GameObject playerStatsCityUi;
	public Text[] allCityText;
	[Header("Количество баффов(производство)")]
	[SerializeField]
	private int countBuffIncomAmmunition;
	[SerializeField]
	private int countBuffIncomMaterial;
	[SerializeField]
	private int countBuffIncomRecruts;
	[SerializeField]
	private int countBuffCapacityAmmunition;
	[SerializeField]
	private int countBuffCapacityMaterial;
	[SerializeField]
	private int countBuffCapacityRecruts;
	public int Rounds;
	[HideInInspector]
	public MovingUnit selectedUnit;
	private Text[] allStatsText;
	private Text[] allStatsCityText;
	public AnimationCurve curve;
	private void Awake()
    {
		singleton = this;
    }
    void Start ()
	{
		rateMining = startStats.rateMining;
		incomAmmunition = startStats.incomAmmunition;
		incomMaterial = startStats.incomMaterial;
		incomRecruts = startStats.incomRecruts;
		capacityAmmunition = startStats.capacityAmmunition;
		capacityMaterial = startStats.capacityMaterial;
		capacityRecruts = startStats.capacityRecruts;
		_ammunition = startStats.capacityAmmunition;
		_materials = startStats.capacityMaterial;
		_recruits = startStats.capacityRecruts;
		CameraController.instance.pressButtonKey += TurnSelectedUnit;
		selectedUnit = null;
		Rounds = 0;
        allStatsText = playerStatsUi.GetComponentsInChildren<Text>();
		allStatsCityText = playerStatsCityUi.GetComponentsInChildren<Text>();
		UpdateResurseUI();
		SetAllCityText();

		//updateResurse += UpdateResurseUI;
	}
	void FixedUpdate()
	{
		if (_miningCountdown <= 0f)
		{
			Incom();
			_miningCountdown = rateMining;

		}
		_miningCountdown -= Time.fixedDeltaTime;
		
	}
	private void Incom()
	{
		_ammunition = (int)Mathf.Clamp(_ammunition + incomAmmunition, 0, capacityAmmunition);
		_materials = (int)Mathf.Clamp(_materials + incomMaterial, 0, capacityMaterial);
		_recruits = (int)Mathf.Clamp(_recruits + incomRecruts, 0, capacityRecruts);
		UpdateResurseUI();
	}
	void TurnSelectedUnit()
    {
		/*if (selectedUnit != null)
        {*//*
			Vector3 vec = selectedUnit.gameObject.transform.localScale;
			selectedUnit.gameObject.transform.localScale = new Vector3(vec.x * (float)selectedUnit.diraction, vec.y, vec.z);*//*
		}*/
			
    }
	public  void SetAmmunition(int ammunition)
    {
		_ammunition = ammunition;
	}
	public  void SetLives(int lives)
	{
		if(lives <= 0)
        {
			return;
		}
		_lives = lives;
	}
	public  int GetAmmunition()
	{
		return _ammunition;
	}
	public  int GetLives()
	{
		return _lives;
	}
	public int GetMaterials()
	{
		return _materials;
	}
	public int GetRecruits()
	{
		return _recruits;
	}
	public bool BuffIncomAmmunition(int deltaA, int deltaM, int deltaR)
    {
        if (countBuffIncomAmmunition > 0)
        {
            if (SubtractResurs(deltaA, deltaM, deltaR))
            {
                incomAmmunition += startStats.incomAmmunition;
                countBuffIncomAmmunition--;
				SetAllCityText();
			}
			return true;
		}
		else
			return false;
	}
	public bool BuffIncomMaterials(int deltaA, int deltaM, int deltaR)
	{
		if (countBuffIncomMaterial > 0)
		{
			if (SubtractResurs(deltaA, deltaM, deltaR))
            {
				incomMaterial += startStats.incomMaterial;
				countBuffIncomMaterial--;
				SetAllCityText();
			}
			return true;
		}
		else
			return false;
	}
	public bool BuffIncomRecruits(int deltaA, int deltaM, int deltaR)
	{
		if (countBuffIncomRecruts > 0)
		{
			if (SubtractResurs(deltaA, deltaM, deltaR))
            {
				incomRecruts += startStats.incomRecruts;
				countBuffIncomRecruts--;
				SetAllCityText();
			}
			return true;
		}
		else
			return false;
	}
	public bool BuffCapacityAmmunition(int deltaA, int deltaM, int deltaR)
	{
		if (countBuffCapacityAmmunition > 0)
		{
			if (SubtractResurs(deltaA, deltaM, deltaR))
            {
				capacityAmmunition += startStats.capacityAmmunition;
				countBuffCapacityAmmunition--;
				SetAllCityText();
			}
			return true;
		}
		else
			return false;

	}
	public bool BuffCapacityMaterials(int deltaA, int deltaM, int deltaR)
	{
		if (countBuffCapacityMaterial > 0)
		{
			if (SubtractResurs(deltaA, deltaM, deltaR))
            {
				capacityMaterial += startStats.capacityMaterial;
				countBuffCapacityMaterial--;
				SetAllCityText();
			}
			return true;
		}
		else
			return false;
	}
	public bool BuffCapacityRecruits(int deltaA, int deltaM, int deltaR)
	{

		if (countBuffCapacityRecruts > 0)
		{
			if (SubtractResurs(deltaA, deltaM, deltaR))
			{
				capacityRecruts += startStats.capacityRecruts;
				countBuffCapacityRecruts--;
				SetAllCityText();
			}
			return true;
		}
		else
			return false;


	}
	public  bool SubtractResurs(int deltaA, int deltaM, int deltaR)
    {
        if (HasMoney(deltaA,deltaM, deltaR))
        {
			_ammunition = (int)Mathf.Clamp(_ammunition - deltaA, 0, float.MaxValue);
			_materials = (int)Mathf.Clamp(_materials - deltaM, 0, float.MaxValue);
			_recruits = (int)Mathf.Clamp(_recruits - deltaR, 0, float.MaxValue);
			UpdateResurseUI();
			return true;
        }
        else
        {
			AudioManager.instance.Play("money");
			Debug.Log("NO Money ");
			return false;
        }
		
	}
	bool HasMoney(int deltaA, int deltaM, int delatR)
    {
		if(deltaA > _ammunition || deltaM > _materials || delatR > _recruits)
        {
			return false;
        }
		return true;
    }

	public void SubtractLives(int value)
	{
		_lives -= value;
		allStatsText[3].text = _lives.ToString();
		allStatsCityText[3].text = allStatsText[3].text;
		if (_lives <= 0)
		{
			gameEnd?.Invoke(false);
		}
	}
	public void SetAllCityText()
    {
		allCityText[0].text = incomAmmunition.ToString() + "u";
		allCityText[1].text = incomMaterial.ToString() + "u";
		allCityText[2].text = incomRecruts.ToString() + "u";
		allCityText[3].text = capacityAmmunition.ToString() + "u";
		allCityText[4].text = capacityMaterial.ToString() + "u";
		allCityText[5].text = capacityRecruts.ToString() + "u";
    }
	public void UpdateResurseUI()
    {
		allStatsText[0].text = _ammunition.ToString();
		allStatsText[1].text = _materials.ToString();
		allStatsText[2].text = _recruits.ToString();
		//allStatsText[3].text = _lives.ToString();
		allStatsCityText[0].text = allStatsText[0].text;
		allStatsCityText[1].text = allStatsText[1].text;
		allStatsCityText[2].text = allStatsText[2].text;
		//allStatsCityText[3].text = allStatsText[3].text;
	}
	public void StartFlashingSelectionUnit(MovingUnit newUnit)
    {
		if(selectedUnit != null)
        {
			selectedUnit.gameObject.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
			StopAllCoroutines();
		}
		selectedUnit = newUnit;
		StartCoroutine(Flashing());
    }
	IEnumerator Flashing()
	{
		float t = 0f;
		SpriteRenderer render = selectedUnit.gameObject.GetComponent<SpriteRenderer>();
		while (selectedUnit != null)
		{
			t = Mathf.Clamp(t + 0.03f, 0f, float.MaxValue);
			float a = curve.Evaluate(t);
			render.color = new Color(1f, a, 1f, a);
			yield return null;
		}
		render.color = new Color(1f, 1f, 1f, 1f);
	}
}
