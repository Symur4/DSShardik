using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class ResourceInfoPanel : MonoBehaviour
    {

        public void SetButtonsVisible()
        {

        }

        public void OnAddResourceClick()
        {
            UIManager.Instance.UpdatePanels(TypeConstants.UIStates.ResourceSelectionView);
        }
    }
}
