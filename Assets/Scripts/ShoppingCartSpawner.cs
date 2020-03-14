using UnityEngine;

public class ShoppingCartSpawner : MonoBehaviour
{
	[SerializeField] private ShoppingCartInput shoppingCartPrefab;

	public ShoppingCartInput Spawn()
	{
		ShoppingCartInput input = Instantiate(shoppingCartPrefab, transform.position, Quaternion.Euler(Vector3.left));
		input.Init();
		return input;
	}
}
