using UnityEngine;

namespace Demo.Scripts
{
    public class Menu : MonoBehaviour
    {
        [HideInInspector] public LayeredSlider[] sliders;

        private void Start()
        {
            sliders = GetComponentsInChildren<LayeredSlider>();
        }

        public void AssetStorePage()
        {
            Application.OpenURL("https://u3d.as/2Y8S");
        }
        
        public void EmailDeveloper()
        {
            Application.OpenURL("mailto:kinemation.studio@gmail.com");
        }

        public void JoinDiscord()
        {
            Application.OpenURL("https://discord.gg/kinemation-1027338787958816860");
        }
        
        public void Exit()
        {
            Application.Quit();
        }
    }
}
