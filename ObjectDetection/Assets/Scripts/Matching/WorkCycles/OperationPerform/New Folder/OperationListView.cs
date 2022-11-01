using System.Collections.Generic;
using ClassesForJsonDeserialize;
using UnityEngine;

public class OperationListView : MonoBehaviour {
    [Header("OperationsList")]
    [SerializeField] private RectTransform container;
    [SerializeField] private OperationButton prefab;

    private readonly List<OperationButton> _buttonList = new();

    public void AddOperations(Operation[] operations){
        Clear();
            foreach (var operation in operations)
                AddOperationButton(operation.mark_with_zeroes, 1);
    }
    
    private void AddOperationButton(string label, int index){
        var item = Object.Instantiate(prefab, container);
        item.Initialize(label, index);
        _buttonList.Add(item);
    }

    private void Clear(){
        foreach (var button in _buttonList)
            Object.Destroy(button.gameObject);
        _buttonList.Clear();
    }

    public void SetColor(){
        
    }
}