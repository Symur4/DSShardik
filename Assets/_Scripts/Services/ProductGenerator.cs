using Assets._Scripts.Managers;
using Assets._Scripts.Models;
using Assets.Scripts.TypeConstants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Assets._Scripts.Services
{
    public class ProductGenerator : MonoBehaviour
    {
        
        private List<ProductItem> _generateProductList = new List<ProductItem>();

        public List<ProductItem> CurrentGeneratedProducts => _generateProductList;

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
            
        }

        private void Update()
        {
            if (_isWorking)
            {
                foreach (var product in _generateProductList)
                {
                    var requiredResources = GetCosts(product.ProductType);

                    if (product.IsProducing == false)
                    {
                        var canGenerate = true;
                        foreach (var rr in requiredResources)
                        {
                            if (rr.ResourceType != ResourceType.None)
                            {
                                canGenerate = ResourceManager.Instance.HasRequiredResources(new List<ResourceData>() {
                                    new ResourceData() {
                                        Amount = rr.Amount,
                                        ResourceType = rr.ResourceType,
                                    }
                                });
                            }
                        }
                        if (canGenerate)
                        {
                            ResourceManager.Instance.SpendResources(
                                requiredResources.Where(w => w.ResourceType != ResourceType.None)
                                .Select(s => new ResourceData() { Amount = s.Amount, ResourceType = s.ResourceType })
                                .ToList()
                            );

                            product.SetIsProducing(true);                            
                        }
                    }

                    if (product.IsProducing)
                    {
                        if (product.CurrentTimer < product.Period)
                        {
                            product.SetCurrentTimer(product.CurrentTimer + Time.deltaTime);
                        }
                        else
                        {
                            ProductReady(product);
                            product.SetIsProducing(false);
                        }
                    }
                }
            }
        }

        public void InitProduct(ProductItem productItem)
        {
            _generateProductList = new List<ProductItem>();
            _generateProductList.Add(productItem);            
        }

        private void ProductReady(ProductItem productGenerated)
        {

            ResourceManager.Instance.ProductCreated(productGenerated.ProductType,
                productGenerated.Amount);                                
        }

        private List<ProductData> GetCosts(ProductType productType)
        {
            var result = new List<ProductData>();

            switch (productType)
            {
                case ProductType.RegolithBrick:
                    result.Add(new ProductData()
                    {
                        Amount = 10f,
                        ResourceType = ResourceType.Regolith,
                        ProductType = ProductType.None
                    });
                    break;
            }

            return result;
        }

    }
}