using System;
using System.Collections.Generic;
using UnityEngine;

public class ShowHideHandler : MonoBehaviour {
   [SerializeField] private List<View> uiViewsList;
   
   public static ShowHideHandler instance{ get; private set; }
   public bool hasDisappearedViews => _disappearedViews.Count > 0;
   
   private readonly List<View> _disappearedViews = new();

   private void Awake(){
      if (instance == null)
         instance = this;
   }

   public void HideEverything(View except = null){
      foreach (var view in uiViewsList){
         if (!view.isActive) continue;
         if (except != null && view == except) continue;

         view.Close();
         _disappearedViews.Add(view);
      }
   }

   public void ShowEverything(){
      if (!hasDisappearedViews) throw new Exception("NotingToAppear");
      foreach (var view in _disappearedViews)
         view.Open();
      
      _disappearedViews.Clear();
   }

}
