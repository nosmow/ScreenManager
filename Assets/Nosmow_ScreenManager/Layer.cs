using UnityEngine;

namespace Nosmow_ScreenManager
{
    //It is a part of the screen
    public class Layer : MonoBehaviour
    {
        private void Start()
        {
            LayerRegistry.Instance.RegisterLayer(gameObject.name, gameObject);
        }
    }
}