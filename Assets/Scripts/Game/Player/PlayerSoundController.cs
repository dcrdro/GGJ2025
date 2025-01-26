using FMOD.Studio;
using Game.Player;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;


public class PlayerSoundController : MonoBehaviour
{
	[SerializeField] private EventReference footsteps;
	[SerializeField] private EventReference jump;
	[SerializeField] private EventReference land;
	[SerializeField] private Player _player;
	private EventInstance leftFootstepInstance;
	private EventInstance rightFootstepInstance;

	private EventInstance jumpInstance;
	private EventInstance landInstance;

	//[SerializeField] private float sceneFootstepsValue;

	private void Awake()
	{
		leftFootstepInstance = RuntimeManager.CreateInstance(footsteps);
		rightFootstepInstance = RuntimeManager.CreateInstance(footsteps);
		jumpInstance = RuntimeManager.CreateInstance(jump);
		landInstance = RuntimeManager.CreateInstance(land);
	}

	private void Start()
	{
		//_player = GetComponent<Player>();
		_player.CurrentState.OnValueChanged += HandlePlayerState;

		_player.OnJump += HandleJump;
		_player.OnLand += HandleLand;

		leftFootstepInstance.set3DAttributes(transform.position.To3DAttributes());
		rightFootstepInstance.set3DAttributes(transform.position.To3DAttributes());
		jumpInstance.set3DAttributes(transform.position.To3DAttributes());
	}
	
	private void OnDestroy()
	{
		_player.CurrentState.OnValueChanged -= HandlePlayerState;

		_player.OnJump -= HandleJump;

		rightFootstepInstance.stop(STOP_MODE.IMMEDIATE);
		rightFootstepInstance.release();
		rightFootstepInstance.clearHandle();


		leftFootstepInstance.stop(STOP_MODE.IMMEDIATE);
		leftFootstepInstance.release();
		leftFootstepInstance.clearHandle();


		jumpInstance.stop(STOP_MODE.IMMEDIATE);
		jumpInstance.release();
		jumpInstance.clearHandle();
	}

	private void HandlePlayerState(Player.State prev, Player.State current)
	{
	}

	private void HandleJump()
	{
		jumpInstance.getPlaybackState(out PLAYBACK_STATE jumpPlaybackState);
		if (jumpPlaybackState == PLAYBACK_STATE.PLAYING)
		{
			jumpInstance.stop(STOP_MODE.IMMEDIATE);
		}

		jumpInstance.set3DAttributes(transform.position.To3DAttributes());
		jumpInstance.start();
	}

	private void HandleLand()
	{
		landInstance.getPlaybackState(out PLAYBACK_STATE landPlaybackState);
		if (landPlaybackState == PLAYBACK_STATE.PLAYING)
		{
			landInstance.stop(STOP_MODE.IMMEDIATE);
		}

		landInstance.set3DAttributes(transform.position.To3DAttributes());
		landInstance.start();
	}

	public void OnLeftFoot()
	{
		leftFootstepInstance.getPlaybackState(out PLAYBACK_STATE state);
		if (state == PLAYBACK_STATE.PLAYING)
		{
			leftFootstepInstance.stop(STOP_MODE.ALLOWFADEOUT);
		}

		leftFootstepInstance.set3DAttributes(transform.position.To3DAttributes());
		leftFootstepInstance.start();
	}

	public void OnRightFoot()
	{
		rightFootstepInstance.getPlaybackState(out PLAYBACK_STATE state);
		if (state == PLAYBACK_STATE.PLAYING)
		{
			rightFootstepInstance.stop(STOP_MODE.ALLOWFADEOUT);
		}

		rightFootstepInstance.set3DAttributes(transform.position.To3DAttributes());
		rightFootstepInstance.start();
	}
}