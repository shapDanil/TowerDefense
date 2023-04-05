using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSpawner : MonoBehaviour
{
	public static WaveSpawner singleton;
	public int enemyAlive;
	public Wave[] waves;
	public GameObject enemyPrefab;
	public Transform spawnPoint;

	public float timeBetweenWaves = 5f;
	private float countdown = 2f;
	public delegate void SomeAction(bool resolteGame);
	public event SomeAction gameEnd;
	private int waveIndex = -1;
	//int count = 0;
    private void Awake()
    {
		if (singleton != null)
			Debug.Log("asdas"); 
		singleton = this;
    }
    private void Start()
    {
		for (int i = 0; i < waves.Length; i++)
			enemyAlive += waves[i].count;
		//Debug.Log("alive:" + enemyAlive);
    }
    void Update()
	{
		
		if (countdown <= 0f )
		{
			waveIndex++;
			if (waveIndex < waves.Length)
				StartCoroutine(SpawnWave());
			
			countdown = timeBetweenWaves;
		}
		countdown = Mathf.Clamp(countdown - Time.deltaTime, 0f, Mathf.Infinity);
	}
    private void FixedUpdate()
    {
        if (enemyAlive <= 0)
		{
			gameEnd?.Invoke(true);
			this.enabled = false;
			return;
		}
    }

    IEnumerator SpawnWave()
	{
		Wave wave = waves[waveIndex];
        if (wave.isOrder)
        {
			for (int i = 0; i < wave.typesEnemy.Length; i++)
            {
				for(int j = 0; j < wave.countOfTypes[i]; j++)
                {
					//count++;
					//Debug.Log(count + " " + i + "-i " + j + "-j");
					SpawnEnemy(wave.typesEnemy[i]);
					yield return new WaitForSeconds(1f / wave.rate);
				}
			}	
		}
	}

	void SpawnEnemy(EnemyData enemyInfo)
	{
		Vector3 position = Vector3.zero;
		Transform targetPosition = null;
		int rand = Random.Range(1,3);
		switch (rand)
		{
			case 1:
				{
					position = spawnPoint.Find("LowerSpawn").position;
					break;
				}
				
			case 2:
				{ 				
					position = spawnPoint.Find("MediumSpawn").position;
					break;
				}
			case 3:
				{ 
					position = spawnPoint.Find("UpperSpawn").position;
					break;
				}
		}
		targetPosition = GameObject.Find("EnemyTarget").transform;
		GameObject newEnemy = Instantiate(enemyPrefab, position, spawnPoint.rotation);
		newEnemy.SetActive(false);
		Enemy enemy = newEnemy.GetComponent<Enemy>();
		enemy.Init(enemyInfo);
		newEnemy.GetComponent<EnemyMovement>().setTarget(targetPosition);
		newEnemy.SetActive(true);
	}
}
