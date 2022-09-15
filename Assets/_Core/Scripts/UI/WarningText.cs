using DG.Tweening;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

namespace HoofGame.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class WarningText : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private List<string> _tooFastStrings;
        [SerializeField] private List<string> _rangeStrings;

        private CanvasGroup _canvasGroup;
        private Sequence _anim;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public void ShowRangeWarning() => ShowText(_rangeStrings);
        public void ShowTooFastWarning() => ShowText(_tooFastStrings);

        private void ShowText(List<string> textPool)
        {
            _text.text = textPool[Random.Range(0, textPool.Count - 1)];
            gameObject.SetActive(true);
            _anim?.Kill();
            _anim = DOTween.Sequence()
                .Append(_canvasGroup.DOFade(1f, 0.1f))
                .Append(_canvasGroup.DOFade(0f, 0.25f).SetDelay(1f))
                .OnComplete(() => gameObject.SetActive(false));
        }
    }
}
