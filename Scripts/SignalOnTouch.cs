using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Collider2D))]
public class SignalOnTouch : MonoBehaviour
{
	public UnityEvent onTouch;

	public bool playAudioOnTouch = true;

	private void OnTriggerEnter2D(Collider2D collider)
	{
		SendSignal(collider.gameObject);
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		SendSignal(collision.gameObject);
	}

	private void SendSignal(GameObject objectThatHit)
	{
		if (!objectThatHit.CompareTag("Player"))
		{
			return;
		}
		if (playAudioOnTouch)
		{
			AudioSource component = GetComponent<AudioSource>();
			if ((bool)component && component.gameObject.activeInHierarchy)
			{
				component.Play();
			}
		}
		onTouch.Invoke();
	}
}
