using Assets._Scripts.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Assets._Scripts.UI
{
    public class EnergyPanel : MonoBehaviour
    {
        [SerializeField]
        Text EnergyText;

        private void Update()
        {
            EnergyText.text = EnergyManager.Instance.CurentEnergyLevel.ToString();
        }
    }
}
