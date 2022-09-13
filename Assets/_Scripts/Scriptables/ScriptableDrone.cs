using System.Collections;
using UnityEngine;

namespace Assets._Scripts.Scriptables
{
    [CreateAssetMenu(fileName = "New drone")]
    public class ScriptableDrone : ScriptableObject
    {
        public MonoBehaviour Prefab;

        public float MaxSpeed = 10f;
        
        public float MaxHeight = 5f;
    }
}