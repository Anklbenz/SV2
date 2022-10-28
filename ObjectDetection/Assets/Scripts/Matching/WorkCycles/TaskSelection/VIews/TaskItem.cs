using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using ClassesForJsonDeserialize;

public class TaskItem : MonoBehaviour {
    [SerializeField] private TMP_Text typeText, operationIdText, statusText, commentText;
    [SerializeField] private Image statusColor;
    [SerializeField] private Button mainButton;

    public event Action<int> OnClickEvent;
    private int _taskIndex;

    public void SetData(TaskData taskData, int tasksListIndex){
        typeText.text = taskData.row_type_name;
        operationIdText.text = taskData.row_code;
        statusText.text = taskData.row_status_name;
        commentText.text = taskData.row_comment;

        if (ColorUtility.TryParseHtmlString(taskData.row_status_color, out var color))
            statusColor.color = color;

        _taskIndex = tasksListIndex;
        mainButton.onClick.AddListener(OnClick);
    }

    private void OnClick() =>
        OnClickEvent?.Invoke(_taskIndex);

    private void OnDestroy() =>
        mainButton.onClick.RemoveAllListeners();
}