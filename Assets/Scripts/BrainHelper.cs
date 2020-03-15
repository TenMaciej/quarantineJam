using Cinemachine;
using UnityEngine;

public class BrainHelper : MonoBehaviour
{
	public CinemachineVirtualCamera[] virtualCameras;
	public Camera[] cameras;

	public void AttachCam(PlayerInput player)
	{
		virtualCameras[player.inputData.cameraId].Follow = player.transform;
		virtualCameras[player.inputData.cameraId].LookAt = player.transform;
	}

	public void AttachCam(Transform target)
	{
		virtualCameras[0].Follow = target;
		virtualCameras[0].LookAt = target;
	}
}
