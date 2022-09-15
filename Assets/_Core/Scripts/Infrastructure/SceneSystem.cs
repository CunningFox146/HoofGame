using HoofGame.GameStates;
using HoofGame.Util;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace HoofGame.Infrastructure
{
    public class SceneSystem : MonoBehaviour
    {
        public void Reload()
        {
            SceneManager.LoadScene((int)Scenes.Gameplay);
            GameState.CurrentState = null;
        }
    }
}
