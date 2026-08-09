using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
	public string sceneToLoad;

	public RectTransform loadingOverlay;

	private AsyncOperation sceneLoadingOperation;

	public void Start()
	{
		loadingOverlay.gameObject.SetActive(value: false);
		sceneLoadingOperation = SceneManager.LoadSceneAsync(sceneToLoad);
		sceneLoadingOperation.allowSceneActivation = false;
	}

	public void LoadScene()
	{
		loadingOverlay.gameObject.SetActive(value: true);
		sceneLoadingOperation.allowSceneActivation = true;
	}
}
