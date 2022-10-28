using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ClassesForJsonDeserialize;
using Cysharp.Threading.Tasks;
using Object = UnityEngine.Object;

[System.Serializable]
public class TasksView : View {
    
    [Header("Task")]
    [SerializeField] private TaskItem itemPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform contentParent;

    public event Action<int> OnSelectEvent;
    public event Action OnCloseEvent;
    
    private readonly List<TaskItem> _taskListItems = new();
    
    public void Initialize(TaskData[] taskList){
        Clear();

        for (var i = 0; i < taskList.Length; ++i)
            AddListItem(taskList[i], i);
       
    }
    
    private void AddListItem(TaskData task, int taskIndex){
        var item = Object.Instantiate(itemPrefab, contentParent);
        item.SetData(task, taskIndex);
        item.OnClickEvent += OnItemClick;

        _taskListItems.Add(item);
    }

    private void Clear(){
        foreach (var item in _taskListItems){
            item.OnClickEvent -= OnItemClick;
            Object.Destroy(item.gameObject);
        }

        _taskListItems.Clear();
    }

    private void OnItemClick(int taskIndex) =>
        OnSelectEvent?.Invoke(taskIndex);

    public override void Open(){
        closeButton.onClick.AddListener(delegate { OnCloseEvent?.Invoke(); });
        base.Open();
    }

    public override void Close(){
        closeButton.onClick.RemoveAllListeners();
        base.Close();
    }
    
}