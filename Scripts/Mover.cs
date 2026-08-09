using UnityEngine;

public class Mover : MonoBehaviour
{
	public float speed = 6f;

	public Vector3 direction;

	private void Update()
	{
		base.transform.Translate(direction * speed * Time.deltaTime);
	}
}
