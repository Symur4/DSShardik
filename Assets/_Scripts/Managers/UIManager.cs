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

        [SerializeField]
        private GameObject productInfoCanvas = null;
        [SerializeField]
        private GameObject productSelectorCanvas = null;

        [SerializeField]
        private GameUIManager _gameUIManager;


        private BuildPanel _buildPanel;
        private ResourceInfoPanel _resourceInfoPanel;        
        private ResourceSelectionPanel _resourceSelectionPanel;
        
        private ProductInfoPanel _productInfoPanel;
        private ProductSelectionPanel _productSelectionPanel;

        private UIStates _currentUIState = UIStates.MapView;

        private void Awake()
        {
            _buildPanel = buildCanvas.GetComponent<BuildPanel>();
            _resourceInfoPanel = resourceInfoCanvas.GetComponent<ResourceInfoPanel>();
            _productInfoPanel = productInfoCanvas.GetComponent<ProductInfoPanel>();

            _resourceSelectionPanel = resourceSelectorCanvas.GetComponent<ResourceSelectionPanel>();
            _productSelectionPanel = productSelectorCanvas.GetComponent<ProductSelectionPanel>();
        }

        public void ResetUI()
        {
            _currentUIState = UIStates.MapView;
           HideAllPanel();
        }

        private void HideAllPanel()
        {
            buildCanvas.SetActive(false);
            resourceInfoCanvas.SetActive(false);
            resourceSelectorCanvas.SetActive(false);

            productInfoCanvas.SetActive(false);
            productSelectorCanvas.SetActive(false);
        }
        
        public void UpdateUI()
        {
            var selectedTile = MapManager.Instance.SelectedTile;

            
            if (selectedTile == null)
            {
                _currentUIState = UIStates.MapView;
                return;
            }

            _gameUIManager.ShowExploreModal();


        }

        public void UpdatePanels_Deprecated(UIStates? nextState = null)
        {
            var selectedTile = MapManager.Instance.SelectedTile;

            HideAllPanel();

            
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
                        _resourceInfoPanel.ShowPanel(building);
                        _currentUIState = UIStates.ResourceInfoView;
                    }

                    if (building.BuildingData.BuildingType == TypeConstants.BuildingType.Factory)
                    {
                        productInfoCanvas.SetActive(true);
                        _productInfoPanel.ShowPanel(building);
                        _currentUIState = UIStates.ProductInfoView;
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

            if (_currentUIState == UIStates.ProductInfoView
                && nextState == UIStates.ProductSelectionView)
            {
                productSelectorCanvas.SetActive(true);
                _productSelectionPanel.InitProducts();
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