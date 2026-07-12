using UnityEngine;
using UnityEngine.UIElements;

namespace MiniGameWorld.UI
{
    public class ScoreView
    {
        private readonly Label m_ScoreLabel;
        public ScoreView(VisualElement root)
        {
            m_ScoreLabel = root.Q<Label>("score-label");
        }
        public void SetScore(int score)
        {
            m_ScoreLabel.text = $"Score : {score}";
        }
    }
}