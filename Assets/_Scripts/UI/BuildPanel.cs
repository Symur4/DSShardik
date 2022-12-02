using Assets._Scripts.Managers;
using Assets.Scripts.Core;
using Assets.Scripts.Managers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class BuildPanel : MonoBehaviour
    {

        [SerializeField]
        private GameObject ButtonContainer;

        private List<BuildButton> _buildButtons;
        private void Awake()
        {
            

        }

        public void SetButtonsVisible()
        {
            _buildButtons = ButtonContainer.GetComponentsInChildren<BuildButton>(true).ToList();
            //foreach (var b in _buildButtons)
            //{
            //    b.gameObject.SetActive(false);
            //}

            var tile = MapManager.Instance.SelectedTile.TileData;
            var buildingInPos = BuildManager.Instance.GetBuildingInPos(tile.Hex);
            if (tile == null
                || tile.IsExplored == false
                || tile.IsBuildable == false)
            {
                return;
            }

            if (tile.IsExplored
                && tile.HasEnergy == false
                && MapManager.Instance.HasEnergyInNeighbour(tile))
            {
                SetButton(TypeConstants.BuildingType.Pylon);                
                SetButton(TypeConstants.BuildingType.PowerPlant);                
            }

            if (tile.HasEnergy
                //&& tile.ResourceType == Scripts.TypeConstants.ResourceType.Limestone
                && tile.IsBuildable
                && (buildingInPos == null || buildingInPos.BuildingData.BuildingType == TypeConstants.BuildingType.Pylon))
            {
                SetButton(TypeConstants.BuildingType.SurfaceMiner);
                SetButton(TypeConstants.BuildingType.Storage);
                SetButton(TypeConstants.BuildingType.Factory);               
            }
        }

        private void SetButton(TypeConstants.BuildingType buildingType)
        {
            var b = _buildButtons.Where(w => w.BuildingType == buildingType).FirstOrDefault();

            if (b == null) return;

            var r = ResourceCore.Instance.Buildings.Where(w => w.BuildingType == buildingType).FirstOrDefault();
                    
            if(r == null) return;

            var hasCost = ResourceManager.Instance.HasRequiredResources(r.ResourceCost);

            if (hasCost)
            {
                b.Enable();
            } else
            {
                b.Disable();
            }

        }

    }
}