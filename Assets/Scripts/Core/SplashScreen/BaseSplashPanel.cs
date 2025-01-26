using System.Collections;
using UnityEngine;

namespace SceneManagement.Splash
{
	
	public abstract class BaseSplashPanel : MonoBehaviour
	{
		public bool allowFadeOut = true;
		public abstract bool IsVisible();
		public abstract IEnumerator FadeIn();
		public bool Ready { get; protected set; }
		public abstract IEnumerator FadeOut();
	}
}