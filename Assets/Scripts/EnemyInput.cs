using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInput : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private ToiletPaperDetector detector;
	[SerializeField] private Rigidbody rigid;

	private Camera camera;

	public void SetDestination(Vector3 destination)
	{
		agent.SetDestination(destination);
	}

	private void Start()
	{
		camera = Camera.main;
		agent.updatePosition = false;
		agent.updateRotation = false;
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

		Debug.DrawRay(transform.position, agent.velocity, Color.magenta);


		if (detector.CanPick())
		{
			PickItem();
		}
	}

	private void FixedUpdate()
	{
		if (agent.velocity != Vector3.zero)
		{
			rigid.MoveRotation(Quaternion.LookRotation(agent.velocity, Vector3.up));
		}


		Vector3 targetVelocity = agent.velocity;
		Vector3 velocity = rigid.velocity;
		Vector3 deltaVel = (targetVelocity - velocity);
		deltaVel.x = Mathf.Clamp(deltaVel.x, -5f, 5f);
		deltaVel.z = Mathf.Clamp(deltaVel.z, -5f, 5f);
		deltaVel.y = 0;

		rigid.AddForce(deltaVel, ForceMode.VelocityChange);
		agent.nextPosition = transform.position;
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
