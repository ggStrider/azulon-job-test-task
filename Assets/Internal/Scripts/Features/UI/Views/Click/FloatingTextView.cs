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

        public void Initialize(int value)
        {
            _text.text = $"+{value}";
        }

        private void Update()
        {
            _timer += Time.deltaTime;

            var progress = _timer / _duration;
            transform.Translate(Vector3.up * (_moveSpeed * Time.deltaTime));
            
            var alpha = Mathf.Lerp(1f, 0f, progress);
            _text.alpha = alpha;

            if (_timer >= _duration)
                Destroy(gameObject);
        }
    }
}