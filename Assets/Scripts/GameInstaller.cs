using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField] private UI UI;
    
    public override void InstallBindings()
    {
        Container.Bind<UI>().FromComponentOn(UI.gameObject).AsSingle();
        Container.Bind<Battler>().AsSingle();
    }
}