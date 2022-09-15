using HoofGame.Cameras;
using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.Player;
using UnityEngine;

namespace HoofGame.UI
{
    public class GameplayView : View
    {
        [SerializeField] private ProgressBar _progressBar;
        [SerializeField] private WarningText _warning;
        [SerializeField] private CinemachineShake _shake;

        private Hoof _hoof;
        private PlayerController _playerController;

        private void Start()
        {
            _hoof = GameplaySystemsContainer.Instance.Hoof;
            _playerController = GameplaySystemsContainer.Instance.PlayerController;

            _hoof.DirtPercentChanged += UpdateProgress;
            _playerController.SmallRange += OnSmallRangeHandler;
            _playerController.TooFast += OnTooFastHandler;
        }

        private void OnDestroy()
        {
            _hoof.DirtPercentChanged -= UpdateProgress;
            _playerController.SmallRange -= OnSmallRangeHandler;
            _playerController.TooFast -= OnTooFastHandler;
        }

        private void UpdateProgress(float percent)
        {
            _progressBar.SetProgress(1f - percent);
        }

        private void OnTooFastHandler()
        {
            _warning.ShowTooFastWarning();
            _shake.Shake(1f, .25f);
        }

        private void OnSmallRangeHandler()
        {
            _warning.ShowRangeWarning();
            _shake.Shake(1f, .25f);
        }
    }
}
