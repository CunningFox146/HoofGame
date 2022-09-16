using DG.Tweening;
using HoofGame.Infrastructure;
using HoofGame.Levels;
using TMPro;
using UnityEngine;

namespace HoofGame.UI
{
    [RequireComponent(typeof(CanvasGroup))]
    public class GameEndView : View
    {
        [SerializeField] private TMP_Text _text;
        private CanvasGroup _canvasGroup;

        private void Awake()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
        }

        public override void Show()
        {
            base.Show();
            _canvasGroup.alpha = 0f;
            _canvasGroup.DOFade(1f, 0.5f);

            _text.text = $"Level {LevelSystem.Instance.CurrentLevel.LevelNum + 1} passed!";
        }
    }
}
