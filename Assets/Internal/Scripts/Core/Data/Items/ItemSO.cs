using UnityEngine;

namespace Internal.Scripts.Core.Data.Items
{
    [CreateAssetMenu(fileName = "New Item", menuName = "Game/Item")]
    public class ItemSO : ScriptableObject
    {
        [field: SerializeField] public string Name { get; private set; } = "Unnamed";
        [field: SerializeField] public Sprite Icon { get; private set; }

        [field: Space, TextArea(2, 4)]
        [field: SerializeField] public string Description { get; private set; }

        [field: Space, Min(1)]
        [field: SerializeField] public int ClickBonus { get; private set; } = 1;
    }
}
