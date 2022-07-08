using Assets._Scripts.Map;
using Assets._Scripts.TypeConstants;
using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class BuildManager : Singleton<BuildManager>
    {

        // Use this for initialization
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        public void StartBuilding(BuildingType buildingType, MapTile tile)
        {
            var container = tile.transform.Find("Props");
            var buildingResource = ResourceCore.Instance
                .Buildings
                .Where(w => w.BuildingType == buildingType)
                .FirstOrDefault();

            var spawned = Instantiate(buildingResource.Prefab
                ,container.position
                ,Quaternion.identity
                ,container);

            if(buildingType== BuildingType.Pylon )
            {
                tile.TileData.HasEnergy = true;
            }
        }
    }
}