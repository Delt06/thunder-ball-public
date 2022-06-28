using System;
using UnityEngine;

namespace Game.Ui
{
    public class ScreenManager : MonoBehaviour, IScreenManager
    {
        private IScreen[] _screens;

        public void Init()
        {
            _screens = GetComponentsInChildren<IScreen>(true);
        }

        public TScreen Get<TScreen>() where TScreen : IScreen
        {
            foreach (var screen in _screens)
            {
                if (screen is TScreen castedScreen)
                    return castedScreen;
            }

            throw new ArgumentException($"Screen of type {typeof(TScreen)} not found.");
        }
    }
}