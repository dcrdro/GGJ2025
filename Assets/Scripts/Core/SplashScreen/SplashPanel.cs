using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SceneManagement.Splash
{
	[RequireComponent(typeof(CanvasGroup))]
	public class SplashPanel : BaseSplashPanel
	{
		[SerializeField] public bool isEnabled = true;
		[SerializeField] private CanvasGroup canvasGroup;
		[SerializeField] private float fadeInTime = 1f;
		[SerializeField] private float stayTime = 2f;
		[SerializeField] private float fadeOutTime = 1f;

		public override bool IsVisible()
		{
			return isEnabled;
		}

#if UNITY_EDITOR
		private void OnValidate()
		{
			canvasGroup = GetComponent<CanvasGroup>();
		}
#endif

		public override IEnumerator FadeIn()
		{
			Ready = false;
			var tween = canvasGroup.DOFade(1f, fadeInTime);
			yield return tween.WaitForCompletion();
			yield return new WaitForSecondsRealtime(stayTime);
			Ready = true;
		}

		public override IEnumerator FadeOut()
		{
			var tween = canvasGroup.DOFade(0f, fadeOutTime);
			yield return tween.WaitForCompletion();
		}
	}
}