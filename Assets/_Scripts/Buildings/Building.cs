using Assets._Scripts.Models;
using Assets._Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Buildings
{
    [Serializable]
    public class Building : EntityBase
    {
        [SerializeField]
        private BuildingType BuildingType;

        private BuildingData _buildingData; 

        public BuildingData BuildingData => _buildingData;

        public void SetBuildingData(BuildingData buildingData)
        {
            _buildingData = buildingData;
        }
    }
}
