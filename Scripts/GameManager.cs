using System.Collections;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
	public GameObject startingPoint;

	public Rope rope;

	public Fade fade;

	public CameraFollow cameraFollow;

	private Gnome currentGnome;

	public GameObject gnomePrefab;

	public RectTransform mainMenu;

	public RectTransform gameplayMenu;

	public RectTransform gameOverMenu;

	public float delayAfterDeath = 1f;

	public AudioClip gnomeDiedSound;

	public AudioClip gameOverSound;

	public bool gnomeInvincible { get; set; }

	private void Start()
	{
		Reset();
	}

	public void Reset()
	{
		if ((bool)gameOverMenu)
		{
			gameOverMenu.gameObject.SetActive(value: false);
		}
		if ((bool)mainMenu)
		{
			mainMenu.gameObject.SetActive(value: false);
		}
		if ((bool)gameplayMenu)
		{
			gameplayMenu.gameObject.SetActive(value: true);
		}
		Resettable[] array = Object.FindObjectsOfType<Resettable>();
		for (int i = 0; i < array.Length; i++)
		{
			array[i].Reset();
		}
		CreateNewGnome();
		Time.timeScale = 1f;
	}

	private void CreateNewGnome()
	{
		RemoveGnome();
		GameObject gameObject = Object.Instantiate(gnomePrefab, startingPoint.transform.position, Quaternion.identity);
		currentGnome = gameObject.GetComponent<Gnome>();
		rope.gameObject.SetActive(value: true);
		rope.connectedObject = currentGnome.ropeBody;
		rope.ResetLength();
		cameraFollow.target = currentGnome.cameraFollowTarget;
	}

	private void RemoveGnome()
	{
		if (gnomeInvincible)
		{
			return;
		}
		rope.gameObject.SetActive(value: false);
		cameraFollow.target = null;
		if (!(currentGnome != null))
		{
			return;
		}
		currentGnome.holdingTreasure = false;
		currentGnome.gameObject.tag = "Untagged";
		foreach (Transform item in currentGnome.transform)
		{
			item.gameObject.tag = "Untagged";
		}
		currentGnome = null;
	}

	private void KillGnome(Gnome.DamageType damageType)
	{
		AudioSource component = GetComponent<AudioSource>();
		if ((bool)component)
		{
			component.PlayOneShot(gnomeDiedSound);
		}
		currentGnome.ShowDamageEffect(damageType);
		if (!gnomeInvincible)
		{
			currentGnome.DestroyGnome(damageType);
			RemoveGnome();
			StartCoroutine(ResetAfterDelay());
		}
	}

	private IEnumerator ResetAfterDelay()
	{
		yield return new WaitForSeconds(delayAfterDeath);
		Reset();
	}

	public void TrapTouched()
	{
		KillGnome(Gnome.DamageType.Slicing);
	}

	public void FireTrapTouched()
	{
		KillGnome(Gnome.DamageType.Burning);
	}

	public void TreasureCollected()
	{
		currentGnome.holdingTreasure = true;
	}

	public void ExitReached()
	{
		if (currentGnome != null && currentGnome.holdingTreasure)
		{
			AudioSource component = GetComponent<AudioSource>();
			if ((bool)component)
			{
				component.PlayOneShot(gameOverSound);
			}
			Time.timeScale = 0f;
			if ((bool)gameOverMenu)
			{
				gameOverMenu.gameObject.SetActive(value: true);
			}
			if ((bool)gameplayMenu)
			{
				gameplayMenu.gameObject.SetActive(value: false);
			}
		}
	}

	public void SetPaused(bool paused)
	{
		if (paused)
		{
			Time.timeScale = 0f;
			mainMenu.gameObject.SetActive(value: true);
			gameplayMenu.gameObject.SetActive(value: false);
		}
		else
		{
			Time.timeScale = 1f;
			mainMenu.gameObject.SetActive(value: false);
			gameplayMenu.gameObject.SetActive(value: true);
		}
	}

	public void RestartGame()
	{
		Object.Destroy(currentGnome.gameObject);
		currentGnome = null;
		Reset();
	}
}
