using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.UI
{
    public class ProgressBar : MonoBehaviour
    {
        float maxValue = 100f;
        float currentValue = 0f;

        [SerializeField]
        Image Mask;

        private void Update()
        {
            UpdateFill();
        }

        private void UpdateFill()
        {
            if (this.gameObject.activeInHierarchy)
            {
                var fill = currentValue / maxValue;
                Mask.transform.localScale = new Vector3(Mask.transform.localScale.x
                    , fill
                    , Mask.transform.localScale.z);
            }
        }

        public void SetVisibility(bool isVisible)
        {
            this.gameObject.SetActive(isVisible);
        }

        public void SetProgressValues(float max, float current)
        {
            currentValue = current;
            maxValue = max;
        }        
    }
}
