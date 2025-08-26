using SaveSystem;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Global
{
    public class FPSDisplay : MonoBehaviour
    {
        [SerializeField] private float _updateDelay;
        [SerializeField] private TextMeshProUGUI _textMesh;

        private Coroutine _updateCoroutine;
        private bool _isShowing;

        private void Awake()
        {
            if (SaveManager.Data.IsShowFPS)
            {
                Show();
            }
            else
            {
                Hide();
            }
        }

        public void Show()
        {
            if (_updateCoroutine != null)
            {
                return;
            }

            _isShowing = true;

            _updateCoroutine = StartCoroutine(UpdateCoroutine());
            _textMesh.enabled = true;
        }

        public void Hide()
        {
            if (_updateCoroutine != null)
            {
                StopCoroutine(_updateCoroutine);
                _updateCoroutine = null;
            }

            _textMesh.enabled = false;
        }

        public int GetValue()
        {
            return Mathf.RoundToInt(1f / Time.deltaTime);
        }

        private IEnumerator UpdateCoroutine()
        {
            while (_isShowing)
            {
                yield return new WaitForSeconds(_updateDelay);
            }

            _textMesh.text = $"FPS: {GetValue()}";

            _updateCoroutine = null;
        }
    }
}