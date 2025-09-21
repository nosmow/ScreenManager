using System.Collections.Generic;
using UnityEngine;

namespace Nosmow_ScreenManager
{
    /*
     * It is responsible for registering all the layers that are in the scene
     * When changing scenes, the referenced layers must be cleaned
     */
    
    public class LayerRegistry : MonoBehaviour
    {
        public static LayerRegistry Instance { get; private set; }
        private Dictionary<string, GameObject> layers = new();
        
        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        public void RegisterLayer(string layerName, GameObject gameObject)
        {
            layers.TryAdd(layerName, gameObject);
            gameObject.SetActive(false);
        }

        public void SetLayerActive(string layerName, bool active)
        {
            if (layers.TryGetValue(layerName, out var obj))
                obj.gameObject.SetActive(active);
            else 
                Debug.LogWarning($"Layer {layerName} does not exist");
        }
    }
}