using Assets._Scripts.Managers;
using Assets._Scripts.Models;
using Assets.Scripts.TypeConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets._Scripts.Services
{
    public class ResourceGenerator : MonoBehaviour
    {

        [SerializeField]
        private List<ResourceItem> GenerateResources;


        private List<ResourceItem> _resources = new List<ResourceItem>();

        private bool _isWorking;        

        public bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; }
        }

        private void Awake()
        {            
        }

        private void Start()
        {
            _resources = new List<ResourceItem>();

            _resources.AddRange(GenerateResources);
        }

        private void Update()
        {
            if (_isWorking)
            {
                foreach (var resource in _resources)
                {
                    if (resource.CurrentTimer > 0)
                    {
                        resource.SetCurrentTimer(resource.CurrentTimer - Time.deltaTime);
                    }
                    else
                    {
                        GenerateResource(resource);
                    }
                }
            }
        }
      
        private void GenerateResource(ResourceItem resourceGenerated)
        {
            var requiredResources = GetResourceCosts(resourceGenerated.ResourceType);


            var canGenerate = true;
            foreach (var rr in requiredResources)
            {
                canGenerate = ResourceManager.Instance.HasRequiredResources(requiredResources);
            }

            if (canGenerate)
            {
                ResourceManager.Instance.SpendResources(requiredResources);

                ResourceManager.Instance.ResourceGenerated(resourceGenerated.ResourceType, resourceGenerated.Amount);
            }

            resourceGenerated.SetCurrentTimer(resourceGenerated.Period);
        }

        private List<ResourceData> GetResourceCosts(ResourceType resourceType)
        {
            var result = new List<ResourceData>();

            switch (resourceType)
            {
                case ResourceType.IronBar:
                    result.Add(new ResourceData()
                    {
                        Amount = 2f,
                        ResourceType = ResourceType.IronOre
                    });
                    break;
            }

            return result;
        }

    }
}