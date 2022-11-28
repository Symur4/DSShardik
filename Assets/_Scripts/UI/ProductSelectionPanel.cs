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
    public class ProductSelectionPanel : MonoBehaviour
    {
        [SerializeField]
        private GameObject container = null;

        public void InitProducts()
        {

        }

        public void OnProductClick(string resourceType)
        {
            if(nameof(ResourceType.RegolithBrick) == resourceType)
            {
                BuildManager.Instance.AddResourceGenerationToSelectedBuilding(ResourceType.RegolithBrick);

                UIManager.Instance.ResetUI();
            }

            
        }
        
    }
}
