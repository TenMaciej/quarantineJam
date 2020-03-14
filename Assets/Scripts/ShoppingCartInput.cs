using UnityEngine;

public abstract class ShoppingCartInput : MonoBehaviour
{
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float turnSpeed;
	[SerializeField] protected float maxVelocityDelta;

	public float MaxVelocityDelta => maxVelocityDelta;

	public abstract float MoveSpeed();

	public abstract float TurnSpeed();

	public abstract void PickItem();
}
