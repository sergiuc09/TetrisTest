using System;

public class PlayerSave : DataSave<PlayerData>
{
    public PlayerSave()
    {
        //CheckNETConnection(callBack);
        _saveSystem = new LocalSaveSystem<PlayerData>("player.bin");
    }

    /*private async void CheckNETConnection(Action callBack)
    {
        _hasInternetConnection = await CheckHasInternetAsync();

        if (_hasInternetConnection)
            _saveSystem = new ServerSaveSystem<PlayerData>();
        else _saveSystem = new LocalSaveSystem<PlayerData>("player.bin");

        callBack?.Invoke();
    }*/

}
