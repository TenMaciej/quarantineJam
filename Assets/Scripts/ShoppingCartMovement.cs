using UnityEngine;
using UnityEngine.AI;

public class ShoppingCartMovement : MonoBehaviour
{
	[SerializeField] private ShoppingCartInput input;
	[SerializeField] private Rigidbody rigid;
	[SerializeField] private Transform[] frontWheels;
	[SerializeField] private Transform[] rearWheels;
	[SerializeField] private NavMeshAgent agent;

	private void Start()
	{
		agent.updateRotation = false;
		agent.updatePosition = false;
	}

	private void Update()
	{
		agent.nextPosition = transform.position;
		if (Input.GetKeyDown(KeyCode.Space))
		{
			input.PickItem();
		}
	}

	private void FixedUpdate()
	{
		Move(input.MoveSpeed());
		Turn(input.TurnSpeed());
	}

	private void Move(float moveSpeed)
	{
		Vector3 targetVelocity = transform.forward * moveSpeed;
		Vector3 velocity = rigid.velocity;
		Vector3 deltaVel = (targetVelocity - velocity);
		deltaVel.x = Mathf.Clamp(deltaVel.x, -input.MaxVelocityDelta, input.MaxVelocityDelta);
		deltaVel.z = Mathf.Clamp(deltaVel.z, -input.MaxVelocityDelta, input.MaxVelocityDelta);
		deltaVel.y = 0;

		rigid.AddForce(deltaVel, ForceMode.VelocityChange);
	}

	private void Turn(float turnSpeed)
	{
		float turnMultiply = turnSpeed * Time.fixedDeltaTime;
		Vector3 eulerRotation = new Vector3(0, turnMultiply + transform.rotation.eulerAngles.y, 0);
		Quaternion deltaRotation = Quaternion.Euler(eulerRotation);
		rigid.MoveRotation(deltaRotation);
	}
}
