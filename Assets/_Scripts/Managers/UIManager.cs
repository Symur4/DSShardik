using Assets._Scripts.UI;
using Assets.Scripts.Core;
using Assets.Scripts.Managers;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class UIManager : Singleton<UIManager>
    {
        [SerializeField]
        private GameObject buildCanvas = null;
        private BuildPanel _buildPanel;

        private void Awake()
        {
            _buildPanel = buildCanvas.GetComponent<BuildPanel>();
        }

        public void ShowBuildPanel()
        {
            _buildPanel.SetButtonsVisible();

            buildCanvas.SetActive(true);
        }

        public void HideBuildPanel()
        {
            buildCanvas.SetActive(false);
        }

        public void OnBuildButtonClick(GameObject buildButton)
        {
            var bb = buildButton.GetComponent<BuildButton>();

            BuildManager.Instance.StartBuilding(bb.BuildingType
                , MapManager.Instance.SelectedTile);

            Debug.Log("Button cliecked:" + bb.BuildingType);
        }
    }
}