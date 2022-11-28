using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Models
{
    [Serializable]
    public class ProductItem
    {
        private float _currentTimer = 0f;
        private bool _isProducing = false;

        public ProductType ProductType;
        public float Amount;
        public float Period;
        
        

        public float CurrentTimer => _currentTimer;
        public bool IsProducing => _isProducing;

        public void SetCurrentTimer(float currentTimer)
        {
            _currentTimer = currentTimer;
        }

        public void SetIsProducing(bool value)
        {
            _isProducing=value;
        }


    }

}
