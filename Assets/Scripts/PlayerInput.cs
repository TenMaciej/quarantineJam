using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : ShoppingCartInput
{
	[SerializeField] private ToiletPaperDetector detector;
	private UnityAction pickUpCallback;
	public PlayerInputData inputData;

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
