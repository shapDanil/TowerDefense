using UnityEngine.SceneManagement;
using UnityEngine;

public class SelectionMenuManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject fader;
    public void SelectLevel(int index)
    {
        // SceneManager.LoadScene(index);
        fader.GetComponent<SceneFader>().FadeTo(index);
    }

    public void Back()
    {
        fader.GetComponent<SceneFader>().FadeTo("MainMenu");
       // SceneManager.LoadScene("MainMenu");
    }
}
