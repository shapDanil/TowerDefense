using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

	public GameObject ui;
	private string menuSceneName = "MainMenu";

	public SceneFader sceneFader;
	
    void Update ()
	{
		if ((Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)) && PlayerStats.singleton.GetLives() > 0)
		{
			Toggle();
		}
	}

	public void Toggle ()
	{
		ui.SetActive(!ui.activeSelf);

		if (ui.activeSelf)
		{
			Time.timeScale = 0f;
		} else
		{
			Time.timeScale = 1f;
		}
	}

	public void Retry ()
	{
		if(ui.activeSelf)
			Toggle();
		sceneFader.FadeTo(SceneManager.GetActiveScene().name);
    }

	public void Menu ()
	{
		if (ui.activeSelf)
			Toggle();
		sceneFader.FadeTo(menuSceneName);
	}
	public void SelectionMenu()
	{
		if (ui.activeSelf)
			Toggle();
		sceneFader.FadeTo("MenuSelection");
	}

}
