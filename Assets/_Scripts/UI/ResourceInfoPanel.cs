using Assets._Scripts.Buildings;
using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class ResourceInfoPanel : MonoBehaviour
    {
        [SerializeField]
        GameObject AddButton;

        [SerializeField]
        Text CurrentResource;

        public void ShowPanel(Building building )
        {
            CurrentResource.text = "";
            var resources = building.GetGeneratedResourceTypes();
            if ( resources != null 
                && resources.Count() > 0 )
            {
                CurrentResource.text = resources.First().ResourceType.ToString();
            }
        }

        public void OnAddResourceClick()
        {
            UIManager.Instance.UpdatePanels_Deprecated(TypeConstants.UIStates.ResourceSelectionView);
        }
    }
}
