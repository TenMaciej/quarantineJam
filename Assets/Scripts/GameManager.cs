using UnityEngine;

public class GameManager : MonoBehaviour
{
	public float matchDuration;
	private float timeElapsed;
	public PlayerInput player;
	public EnemyInput[] enemy;

	[SerializeField] private PlayerSpawn playerSpawn;
	[SerializeField] private PlayerSpawn[] enemySpawn;

	private void Update()
	{
		timeElapsed += Time.deltaTime;
		if (timeElapsed > matchDuration)
			Debug.Log("Game Over");
	}

	private void Start()
	{
		player = playerSpawn.Spawn().GetComponent<PlayerInput>();
		enemy = new EnemyInput[enemySpawn.Length];
		for (int i = 0; i < enemySpawn.Length; i++)
		{
			enemy[i] = enemySpawn[i].Spawn().GetComponent<EnemyInput>();
		}
	}
}
