using System;
using Cysharp.Threading.Tasks;
using SceneManagement;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace SceneController
{
	public class FutureRoomBootstrap : RoomSceneController
	{
		[SerializeField] private VolumeProfile postProcessingData;
		
		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
			await LoadRoom(sceneContext, progress);
		}

		[ContextMenu("Saturate")]
		public void Saturate()
		{
			if (postProcessingData.TryGet<ColorAdjustments>(typeof(ColorAdjustments), out var component))
			{
				component.saturation.value = 0;
			}
		}
		
		[ContextMenu("Desaturate")]
		public void Desaturate()
		{
			if (postProcessingData.TryGet<ColorAdjustments>(typeof(ColorAdjustments), out var component))
			{
				component.saturation.value = -100;
			}
		}
	}
}