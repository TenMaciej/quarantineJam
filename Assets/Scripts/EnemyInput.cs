using DG.Tweening;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyInput : ShoppingCartInput
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
			float singleStep = turnSpeed * Time.fixedDeltaTime;
			Vector3 newDir = Vector3.Lerp(transform.forward, dir, singleStep);
			rigid.MoveRotation(Quaternion.LookRotation(newDir));
		}
		else
		{
			FinishedPath();
		}

	}

	public override float MoveSpeed()
	{
		agent.nextPosition = transform.position + transform.forward * 0.1f;
		return agent.velocity.magnitude * moveSpeed;
	}

	public override float TurnSpeed()
	{
		return 0;
	}

	public override void PickItem()
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
