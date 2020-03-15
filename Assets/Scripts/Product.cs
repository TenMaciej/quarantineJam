using UnityEngine;

public class Product : MonoBehaviour
{
	public Color color;

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("ShoppingCart"))
		{
			if (gameObject.layer == LayerMask.NameToLayer("ProductInside"))
			{
				transform.SetParent(null);
				gameObject.layer = LayerMask.NameToLayer("Product");
			}
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("ShoppingCart"))
		{
			if (gameObject.layer == LayerMask.NameToLayer("Product"))
			{
				transform.SetParent(other.transform);
				gameObject.layer = LayerMask.NameToLayer("ProductInside");
			}
		}
	}
}
