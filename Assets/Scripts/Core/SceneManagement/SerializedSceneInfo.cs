using System;
using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
	[Serializable]
	public class SerializedSceneInfo : ISceneInfo
	{
		public string sceneName;
		public bool instant;
		
		public string SceneName => sceneName;
		public bool Instant => instant;
	}
}