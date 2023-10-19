using PesPatron.Core;
using UnityEngine;

namespace PesPatron.UI
{
    public class MainGameUI : MonoBehaviour
    {
        [SerializeField] private AnimalsLeftUI _animalsLeftUI;
        [SerializeField] private EndScreen _endScreen;

        public EndScreen EndScreen => _endScreen; 

        public void Construct(ISceneChanger sceneChanger)
        {
            _endScreen.Construct(sceneChanger);
        }

        public void Initialize(AnimalsCatchTarget animalsCatchTarget)
        {
            _animalsLeftUI.SetTargetAnimalsCount(animalsCatchTarget.TotalTargetsToCatch);
            animalsCatchTarget.CatchedTargetsCountChanged += (_, count) => _animalsLeftUI.SetCapturedAnimalsCount(count);

            _endScreen.Initialize();
        }
    }
}