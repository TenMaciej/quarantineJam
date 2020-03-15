using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class EnemyInput : ShoppingCartInput
{
	[SerializeField] private NavMeshAgent agent;
	[SerializeField] private ToiletPaperDetector detector;
	[SerializeField] private float changeDestinationTimer;

	private UnityAction pickUpCallback;
	private Camera camera;
	private float timerToChangeDestination;
	private float randomDashTimer;

	public void SetDestination(Vector3 destination)
	{
		agent.SetDestination(destination);
	}

	private void Start()
	{
		camera = Camera.main;
		agent.updatePosition = false;
		agent.updateRotation = false;
		randomDashTimer = Random.Range(2f, 4f);
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

		if (EnemyInFront() || RandomDash())
		{
			rigid.AddForce(transform.forward * 100f, ForceMode.Impulse);
			randomDashTimer = Random.Range(2f, 4f);
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

	public override void Init(UnityAction firstRollCallback, string playerColorHex, string playerColorName)
	{
		colorHex = playerColorHex;
		colorName = playerColorName;
		pickUpCallback = firstRollCallback;
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

		detector.PickRoll();
		pickUpCallback?.Invoke();
		pickUpCallback = null;
	}

	private void FinishedPath()
	{
		float randomX = Random.Range(-20f, 20f);
		float randomY = Random.Range(-20f, 20f);

		Vector3 destination = new Vector3(randomX, 0, randomY);
		SetDestination(destination);
	}

	private bool EnemyInFront()
	{
		Ray ray = new Ray(transform.position + Vector3.up * 0.05f, Vector3.forward * 3f);
		if (Physics.Raycast(ray, out RaycastHit hit))
		{
			if (hit.collider.gameObject.GetComponent<ShoppingCartInput>())
				return true;
		}

		return false;
	}

	private bool RandomDash()
	{
		if (randomDashTimer > 0)
		{
			randomDashTimer -= Time.deltaTime;
			return false;
		}

		randomDashTimer = Random.Range(2f, 4f);
		return true;
	}
}
