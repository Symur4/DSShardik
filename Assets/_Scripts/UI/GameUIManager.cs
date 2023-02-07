using Assets._Scripts.UI.ModalScreens;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
namespace Assets._Scripts.UI
{
    [RequireComponent(typeof(UIDocument))]
    public class GameUIManager : MonoBehaviour
    {
        private UIDocument _document;

        [SerializeField] ExploreModal _exploreModal;

        List<ModalBase> _allModalScreens = new List<ModalBase>();

        private void OnEnable()
        {
            _document = GetComponent<UIDocument>();
            _document.rootVisualElement.style.display = DisplayStyle.Flex;

            SetupModals();
        }

        private void SetupModals()
        {
            if(_exploreModal != null )
            {
                _exploreModal.HideModal();
                _allModalScreens.Add( _exploreModal );
            }


        }

        public void ShowExploreModal()
        {
            ShowModalScreen(_exploreModal);
        }

        void ShowModalScreen(ModalBase modalScreen)
        {
            foreach (var m in _allModalScreens)
            {
                if (m == modalScreen)
                {
                    m?.ShowModal();
                }
                else
                {
                    m?.HideModal();
                }
            }
        }

        void ShowVisualElement(VisualElement visualElement)
        {
            if (visualElement == null)
                return;

            visualElement.style.display = DisplayStyle.Flex;
        }

        void HideVisualElement(VisualElement visualElement)
        {
            if (visualElement == null)
                return;

            visualElement.style.display = DisplayStyle.None;
        }

    }
}
