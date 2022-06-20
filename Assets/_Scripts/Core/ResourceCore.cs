using Assets._Scripts.Scriptables;
using Assets.Scripts.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts.Core
{
    public class ResourceCore : Singleton<ResourceCore>
    {
        public List<ScriptableTile> Tiles { get; private set; }

        protected void Awake()
        {            
            AssembleResources();
        }

        private void AssembleResources()
        {
            Tiles = Resources.LoadAll<ScriptableTile>("Tiles").ToList();
        }
    }
}
