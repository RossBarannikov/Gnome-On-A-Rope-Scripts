using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class BodyPart : MonoBehaviour
{
	public Sprite detachedSprite;

	public Sprite burnedSprite;

	public Transform bloodFountainOrigin;

	private bool detached;

	public void Detach()
	{
		detached = true;
		base.tag = "Untagged";
		base.transform.SetParent(null, worldPositionStays: true);
	}

	public void Update()
	{
		if (detached && GetComponent<Rigidbody2D>().IsSleeping())
		{
			Joint2D[] componentsInChildren = GetComponentsInChildren<Joint2D>();
			for (int i = 0; i < componentsInChildren.Length; i++)
			{
				Object.Destroy(componentsInChildren[i]);
			}
			Rigidbody2D[] componentsInChildren2 = GetComponentsInChildren<Rigidbody2D>();
			for (int i = 0; i < componentsInChildren2.Length; i++)
			{
				Object.Destroy(componentsInChildren2[i]);
			}
			Collider2D[] componentsInChildren3 = GetComponentsInChildren<Collider2D>();
			for (int i = 0; i < componentsInChildren3.Length; i++)
			{
				Object.Destroy(componentsInChildren3[i]);
			}
			Object.Destroy(this);
		}
	}

	public void ApplyDamageSprite(Gnome.DamageType damageType)
	{
		Sprite sprite = null;
		switch (damageType)
		{
		case Gnome.DamageType.Burning:
			sprite = burnedSprite;
			break;
		case Gnome.DamageType.Slicing:
			sprite = detachedSprite;
			break;
		}
		if (sprite != null)
		{
			GetComponent<SpriteRenderer>().sprite = sprite;
		}
	}
}
