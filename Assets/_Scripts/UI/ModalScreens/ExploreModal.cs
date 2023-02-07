using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Scripts.UI.ModalScreens
{
    public class ExploreModal : ModalBase
    {
        public static event Action OnExploreCliecked;

        // string IDs
        const string _exploreButtonName = "explore-button";

        private VisualElement _exploreButton;

        private void OnEnable()
        {
            
        }

        protected override void SetVisualElements()
        {
            base.SetVisualElements();

            _exploreButton = _root.Q<Button>(name: _exploreButtonName);
        }

        protected override void RegisterButtonCallbacks()
        {
            base.RegisterButtonCallbacks();

            _exploreButton?.RegisterCallback<ClickEvent>(OnExplore);
            
        }

        private void OnExplore(ClickEvent evt)
        {
            HideModal();
            OnExploreCliecked?.Invoke();
        }
    }
}
