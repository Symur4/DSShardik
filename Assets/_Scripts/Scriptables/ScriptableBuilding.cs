using Assets._Scripts.Models;
using Assets._Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Building")]
    public class ScriptableBuilding : ScriptableObject
    {
        public MonoBehaviour Prefab;        

        public BuildingType BuildingType;

        public List<ResourceData> ResourceCost;

        public float BuildLength;

        public float EnergyGenerated;

        public float EnergyConsumed;

    }
}
