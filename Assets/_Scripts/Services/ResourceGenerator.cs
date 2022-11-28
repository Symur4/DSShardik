using Assets._Scripts.Managers;
using Assets._Scripts.Models;
using Assets.Scripts.TypeConstants;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Services
{
    public class ResourceGenerator : MonoBehaviour
    {

        [SerializeField]
        private List<ResourceItem> GenerateResourcesList;


        private List<ResourceItem> _resourcesToGenerate = new List<ResourceItem>();

        public List<ResourceItem> CurrentGeneratedResources => _resourcesToGenerate;

        private bool _isWorking;        

        public bool IsWorking
        {
            get { return _isWorking; }
            set { _isWorking = value; }
        }

        private void Awake()
        {
            _resourcesToGenerate = new List<ResourceItem>();

            _resourcesToGenerate.AddRange(GenerateResourcesList);
        }

        private void Start()
        {
            
        }

        private void Update()
        {
            if (_isWorking)
            {
                foreach (var resource in _resourcesToGenerate)
                {
                    if (resource.IsGenerating == false)
                    {
                        var requiredResources = GetResourceCosts(resource.ResourceType);
                        var canGenereate = CanGenerate(resource, requiredResources);

                        if (canGenereate)
                        {
                            ResourceManager.Instance.SpendResources(
                                   requiredResources.Where(w => w.ResourceType != ResourceType.None)
                                   .Select(s => new ResourceData()
                                   { Amount = s.Amount, ResourceType = s.ResourceType })
                                   .ToList()
                               );

                            resource.SetIsGenerating(true);
                        }

                    }

                    if (resource.IsGenerating)
                    {
                        if (resource.CurrentTimer < resource.Period)
                        {
                            resource.SetCurrentTimer(resource.CurrentTimer + Time.deltaTime);
                        }
                        else
                        {
                            ResourceReady(resource);
                            resource.SetCurrentTimer(0f);
                            resource.SetIsGenerating(false);
                        }
                    }
                }
            }
        }
      
        private bool CanGenerate(ResourceItem resourceItem, List<ResourceData> requiredResources)
        {
            var canGenerate = true;            

            foreach (var rr in requiredResources)
            {
                if(rr.ResourceType != ResourceType.None)
                {
                    canGenerate = ResourceManager.Instance.HasRequiredResources(new List<ResourceData>() {
                        new ResourceData() {
                            Amount = rr.Amount,
                            ResourceType = rr.ResourceType,
                        }
                    });
                }
            }

            return canGenerate;
        }

        public void AddResourceTypeForGenerate(ResourceItem resourceItem)
        {
            _resourcesToGenerate = new List<ResourceItem>();
            _resourcesToGenerate.Add(resourceItem);
        }

        private void ResourceReady(ResourceItem resourceGenerated)
        {
            ResourceManager.Instance.ResourceGenerated(resourceGenerated.ResourceType, resourceGenerated.Amount);            
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
                case ResourceType.RegolithBrick:
                    result.Add(new ResourceData()
                    {
                        Amount = 20f,
                        ResourceType = ResourceType.Regolith
                    });
                    break;

            }

            return result;
        }

    }
}