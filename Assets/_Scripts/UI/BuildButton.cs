using Assets._Scripts.TypeConstants;
using System.Collections;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class BuildButton : MonoBehaviour
    {

        [SerializeField]
        private BuildingType buildingType;

        public BuildingType BuildingType => buildingType;
    }
}