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
    public class ProductInfoPanel : MonoBehaviour
    {
        [SerializeField]
        GameObject AddButton;

        [SerializeField]
        Text CurrentProduct;

        public void ShowPanel(Building building )
        {
            CurrentProduct.text = "";
            var resources = building.GetGeneratedResourceTypes();
            if ( resources != null 
                && resources.Count() > 0 )
            {
                CurrentProduct.text = resources.First().ResourceType.ToString();
            }
        }

        public void OnAddProductClick()
        {
            UIManager.Instance.UpdatePanels_Deprecated(TypeConstants.UIStates.ProductSelectionView);
        }
    }
}
