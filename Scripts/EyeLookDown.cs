using UnityEngine;

public class EyeLookDown : MonoBehaviour
{
	public float rotation = -90f;

	private void LateUpdate()
	{
		base.transform.rotation = Quaternion.Euler(0f, 0f, rotation);
	}
}
