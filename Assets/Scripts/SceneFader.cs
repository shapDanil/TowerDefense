using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour {

	public Image img;
	public AnimationCurve curve;

	void Start ()
	{
		StartCoroutine(FadeIn());
	}

	public void FadeTo (string scene)
	{
		StartCoroutine(FadeOut(scene));
	}
	public void FadeTo(int scene)
	{
		StartCoroutine(FadeOut(scene));
	}


	IEnumerator FadeIn ()
	{
		float t = 1f;

		while (t > 0f)
		{
			t -= Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color (0f, 0f, 0f, a);
			yield return null;
		}
	}

	IEnumerator FadeOut(string scene)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return null;
		}

		SceneManager.LoadScene(scene);
	}
	IEnumerator FadeOut(int scene)
	{
		float t = 0f;

		while (t < 1f)
		{
			t += Time.deltaTime;
			float a = curve.Evaluate(t);
			img.color = new Color(0f, 0f, 0f, a);
			yield return null;
		}

		SceneManager.LoadScene(scene);
	}

}
