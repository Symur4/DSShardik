using Assets.Scripts.Core;
using Assets.Scripts.Core.Map;
using Assets.Scripts.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Map
{
    public class MapTile : MonoBehaviour
    {
        [SerializeField]
        private GameObject ProgressBar;

        private bool _isSelected = false;
        private Material _tempMaterial;
        private MeshRenderer _meshRenderer;
        private bool _isExploring;
        private Action<Tile> _onExploreFinishedAction;
        private float _exploreLenght = 5f;
        private ProgressBar _progressBar;
        public Material OriginalMaterial { private get; set; }

        public Tile TileData { get; set; }
        public bool IsSelected => _isSelected;


        private void Awake()
        {
            _meshRenderer = transform.Find("Main").GetComponentInChildren<MeshRenderer>();

            _progressBar = ProgressBar.GetComponent<ProgressBar>();
            _progressBar.SetVisibility(false);
        }

        public void SetMaterial(Material material)
        {
            _meshRenderer.material = material;
        }

        public void Select()
        {
            _isSelected = true;
            _tempMaterial = _meshRenderer.material;
            _meshRenderer.material = ResourceCore
                .Instance
                .Tiles
                .Where(w => w.TileType == TileData.TileType)
                .FirstOrDefault()
                .SelectedMaterial
                ;

            
        }

        public void ClearSelect()
        {
            _isSelected = false;
            _meshRenderer.material = _tempMaterial;
        }

        public void StartExplore(Action<Tile> onExploreFinished, float exploreLength)
        {
            _exploreLenght = exploreLength;
            //TileData.ExploreProgress = 0f;
            _isExploring = true;
            _onExploreFinishedAction = onExploreFinished;

            InvokeRepeating("ExploreProgress", 1f, 1f);

            _progressBar.SetProgressValues(_exploreLenght, TileData.ExploreProgress);
            _progressBar.SetVisibility(true);
        }

        public void ExploreProgress()
        {
            if (_isExploring)
            {
                _progressBar.SetProgressValues(_exploreLenght, TileData.ExploreProgress);
                TileData.ExploreProgress += 1f;                
                if (TileData.ExploreProgress >= _exploreLenght)
                {
                   ExploreFinished();
                }
            }
        }

        private void ExploreFinished()
        {
            
            _meshRenderer.material = OriginalMaterial;
            _tempMaterial = OriginalMaterial;
            _isExploring = false;
            TileData.IsExplored = true;
            TileData.ExploreProgress = 0f;
            _progressBar.SetVisibility(false);
            CancelInvoke("ScanFinished");
            if (_onExploreFinishedAction != null)
            {
                _onExploreFinishedAction.Invoke(TileData);                
            }

            
        }
    }
}
