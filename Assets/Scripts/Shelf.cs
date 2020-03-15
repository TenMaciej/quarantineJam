using UnityEngine;

public class Shelf : MonoBehaviour
{
	[SerializeField] private Transform[] productSpawns;

	public void SpawnProduct(Product product)
	{
		Renderer renderer = GetComponent<Renderer>();
		renderer.material.SetColor("_BaseColor", product.color);

		foreach (Transform productSpawn in productSpawns)
		{
			bool spawn = Random.Range(0, 2) > 0;
			if (spawn)
				Instantiate(product, productSpawn.position, Quaternion.identity);
		}
		CleanShelf();
	}

	public void CleanShelf()
	{
		foreach (Transform child in transform)
		{
			Destroy(child.gameObject);
		}
		productSpawns = new Transform[0];
	}
}
