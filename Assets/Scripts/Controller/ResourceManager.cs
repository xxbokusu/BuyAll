///<summary>
/// <filename>ResourceManager.cs</filename>
///</summary>

using Scene;
using UniRx;
using ResourceLoad;

public class ResourceManager : Singleton<ResourceManager>
{
    #region field
    /// <summary>Prefab関係</summary>
    private PrefabLoader prefab_loader = new PrefabLoader();
    /// <summary>BGM読み込み</summary>
    private BgmDataLoader bgm_data_loader = new BgmDataLoader();
    /// <summary>SEの読み込み部分</summary>
    private SeDataLoader se_data_loader = new SeDataLoader();

    /// <summary>リソース読み込み完了通知</summary>
    private Subject<Unit> resource_endinng = new Subject<Unit>();
    #endregion field

    #region propaty
    /// <summary>Prefabローダー</summary>
    public PrefabLoader Prefab { get { return prefab_loader; } }

    /// <summary>BGMへのリソースアクセス</summary>
    public BgmDataLoader Bgm { get { return bgm_data_loader; } }

    /// <summary>Seへのリソースアクセス</summary>
    public SeDataLoader Se { get { return se_data_loader; } }

    /// <summary>ロード完了時に終了</summary>
    public Subject<Unit> CallLoadEndSubject { get { return resource_endinng; } }

    /// <summary>ロードが完了？</summary>
    public bool IsComplete
    {
        get
        {
            return (prefab_loader.IsComplete
                && bgm_data_loader.IsComplete
                && se_data_loader.IsComplete);
        }
    }
    #endregion　propaty

    #region method
    public void Init()
    {
        var _observable = Observable.Merge(
            prefab_loader.ProgressNotification,
            bgm_data_loader.ProgressNotification,
            se_data_loader.ProgressNotification
            );
        //3つ揃ったら完了通知
        _observable.Buffer(3).Subscribe(_t => complete());
    }

    public void Ready()
    {
        var _observable = Observable.Merge(
            prefab_loader.ProgressNotification,
            bgm_data_loader.ProgressNotification,
            se_data_loader.ProgressNotification
            );
        //3つ揃ったら完了通知
        _observable.Buffer(3).Subscribe(_t => complete());
    }

    /// <summary>次のシーンを読み込む</summary>
    public void ChangeScene(SceneDefinition next_scene)
    {
        bool is_load_ok = false;
        if (null == prefab_loader)//以下null対策
        {
            prefab_loader = new PrefabLoader();
            is_load_ok = true;
        }
        if (null == bgm_data_loader)
        {
            bgm_data_loader = new BgmDataLoader();
            is_load_ok = true;
        }
        if (null == se_data_loader)
        {
            se_data_loader = new SeDataLoader();
            is_load_ok = true;
        }

        if (is_load_ok)
        {
            Init();
        }
        else
        {
            Ready();
        }
        prefab_loader.LoadResource(next_scene);
        bgm_data_loader.LoadResource(next_scene);
        se_data_loader.LoadResource(next_scene);
    }

    /// <summary>読み込み完了</summary>
    private void complete()
    {
        Debug.Util.Log("Scene Resources Loading Complete");
        resource_endinng.OnNext(new Unit());
    }
    #endregion method

}
