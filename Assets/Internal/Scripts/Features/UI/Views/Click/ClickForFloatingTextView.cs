using Internal.Scripts._Bootstrap.Locator;
using Internal.Scripts.Features.Click;
using UnityEngine;
using UnityEngine.Pool;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI.Views.Click
{
    public class ClickForFloatingTextView : MonoBehaviour
    {
        [SerializeField] private Button _clickButton;
        [SerializeField] private FloatingTextView _floatingTextPrefab;
        [SerializeField] private Transform _floatingTextSpawnPoint;

        [Space]
        [SerializeField] private int _defaultPoolSize = 5;
        [SerializeField] private int _maxPoolSize = 20;

        private IClickService _clickService;
        private ObjectPool<FloatingTextView> _floatingTextPool;

        private void Start()
        {
            _clickService = ServiceLocator.Get<IClickService>();
            
            _floatingTextPool = new ObjectPool<FloatingTextView>(
                createFunc: () => Instantiate(_floatingTextPrefab, _floatingTextSpawnPoint),
                actionOnGet: view => view.gameObject.SetActive(true),
                actionOnRelease: view => view.gameObject.SetActive(false),
                actionOnDestroy: view => Destroy(view.gameObject),
                defaultCapacity: _defaultPoolSize,
                maxSize: _maxPoolSize);

            _clickButton.onClick.AddListener(OnClickButton);
        }

        private void OnDestroy()
        {
            _clickButton.onClick.RemoveListener(OnClickButton);
            _floatingTextPool.Dispose();
        }

        private void OnClickButton()
        {
            var clickValue = _clickService.ClickValue.Value;
            var floatingText = _floatingTextPool.Get();
            
            floatingText.transform.position = _floatingTextSpawnPoint.position;
            floatingText.Initialize(clickValue, OnFloatingTextComplete);
        }

        private void OnFloatingTextComplete(FloatingTextView floatingText)
        {
            _floatingTextPool.Release(floatingText);
        }
    }
}