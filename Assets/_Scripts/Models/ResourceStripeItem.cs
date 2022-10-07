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
        private Text textField;

        public void SetText(string text)
        {
            textField.text = text;
        }
    }
}
