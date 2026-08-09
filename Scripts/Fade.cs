using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public class Fade : MonoBehaviour
{
	private float fadeTimeRemaining;

	private float fadeSpeed;

	public void FadeIn()
	{
		SetAlpha(1f);
		FadeTo(0f, 0.5f);
	}

	public void FadeTo(float alpha, float time)
	{
		float a = GetComponent<Image>().color.a;
		float num = alpha - a;
		fadeSpeed = num / time;
		fadeTimeRemaining = time;
	}

	public void SetAlpha(float alpha)
	{
		Color color = GetComponent<Image>().color;
		color.a = alpha;
		GetComponent<Image>().color = color;
	}

	private void Update()
	{
		if (fadeTimeRemaining > 0f)
		{
			fadeTimeRemaining -= Time.deltaTime;
			Color color = GetComponent<Image>().color;
			color.a += fadeSpeed * Time.deltaTime;
			GetComponent<Image>().color = color;
		}
	}
}
