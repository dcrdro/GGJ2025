using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace SceneController
{
	public class TeleportTutorial : MonoBehaviour
	{
		[SerializeField] private bool isPlayerMoved = false;
		[SerializeField] private CanvasGroup tutorial;
		[SerializeField] private GameObject player;
		
		private bool isTutorialActive;
		private bool isTutorialComplete;

		private Coroutine _fadeOut;
		
		public void Update()
		{
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

		private void OnTriggerExit(Collider other)
		{
			isPlayerMoved = true;
		}

		private IEnumerator FadeOut()
		{
			var tween = tutorial.DOFade(0, 1f);
			yield return tween.WaitForCompletion();
			isTutorialComplete = true;
		}
	}
}