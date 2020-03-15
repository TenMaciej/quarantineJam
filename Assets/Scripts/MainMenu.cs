using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	[SerializeField] private GameObject creditsWindow;
	[SerializeField] private TextMeshProUGUI playerCount;
	[SerializeField] private GameManager gameManager;
	[SerializeField] private GameObject[] playerPanel;

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

	public void ChangePlayerCount(float count)
	{
		playerCount.text = count.ToString();
		gameManager.gameData.playerCount = Mathf.FloorToInt(count);
		foreach (GameObject panel in playerPanel)
		{
			panel.SetActive(false);
		}

		for (int i = 0; i < count; i++)
		{
			playerPanel[i].SetActive(true);
		}
	}
}
