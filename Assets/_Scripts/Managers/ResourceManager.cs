﻿using Assets._Scripts.Models;
using Assets.Scripts.Core;
using Assets.Scripts.TypeConstants;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Assets._Scripts.Managers
{
    public class ResourceManager : Singleton<ResourceManager>
    {
        Dictionary<ResourceType, float> _resources = new Dictionary<ResourceType, float>();
        Dictionary<ProductType, float> _products = new Dictionary<ProductType, float>();

        #region Events
        public delegate void ResourceUpdateHandler(ResourceType resourceType, float amount, float totalAmount);
        public delegate void ProductUpdateHandler(ProductType productType, float amount, float totalAmount);
        public event ResourceUpdateHandler OnResourceUpdate;
        public event ProductUpdateHandler OnProductUpdate;
        #endregion


        private void Start()
        {
            
        }

        public void ClearResources()
        {
            _resources = new Dictionary<ResourceType, float>();
            _products = new Dictionary<ProductType, float>();
        }

        public void ResourceGenerated(ResourceType resourceType, float amount)
        {
            if (_resources.ContainsKey(resourceType))
            {
                _resources[resourceType] += amount;
            }
            else
            {
                _resources.Add(resourceType, amount);
            }

            Debug.Log("Added resource:" + resourceType.ToString());

            OnResourceUpdate?.Invoke(resourceType, amount, _resources[resourceType]);
        }

        public void ProductCreated(ProductType productType, float amount)
        {
            if (_products.ContainsKey(productType))
            {
                _products[productType] += amount;
            }
            else
            {
                _products.Add(productType, amount);
            }

            Debug.Log("Added product:" + productType.ToString());

            OnProductUpdate?.Invoke(productType, amount, _products[productType]);
        }

        public void SpendResources(List<ResourceData> spendResources)
        {
            foreach (var resource in spendResources)
            {
                _resources[resource.ResourceType] -= resource.Amount;
                OnResourceUpdate?.Invoke(resource.ResourceType
                    , resource.Amount
                    , _resources[resource.ResourceType]);
            }
        }

        public bool HasRequiredResources(List<ResourceData> requiredResources)
        {
            foreach (var resource in requiredResources)
            {
                if (_resources.Where(w => w.Key == resource.ResourceType
                                      && w.Value >= resource.Amount).Count() < 1)
                {
                    return false;
                }
            }

            return true;
        }

        public Dictionary<ResourceType, float> GetResources()
        {
            return _resources;
        }

        public List<ResourceData> GetResourceData()
        {
            return _resources.Select(s => new ResourceData() { ResourceType = s.Key, Amount = s.Value }).ToList();
        }

        public void InitResources(List<ResourceData> resourceData)
        {
            foreach (var r in resourceData)
            {
                ResourceGenerated(r.ResourceType, r.Amount);
            }
        }

        public float GetResource(ResourceType resourceType)
        {
            if (_resources.ContainsKey(resourceType))
                return _resources[resourceType];

            return 0f;
        }


    }
}