using UnityEngine;

public class Swinging : MonoBehaviour
{
	public float swingSensitivity = 100f;

	private void FixedUpdate()
	{
		if (GetComponent<Rigidbody2D>() == null)
		{
			Object.Destroy(this);
			return;
		}
		float sidewaysMotion = Singleton<InputManager>.instance.sidewaysMotion;
		Vector2 force = new Vector2(sidewaysMotion * swingSensitivity, 0f);
		GetComponent<Rigidbody2D>().AddForce(force);
	}
}
