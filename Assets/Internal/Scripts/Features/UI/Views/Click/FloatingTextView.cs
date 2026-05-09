using System;
using System.Text;
using TMPro;
using UnityEngine;

namespace Internal.Scripts.Features.UI.Views.Click
{
    public class FloatingTextView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private float _duration = 1f;
        [SerializeField] private float _moveSpeed = 100f;

        private float _timer;
        private Action<FloatingTextView> _onComplete;

        private readonly StringBuilder _stringBuilder = new StringBuilder();

        public void Initialize(int value, Action<FloatingTextView> onComplete)
        {
            _stringBuilder.Clear();
            _stringBuilder.Append("+");
            _stringBuilder.Append(value);
            _text.text = _stringBuilder.ToString();

            _timer = 0f;
            _onComplete = onComplete;
            _text.alpha = 1f;
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            transform.Translate(Vector3.up * (_moveSpeed * Time.deltaTime));
            _text.alpha = Mathf.Lerp(1f, 0f, _timer / _duration);

            if (_timer >= _duration)
                _onComplete?.Invoke(this);
        }
    }
}