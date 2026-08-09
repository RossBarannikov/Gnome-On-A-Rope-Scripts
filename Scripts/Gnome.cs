using System.Collections;
using UnityEngine;

public class Gnome : MonoBehaviour
{
	public enum DamageType
	{
		Slicing,
		Burning
	}

	public Transform cameraFollowTarget;

	public Rigidbody2D ropeBody;

	public Sprite armHoldingEmpty;

	public Sprite armHoldingTreasure;

	public SpriteRenderer holdingArm;

	public GameObject deathPrefab;

	public GameObject flameDeathPrefab;

	public GameObject ghostPrefab;

	public float delayBeforeRemoving = 3f;

	public float delayBeforeReleasingGhost = 0.25f;

	public GameObject bloodFountainPrefab;

	private bool dead;

	private bool _holdingTreasure;

	public bool holdingTreasure
	{
		get
		{
			return _holdingTreasure;
		}
		set
		{
			if (dead)
			{
				return;
			}
			_holdingTreasure = value;
			if (holdingArm != null)
			{
				if (_holdingTreasure)
				{
					holdingArm.sprite = armHoldingTreasure;
				}
				else
				{
					holdingArm.sprite = armHoldingEmpty;
				}
			}
		}
	}

	public void ShowDamageEffect(DamageType type)
	{
		switch (type)
		{
		case DamageType.Burning:
			if (flameDeathPrefab != null)
			{
				Object.Instantiate(flameDeathPrefab, cameraFollowTarget.position, cameraFollowTarget.rotation);
			}
			break;
		case DamageType.Slicing:
			if (deathPrefab != null)
			{
				Object.Instantiate(deathPrefab, cameraFollowTarget.position, cameraFollowTarget.rotation);
			}
			break;
		}
	}

	public void DestroyGnome(DamageType type)
	{
		holdingTreasure = false;
		dead = true;
		BodyPart[] componentsInChildren = GetComponentsInChildren<BodyPart>();
		foreach (BodyPart bodyPart in componentsInChildren)
		{
			switch (type)
			{
			case DamageType.Burning:
				if (Random.Range(0, 2) == 0)
				{
					bodyPart.ApplyDamageSprite(type);
				}
				break;
			case DamageType.Slicing:
				bodyPart.ApplyDamageSprite(type);
				break;
			}
			if (Random.Range(0, 2) == 0)
			{
				bodyPart.Detach();
				if (type == DamageType.Slicing && bodyPart.bloodFountainOrigin != null && bloodFountainPrefab != null)
				{
					Object.Instantiate(bloodFountainPrefab, bodyPart.bloodFountainOrigin.position, bodyPart.bloodFountainOrigin.rotation).transform.SetParent(cameraFollowTarget, worldPositionStays: false);
				}
				Joint2D[] componentsInChildren2 = bodyPart.GetComponentsInChildren<Joint2D>();
				for (int j = 0; j < componentsInChildren2.Length; j++)
				{
					Object.Destroy(componentsInChildren2[j]);
				}
			}
		}
		base.gameObject.AddComponent<RemoveAfterDelay>().delay = delayBeforeRemoving;
		StartCoroutine(ReleaseGhost());
	}

	private IEnumerator ReleaseGhost()
	{
		if (!(ghostPrefab == null))
		{
			yield return new WaitForSeconds(delayBeforeReleasingGhost);
			Object.Instantiate(ghostPrefab, base.transform.position, Quaternion.identity);
		}
	}
}
