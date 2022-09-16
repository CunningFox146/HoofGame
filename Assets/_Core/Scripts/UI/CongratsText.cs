using DG.Tweening;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace HoofGame.UI
{
    public class CongratsText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private List<string> _messages;
        [SerializeField] private CanvasGroup _canvasGroup;

        private Sequence _anim;

        public void ShowText()
        {
            _canvasGroup.alpha = 0f;
            gameObject.SetActive(true);
            _text.text = _messages[Random.Range(0, _messages.Count - 1)];
            _anim?.Kill();
            _anim = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, 0.25f))
                .Join(_text.rectTransform.DOScale(Vector3.one * 1.2f, 0.25f))
                .Append(_text.rectTransform.DOScale(Vector3.zero, 0.25f).SetDelay(0.5f))
                .Join(_canvasGroup.DOFade(0f, 0.75f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
