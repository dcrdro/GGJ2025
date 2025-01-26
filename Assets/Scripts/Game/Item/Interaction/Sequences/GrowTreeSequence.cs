using System;
using System.Collections;
using DG.Tweening;
using Game.Sequence;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;


public class GrowTreeSequence : BaseSequence
{
	private static readonly int Dissolve = Shader.PropertyToID("_Dissolve");
	
	[SerializeField] private VolumeProfile postProcessingData;
	[SerializeField] private float saturationFrom;
	[SerializeField] private float saturationTo;
	[SerializeField] private float saturationTime = 2f;
	[SerializeField] private GameObject oldTree;
	[SerializeField] private GameObject newTree;
	[SerializeField] private GameObject treeView;
	[SerializeField] private float targetScale;
	[SerializeField] private GameObject sphere;
	[SerializeField] private UnityEvent additional;
	

	private ColorAdjustments _colorAdjustments;
	[SerializeField] private Material leafInstance;
	private bool enableLeaf;
	private bool enableSaturation;
	private float currentDissolve = 1f;
	private float saturation;

	private void Awake()
	{
		if (postProcessingData.TryGet<ColorAdjustments>(typeof(ColorAdjustments), out var component))
		{
			_colorAdjustments = component;
		}
		leafInstance.SetFloat(Dissolve, 0);
	}

	private void Update()
	{
		if (enableLeaf)
		{
			if (currentDissolve > 0.01)
			{
				currentDissolve -= Time.deltaTime/2f;
				if (currentDissolve < 1f)
				{
					leafInstance.SetFloat(Dissolve, currentDissolve);
					Debug.Log(currentDissolve);
				}
			}
		}

		if (enableSaturation)
		{
			if (saturation < saturationTime)
			{
				saturation += Time.deltaTime;
			}
			
			_colorAdjustments.saturation.value = Mathf.Lerp(saturationFrom, saturationTo, saturation/saturationTime);
		}
	}

	public override IEnumerator Sequence()
	{
		yield return new WaitForSeconds(0.5f);
		oldTree.SetActive(false);
		newTree.SetActive(true);
		enableLeaf = true;
		currentDissolve = 1.3f;
		leafInstance.SetFloat(Dissolve, 1f);
		yield return Scale();
	}

	private IEnumerator Scale()
	{
		var tween = treeView.transform.DOScale(new Vector3(targetScale, targetScale, targetScale), 3f).SetEase(Ease.OutSine);
		yield return tween;
		additional.Invoke();
		yield return new WaitForSeconds(1f);
		sphere.gameObject.SetActive(true);
	}
	
	public override float TotalTime => 4f;
}
