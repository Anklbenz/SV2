using System;
using ClassesForJsonDeserialize;
using Enums;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class OperationView : View, IDisposable {
     [SerializeField] private InfoView info;
     [SerializeField] private Button acceptButton;
     [SerializeField] private ParamButton noteButton, paramsButton, photoButton;
     [SerializeField] private AutoHidePanel autoHidePanel;
     [SerializeField] public OperationListView operationList;
     public event Action NoteClickedEvent, ParamsClickedEvent, PhotoClickedEvent, AcceptClickedEvent;

     public void Initialize(){
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

          acceptButton.onClick.AddListener(delegate { AcceptClickedEvent?.Invoke(); });
     }
     
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
          
          if(paramsRequired /*|| photoRequired*/)
               acceptButton.gameObject.SetActive(false);
     }

     public void AcceptAvailable(ParamState state){
          acceptButton.gameObject.SetActive(state == ParamState.Complete);
     }

     public void ParamUpdate(ParamState state) =>
          paramsButton.SetParamButtonState(state);

     public void PhotoUpdate(ParamState state) =>
          paramsButton.SetParamButtonState(state);

     public void Dispose(){
          paramsButton.button.onClick.RemoveAllListeners();
          noteButton.button.onClick.RemoveAllListeners();
          photoButton.button.onClick.RemoveAllListeners();
     }
}
