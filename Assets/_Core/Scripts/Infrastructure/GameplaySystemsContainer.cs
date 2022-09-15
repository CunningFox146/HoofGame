using HoofGame.Cameras;
using HoofGame.GameStates;
using HoofGame.Horse;
using HoofGame.InputActions;
using HoofGame.Player;
using UnityEngine;

namespace HoofGame.Infrastructure
{
    public class GameplaySystemsContainer : MonoBehaviour
    {
        public static GameplaySystemsContainer Instance { get; private set; }

        [field: SerializeField] public Hoof Hoof { get; private set; }
        [field: SerializeField] public PlayerController PlayerController { get; private set; }
        [field: SerializeField] public HorseAnimation Horse { get; private set; }
        [field: SerializeField] public CameraSystem CameraSystem { get; private set; }
        public GameplayInputActions GameplayInputActions { get; private set; }

        private void Awake()
        {
            Instance = this;
            GameplayInputActions = new();
            GameState.CurrentState = new GameStartState();
        }
    }
}
