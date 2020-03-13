using UnityEngine;

public class ShoppingCartInput : MonoBehaviour
{
	[SerializeField] private float moveSpeed;
	[SerializeField] private float turnSpeed;
	[SerializeField] private float tiltSpeed;

	public float MoveSpeed => moveSpeed;

	public float TurnSpeed => turnSpeed;

	public float TiltSpeed => tiltSpeed;
}
