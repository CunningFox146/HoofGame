using HoofGame.Cameras;
using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.Levels;
using HoofGame.UI;

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
            _hoof.gameObject.SetActive(true);
            _horse.StandUp();
            _viewSystem.HideAllViews();
            _viewSystem.ShowView<GameplayView>();

            _hoof.DirtPercentChanged += OnCleanPercentChanged;
        }

        public void OnExit()
        {
            _hoof.gameObject.SetActive(true);
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
