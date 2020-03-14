using UnityEngine;

public class ToiletPaperDetector : MonoBehaviour
{
	[SerializeField] private float detectionRadius;
	[SerializeField] private LayerMask toiletPaperLayer;
	public Collider[] nearToiletPaperColliders;

	private void Update()
	{
		nearToiletPaperColliders = Physics.OverlapSphere(transform.position, detectionRadius, toiletPaperLayer);
		foreach (Collider tolietPaperCollider in nearToiletPaperColliders)
		{

		}
	}
}
