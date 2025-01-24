using System;
using FMOD.Studio;
using FMODUnity;
using UnityEngine;
using STOP_MODE = FMOD.Studio.STOP_MODE;

namespace Game.Player
{
	[RequireComponent(typeof(Player))]
	public class PlayerSoundController : MonoBehaviour
	{
		[SerializeField] private EventReference footsteps; 
		[SerializeField] private EventReference jump; 
		private Player _player;
		private EventInstance footstepInstance;
		private EventInstance jumpInstance;

		//[SerializeField] private float sceneFootstepsValue;

		private void Awake()
		{
			footstepInstance = RuntimeManager.CreateInstance(footsteps);
			jumpInstance = RuntimeManager.CreateInstance(jump);
		}

		private void Start()
		{
			_player = GetComponent<Player>();
			_player.CurrentState.OnValueChanged += HandlePlayerState;
			
			footstepInstance.set3DAttributes(transform.position.To3DAttributes());
			jumpInstance.set3DAttributes(transform.position.To3DAttributes());
			
			//_footsteps.setParameterByName("footsteps", sceneFootstepsValue);
		}

		private void OnDestroy()
		{
			_player.CurrentState.OnValueChanged -= HandlePlayerState;
			footstepInstance.stop(STOP_MODE.IMMEDIATE);
			footstepInstance.release();
			footstepInstance.clearHandle();


			jumpInstance.stop(STOP_MODE.IMMEDIATE);
			jumpInstance.release();
			jumpInstance.clearHandle();
		}

		private void HandlePlayerState(Player.State prev, Player.State current)
		{

		}

		private void Update()
		{
			UpdateSounds();
		}

		private void UpdateSounds()
		{
			footstepInstance.getPlaybackState(out PLAYBACK_STATE playbackState);
			jumpInstance.getPlaybackState(out PLAYBACK_STATE jumpPlaybackState);
			if (!_player.IsGrounded && jumpPlaybackState == PLAYBACK_STATE.STOPPED)
			{
				jumpInstance.set3DAttributes(transform.position.To3DAttributes());
				jumpInstance.start();
			}
			else
			{
				jumpInstance.stop(STOP_MODE.IMMEDIATE);
			}
			if (_player.CurrentState.Value == Player.State.Running && playbackState == PLAYBACK_STATE.STOPPED && _player.IsGrounded)
			{
				footstepInstance.set3DAttributes(transform.position.To3DAttributes());
				//_footsteps.set3DAttributes(FMODUnity.RuntimeUtils.To3DAttributes(transform.position));
				footstepInstance.start();
			}
			else
			{
				footstepInstance.stop(STOP_MODE.IMMEDIATE);
			}
		}
	}
}