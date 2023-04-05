using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	public GameObject gameLoseUI;
	public GameObject completeLevelUI;
	private bool isEnd = false;

	void Start()
	{
		GetComponent<WaveSpawner>().gameEnd += EndGame;
		PlayerStats.singleton.gameEnd += EndGame;
	}


	void EndGame(bool result)
	{
        if (!isEnd)
        {
			if (result)
				completeLevelUI.SetActive(true);
            else
            {
				gameLoseUI.SetActive(true);
				AudioManager.instance.Play("lose");
			}
				
			isEnd = true;
		}
		
	}


}
