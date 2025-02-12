using System;
using System.Collections.Generic;
using UnityEngine;

public class VariableSystem : MonoBehaviour
{
    public static VariableSystem Instance { get; private set; }
    public event Action<GameVar> OnCreateVariable;
    
    public Inventory Inventory;
    

    public List<VariableContext> initialVariables;
    public IReadOnlyDictionary<string, GameVar> Variables => _gameVariables; 
    
    
    [SerializeField] private List<EntityInfo> debugItems;
    
    private readonly Dictionary<string, GameVar> _gameVariables = new Dictionary<string, GameVar>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializeSystem();
            DontDestroyOnLoad(gameObject);
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSystem()
    {
        Inventory.Init(this);
        foreach (var variable in initialVariables)
        {
            var gameVar = new GameVar(variable.name, variable.value);
            _gameVariables.Add(variable.name, gameVar);
        }
#if UNITY_EDITOR
        foreach (EntityInfo debugItem in debugItems)
        {
            if (debugItem != null)
            {
                Inventory.AddItem(debugItem);
            }
        }
#endif
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public GameVar GetVariable(string variableName, bool createVariableIfMissing = false )
    {
        if (_gameVariables.TryGetValue(variableName, out var gameVar))
        {
            return gameVar;
        }

        if (createVariableIfMissing)
        {
            var variable = CreateVariable(variableName, "");
            return variable;
        }
        return null;
    }
    
    public bool SetVariable(string variableName, string newValue, bool createVariable = false)
    {
        if (string.IsNullOrEmpty(variableName))
            return false;
        
        if (_gameVariables.TryGetValue(variableName, out GameVar gameVar))
        {
            gameVar.Value = newValue;
            return true;
        }
        if (createVariable)
        {
            CreateVariable(variableName, newValue);
            return true;
        }
        Debug.LogWarning($"[GameVar] Variable {variableName} doesn't exists");
        return false;
    }
    
    public GameVar CreateVariable(string variableName, string initialValue)
    {
        if (string.IsNullOrEmpty(variableName))
            return null;
        
        var gameVar = new GameVar(variableName, initialValue);
        if (_gameVariables.TryAdd(variableName, gameVar))
        {
            OnCreateVariable?.Invoke(gameVar);
            return gameVar;
        }
        else
        {
            Debug.LogWarning($"[GameVar] Variable {variableName} already exists");
            return null;
        }
    }
}
