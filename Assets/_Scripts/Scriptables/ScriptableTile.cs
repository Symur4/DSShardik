using Assets.Scripts.TypeConstants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._Scripts.Scriptables
{
    [CreateAssetMenu(fileName ="New Tile")]
    public class ScriptableTile : ScriptableObject
    {
        public MonoBehaviour Prefab;

        public Material BlankMaterial;

        public Material TileMaterail;

        public Material SelectedMaterial;

        public Shader PylonShader;


        public TileType TileType;

        public bool IsBuildable;

    }
}
