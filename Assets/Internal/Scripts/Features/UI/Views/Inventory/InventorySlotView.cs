using System.Text;
using Internal.Scripts.Core.Data.Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI.Views.Inventory
{
    public class InventorySlotView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;
        
        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void Render(InventoryItem item)
        {
            var isEmpty = item == null;

            _icon.gameObject.SetActive(!isEmpty);
            _amountText.gameObject.SetActive(!isEmpty);

            if (isEmpty)
                return;

            _icon.sprite = item.Item.Icon;

            _stringBuilder.Clear();
            if (item.Amount > 1)
            {
                _stringBuilder.Append('x');
                _stringBuilder.Append(item.Amount);
            }
            
            _amountText.text = _stringBuilder.ToString();
        }
    }
}