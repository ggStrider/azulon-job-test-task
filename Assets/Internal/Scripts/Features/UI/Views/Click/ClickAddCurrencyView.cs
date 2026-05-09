using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Features.Click;
using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI.Views.Click
{
    public class ClickAddCurrencyView : MonoBehaviour
    {
        [SerializeField] private Button _clickButton;

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
            _clickService.Click();
        }
        
#if UNITY_EDITOR
        private void Reset()
        {
            if (_clickButton == null)
            {
                if (TryGetComponent<Button>(out var button))
                {
                    _clickButton = button;
                }
                else
                {
                    var buttonInChildren = GetComponentInChildren<Button>();
                    if (buttonInChildren != null)
                    {
                        _clickButton = buttonInChildren;
                    }
                }
            }
        }
#endif
    }
}