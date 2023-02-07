using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine.UIElements;

namespace Assets._Scripts.UI
{
    public static class UIHelper
    {
        public static void ShowVisualElement(VisualElement visualElement)
        {
            if (visualElement == null)
                return;

            visualElement.style.display = DisplayStyle.Flex;
        }

        public static void HideVisualElement(VisualElement visualElement)
        {
            if (visualElement == null)
                return;

            visualElement.style.display = DisplayStyle.None;
        }

    }
}
