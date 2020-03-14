using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float matchDuration;
	private float timeElapsed;
	public ShoppingCartInput[] shoppingCarts;

	[SerializeField] private ShoppingCartSpawner[] shoppingCartSpawners;

	private void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > matchDuration)
			Debug.Log("Game Over");
	}

	private void Start()
	{
		shoppingCarts = new ShoppingCartInput[shoppingCartSpawners.Length];
		for (int i = 0; i < shoppingCartSpawners.Length; i++)
		{
			shoppingCarts[i] = shoppingCartSpawners[i].Spawn();
		}
	}
}
