using UnityEngine;

public class PlayerSpawn : MonoBehaviour
{
	[SerializeField] private PlayerInput playerPrefab;
	[SerializeField] private EnemyInput enemyPrefab;

	public GameObject Spawn()
	{
		if (playerPrefab == null)
			return Instantiate(enemyPrefab, transform.position, Quaternion.Euler(Vector3.left)).gameObject;
		else
		{
			Transform player = Instantiate(playerPrefab, transform.position, Quaternion.Euler(Vector3.left)).GetComponent<Transform>();
			Camera.main.GetComponent<BrainHelper>().AttachCam(player);
			return player.gameObject;
		}
	}
}
