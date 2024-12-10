// Designed by KINEMATION, 2024.

using KINEMATION.MagicBlend.Runtime;
using UnityEngine;

namespace Demo.Scripts
{
    public class OverlayItem : MonoBehaviour
    {
        public MagicBlendAsset blendAsset;

        public void SetVisibility(bool isVisible)
        {
            gameObject.SetActive(isVisible);
        }
    }
}
