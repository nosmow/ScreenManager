using Nosmow_ScreenManager;
using UnityEngine;

namespace Scenes
{
    public class Test : MonoBehaviour
    {
        private ScreenManager screenManager => ScreenManager.Instance;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                screenManager.PushScreen("LoginScreen", "Home");
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                screenManager.PushScreen("MarScreen", "Home");
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                screenManager.RemoveScreen("LoginScreen", "Home");
            }
            
            if (Input.GetKeyDown(KeyCode.N))
            {
                screenManager.RemoveScreen("MarScreen", "Home");
            }
        }
    }
}