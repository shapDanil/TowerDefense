
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public GameObject buttonUI;
    public GameObject soundUI;
    public GameObject fader;
    public void StartGame()
    {
       // SceneManager.LoadScene(1);
        fader.GetComponent<SceneFader>().FadeTo("MenuSelection"); 
    }

    public void Exit()
    {
        Application.Quit();
    }
    public void SoundUIOn()
    {
        buttonUI.SetActive(false);
        soundUI.SetActive(true);
    }
    public void ButtonUIOn()
    {
        buttonUI.SetActive(true);
        soundUI.SetActive(false);
    }
}
