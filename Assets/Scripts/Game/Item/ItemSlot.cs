using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour //, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
	[SerializeField] private Image icon;
	public bool Occupied;
	public EntityInfo EntityInfo => entityInfo;
	
	private EntityInfo entityInfo;

	//public IItemHandler ItemHandler { get; set; }

	public void SetItem(EntityInfo entityInfo)
	{
		this.entityInfo = entityInfo;
		if (entityInfo != null)
		{
			Occupied = true;
			icon.sprite = entityInfo.icon;
			icon.enabled = true;
		}
		else
		{
			Occupied = false;
			icon.enabled = false;
		}
	}

	// public void OnPointerClick(PointerEventData eventData)
	// {
	// 	if (ItemHandler != null && _itemInfo != null)
	// 	{
	// 		ItemHandler.ProcessItem(_itemInfo);
	// 	}
	// }
	//
	// public void OnPointerEnter(PointerEventData eventData)
	// {
	// 	if (_itemInfo != null)
	// 	{
	// 		if (VariableSystem.Instance != null)
	// 		{
	// 			VariableSystem.Instance.ItemOverview.SetItemInfo(_itemInfo);
	// 		}
	// 	}
	// }
	//
	// public void OnPointerExit(PointerEventData eventData)
	// {
	// 	if (VariableSystem.Instance != null)
	// 	{
	// 		VariableSystem.Instance.ItemOverview.SetItemInfo((ItemInfo)null);
	// 	}
	// }
}