using UnityEngine;

public class Product : MonoBehaviour
{
	[SerializeField] protected LayerMask productLayer;
	[SerializeField] protected LayerMask productInsideLayer;

	private void OnTriggerExit(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("ShoppingCart"))
		{
			transform.SetParent(null);
			gameObject.layer = productLayer;
		}
	}

	private void OnTriggerEnter(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("ShoppingCart"))
		{
			gameObject.layer = productInsideLayer;
		}
	}

	private void OnTriggerStay(Collider other)
	{
		if (other.gameObject.layer == LayerMask.NameToLayer("ShoppingCart"))
		{
			if (transform.parent == other.transform)
			{
				if (gameObject.layer == productInsideLayer)
					return;

				gameObject.layer = productInsideLayer;
			}
			transform.SetParent(other.transform);
		}
	}
}
