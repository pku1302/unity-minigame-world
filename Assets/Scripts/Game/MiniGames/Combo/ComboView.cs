using System.Collections;
using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.Game
{
    public class ComboView : MonoBehaviour
    {
        [SerializeField]
        private UIDocument m_Document;

        private Label m_ComboLabel;
        private Coroutine m_Routine;

        private void Awake()
        {
            VisualElement root = m_Document.rootVisualElement;
            m_ComboLabel = root.Q<Label>("combo-label");
            m_ComboLabel.style.display = DisplayStyle.None;
        }
        public void Show(int combo)
        {
            if (m_Routine != null)
                StopCoroutine(m_Routine);

            m_Routine = StartCoroutine(ShowRoutine(combo));
        }
        private IEnumerator ShowRoutine(int combo)
        {
            m_ComboLabel.text = $"{combo} Combo!";
            m_ComboLabel.style.display = DisplayStyle.Flex;

            yield return new WaitForSeconds(1f);

            m_ComboLabel.style.display = DisplayStyle.None;

            m_Routine = null;
        }
    }

}