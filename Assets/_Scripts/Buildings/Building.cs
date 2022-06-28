using Assets._Scripts.Models;
using Assets._Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Building
{
    [Serializable]
    public class Building : EntityBase
    {
        [SerializeField]
        private BuildingType BuildingType;

    }
}
