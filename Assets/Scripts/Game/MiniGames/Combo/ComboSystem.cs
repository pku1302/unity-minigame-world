using System;
using UnityEngine;

namespace MiniGameWorld.Game
{
    public class ComboSystem : MonoBehaviour
    {
        public event Action<int> ComboChanged;
        public int Combo => m_Combo;

        int m_Combo;
        public void AddCombo()
        {
            m_Combo++;

            ComboChanged?.Invoke(m_Combo);
        }
        public void ResetCombo()
        {
            if (m_Combo == 0)
                return;

            m_Combo = 0;
        }
    }
}
