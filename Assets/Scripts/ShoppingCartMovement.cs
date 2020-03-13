using UnityEngine;

public class ShoppingCartMovement : MonoBehaviour
{
	[SerializeField] private ShoppingCartInput input;
	[SerializeField] private Rigidbody rigid;

	private void FixedUpdate()
	{
		Move(input.MoveSpeed);
		Turn(input.TurnSpeed);
	}

	private void Move(float moveSpeed)
	{
		float move = Input.GetAxis("Vertical");
		rigid.MovePosition(-transform.forward * (moveSpeed * move * Time.fixedDeltaTime) + transform.position);
	}

	private void Turn(float turnSpeed)
	{
		float turn = Input.GetAxis("Horizontal");
		Vector3 eulerRotation = new Vector3(0, turn * turnSpeed * Time.fixedDeltaTime + transform.rotation.eulerAngles.y, Tilt(turn));
		Quaternion deltaRotation = Quaternion.Euler(eulerRotation);
		rigid.MoveRotation(deltaRotation);
	}

	private float Tilt(float tilt)
	{
		return tilt * input.TiltSpeed * Time.fixedDeltaTime;
	}

	//TODO: poprawić rotację fbx shoppingCart (forward ma być forward);
	//TODO: tilt bez offsetu na obrocie w Y;
	//TODO: reakcja kółek wózka na rotacje i kierunek ruchu;
}
