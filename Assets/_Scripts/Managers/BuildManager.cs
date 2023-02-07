using Assets._Scripts.Buildings;
using Assets._Scripts.Core.Events;
using Assets._Scripts.Map;
using Assets._Scripts.Models;
using Assets._Scripts.Services;
using Assets._Scripts.TypeConstants;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using Assets.Scripts.Managers;
using Assets.Scripts.TypeConstants;
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

        public string StartBuilding(BuildingData buildingData, bool isFree =false)
        {
            var tile = MapManager.Instance.FindTile(buildingData.Position.q, buildingData.Position.r);

            var container = tile.transform.Find("Props");
            foreach (Transform child in container)
            {
                GameObject.Destroy(child.gameObject);
            }

            var buildingInPos = GetBuildingInPos(buildingData.Position);
            if (buildingInPos != null
                 && buildingInPos.BuildingData.BuildingType != BuildingType.Pylon)
            {
                return null;
            }

            var buildingResource = ResourceCore.Instance
              .Buildings
              .Where(w => w.BuildingType == buildingData.BuildingType)
              .FirstOrDefault();

            if(isFree == false
               && !ResourceManager.Instance.HasRequiredResources(buildingResource.ResourceCost))
            {
                return String.Empty;
            }

            var spawned = Instantiate(buildingResource.Prefab
               , container.position
               , Quaternion.identity
               , container);
            buildingData.BuildLength = buildingResource.BuildLength;

            var building = spawned.GetComponent<Building>();
            building.SetBuildingData(buildingData);

            if (isFree == false)
            {
                ResourceManager.Instance.SpendResources(buildingResource.ResourceCost);
            }

            building.BuildingData.EnergyConsumed = buildingResource.EnergyConsumed;
            building.BuildingData.EnergyGenerated = buildingResource.EnergyGenerated;

            ActivateBuilding(building);

            _buildings.Add(building);



            return building.BuildingData.Id;
        }

        private void BuildingFinished(string id)
        {
            Debug.Log("Build finished:" + id);
            var building = _buildings.Where(w => w.BuildingData.Id == id).FirstOrDefault();

            ActivateBuilding(building);

            
        }

        private void ActivateBuilding(Building building)
        {
            if (building == null)
            {
                return;
            }

            var tile = MapManager.Instance.FindTile(building.BuildingData.Position);
            if (building.BuildingData.BuildingType == BuildingType.Pylon)
            {                               
                  tile.TileData.HasEnergy = true;
            }

            if (string.IsNullOrEmpty(building.BuildingData.Id) == true)
            {
                building.gameObject.SetActive(false);
                building.BuildingData.Id = Guid.NewGuid().ToString();
                building.StartBuild(BuildingFinished);
            }
            else
            {
                building.gameObject.SetActive(true);
                if (building.BuildingData.BuildingType == BuildingType.Pylon)
                {
                    tile.TileData.HasEnergy = true;
                }

                if (building.BuildingData.GenerateResource != null)
                {
                    foreach (var r in building.BuildingData.GenerateResource)
                    {
                        building.AddGeneratedResourceType(r);
                    }
                    building.StartResourceGenerator();
                }
            }

            EventManager.Instance.Publish<BuildingArg>(Core.Events.EventType.BuildingBuilt,
                new BuildingArg()
                {
                    Id = building.BuildingData.Id,
                });
        }

        public void AddResourceGenerationToSelectedBuilding(ResourceType resourceType)
        {
            var selectedTile = MapManager.Instance.SelectedTile;

            if(selectedTile == null)
            {
                return;
            }

            var building = GetBuildingInPos(selectedTile.TileData.Hex);
            if(building == null)
            {
                return;
            }

            var newResource = new Models.ResourceItem()
            {
                Amount = 1
                ,Period = 2
                ,ResourceType = resourceType
            };
            building.AddGeneratedResourceType(newResource);
            building.StartResourceGenerator();       
            
        }

        public List<BuildingData> GetBuildingDatas()
        {
            var result = new List<BuildingData>();
            foreach (var b in _buildings.Where(x => x != null))
            {
                var bd = b.BuildingData;
                bd.GenerateResource = b.GetGeneratedResourceTypes();
                result.Add(bd);
            }

            return result;
        }
        
    }
}