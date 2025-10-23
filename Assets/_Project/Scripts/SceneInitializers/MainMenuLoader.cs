using _Project.Scripts.AddressablesHandling;
using _Project.Scripts.Common;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.InAppPurchasing;
using _Project.Scripts.Menu;
using UnityEngine;
using Zenject;

public class MainMenuLoader : MonoBehaviour
{
    private MainMenu _mainMenu;
    private IAPPresenter _iAPPresenter;
    private DataPersistenceHandler _dataPersistenceHandler;
    private ShopItemModel _shopItemModel;
    private PurchasingUI _purchasingUI;
    private SceneSwitcher _sceneSwitcher;

    private ILocalAssetLoader _assetLoader;

    [Inject]
    private void Construct(
        ILocalAssetLoader assetLoader,
        IAPPresenter iAPPresenter,
        DataPersistenceHandler dataPersistenceHandler,
        ShopItemModel shopItemModel,
        SceneSwitcher sceneSwitcher
        )
    {
        _assetLoader = assetLoader;
        _iAPPresenter = iAPPresenter;
        _dataPersistenceHandler = dataPersistenceHandler;
        _shopItemModel = shopItemModel;
        _sceneSwitcher = sceneSwitcher;
    }

    private async void Start()
    {
        _purchasingUI = await _assetLoader.InstantiateAsset<PurchasingUI>(LocalAssetsIDs.NO_ADS_MENU);
        _purchasingUI.Init(_iAPPresenter);
        _shopItemModel.Init(_purchasingUI);
        _mainMenu = await _assetLoader.InstantiateAsset<MainMenu>(LocalAssetsIDs.MAIN_MENU);
        _mainMenu.Init(_dataPersistenceHandler, _shopItemModel, _purchasingUI, _sceneSwitcher);
    }
}
