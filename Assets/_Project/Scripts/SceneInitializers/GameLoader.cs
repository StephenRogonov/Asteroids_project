using _Project.Scripts.AddressablesHandling;
using _Project.Scripts.Bootstrap.Analytics;
using _Project.Scripts.DataPersistence;
using _Project.Scripts.GameFlow;
using _Project.Scripts.Obstacles;
using _Project.Scripts.Player;
using _Project.Scripts.PlayerWeapons;
using _Project.Scripts.ScriptableObjects;
using _Project.Scripts.UI;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

public class GameLoader : MonoBehaviour
{
    [SerializeField] private Transform _shipSpawnPoint;

    private ILocalAssetLoader _assetLoader;

    private HudView _hudView;
    private HudPresenter _hudPresenter;
    private MobileControls _mobileControls;
    private PauseModel _pauseModel;
    private GameOverModel _gameOverModel;
    private PauseView _pauseView;
    private PausePresenter _pausePresenter;
    private GameOverView _gameOverView;
    private GameOverPresenter _gameOverPresenter;
    private ShipMovement _shipMovement;
    private MissilesFactory _missilesFactory;
    private PlayerInput _playerInput;
    private AnalyticsEventManager _analyticsEventManager;
    private ShipLaserAttack _shipLaserAttack;
    private ShipMissilesAttack _shipMissilesAttack;
    private ShipCollision _shipCollision;
    private WeaponTrigger _weaponTrigger;
    private ObstaclesFactory _obstaclesFactory;
    private ShipLaserConfig _shipLaserConfig;
    private DataPersistenceHandler _dataPersistenceHandler;
    private PauseSwitcher _pauseSwitcher;

    [Inject]
    private void Construct(
        HudPresenter hudPresenter,
        PauseModel pauseModel,
        GameOverModel gameOverModel,
        PausePresenter pausePresenter,
        GameOverPresenter gameOverPresenter,
        MissilesFactory missilesFactory,
        PlayerInput playerInput,
        AnalyticsEventManager analyticsEventManager,
        WeaponTrigger weaponTrigger,
        ObstaclesFactory obstaclesFactory,
        ShipLaserConfig shipLaserConfig,
        DataPersistenceHandler dataPersistenceHandler,
        PauseSwitcher pauseSwitcher
        )
    {
        _hudPresenter = hudPresenter;
        _pauseModel = pauseModel;
        _gameOverModel = gameOverModel;
        _pausePresenter = pausePresenter;
        _gameOverPresenter = gameOverPresenter;
        _missilesFactory = missilesFactory;
        _playerInput = playerInput;
        _analyticsEventManager = analyticsEventManager;
        _weaponTrigger = weaponTrigger;
        _obstaclesFactory = obstaclesFactory;
        _shipLaserConfig = shipLaserConfig;
        _dataPersistenceHandler = dataPersistenceHandler;
        _pauseSwitcher = pauseSwitcher;
    }

    private void Awake()
    {
        _assetLoader = new LocalAssetLoader();
    }

    private async void Start()
    {
        await InstantiateAddressables();
        PositionShip();
        GetShipComponents();
        PassDependencies();
    }

    private async UniTask InstantiateAddressables()
    {
        _hudView = await _assetLoader.InstantiateInternalAsset<HudView>(LocalAssetsIDs.HUD);
        _mobileControls = await _assetLoader.InstantiateInternalAsset<MobileControls>(LocalAssetsIDs.MOBILE_CONTROLS);
        _pauseView = await _assetLoader.InstantiateInternalAsset<PauseView>(LocalAssetsIDs.PAUSE_MENU);
        _gameOverView = await _assetLoader.InstantiateInternalAsset<GameOverView>(LocalAssetsIDs.GAME_OVER_MENU);
        _shipMovement = await _assetLoader.InstantiateInternalAsset<ShipMovement>(LocalAssetsIDs.PLAYER_SHIP);
    }

    private void PositionShip()
    {
        _shipMovement.DeactivateObject();
        _shipMovement.transform.position = _shipSpawnPoint.position;
        _shipMovement.ActivateObject();
    }

    private void GetShipComponents()
    {
        _shipLaserAttack = _shipMovement.GetComponent<ShipLaserAttack>();
        _shipMissilesAttack = _shipMovement.GetComponent<ShipMissilesAttack>();
        _shipCollision = _shipMovement.GetComponent<ShipCollision>();
    }

    private void PassDependencies()
    {
        _shipMovement.Init(_dataPersistenceHandler, _pauseSwitcher);
        _shipLaserAttack.Init(_shipLaserConfig, _dataPersistenceHandler);
        _shipMissilesAttack.Init(_missilesFactory);
        _pauseModel.Init(_mobileControls);
        _pausePresenter.Init(_pauseView);
        _gameOverPresenter.Init(_gameOverView);
        _hudPresenter.Init(_hudView, _shipMovement);
        _missilesFactory.Init(_shipMovement);
        _playerInput.Init(_shipMovement);
        _gameOverModel.Init(_mobileControls,_shipMovement, _shipCollision);
        _analyticsEventManager.Init(_shipMovement, _shipLaserAttack, _shipCollision);
        _weaponTrigger.Init(_shipLaserAttack, _shipMissilesAttack);
        _obstaclesFactory.Init(_shipMovement);
    }
}
