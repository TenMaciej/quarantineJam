using DG.Tweening;
using UnityEngine;

public class PlayerInput : ShoppingCartInput
{
	[SerializeField] private ToiletPaperDetector detector;

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
			Transform toiletRoll = detector.nearToiletPaperColliders[0].transform;
			toiletRoll.SetParent(transform);
			toiletRoll.DOLocalMove(Vector3.up, 0.2f);
		}
	}
}
