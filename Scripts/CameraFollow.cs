using UnityEngine;

public class CameraFollow : MonoBehaviour
{
	public Transform target;

	public float topLimit = 10f;

	public float bottomLimit = -10f;

	public float followSpeed = 0.5f;

	private void LateUpdate()
	{
		if (target != null)
		{
			Vector3 position = base.transform.position;
			position.y = Mathf.Lerp(position.y, target.position.y, followSpeed);
			position.y = Mathf.Min(position.y, topLimit);
			position.y = Mathf.Max(position.y, bottomLimit);
			base.transform.position = position;
		}
	}

	private void OnDrawGizmosSelected()
	{
		Gizmos.color = Color.yellow;
		Vector3 vector = new Vector3(base.transform.position.x, topLimit, base.transform.position.z);
		Vector3 to = new Vector3(base.transform.position.x, bottomLimit, base.transform.position.z);
		Gizmos.DrawLine(vector, to);
	}
}
