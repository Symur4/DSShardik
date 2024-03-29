﻿using Assets._Scripts.Map;
using Assets._Scripts.Models;
using Assets._Scripts.Services;
using Assets._Scripts.TypeConstants;
using Assets.Scripts.Managers;
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
        private MapTile _tile;
        private bool _isBuilding;
        private Action<string> _onBuildingFinishedAction;
        private ResourceGenerator _resourceGenerator;

        public bool IsBuilding => _isBuilding;
        public BuildingData BuildingData => _buildingData;

        private void Awake()
        {
            _resourceGenerator = this.GetComponent<ResourceGenerator>();
        }

        public void SetBuildingData(BuildingData buildingData)
        {
            _buildingData = buildingData;
            _tile = MapManager.Instance.FindTile(_buildingData.Position);
            
        }

        public List<ResourceItem> GetGeneratedResourceTypes()
        {
            if (_resourceGenerator != null)
            {
                return _resourceGenerator.CurrentGeneratedResources;
            }

            return new List<ResourceItem>();
        }

        public void AddGeneratedResourceType(ResourceItem resourceItem)
        {
            if (_resourceGenerator != null)
            {
                _resourceGenerator.AddResourceTypeForGenerate(resourceItem);
            }
        }

        public void StartResourceGenerator()
        {
            if (_resourceGenerator != null)
            {
                _resourceGenerator.IsWorking = true;
            }
        }

        public void StopResourceGenerator()
        {
            if (_resourceGenerator != null)
            {
                _resourceGenerator.IsWorking = false;
            }
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
            _tile.ShowProgress(_buildingData.BuildLength, _buildingData.BuildProgress);
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
            _tile.HideProgress();
        }


    }
}
