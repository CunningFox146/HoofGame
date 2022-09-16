using Cysharp.Threading.Tasks;
using DG.Tweening;
using HoofGame.Cameras;
using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.Levels;
using HoofGame.UI;
using System;
using UnityEngine;
using CameraType = HoofGame.Cameras.CameraType;

namespace HoofGame.GameStates
{
    public class GameplayState : IGameState
    {
        private HorseAnimation _horse;
        private Hoof _hoof;
        private CameraSystem _cameraSystem;
        private float _dirtPercentTarget;
        private LevelSystem _levelSystem;
        private ViewSystem _viewSystem;

        public GameplayState()
        {
            var container = GameplaySystemsContainer.Instance;
            _cameraSystem = container.CameraSystem;
            _hoof = container.Hoof;
            _horse = container.Horse;
            _levelSystem = LevelSystem.Instance;
            _dirtPercentTarget = _levelSystem.CurrentLevel.WinPercent;
            _viewSystem = container.ViewSystem;
        }

        public void OnEnter()
        {
            _cameraSystem.SetCamera(CameraType.Gameplay);
            AnimateHoof();
            _horse.StandUp();
            _viewSystem.HideAllViews();
            _viewSystem.ShowView<GameplayView>();

            _hoof.DirtPercentChanged += OnCleanPercentChanged;
        }

        private void AnimateHoof()
        {
            _hoof.transform.eulerAngles = Vector3.right * 90f;
            _hoof.transform.localScale = Vector3.zero;
            _hoof.gameObject.SetActive(true);

            DOTween.Sequence()
                .Append(_hoof.transform.DOScale(Vector3.one, 0.5f))
                .Join(_hoof.transform.DORotate(Vector3.zero, 0.5f))
                .SetDelay(1f);
        }

        public void OnExit()
        {
            _hoof.gameObject.SetActive(true);
            DOTween.Sequence()
                .Append(_hoof.transform.DOScale(Vector3.zero, 0.5f))
                .Join(_hoof.transform.DORotate(Vector3.right * 90f, 0.5f))
                .SetDelay(0.75f);
            _hoof.DirtPercentChanged -= OnCleanPercentChanged;
        }

        private void OnCleanPercentChanged(float dirtPercent)
        {
            if (dirtPercent < _dirtPercentTarget)
            {
                _levelSystem.LevelPassed();
                GameState.CurrentState = new GameEndState();
            }
        }
    }
}
