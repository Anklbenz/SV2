using System;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using ClassesForJsonDeserialize;
using Object = UnityEngine.Object;

public class TasksView : View {
    [SerializeField] private TaskItem itemPrefab;
    [SerializeField] private Button closeButton;
    [SerializeField] private RectTransform contentParent;

    public event Action<int> OnSelectEvent;
    public event Action OnCloseEvent;

    private readonly List<TaskItem> _taskListItems = new();

    public void Initialize(TaskData[] taskList){
        Clear();

        for (var i = 0; i < taskList.Length; ++i)
            Add(taskList[i], i);
    }

    private void Add(TaskData task, int taskIndex){
        var item = Object.Instantiate(itemPrefab, contentParent);
        item.SetData(task, taskIndex);
        item.OnClickEvent += OnItemSelect;

        _taskListItems.Add(item);
    }

    private void Clear(){
        foreach (var item in _taskListItems){
            item.OnClickEvent -= OnItemSelect;
            Object.Destroy(item.gameObject);
        }

        _taskListItems.Clear();
    }

    private void OnItemSelect(int taskIndex) =>
        OnSelectEvent?.Invoke(taskIndex);

    private void OnEnable() =>
        closeButton.onClick.AddListener(delegate { OnCloseEvent?.Invoke(); });

    private void OnDisable() =>
        closeButton.onClick.RemoveAllListeners();
}