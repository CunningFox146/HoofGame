using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace HoofGame.UI
{
    public class ProgressBar : MonoBehaviour
    {
        [SerializeField] private Image _fg;
        [SerializeField] private TMP_Text _text;

        private Tween _fgTween;

        private void Awake()
        {
            _fg.fillAmount = 0f;
            SetProgress(0f);
        }

        public void SetProgress(float progress)
        {
            _text.text = $"{Mathf.Floor(progress * 100f)}%";
            _fgTween?.Kill();
            _fgTween = _fg.DOFillAmount(progress, 0.25f);
        }
    }
}
