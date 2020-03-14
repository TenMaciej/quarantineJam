using UnityEngine;
using UnityEngine.AI;

public class EnemyInput : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	private Camera camera;

	public void SetDestination(Vector3 destination)
	{
		agent.SetDestination(destination);
	}

	private void Start()
	{
		camera = Camera.main;
	}

	private void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Ray ray = camera.ScreenPointToRay(Input.mousePosition);
			if (Physics.Raycast(ray, out RaycastHit hit))
			{
				if (hit.collider.CompareTag("Ground"))
					SetDestination(hit.point);
			}
		}
	}
}
