using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Features.Click;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI.Views.Click
{
    public class ClickView : MonoBehaviour
    {
        [SerializeField] private Button _clickButton;
        [SerializeField] private FloatingTextView _floatingTextPrefab;
        [SerializeField] private Transform _floatingTextSpawnPoint;

        private IClickService _clickService;

        private void Start()
        {
            _clickService = ServiceLocator.Get<IClickService>();
            _clickButton.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            _clickButton.onClick.RemoveListener(OnClickButton);
        }

        private void OnClickButton()
        {
            var clickValue = _clickService.ClickValue.Value;
            _clickService.Click();
            
            var floatingText = Instantiate(_floatingTextPrefab, _floatingTextSpawnPoint);
            floatingText.Initialize(clickValue);
        }
    }
}