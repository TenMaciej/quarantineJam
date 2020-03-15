using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	public float matchDuration;
	public ShoppingCartInput[] shoppingCarts;
	private float timeElapsed;
	private int shoppingCartReachedCount;
	private bool gameOver;
	public GameDataScriptable gameData;

	[SerializeField] private TextMeshProUGUI timer;
	[SerializeField] private GameObject outcomeWindow;
	[SerializeField] private TextMeshProUGUI outcome;
	[SerializeField] private ShoppingCartSpawner[] shoppingCartSpawners;
	[SerializeField] private ShoppingCartInput playerPrefab;
	[SerializeField] private ShoppingCartInput enemyPrefab;
	[SerializeField] private PlayerInputData[] playerInputData;
	[SerializeField] private PlayerScreenRectScriptable screenRectData;


	private void Update()
	{
		if (!gameOver)
		{
			if (timer == null)
				return;

			timeElapsed += Time.deltaTime;
			timer.text = FormatTimer(timeElapsed);
			if (timeElapsed > matchDuration)
				EndGame();
		}
	}

	private void Start()
	{
		if (outcomeWindow != null)
			outcomeWindow.SetActive(false);
		shoppingCarts = new ShoppingCartInput[shoppingCartSpawners.Length];
		int spawnedPlayer = 0;
		for (int i = 0; i < shoppingCartSpawners.Length; i++)
		{
			ShoppingCartInput prefab = enemyPrefab;
			if (SceneManager.GetActiveScene().name == "Shop")
			{
				if (spawnedPlayer < gameData.playerCount)
				{
					prefab = playerPrefab;
					spawnedPlayer++;
				}
			}

			ShoppingCartInput shoppingCart = shoppingCartSpawners[i].Spawn(prefab);
			shoppingCarts[i] = shoppingCart;
			shoppingCart.reachedCounterCallback = ReachedCounter;
			if (shoppingCart is PlayerInput input)
			{
				input.SetInput(playerInputData[i]);
			}
		}

		BrainHelper brainHelper = Camera.main.GetComponent<BrainHelper>();
		if (SceneManager.GetActiveScene().name == "MainMenu")
		{
			brainHelper.AttachCam(shoppingCarts[2].transform);
			gameData.playerCount = 1;
		}
		else
		{
			for (int i = 0; i < brainHelper.cameras.Length; i++)
			{
				brainHelper.cameras[i].rect = screenRectData.variants[gameData.playerCount - 1].screenRect[i];
				if (i < gameData.playerCount) continue;
				brainHelper.cameras[i].gameObject.SetActive(false);
				brainHelper.virtualCameras[i].gameObject.SetActive(false);
			}
		}

	}

	private void ReachedCounter(ShoppingCartInput shoppingCart)
	{
		shoppingCartReachedCount++;
		CalcScore(shoppingCart);
		if (shoppingCartReachedCount >= shoppingCarts.Length - 1) // prevents from playing alone when all other players finished game
		{
			EndGame();
		}
	}

	private void EndGame()
	{
		timer.text = "0:00";
		Outcome();
		foreach (ShoppingCartInput shoppingCart in shoppingCarts)
		{
			shoppingCart.enabled = false;
			shoppingCart.GetComponent<ShoppingCartMovement>().enabled = false;
		}
	}

	private int CalcScore(ShoppingCartInput shoppingCart)
	{
		Collider[] productsAtCounter = Physics.OverlapSphere(shoppingCart.transform.position, 1f);
		List<Collider> paperRolls = new List<Collider>();
		paperRolls.AddRange(Array.FindAll(productsAtCounter, p => p.GetComponent<ToiletPaperRoll>() != null).ToList());
		shoppingCart.gatheredToiletRolls = paperRolls.Count;
		Debug.Log(shoppingCart.gatheredToiletRolls);

		return shoppingCart.gatheredToiletRolls;
	}

	private string FormatTimer(float timeElapsed)
	{
		float timeLeft = matchDuration - timeElapsed;
		int elapsed = Mathf.CeilToInt(timeLeft);
		int minutes = Mathf.CeilToInt(elapsed / 60);

		int seconds = elapsed - minutes * 60;
		if (seconds < 10)
			return $"{minutes}:0{seconds}";
		else
			return $"{minutes}:{seconds}";
	}

	private void Outcome()
	{
		ShoppingCartInput cart = shoppingCarts.OrderByDescending(c => c.gatheredToiletRolls).FirstOrDefault();
		string outcomeText = "";
		if (cart.gatheredToiletRolls == 0)
		{
			outcomeText = "NO PLAYER HAS WON.";
		}
		else
		{
			outcomeText = $"<color=#{cart.colorHex}>{cart.colorName}</color>";
			outcomeText += $"\n PLAYER WINS \n {cart.gatheredToiletRolls} TOILET PAPER ROLLS.";
		}

		outcome.text = outcomeText;
		outcomeWindow.SetActive(true);
	}

	public void PlayAgain()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void ReturnToMenu()
	{
		SceneManager.LoadScene("MainMenu");
	}
}
