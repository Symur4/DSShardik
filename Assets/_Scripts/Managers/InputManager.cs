using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Assets._Scripts.Managers
{
    public class InputManager : Singleton<InputManager>
    {
        public event Action<Vector3> OnRightMouseClicked;

        private void Update()
        {
            DetectMouseClick();
        }

        private void DetectMouseClick()
        {
            if (Input.GetMouseButtonDown(1))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    OnRightMouseClicked?.Invoke(Input.mousePosition);
                }
            }
        }
    }
}
