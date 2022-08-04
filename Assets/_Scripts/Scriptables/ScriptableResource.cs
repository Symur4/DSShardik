using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New Resource")]
    public class ScriptableResource : ScriptableObject    
    {
        public MonoBehaviour Prefab;

        public ResourceType ResourceType;
    }
}
