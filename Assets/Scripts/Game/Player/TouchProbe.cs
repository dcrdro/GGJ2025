
using UnityEngine;

namespace Game.Player
{
	public class TouchProbe : MonoBehaviour
	{
		public bool IsTouching { get; private set; }
		
		private void Start()
		{
			IsTouching = false;
		}

		private void OnTriggerEnter(Collider other)
		{
			IsTouching = true;
		}
		
		private void OnTriggerExit(Collider other)
		{
			IsTouching = false;
		}
	}
}