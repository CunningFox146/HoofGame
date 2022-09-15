using UnityEngine;

namespace HoofGame.Levels
{
    [CreateAssetMenu(menuName = "Scriptable Objects / Level")]
    public class Level : ScriptableObject
    {
        [field: SerializeField] public int LevelNum { get; private set; }
        [field: SerializeField] public float WinPercent { get; private set; }
    }
}
