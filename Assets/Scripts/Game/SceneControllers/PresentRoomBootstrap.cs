using System;
using Cysharp.Threading.Tasks;
using SceneManagement;

namespace SceneController
{
	public class PresentRoomBootstrap : RoomSceneController
	{
		public override async UniTask Load(SceneContext sceneContext, IProgress<LoadingProgress> progress)
		{
			await LoadRoom(sceneContext, progress);
		}
	}
}