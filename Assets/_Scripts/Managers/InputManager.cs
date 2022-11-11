using Assets.Scripts.Core;
using Assets.Scripts.Managers;
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
        public event Action<Vector3> OnLeftMouseClicked;
        public event Action<KeyCode> OnKeyboardAction;


        private void Update()
        {
            DetectMouseClick();
            DetectKeyboardDown();
        }

        private void DetectKeyboardDown()
        {
            //if (Input.GetKeyDown(KeyCode.B))
            //{
            //    OnKeyDown?.Invoke("B");
            //}

            //if (Input.GetKeyDown(KeyCode.R))
            //{
            //    UIManager.Instance.ToggleResourcePanel();
            //}

            //if (Input.GetKeyDown(KeyCode.S))
            //{
            //    GameManager.Instance.SaveGame();
            //}

            if (Input.GetKeyDown(KeyCode.F2))
            {
                MapManager.Instance.ExploreMap();
            }

            if (Input.GetKeyDown(KeyCode.X))
            {
                GameManager.Instance.Init();                
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                OnKeyboardAction?.Invoke(KeyCode.M);
            }


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

            if (Input.GetMouseButtonDown(0))
            {
                if (!EventSystem.current.IsPointerOverGameObject())
                {
                    OnLeftMouseClicked?.Invoke(Input.mousePosition);
                }
            }
        }
    }
}
