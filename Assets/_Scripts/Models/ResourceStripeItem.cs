using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.Models
{
    public class ResourceStripeItem : MonoBehaviour
    {
        [SerializeField]
        private Text nameField;

        [SerializeField]
        private Text valueField;

        public void SetText(string name, string value)
        {
            nameField.text = name;
            valueField.text = value;
        }
    }
}
