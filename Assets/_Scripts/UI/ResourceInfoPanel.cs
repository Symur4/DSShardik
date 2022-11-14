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
            var resource = building.BuildingData.GenerateResource.FirstOrDefault();
            if ( resource != null )
            {
                CurrentResource.text = resource.ResourceType.ToString();
            }
        }

        public void OnAddResourceClick()
        {
            UIManager.Instance.UpdatePanels(TypeConstants.UIStates.ResourceSelectionView);
        }
    }
}
