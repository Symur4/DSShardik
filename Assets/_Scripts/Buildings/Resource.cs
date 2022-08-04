using Assets._Scripts.Models;
using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Buildings
{
    internal class Resource : EntityBase
    {
        [SerializeField]
        private ResourceType ResourceType;
    }
}
