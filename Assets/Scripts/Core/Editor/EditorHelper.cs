using UnityEditor;
using UnityEditor.SceneManagement;

namespace Core.Editor
{
	public static class EditorHelper
	{
		[MenuItem("Bubbles/Open Hub Scene")]
		public static void OpenHubScene()
		{
			EditorSceneManager.OpenScene("Assets/Scenes/HubScene.unity", OpenSceneMode.Single);
		}
		
		[MenuItem("Bubbles/Open Scene PAST")]
		public static void OpenScenePast()
		{
			EditorSceneManager.OpenScene("Assets/Scenes/PastRoom.unity", OpenSceneMode.Single);
		}
		
		[MenuItem("Bubbles/Open Scene PRESENT")]
		public static void OpenScenePresent()
		{
			EditorSceneManager.OpenScene("Assets/Scenes/PresentRoom.unity", OpenSceneMode.Single);
		}
		
		[MenuItem("Bubbles/Open Scene FUTURE")]
		public static void OpenSceneFuture()
		{
			EditorSceneManager.OpenScene("Assets/Scenes/FutureRoom.unity", OpenSceneMode.Single);
		}
	}
}