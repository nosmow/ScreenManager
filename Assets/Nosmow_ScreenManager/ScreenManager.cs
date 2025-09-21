using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nosmow_ScreenManager
{
    public class ScreenManager : MonoBehaviour
    {
        public static ScreenManager Instance { get; set; }
        
        [SerializeField] private List<Stack> stacks = new();
        private List<Screen> registeredScreens = new();
        
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

        public void RegisterScreen(Screen screen)
        {
            if (registeredScreens.Contains(screen))
            {
                return;
            }
            
            registeredScreens.Add(screen);
        }
        
        public void PushScreen(string screenName, string stackName)
        {
            var screen = FindScreen(screenName);
            if (screen == null)
            {
                Debug.LogWarning($"Cannot find screen {screenName}");
                return;
            }

            var stack = GetOrCreateStack(stackName);
            
            if (stack.Count > 0)
                HideLayers(stack.Peek());
            
            stack.Push(screen);
            ShowLayers(screen);
        }
        
        public void RemoveScreen(string screenName,  string stackName)
        {
            var stack = GetStack(stackName);
            
            if (stack == null || stack.Screens.Count == 0)
                return;
            
            var screen = FindScreen(screenName);
            if (screen == null)
            {
                Debug.LogWarning($"Cannot find screen {screenName}");
                return;
            }

            if (stack.Peek() == screen)
            {
                PopScreen(stackName);
            }
            else
            {
                var temp = new Stack<Screen>();
                bool found = false;

                while (stack.Count > 0)
                {
                    var s = stack.Pop();
                    if (s == screen)
                    {
                        HideLayers(s);
                        found = true;
                        break;
                    }
                    temp.Push(s);
                }

                while (temp.Count > 0)
                {
                    stack.Push(temp.Pop());
                }
                
                if (!found)
                    Debug.LogWarning($"Cannot find screen {screenName} in stack {stackName}");
            }
        }

        private Stack GetStack(string stackName)
        {
            return stacks.FirstOrDefault(s => s.Name == stackName);
        }
        
        private Screen FindScreen(string screenName)
        {
            return registeredScreens.Find(s => s.screenName == screenName);
        }

        private Stack GetOrCreateStack(string stackName)
        {
            var stack = stacks.FirstOrDefault(s => s.Name == stackName);
            if (stack != null) return stack;
            stack = new Stack { Name = stackName };
            stacks.Add(stack);
            return stack;
        }
        
        private void PopScreen(string stackName)
        {
            var stack = GetStack(stackName);
            if (stack == null || stack.Count == 0)
                return;
            
            var current = stack.Pop();
            HideLayers(current);
            
            if (stack.Count > 0)
                ShowLayers(stack.Peek());
        }

        private static void ShowLayers(Screen screen)
        {
            foreach (var layerName in screen.layerNames)
            {
                LayerRegistry.Instance.SetLayerActive(layerName, true);
            }
        }
        
        private static void HideLayers(Screen screen)
        {
            foreach (var layerName in screen.layerNames)
            {
                LayerRegistry.Instance.SetLayerActive(layerName, false);
            }
        }
    }
}