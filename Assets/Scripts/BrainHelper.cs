using Cinemachine;
using UnityEngine;

public class BrainHelper : MonoBehaviour
{
	[SerializeField] private CinemachineVirtualCamera mainVirtualCamera;

	public void AttachCam(Transform player)
	{
		mainVirtualCamera.Follow = player;
		mainVirtualCamera.LookAt = player;
	}
}
