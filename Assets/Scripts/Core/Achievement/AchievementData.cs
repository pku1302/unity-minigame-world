using UnityEngine;

namespace MiniGameWorld.Core
{
    [CreateAssetMenu(fileName = "AchievementData", menuName = "Scriptable Objects/AchievementData")]
    public class AchievementData : ScriptableObject
    {
        [field: SerializeField]
        public AchievementType Id { get; private set; }
        
        [field: SerializeField]
        public string Title { get; private set; }
        
        [field: SerializeField]
        public string Description { get; private set; }
        
        [field: SerializeField]
        public Sprite Icon { get; private set; }
    }

}