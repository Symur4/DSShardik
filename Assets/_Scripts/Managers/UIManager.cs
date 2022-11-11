using Assets._Scripts.Buildings;
using Assets._Scripts.TypeConstants;
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
        [SerializeField]
        private GameObject resourceInfoCanvas = null;
        [SerializeField]
        private GameObject resourceSelectorCanvas = null;

        private BuildPanel _buildPanel;
        private ResourceInfoPanel _resourceInfoPanel;
        private ResourceSelectionPanel _resourceSelectionPanel;
        private UIStates _currentUIState = UIStates.MapView;

        private void Awake()
        {
            _buildPanel = buildCanvas.GetComponent<BuildPanel>();
            _resourceInfoPanel = resourceInfoCanvas.GetComponent<ResourceInfoPanel>();
            _resourceSelectionPanel = resourceSelectorCanvas.GetComponent<ResourceSelectionPanel>();
        }

        public void ResetUI()
        {
            _currentUIState = UIStates.MapView;
            buildCanvas.SetActive(false);
            resourceInfoCanvas.SetActive(false);
            resourceSelectorCanvas.SetActive(false);
        }
        
        public void UpdatePanels(UIStates? nextState = null)
        {
            var selectedTile = MapManager.Instance.SelectedTile;
            
            buildCanvas.SetActive(false);
            resourceInfoCanvas.SetActive(false);
            resourceSelectorCanvas.SetActive(false);
            
            if(selectedTile == null)
            {
                _currentUIState = UIStates.MapView;
                return;
            }
                
            if(_currentUIState == UIStates.MapView)
            {
                var building = BuildManager.Instance.GetBuildingInPos(selectedTile.TileData.Hex);
                if(building != null)
                {
                    if(building.BuildingData.BuildingType == TypeConstants.BuildingType.SurfaceMiner)
                    {
                        resourceInfoCanvas.SetActive(true);
                        _resourceInfoPanel.SetButtonsVisible();
                        _currentUIState = UIStates.ResourceInfoView;
                    }
                }
                else
                {
                    buildCanvas.SetActive(true);
                    _buildPanel.SetButtonsVisible();
                    _currentUIState = UIStates.BuildView;
                }
            }

            if(_currentUIState == UIStates.ResourceInfoView
                && nextState == UIStates.ResourceSelectionView)
            {
                resourceSelectorCanvas.SetActive(true);
                _resourceSelectionPanel.InitResources();
            }


        }

        public void OnBuildButtonClick(GameObject buildButton)
        {
            var bb = buildButton.GetComponent<BuildButton>();

            //BuildManager.Instance.StartBuilding(bb.BuildingType
            //    , MapManager.Instance.SelectedTile);

            TaskManager.Instance.AddTask(new BuildTask(bb.BuildingType
                , MapManager.Instance.SelectedTile));

            Debug.Log("Button clicked:" + bb.BuildingType);
        }
    }
}