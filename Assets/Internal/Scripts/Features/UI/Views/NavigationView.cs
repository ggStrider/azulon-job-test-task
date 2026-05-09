using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI.Views
{
    public class NavigationView : MonoBehaviour
    {
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _inventoryButton;
        [SerializeField] private GameObject _shopView;
        [SerializeField] private GameObject _inventoryView;

        private void Start()
        {
            _shopButton.onClick.AddListener(() => Show(_shopView));
            _inventoryButton.onClick.AddListener(() => Show(_inventoryView));
        }

        private void OnDestroy()
        {
            _shopButton.onClick.RemoveAllListeners();
            _inventoryButton.onClick.RemoveAllListeners();
        }

        private void Show(GameObject target)
        {
            _shopView.SetActive(_shopView == target);
            _inventoryView.SetActive(_inventoryView == target);
        }
    }
}
