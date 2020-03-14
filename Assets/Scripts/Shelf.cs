using UnityEngine;

public class Shelf : MonoBehaviour
{
	[SerializeField] private Product[] productPrefabs;
	[SerializeField] private Transform[] productSpawns;

	private void Start()
	{
		foreach (Transform productSpawn in productSpawns)
		{
			bool spawn = Random.Range(0, 2) > 0;
			if (spawn)
			{
				int randomProduct = Random.Range(0, productPrefabs.Length);
				Instantiate(productPrefabs[randomProduct], productSpawn.position, Quaternion.identity);
			}
		}
	}
}
