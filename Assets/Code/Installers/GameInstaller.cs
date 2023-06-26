using Code.Configs;
using UnityEngine;
using Zenject;

namespace Code.Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private CardsTexturesHolder cardsTexturesHolder;
        
        [Space,Header("Configs")]
        [SerializeField] private CardsConfig cardsConfig;

        public override void InstallBindings()
        {
            Container.Bind<CardsTexturesHolder>().FromInstance(cardsTexturesHolder).AsSingle().NonLazy();
            
            //Configs
            Container.Bind<CardsConfig>().FromInstance(cardsConfig).AsSingle().NonLazy();
        }
    }
}