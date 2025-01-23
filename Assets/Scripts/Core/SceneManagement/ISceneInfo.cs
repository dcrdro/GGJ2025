using System.Collections.Generic;
using UnityEngine;

namespace SceneManagement
{
	public interface ISceneInfo
	{
		string SceneName { get; }
		bool Instant { get; }
	}
}