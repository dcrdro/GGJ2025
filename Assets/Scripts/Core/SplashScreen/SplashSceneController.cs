using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement.Splash
{
	public class SplashSceneController : MonoBehaviour
	{
		[SerializeField] private SerializedSceneInfo hubSceneInfo;
		[SerializeField] private List<BaseSplashPanel> splashPanels;
		[SerializeField] private SerializedSceneInfo nextSceneInfo;
		[SerializeField] private float startDelay = 0.3f;

		private WaitForSecondsRealtime _delay;

		private void Start()
		{
			_delay = new WaitForSecondsRealtime(0.1f);
			StartCoroutine(ProcessSplashScreens());
		}

		private IEnumerator ProcessSplashScreens()
		{
			yield return new WaitForSecondsRealtime(startDelay);
			for (int i = 0, iMax = splashPanels.Count; i < iMax; ++i)
			{
				var splashScreen = splashPanels[i];
				if (!splashScreen.IsVisible())
					continue;
				
				yield return splashScreen.FadeIn();
				while (!splashScreen.Ready)
					yield return _delay;
				yield return splashScreen.FadeOut();
			}
			
			// Switch to MainMenu scene
			yield return null;
			
			var context = new SceneContext();
			context.sceneInfo = nextSceneInfo;
			SceneLoader.LoadScene(context);
		}
	}
}