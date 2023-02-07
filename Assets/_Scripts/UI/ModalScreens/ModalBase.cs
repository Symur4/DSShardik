using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets._Scripts.UI.ModalScreens
{
    public class ModalBase : MonoBehaviour
    {
        [Tooltip("String ID from the UXML for this menu panel/screen.")]
        [SerializeField] protected string _modalName;

        [SerializeField] protected UIDocument _document;

        protected VisualElement _modal;
        protected VisualElement _root;

        protected virtual void Awake()
        {
            if (_document == null)
                _document = GetComponent<UIDocument>();

            SetVisualElements();
            RegisterButtonCallbacks();
        }

        protected virtual void SetVisualElements()
        {
            if (_document != null)
                _root = _document.rootVisualElement;

            _modal = GetVisualElement(_modalName);
        }

        protected virtual void RegisterButtonCallbacks()
        {

        }

        public VisualElement GetVisualElement(string name)
        {
            return _root.Q(name);
        }

        internal void ShowModal()
        {
            UIHelper.ShowVisualElement(_modal);
        }

        internal void HideModal()
        {
            UIHelper.HideVisualElement(_modal);
        }
    }
}
