using UnityEngine;
using UnityEngine.Events;

public class PlayerInput : ShoppingCartInput
{
	[SerializeField] private ToiletPaperDetector detector;
	private UnityAction pickUpCallback;

	public override void Init(UnityAction firstRollCallback, string playerColorHex, string playerColorName)
	{
		colorHex = playerColorHex;
		colorName = playerColorName;
		Camera.main.GetComponent<BrainHelper>().AttachCam(transform);
		pickUpCallback = firstRollCallback;
	}

	private void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			PickItem();
		}
	}

	public override float MoveSpeed()
	{
		return moveSpeed * Input.GetAxis("Vertical");
	}

	public override float TurnSpeed()
	{
		return turnSpeed * Input.GetAxis("Horizontal");
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
