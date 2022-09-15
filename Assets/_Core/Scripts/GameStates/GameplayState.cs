using HoofGame.Cameras;
using HoofGame.Horse;
using HoofGame.Infrastructure;
using HoofGame.Levels;

namespace HoofGame.GameStates
{
    public class GameplayState : IGameState
    {
        private HorseAnimation _horse;
        private Hoof _hoof;
        private CameraSystem _cameraSystem;
        private float _dirtPercentTarget;
        private LevelSystem _levelSystem;

        public GameplayState()
        {
            var container = GameplaySystemsContainer.Instance;
            _cameraSystem = container.CameraSystem;
            _hoof = container.Hoof;
            _horse = container.Horse;
            _levelSystem = LevelSystem.Instance;
            _dirtPercentTarget = _levelSystem.CurrentLevel.WinPercent;
        }

        public void OnEnter()
        {
            _cameraSystem.SetCamera(CameraType.Gameplay);
            _hoof.gameObject.SetActive(true);
            _horse.StandUp();

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
