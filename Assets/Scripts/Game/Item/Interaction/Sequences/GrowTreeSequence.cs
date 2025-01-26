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
	[SerializeField] private GameObject treeView;
	[SerializeField] private float targetScale;
	[SerializeField] private MeshRenderer treeRenderer;
	[SerializeField] private GameObject sphere;
	[SerializeField] private UnityEvent additional;
	

	private ColorAdjustments _colorAdjustments;
	private Material leafInstance;
	private bool enableLeaf;
	private float currentValue = 1f;
	private float saturation;
	
	
	

	private void Awake()
	{
		if (postProcessingData.TryGet<ColorAdjustments>(typeof(ColorAdjustments), out var component))
		{
			_colorAdjustments = component;
		}
		leafInstance = treeRenderer.materials[1];
	}

	private void Update()
	{
		if (!enableLeaf)
			return;
		
		if (currentValue < 0.1)
		{
			currentValue -= Time.deltaTime;
			leafInstance.SetFloat(Dissolve, currentValue);
		}

		if (saturation < saturationTime)
		{
			saturation += Time.deltaTime;
		}
		
		_colorAdjustments.saturation.value = Mathf.Lerp(saturationFrom, saturationTo, saturation/saturationTime);
	}

	private void OnDestroy()
	{
		Destroy(leafInstance);
	}

	public override IEnumerator Sequence()
	{
		currentValue = 1f;
		StartCoroutine(Appear());
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
	
	private IEnumerator Appear()
	{
		yield return new WaitForSeconds(1f);
		enableLeaf = true;
	}

	public override float TotalTime => 4f;
}
