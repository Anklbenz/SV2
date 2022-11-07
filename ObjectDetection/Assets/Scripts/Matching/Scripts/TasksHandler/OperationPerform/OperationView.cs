using System;
using ClassesForJsonDeserialize;
using Enums;
using UnityEngine;
using UnityEngine.UI;

public class OperationView : View {
     [SerializeField] private InfoView info;

     [SerializeField] private Button acceptButton;
     [SerializeField] private AddButton addButton;
     [SerializeField] private ParamButton noteButton, paramsButton, photoButton;
     [SerializeField] private AutoHidePanel autoHidePanel;
     [SerializeField] public OperationListView operationList;
     public event Action NoteClickedEvent, ParamsClickedEvent, PhotoClickedEvent, AcceptClickedEvent;

     public void Show(Operation operation){
          var paramsRequired = operation.control_params_count > 0;
          var photoRequired = operation.is_foto_required;

          info.Show(operation.name, operation.description);
          paramsButton.SetActive(paramsRequired);

          noteButton.SetClear();

          if (paramsRequired)
               paramsButton.SetWarning();
          else
               paramsButton.SetClear();

          if (photoRequired)
               photoButton.SetWarning();
          else
               photoButton.SetClear();
     }

     public void ReadyToNext(bool isReady){
          if (isReady) addButton.SetStandard();
          else addButton.SetWarning();

          acceptButton.gameObject.SetActive(isReady);
     }

     public void ParamUpdate(ParamState state) =>
          paramsButton.SetParamButtonState(state);

     public void PhotoUpdate(ParamState state) =>
          paramsButton.SetParamButtonState(state);

     public void OnEnable(){
          paramsButton.button.onClick.AddListener(delegate
          {
               ParamsClickedEvent?.Invoke();
               autoHidePanel.Hide();
          });
          noteButton.button.onClick.AddListener(delegate
          {
               NoteClickedEvent?.Invoke();
               autoHidePanel.Hide();
          });
          photoButton.button.onClick.AddListener(delegate
          {
               PhotoClickedEvent?.Invoke();
               autoHidePanel.Hide();
          });

          addButton.button.onClick.AddListener(delegate { autoHidePanel.Show(); });
          acceptButton.onClick.AddListener(delegate { AcceptClickedEvent?.Invoke(); });
     }

     public void OnDisable(){
          paramsButton.button.onClick.RemoveAllListeners();
          noteButton.button.onClick.RemoveAllListeners();
          photoButton.button.onClick.RemoveAllListeners();
     }
}