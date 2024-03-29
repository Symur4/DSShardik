﻿using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._Scripts.Models
{
    [Serializable]
    public class ResourceItem
    {
        public ResourceType ResourceType;
        public float Amount;
        public float Period;
        
        private float _currentTimer;
        private bool _isGenerating;

        public bool IsGenerating => _isGenerating;

        public float CurrentTimer => _currentTimer;

        public void SetCurrentTimer(float currentTimer)
        {
            _currentTimer = currentTimer;
        }

        public void SetIsGenerating(bool value)
        {
            _isGenerating = value;
        }
    }

}
