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

			_player.OnJump += HandleJump;
			
			footstepInstance.set3DAttributes(transform.position.To3DAttributes());
			jumpInstance.set3DAttributes(transform.position.To3DAttributes());
		}
		
		private void Update()
		{
			UpdateSounds();
		}

		private void OnDestroy()
		{
			_player.CurrentState.OnValueChanged -= HandlePlayerState;
			
			_player.OnJump -= HandleJump;
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

		private void UpdateSounds()
		{
			footstepInstance.getPlaybackState(out PLAYBACK_STATE footstepState);
			
			if (_player.CurrentState.Value == Player.State.Running)
			{
				if (footstepState == PLAYBACK_STATE.STOPPED && _player.IsGrounded)
				{
					footstepInstance.set3DAttributes(transform.position.To3DAttributes());
					footstepInstance.start();
				}
			}
			else if (footstepState == PLAYBACK_STATE.PLAYING)
			{
				footstepInstance.stop(STOP_MODE.IMMEDIATE);
			}
		}
	}
}