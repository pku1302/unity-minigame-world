using UnityEngine;

namespace MiniGameWorld.Game
{
    public class ComboPresenter
    {
        private readonly ComboView m_View;
        private readonly ComboSystem m_System;

        public ComboPresenter(ComboSystem comboSystem, ComboView view)
        {
            m_System = comboSystem;
            m_View = view;

            m_System.ComboChanged += OnComboChanged;
        }
        public void Dispose()
        {
            m_System.ComboChanged -= OnComboChanged;
        }

        private void OnComboChanged(int combo)
        {
            Debug.Log("Combo!");

            if (combo <= 1)
                return;

            m_View.Show(combo);
        }
    }
}
