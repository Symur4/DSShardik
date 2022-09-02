using Assets._Scripts.Buildings;
using Assets._Scripts.Map;
using Assets._Scripts.Services;
using Assets._Scripts.TypeConstants;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class BuildManager : Singleton<BuildManager>
    {
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

        public void StartBuilding(BuildingType buildingType, MapTile tile)
        {
            var buildingInPos = GetBuildingInPos(tile.TileData.Hex);
            if(buildingInPos != null 
                 && buildingInPos.BuildingData.BuildingType != BuildingType.Pylon)
            {
                return;
            }

            var container = tile.transform.Find("Props");
            foreach (Transform child in container)
            {
                GameObject.Destroy(child.gameObject);
            }
            

            var buildingResource = ResourceCore.Instance
                .Buildings
                .Where(w => w.BuildingType == buildingType)
                .FirstOrDefault();

            var spawned = Instantiate(buildingResource.Prefab
                ,container.position
                ,Quaternion.identity
                ,container);

            var building = spawned.GetComponent<Building>();
            building.SetBuildingData(new BuildingData() { 
                BuildingType = buildingType,
                Position = tile.TileData.Hex
            });

            _buildings.Add(building);

            if(buildingType== BuildingType.Pylon )
            {
                tile.TileData.HasEnergy = true;
            }

            var resourceGenerator = building.GetComponent<ResourceGenerator>();
            if (resourceGenerator != null)
            {
                resourceGenerator.IsWorking = true;
            }




        }
    }
}