using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SceneController
{
	public class MoveTutorial : MonoBehaviour
	{
		public static bool isPlayerMoved = false;
		[SerializeField] private CanvasGroup tutorial;
		private bool isTutorialActive;
		private bool isTutorialComplete;

		private Coroutine _fadeOut;

		public void Init(bool isActive)
		{
			if (isActive)
				tutorial.alpha = 1f;
			isTutorialActive = isActive;
		}
		
		public void Update()
		{
			if (!isTutorialActive)
				return;
			
			if (isTutorialComplete)
				return;

			if (isPlayerMoved)
			{
				if (_fadeOut == null)
				{
					_fadeOut = StartCoroutine(FadeOut());
				}
			}
		}

		private IEnumerator FadeOut()
		{
			var tween = tutorial.DOFade(0, 1f);
			yield return tween.WaitForCompletion();
			isTutorialComplete = true;
		}
	}
}