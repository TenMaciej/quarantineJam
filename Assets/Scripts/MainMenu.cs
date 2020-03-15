using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject creditsWindow;

	public void Play()
	{
		SceneManager.LoadScene("Shop");
		//Camera fade out;
	}

	public void Credits()
	{
		creditsWindow.SetActive(true);
	}

	public void Exit()
	{
		Application.Quit();
	}

	public void CloseCredits()
	{
		creditsWindow.SetActive(false);
	}
}
