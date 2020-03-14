using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;

public class EnemyInput : MonoBehaviour
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private ToiletPaperDetector detector;
	[SerializeField] private Rigidbody rigid;
	[SerializeField] private float changeDestinationTimer;

	private Camera camera;
	private float timerToChangeDestination;

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
		if (timerToChangeDestination > 0)
		{
			timerToChangeDestination -= Time.deltaTime;
		}
		else
		{
			timerToChangeDestination = changeDestinationTimer;
			FinishedPath();
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

		if (Vector3.Distance(transform.position, agent.destination) >= agent.stoppingDistance)
		{
			float singleStep = 3f * Time.fixedDeltaTime;
			Vector3 newDir = Vector3.RotateTowards(transform.forward, dir, singleStep, 0f);
			rigid.MoveRotation(Quaternion.LookRotation(newDir));
		}
		else
		{
			FinishedPath();
		}

		Vector3 targetVelocity = transform.forward * agent.velocity.magnitude;
		Vector3 velocity = rigid.velocity;
		Vector3 deltaVel = (targetVelocity - velocity);
		deltaVel.x = Mathf.Clamp(deltaVel.x, -1f, 1f);
		deltaVel.z = Mathf.Clamp(deltaVel.z, -1f, 1f);
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

	private void FinishedPath()
	{
		float randomX = Random.Range(-20f, 20f);
		float randomY = Random.Range(-20f, 20f);

		Vector3 destination = new Vector3(randomX, 0, randomY);
		SetDestination(destination);
	}
}
