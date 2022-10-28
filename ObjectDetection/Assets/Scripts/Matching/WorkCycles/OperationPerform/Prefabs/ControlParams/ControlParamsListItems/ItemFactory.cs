using System;
using UnityEngine;
using Object = UnityEngine.Object;

[System.Serializable]
public class ItemFactory {
   private const string VALUE_CODE = "CP_VALUE";
   private const string STATE_CODE = "CP_STATE";
   private const string DROPDOWN_CODE = "CP_DROP_DOWN";

   [SerializeField] private InputItem inputPrefab;
   [SerializeField] private StateItem statePrefab;
   [SerializeField] private DropDownItem dropdownPrefab;

   public ControlParamItem MenuItemCreate(string gettingType, Transform parent){
      switch (gettingType){
         case VALUE_CODE:
            return Get(inputPrefab, parent);
         case STATE_CODE:
            return Get(statePrefab, parent);
         case DROPDOWN_CODE:
            return Get(dropdownPrefab, parent);
      }

      throw new ArgumentException($"Control param menu element {gettingType} not found");
   }

   private T Get<T>(T prefabType, Transform parent) where T : MonoBehaviour{
      return Object.Instantiate(prefabType, parent);
   }
}