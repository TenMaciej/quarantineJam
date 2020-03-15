using UnityEngine;

[CreateAssetMenu(fileName = "PlayerInputData", menuName = "PlayerInputData", order = 1)]
public class PlayerInputData : ScriptableObject
{
	public string horizontal;
	public string vertical;
	public string action;
	public int cameraId;
	public float actionCooldown;
}
