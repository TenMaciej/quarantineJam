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

		if (detector.CanPick())
		{
			PickItem();
		}
	}

	private void FixedUpdate()
	{
		Vector3 dir = agent.nextPosition - transform.position;
		dir.Normalize();
		dir.y = 0;

		if (agent.velocity.magnitude >= agent.stoppingDistance)
		{
			float singleStep = 2f * Time.fixedDeltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, singleStep, 0f);
			rigid.MoveRotation(Quaternion.LookRotation(newDir));
		}

		Vector3 targetVelocity = transform.forward * agent.velocity.magnitude;
		Vector3 velocity = rigid.velocity;
		Vector3 deltaVel = (targetVelocity - velocity);
		deltaVel.x = Mathf.Clamp(deltaVel.x, -10f, 10f);
		deltaVel.z = Mathf.Clamp(deltaVel.z, -10f, 10f);
		deltaVel.y = 0;

		rigid.AddForce(deltaVel, ForceMode.VelocityChange);
		agent.nextPosition = transform.position + transform.forward * 0.1f;
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
