using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInput : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private ToiletPaperDetector detector;

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

		if (detector.CanPick())
		{
			PickItem();
		}
	}

	public void PickItem()
	{
		if (detector.nearToiletPaperColliders == null || detector.nearToiletPaperColliders.Length <= 0)
			return;

		Transform toiletRoll = detector.nearToiletPaperColliders[0].transform;
		toiletRoll.SetParent(transform);
		toiletRoll.DOLocalMove(Vector3.up, 0.2f);
	}
}
