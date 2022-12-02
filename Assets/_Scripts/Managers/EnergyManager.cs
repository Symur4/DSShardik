using Assets._Scripts.Core.Events;
using Assets.Scripts.Core;
using System.Collections;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class EnergyManager : Singleton<EnergyManager>
    {
        private float _curentEnergyLevel; 

        public float CurentEnergyLevel => _curentEnergyLevel;

        private void Awake()
        {
            EventManager.Instance.StartListening<BuildingArg>(
                Core.Events.EventType.BuildingBuilt,
                OnBuildingBuilt);
        }

        void OnBuildingBuilt(BuildingArg arg)
        {
            Refresh();
        }
       
        public void Refresh()
        {
            var generated = BuildManager.Instance.Buildings.Sum(w => w.BuildingData.EnergyGenerated);
            var consumed = BuildManager.Instance.Buildings.Sum(w => w.BuildingData.EnergyConsumed);

            _curentEnergyLevel = generated - consumed;
        }
    }
}