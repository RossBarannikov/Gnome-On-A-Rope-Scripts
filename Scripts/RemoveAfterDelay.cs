using System.Collections;
using UnityEngine;

public class RemoveAfterDelay : MonoBehaviour
{
	public float delay = 1f;

	private void Start()
	{
		StartCoroutine("Remove");
	}

	private IEnumerator Remove()
	{
		yield return new WaitForSeconds(delay);
		Object.Destroy(base.gameObject);
	}
}
