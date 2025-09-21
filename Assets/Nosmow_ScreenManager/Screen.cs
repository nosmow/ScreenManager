using System.Collections.Generic;
using UnityEngine;

namespace Nosmow_ScreenManager 
{
    public class Screen : MonoBehaviour
    {
        [HideInInspector] 
        public string screenName;
        
        //Name of the layers that the screen will contain
        public List<string> layerNames = new();

        private void Start()
        {
            screenName = gameObject.name;
            ScreenManager.Instance.RegisterScreen(this);
        }
    }
}
