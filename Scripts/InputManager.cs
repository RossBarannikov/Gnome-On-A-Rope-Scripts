using UnityEngine;

public class InputManager : Singleton<InputManager>
{
	private float _sidewaysMotion;

	public float sidewaysMotion => _sidewaysMotion;

	private void Update()
	{
		_sidewaysMotion = Input.acceleration.x;
	}
}
