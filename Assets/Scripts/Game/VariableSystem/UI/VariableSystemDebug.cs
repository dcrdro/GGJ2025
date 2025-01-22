using UnityEngine;

public class VariableSystemDebug : MonoBehaviour
{
    [SerializeField] private UIVariable variableTemplate;

    [SerializeField] private VariableSystem variableSystem;
    [SerializeField] private Transform variablesRoot;

    private void Awake()
    {
        variableSystem.OnCreateVariable += CreateVariableView;
    }

    private void CreateVariableView(GameVar gameVar)
    {
        var view = Instantiate(variableTemplate, variablesRoot);
        view.Init(gameVar);
    }

    private void Start()
    {
        foreach (var gameVar in variableSystem.Variables)
        {
            CreateVariableView(gameVar.Value);
        }
    }
}
