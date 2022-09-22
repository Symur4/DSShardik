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
        private bool _isBuilding;
        private Action<string> _onBuildingFinishedAction;

        public bool IsBuilding => _isBuilding;
        public BuildingData BuildingData => _buildingData;

        public void SetBuildingData(BuildingData buildingData)
        {
            _buildingData = buildingData;
        }
        
        public void StartBuild(Action<string> onBuildFinished)
        {
            _isBuilding = true;            
            _buildingData.BuildProgress = 0f;
            _onBuildingFinishedAction = onBuildFinished;

            InvokeRepeating("BuildProgress", 1f, 1f);
        }

        private void BuildProgress()
        {
            if(_isBuilding)
            {
                _buildingData.BuildProgress += 1f;
            }
            if(_buildingData.BuildProgress == _buildingData.BuildLength)
            {
                BuildFinished();
            }
        }

        private void BuildFinished()
        {
            _isBuilding=false;
            _buildingData.BuildProgress = 0f;
            CancelInvoke("BuildProgress");
            if(_onBuildingFinishedAction != null)
            {
                _onBuildingFinishedAction.Invoke(_buildingData.Id);
            }
        }
    }
}
