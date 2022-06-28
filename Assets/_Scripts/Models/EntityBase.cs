using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Models
{
    [Serializable]
    public abstract class EntityBase : MonoBehaviour
    {
        private string Id;

        [SerializeField]
        private string Name;

        [SerializeField]
        private Sprite Icon;
    }
}
