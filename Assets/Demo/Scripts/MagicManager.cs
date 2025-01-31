// Designed by KINEMATION, 2024.

using System;
using KINEMATION.MagicBlend.Runtime;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Demo.Scripts
{
    public struct MagicBlendWeights
    {
        public float baseWeight;
        public float additiveWeight;
        public float localWeight;
    }
    
    public class MagicManager : MonoBehaviour
    {
        [SerializeField] private List<GameObject> overlayItems;
        
        [SerializeField] private Menu menu;
        [SerializeField] private Text overlayText;
        
        private MagicBlending _magicBlending;
        private int _selectedItemIndex;
        private List<OverlayItem> _items;

        private List<List<MagicBlendWeights>> _weights = new List<List<MagicBlendWeights>>();

        private void OnItemEquipped()
        {
            _magicBlending.UpdateMagicBlendAsset(GetActiveItem().blendAsset, true);
            overlayText.text = GetActiveItem().blendAsset.overlayPose.name;
            
            int num = menu.sliders.Length;
            for (int i = 0; i < num; i++)
            {
                var weight = _weights[_selectedItemIndex][i];

                menu.sliders[i].baseSlider.value = weight.baseWeight;
                menu.sliders[i].additiveSlider.value = weight.additiveWeight;
                menu.sliders[i].localSlider.value = weight.localWeight;
            }
        }

        private OverlayItem GetActiveItem()
        {
            return _items[_selectedItemIndex];
        }

        private void Start()
        {
            _magicBlending = GetComponent<MagicBlending>();
            _selectedItemIndex = 0;
            
            _items = new List<OverlayItem>();
            foreach (var prefab in overlayItems)
            {
                var overlayItem = prefab.GetComponent<OverlayItem>();
                if(overlayItem == null) continue;

                overlayItem.SetVisibility(false);

                var list = new List<MagicBlendWeights>();
                foreach (var blend in overlayItem.blendAsset.layeredBlends)
                {
                    list.Add(new MagicBlendWeights()
                    {
                        baseWeight = blend.baseWeight,
                        additiveWeight = blend.additiveWeight,
                        localWeight = blend.localWeight
                    });
                }
                
                _weights.Add(list);
                _items.Add(overlayItem);
            }
            
            _items[0].SetVisibility(true);
            OnItemEquipped();
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.RightArrow)) EquipForward();
            if(Input.GetKeyDown(KeyCode.LeftArrow)) EquipBackward();
        }

        public void EquipForward()
        {
            GetActiveItem().SetVisibility(false);
            _selectedItemIndex++;
            _selectedItemIndex = _selectedItemIndex > _items.Count - 1 ? 0 : _selectedItemIndex;
            GetActiveItem().SetVisibility(true);

            OnItemEquipped();
        }

        public void EquipBackward()
        {
            GetActiveItem().SetVisibility(false);
            _selectedItemIndex--;
            _selectedItemIndex = _selectedItemIndex < 0 ? _items.Count - 1 : _selectedItemIndex;
            GetActiveItem().SetVisibility(true);
                
            OnItemEquipped();
        }

        private void LateUpdate()
        {
            int num = menu.sliders.Length;
            for (int i = 0; i < num; i++)
            {
                var blend = _magicBlending.BlendAsset.layeredBlends[i];
                var weight = _weights[_selectedItemIndex][i];

                weight.baseWeight = menu.sliders[i].baseSlider.value;
                weight.additiveWeight = menu.sliders[i].additiveSlider.value;
                weight.localWeight = menu.sliders[i].localSlider.value;

                blend.baseWeight = weight.baseWeight;
                blend.additiveWeight = weight.additiveWeight;
                blend.localWeight = weight.localWeight;

                _magicBlending.BlendAsset.layeredBlends[i] = blend;
                _weights[_selectedItemIndex][i] = weight;
            }
        }
    }
}
