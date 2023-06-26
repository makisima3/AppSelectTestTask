using UnityEngine;

namespace Code.Configs
{
    [CreateAssetMenu(fileName = "CardsConfig", menuName = "ScriptableObjects/CardsConfig", order = 1)]
    public class CardsConfig : ScriptableObject
    {
        [SerializeField] private float flipTime;

        public float FlipTime => flipTime;
    }
}