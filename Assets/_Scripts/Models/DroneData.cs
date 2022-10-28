using Assets.Scripts.Core.Map;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [Serializable]
    public class DroneData
    {
        public Vector3? TargetPosition;
        public Vector3 CurrentPosition;
        public int Id = 0;

    }
}
