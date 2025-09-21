using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Nosmow_ScreenManager
{
    /*
     * The Stack can contain several screens, which is done to organize the screens by section and to be able to turn off
     * those sections completely.
     */
    
    [Serializable]
    public class Stack
    {
        public string Name;
        public List<Screen> Screens = new();

        public override string ToString()
        {
            return JsonUtility.ToJson(this);
        }

        public bool ContainsScreen(string screen)
        {
            return Screens.Any(s => s.name == screen);
        }

        public void Push(Screen screen)
        {
            Screens.Add(screen);
        }

        public Screen Pop()
        {
            if (Screens.Count == 0) return null;
            var last = Screens[^1];
            Screens.RemoveAt(Screens.Count - 1);
            return last;
        }

        public Screen Peek()
        {
            return Screens.Count == 0 ? null : Screens[^1];
        }

        public int Count => Screens.Count;
    }
}