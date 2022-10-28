    using System;
    using System.Net.Mime;
    using ClassesForJsonDeserialize;
    using UnityEngine;

    public abstract class ControlParamItem : MonoBehaviour {
        public abstract event Action ValueChangedEvent;
        public abstract void Initialize(ControlParam controlParam, bool previewMode = false);

     //   public abstract ControlParam Get(ControlParam param);
        public void Delete() =>
            Destroy(gameObject);
    }

