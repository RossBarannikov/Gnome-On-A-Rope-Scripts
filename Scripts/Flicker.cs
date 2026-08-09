using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Flicker : MonoBehaviour
{
	[Range(0f, 1f)]
	public float minimumBrightness = 0.5f;

	[Range(0f, 1f)]
	public float maximumBrightness = 1f;

	[Range(0f, 1f)]
	public float flickerStrength = 0.1f;

	private void Update()
	{
		SpriteRenderer component = GetComponent<SpriteRenderer>();
		float num = Random.Range(minimumBrightness, maximumBrightness);
		Color color = component.color;
		color.a *= 1f - flickerStrength;
		color.a += num * flickerStrength;
		component.color = color;
	}
}
