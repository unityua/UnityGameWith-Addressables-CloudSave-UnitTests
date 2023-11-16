using PesPatron.Bundles;
using PesPatron.Core.SaveLoad;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace PesPatron.Core
{
    public class ProjectServicesBinder : ServiceBinder
    {
        [SerializeField] private AssetReferenceT<WebLevelsData> _webLevelsDataReference;
        [SerializeField] private LevelDataProvider _levelDataProvider;
        [SerializeField] private BundlesLoader _bundlesLoader;

        public override void BindServices(ServiceLocator projectServices)
        {
            GlobalGameData globalGameData = new GlobalGameData();

            projectServices.Bind(new WebLevelsLoader(_webLevelsDataReference));
            projectServices.Bind(_levelDataProvider);
            projectServices.Bind(globalGameData);
            projectServices.BindAs<SceneChanger, ISceneChanger>(new SceneChanger(_levelDataProvider));
            projectServices.BindAs<CloudSaveLoadSystem, ISaveLoadSystem>(new CloudSaveLoadSystem(globalGameData));
            projectServices.BindAs<BundlesLoader, IBundlesLoader>(_bundlesLoader);
        }
    }
}