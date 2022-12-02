using Assets._Scripts.Managers;
using Assets._Scripts.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.UI
{
    public class ResourceStripe : MonoBehaviour
    {
        [SerializeField]
        private GameObject ContentPanel = null;

        [SerializeField]
        private GameObject ResourceStripeItem = null;

        private void Awake()
        {
            ResourceManager.Instance.OnResourceUpdate += OnResourceUpdate;
        }

        private void OnResourceUpdate(Scripts.TypeConstants.ResourceType resourceType, float amount, float totalAmount)
        {
            FillResources();
        }

        private void FillResources()
        {
            ClearChildren();

            var list = ResourceManager.Instance.GetResources();
            foreach (var item in list)
            {
                var resourceEntry = Instantiate(ResourceStripeItem, new Vector3(0, 0, 0), transform.rotation);
                ////Parent to the panel
                resourceEntry.transform.SetParent(ContentPanel.transform, false);
                ////Set the text box's text element font size and style:
                //tempTextBox.fontSize = defaultFontSize;
                ////Set the text box's text element to the current textToDisplay:

                var ri = resourceEntry.GetComponent<ResourceStripeItem>();

                ri.SetText(item.Key.ToString(), item.Value.ToString());
            }
        }

        private void ClearChildren()
        {
            int i = 0;

            //Array to hold all child obj
            GameObject[] allChildren = new GameObject[ContentPanel.transform.childCount];

            //Find all child obj and store to that array
            foreach (Transform child in ContentPanel.transform)
            {
                allChildren[i] = child.gameObject;
                i += 1;
            }

            //Now destroy them
            foreach (GameObject child in allChildren)
            {
                DestroyImmediate(child.gameObject);
            }

        }
    }
}
