using System.Collections.Generic;
using UnityEngine;
using Game.Core;

public class Inventory : MonoBehaviour
{
    [SerializeField] private List<ItemSlot> slots;
    
    private VariableSystem _variableSystem;

    public void Init(VariableSystem variableSystem)
    {
        _variableSystem = variableSystem;
    }

    public List<EntityInfo> GetItems()
    {
        List<EntityInfo> items = new List<EntityInfo>();
        foreach (var slot in slots)
        {
            if (slot.Occupied)
            {
                items.Add(slot.EntityInfo);
            }
        }
        return items;
    }

    public void AddItem(EntityInfo entityInfo)
    {
        _variableSystem.SetVariable(entityInfo.variableName + Entity.TakenSuffix, "true", true);
        foreach (ItemSlot slot in slots)
        {
            if (slot.Occupied) 
                continue;
            slot.Occupied = true;
            slot.SetItem(entityInfo);
            break;
        }
    }

    public void RemoveItem(EntityInfo entityInfo)
    {
        foreach (ItemSlot slot in slots)
        {
            if (slot.Occupied && slot.EntityInfo == entityInfo)
            {
                slot.Occupied = false;
                _variableSystem.SetVariable(entityInfo.variableName + Entity.TakenSuffix, "false", true);
                slot.SetItem(null);
                break;
            }
        }
    }
}
