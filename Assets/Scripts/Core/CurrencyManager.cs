using System;
using UnityEngine;

namespace MiniGameWorld.Core
{
    public class CurrencyManager
    {
        public event Action<int> CurrencyChanged;
        private readonly SaveManager m_SaveManager;
        private const string CurrencyKey = "Currency";

        private int m_Currency;
        public int Currency => m_Currency;
        public CurrencyManager(SaveManager saveManager)
        {
            m_SaveManager = saveManager;
        }
        public void Load()
        {
            m_Currency = m_SaveManager.LoadInt(CurrencyKey);
            CurrencyChanged?.Invoke(m_Currency);
        }
        public void Save()
        {
            m_SaveManager.SaveInt(CurrencyKey, m_Currency);
            m_SaveManager.Save();
        }
        public void Add(int amount)
        {
            if (amount <= 0)
                return;

            SetCurrency(m_Currency + amount);
        }
        public bool Spend(int amount)
        {
            if (amount <= 0)
                return false;

            if (m_Currency < amount)
                return false;

            SetCurrency(m_Currency - amount);

            return true;
        }
        private void SetCurrency(int value)
        {
            m_Currency = value;

            Save();

            CurrencyChanged?.Invoke(m_Currency);
        }
    }
}