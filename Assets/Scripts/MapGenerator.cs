using UnityEngine;

public class MapGenerator : MonoBehaviour
{
	[SerializeField] private Shelf[] shelves;
	[SerializeField] private Transform[] stacks;
	[SerializeField] private int shelvesWithPaperCount;
	[SerializeField] private int standsWithPaperCount;
	[SerializeField] private Product[] productPrefabs;

	public void Start()
	{
		Shelf[] randomizedShelves = shelves;
		System.Random rng = new System.Random();
		rng.Shuffle(randomizedShelves);

		SpawnProduct(randomizedShelves);
		shelves = new Shelf[0];

		Transform[] randomizeStacks = stacks;
		rng = new System.Random();
		rng.Shuffle(randomizeStacks);
		for (int i = 0; i < stacks.Length; i++)
		{
			if (i < standsWithPaperCount)
				SpawnStack(productPrefabs[0], 5, stacks[i].position);
			else
				Destroy(stacks[i].gameObject);
		}
		stacks = new Transform[0];
	}

	public void SpawnStack(Product product, int baseSize, Vector3 stackPosition)
	{
		Renderer renderer = product.GetComponent<Renderer>();
		Bounds bounds = renderer.bounds;
		float offsetX = (baseSize - 1) * bounds.extents.x;
		float offsetZ = (baseSize - 1) * bounds.extents.z;
		for (int y = 0; y < baseSize; y++)
		{
			for (int x = 0; x < baseSize - y; x++)
			{
				for (int z = 0; z < baseSize - y; z++)
				{
					Vector3 pos = new Vector3((x + y * 0.5f) * bounds.size.x, bounds.size.y * y, (z + y * 0.5f) * bounds.size.z);
					pos += stackPosition;
					pos.x -= offsetX;
					pos.z -= offsetZ;
					Instantiate(product, pos, Quaternion.identity);
				}
			}
		}
	}

	private void SpawnProduct(Shelf[] randomizedShelves)
	{
		int halfOfShelves = Mathf.CeilToInt(shelves.Length * 0.5f);
		for (int i = 0; i < shelves.Length; i++)
		{
			if (i < shelvesWithPaperCount)
				randomizedShelves[i].SpawnProduct(productPrefabs[0]);
			else
			{
				if (i > halfOfShelves)
				{
					randomizedShelves[i].SpawnProduct(productPrefabs[1]);
				}
				else
				{
					randomizedShelves[i].SpawnProduct(productPrefabs[2]);
				}
			}

		}
	}
}
