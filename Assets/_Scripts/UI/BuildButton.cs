using Assets._Scripts.TypeConstants;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class BuildButton : MonoBehaviour
    {

        [SerializeField]
        private BuildingType buildingType;

        public BuildingType BuildingType => buildingType;

        private Button _currentButton;

        private void Awake()
        {
            _currentButton = this.gameObject.GetComponent<Button>();
            _currentButton.interactable = false;
        }

        public void Enable()
        {
            _currentButton.interactable = true;
        }

        public void Disable()
        {
            _currentButton.interactable = false;
        }
    }
}