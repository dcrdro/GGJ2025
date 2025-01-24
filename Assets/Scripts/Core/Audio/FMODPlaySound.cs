using UnityEngine;

public class FMODPlaySound : MonoBehaviour
{
	public FMODUnity.EventReference eventReference;

	public void Play()
	{
		FMODUnity.RuntimeManager.PlayOneShot(eventReference);
	}
}