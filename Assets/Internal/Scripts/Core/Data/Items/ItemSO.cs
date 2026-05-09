using UnityEngine;

namespace Internal.Scripts.Core.Data.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; } = "Unnamed";
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: Space, TextArea(2, 4)]
        [field: SerializeField]
        public string Description { get; private set; }

        [field: Space]
        [field: SerializeField, Min(1)] public int Price { get; private set; } = 1;
        [field: SerializeField, Min(0)] public int ClickBonus { get; private set; } = 1;
        [field: SerializeField, Min(0)] public float PassiveIncomePerSecond { get; private set; } = 0f;
    }
}