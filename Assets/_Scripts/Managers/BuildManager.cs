﻿using Assets._Scripts.Buildings;
using Assets._Scripts.Map;
using Assets._Scripts.Services;
using Assets._Scripts.TypeConstants;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using Assets.Scripts.Managers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class BuildManager : Singleton<BuildManager>
    {
        [SerializeField]
        public GameObject TempContainer;

        private List<Building> _buildings = new List<Building>();

        public List<Building> Buildings => _buildings;

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public Building GetBuildingInPos(Hex hex)
        {            
            return _buildings.Where(w => w.BuildingData.Position.IsEqual(hex)
                                && w.BuildingData.BuildingType != BuildingType.Pylon).FirstOrDefault();
        }

        public string StartBuilding(BuildingData buildingData)
        {
            var tile = MapManager.Instance.FindTile(buildingData.Position.q, buildingData.Position.r);
            
            var container = tile.transform.Find("Props");
            foreach (Transform child in container)
            {
                GameObject.Destroy(child.gameObject);
            }

            var buildingInPos = GetBuildingInPos(buildingData.Position);
            if(buildingInPos != null 
                 && buildingInPos.BuildingData.BuildingType != BuildingType.Pylon)
            {
                return null;
            }

            var buildingResource = ResourceCore.Instance
              .Buildings
              .Where(w => w.BuildingType == buildingData.BuildingType)
              .FirstOrDefault();

            var spawned = Instantiate(buildingResource.Prefab
               , container.position
               , Quaternion.identity
               , container);
            buildingData.BuildLength = buildingResource.BuildLength;

            var building = spawned.GetComponent<Building>();
            building.SetBuildingData(buildingData);            

            if(string.IsNullOrEmpty(buildingData.Id) == true)
            {
                spawned.gameObject.SetActive(false);
                buildingData.Id = Guid.NewGuid().ToString();
                building.StartBuild(BuildingFinished);

            } else
            {
                spawned.gameObject.SetActive(true);
                if (buildingData.BuildingType == BuildingType.Pylon)
                {
                    tile.TileData.HasEnergy = true;
                }

                var resourceGenerator = building.GetComponent<ResourceGenerator>();
                if (resourceGenerator != null)
                {
                    resourceGenerator.IsWorking = true;
                }
            }

            _buildings.Add(building);

            return building.BuildingData.Id;
        }
             
        private void BuildingFinished(string id)
        {
            Debug.Log("Build finished:" + id);
            var building = _buildings.Where(w => w.BuildingData.Id == id).FirstOrDefault();
            if (building != null)
            {
                building.gameObject.SetActive(true);
            }
        }
    }
}