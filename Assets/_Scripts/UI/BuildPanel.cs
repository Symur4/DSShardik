using Assets._Scripts.Managers;
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
            foreach (var b in _buildButtons)
            {
                b.gameObject.SetActive(false);
            }

            var tile = MapManager.Instance.SelectedTile.TileData;
            var buildingInPos = BuildManager.Instance.GetBuildingInPos(tile.Hex);
            if(tile == null
                || tile.IsExplored == false)
            {
                return;
            }

            if(tile.IsExplored
                && tile.HasEnergy == false
                && MapManager.Instance.HasEnergyInNeighbour(tile))
            {
                _buildButtons.Where(w => w.BuildingType == TypeConstants.BuildingType.Pylon)
                    .ToList()
                    .ForEach(b => b.gameObject.SetActive(true));
            }

            if(tile.IsExplored
                && tile.HasEnergy
                && tile.ResourceType == Scripts.TypeConstants.ResourceType.Limestone
                && (buildingInPos == null || buildingInPos.BuildingData.BuildingType == TypeConstants.BuildingType.Pylon))
            {
                _buildButtons.Where(w => w.BuildingType == TypeConstants.BuildingType.SurfaceMiner)
                    .ToList()
                    .ForEach(b => b.gameObject.SetActive(true));
            }




        }

    }
}