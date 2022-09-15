using HoofGame.Cameras;
using HoofGame.Infrastructure;

namespace HoofGame.GameStates
{
    public class GameplayState : IGameState
    {
        private GameplaySystemsContainer _container;

        public GameplayState()
        {
            _container = GameplaySystemsContainer.Instance;
        }

        public void OnEnter()
        {
            _container.CameraSystem.SetCamera(CameraType.Gameplay);
            _container.Hoof.gameObject.SetActive(true);
            _container.Horse.StandUp();
        }

        public void OnExit()
        {
            _container.Hoof.gameObject.SetActive(true);
        }
    }
}
