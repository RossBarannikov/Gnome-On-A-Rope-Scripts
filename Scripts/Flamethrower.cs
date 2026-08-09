using System.Collections;
using UnityEngine;

public class Flamethrower : MonoBehaviour
{
	public Sprite activeSprite;

	public Sprite inactiveSprite;

	public SpriteRenderer spriteRenderer;

	public GameObject fireballPrefab;

	public float timeBetweenShots = 4f;

	public float timeToCoolDown = 0.2f;

	public Transform emissionPoint;

	public float timeToStart = 1f;

	public AudioClip fireballSound;

	private void Start()
	{
		spriteRenderer.sprite = inactiveSprite;
		StartCoroutine("ShootFireballs");
	}

	private IEnumerator ShootFireballs()
	{
		yield return new WaitForSeconds(timeToStart);
		while (true)
		{
			StartCoroutine("Cooldown");
			if (fireballPrefab != null)
			{
				AudioSource component = GetComponent<AudioSource>();
				if ((bool)component && (bool)fireballSound)
				{
					component.PlayOneShot(fireballSound);
				}
				GameObject obj = Object.Instantiate(fireballPrefab, emissionPoint.position, Quaternion.identity);
				obj.GetComponent<Mover>().direction = base.transform.right;
				obj.GetComponent<SignalOnTouch>().onTouch.AddListener(delegate
				{
					Singleton<GameManager>.instance.FireTrapTouched();
				});
			}
			yield return new WaitForSeconds(timeBetweenShots);
		}
	}

	private IEnumerator Cooldown()
	{
		spriteRenderer.sprite = activeSprite;
		yield return new WaitForSeconds(timeToCoolDown);
		spriteRenderer.sprite = inactiveSprite;
	}
}
