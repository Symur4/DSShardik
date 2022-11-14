using Assets._Scripts.Managers;
using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class ResourceSelectionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject container = null;

        public void InitResources()
        {

        }

        public void OnResourceClick(string resourceType)
        {
            if(nameof(ResourceType.Regolith) == resourceType)
            {
                BuildManager.Instance.AddResourceGenerationToSelectedBuilding(ResourceType.Regolith);

                UIManager.Instance.ResetUI();
            }

            if (nameof(ResourceType.Silica) == resourceType)
            {
                BuildManager.Instance.AddResourceGenerationToSelectedBuilding(ResourceType.Silica);

                UIManager.Instance.ResetUI();
            }
        }
        
    }
}
