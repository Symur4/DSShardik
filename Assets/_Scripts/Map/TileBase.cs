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
    public class TileBase : MonoBehaviour
    {
        [SerializeField]
        private GameObject ProgressBar;

        private MeshRenderer _meshRenderer;
        private float _exploreCurrentProgress;
        private bool _isExploring;
        private Action<Tile> _onExploreFinishedAction;
        private float _exploreLenght = 5f;
        private ProgressBar _progressBar;
        public Material OriginalMaterial { private get; set; }

        public Tile TileData { get; set; }

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

        public void StartExplore(Action<Tile> onExploreFinished, float exploreLength)
        {
            _exploreLenght = exploreLength;
            _exploreCurrentProgress = 0f;
            _isExploring = true;
            _onExploreFinishedAction = onExploreFinished;

            InvokeRepeating("ExploreProgress", 1f, 1f);

            _progressBar.SetProgressValues(_exploreLenght, _exploreCurrentProgress);
            _progressBar.SetVisibility(true);
        }

        public void ExploreProgress()
        {
            if (_isExploring)
            {
                _progressBar.SetProgressValues(_exploreLenght, _exploreCurrentProgress);
                _exploreCurrentProgress += 1f;
                if (_exploreCurrentProgress == _exploreLenght)
                {
                   ExploreFinished();
                }
            }
        }

        private void ExploreFinished()
        {
            
            _meshRenderer.material = OriginalMaterial;
            _isExploring = false;
            TileData.IsExplored = true;
            _progressBar.SetVisibility(false);
            CancelInvoke("ScanFinished");
            if (_onExploreFinishedAction != null)
            {
                _onExploreFinishedAction.Invoke(TileData);
            }

            
        }
    }
}
