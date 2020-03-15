using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : ShoppingCartInput
{
	[SerializeField] private ToiletPaperDetector detector;
	private UnityAction pickUpCallback;
	public PlayerInputData inputData;
	public float actionCooldown;

	public override void Init(UnityAction firstRollCallback, string playerColorHex, string playerColorName)
	{
		colorHex = playerColorHex;
		colorName = playerColorName;
		pickUpCallback = firstRollCallback;
	}

	public void SetInput(PlayerInputData playerInputData)
	{
		inputData = playerInputData;
		Camera.main.GetComponent<BrainHelper>().AttachCam(this);
	}

	private void Update()
	{
		PickItem();
		if (actionCooldown > 0)
		{
			actionCooldown -= Time.deltaTime;
		}
		if (Input.GetButtonDown(inputData.action) && actionCooldown <= 0)
		{
			actionCooldown = inputData.actionCooldown;
			rigid.AddForce(transform.forward * 200f, ForceMode.Impulse);
		}
	}

	public override float MoveSpeed()
	{
		return moveSpeed * Input.GetAxis(inputData.vertical);
	}

	public override float TurnSpeed()
	{
		return turnSpeed * Input.GetAxis(inputData.horizontal);
	}

	public override void PickItem()
	{
		if (detector.nearToiletPaperColliders == null || detector.nearToiletPaperColliders.Length <= 0)
			return;

		if (detector.CanPick())
		{
			detector.PickRoll();
			pickUpCallback?.Invoke();
			pickUpCallback = null;
		}
	}
}
