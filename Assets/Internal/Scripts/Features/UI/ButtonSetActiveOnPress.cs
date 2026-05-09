using UnityEngine;
using UnityEngine.UI;

namespace Internal.Scripts.Features.UI
{
    public class ButtonSetActiveOnPress : MonoBehaviour
    {
        [SerializeField] private Button _button;

        [Space]
        [SerializeField] private GameObject _gameObjectToToggle;
        [SerializeField] private bool _toggleToState;

        private void Awake()
        {
            _button.onClick.AddListener(() => _gameObjectToToggle.SetActive(_toggleToState));
        }

#if UNITY_EDITOR
        private void Reset()
        {
            if (_button == null)
            {
                if (TryGetComponent<Button>(out var button))
                {
                    _button = button;
                }
            }

            if (_gameObjectToToggle == null)
            {
                _gameObjectToToggle = gameObject;
            }
        }
#endif
    }
}