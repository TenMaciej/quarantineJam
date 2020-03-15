using UnityEngine;

[CreateAssetMenu(fileName = "PlayerScreenRect", menuName = "PlayerScreenRect", order = 2)]
public class PlayerScreenRectScriptable : ScriptableObject
{
	public PlayerCountVariant[] variants;

	[System.Serializable]
	public class PlayerCountVariant
	{
		public Rect[] screenRect;
	}

	[System.Serializable]
	public class ScreenRect
	{
		public float x;
		public float y;
		public float width;
		public float height;
	}
}
