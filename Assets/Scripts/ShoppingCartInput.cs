using UnityEngine;

public abstract class ShoppingCartInput : MonoBehaviour
{
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float turnSpeed;

	public abstract float MoveSpeed();

	public abstract float TurnSpeed();

	public abstract void PickItem();
}
