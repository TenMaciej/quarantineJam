using UnityEngine;
using UnityEngine.Events;

public abstract class ShoppingCartInput : MonoBehaviour
{
	[SerializeField] protected float moveSpeed;
	[SerializeField] protected float turnSpeed;
	[SerializeField] protected float maxVelocityDelta;
	[SerializeField] protected Renderer cartRenderer;

	public string colorName;
	public string colorHex;
	public UnityAction<ShoppingCartInput> reachedCounterCallback;
	public int gatheredToiletRolls;

	public float MaxVelocityDelta => maxVelocityDelta;
	public Renderer CartRenderer => cartRenderer;

	public abstract void Init(UnityAction firstRollCallback, string playerColorHex, string playerColorName);

	public abstract float MoveSpeed();

	public abstract float TurnSpeed();

	public abstract void PickItem();
}
