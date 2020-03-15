using UnityEngine;
using UnityEngine.SceneManagement;

public class ShoppingCartSpawner : MonoBehaviour
{
	[SerializeField] private BoxCollider counter;
	[SerializeField] private Renderer counterRenderer;
	[SerializeField] private Color color;
	[SerializeField] private string colorHex;
	[SerializeField] private string colorName;
	private bool canFinishGame;
	private ShoppingCartInput shoppingCart;

	public ShoppingCartInput Spawn(ShoppingCartInput shoppingCartPrefab)
	{
		shoppingCart = Instantiate(shoppingCartPrefab, transform.position, Quaternion.Euler(Vector3.left));
		shoppingCart.Init(CanReturnCallback, colorHex, colorName);
		ChangeColor(shoppingCart, color);
		return shoppingCart;
	}

	public void CanReturnCallback()
	{
		if (SceneManager.GetActiveScene().name == "Shop")
		{
			counter.enabled = true;
			canFinishGame = true;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (canFinishGame)
		{
			if (other.gameObject == shoppingCart.gameObject)
			{
				shoppingCart.enabled = false;
				shoppingCart.GetComponent<ShoppingCartMovement>().enabled = false;
				shoppingCart.reachedCounterCallback?.Invoke(shoppingCart);
				shoppingCart.reachedCounterCallback = null;
			}
		}
	}

	private void ChangeColor(ShoppingCartInput input, Color color)
	{
		input.CartRenderer.material.SetColor("_BaseColor", color);
		counterRenderer.materials[1].SetColor("_BaseColor", color);
	}
}
