using UnityEngine;

public class ToiletPaperDetector : MonoBehaviour
{
	[SerializeField] private float detectionRadius;
	[SerializeField] private LayerMask toiletPaperLayer;
	[SerializeField] private LayerMask toiletPaperInsideLayer;
	[SerializeField] private float pickCooldown;
	public Collider[] nearToiletPaperColliders;

	private float cooldown;

	private void Update()
	{
		GetNearToiletPapers();
		cooldown -= Time.deltaTime;
		cooldown = Mathf.Max(0, cooldown);
	}

	private void GetNearToiletPapers()
	{
		nearToiletPaperColliders = Physics.OverlapSphere(transform.position, detectionRadius, toiletPaperLayer);
	}

	public bool CanPick()
	{
		if (cooldown <= 0)
		{
			cooldown = pickCooldown;
			return true;
		}

		return false;
	}
}
