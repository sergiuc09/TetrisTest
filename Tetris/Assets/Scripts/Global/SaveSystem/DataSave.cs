using System.Threading.Tasks;

public abstract class DataSave<T>
{
    protected bool _hasInternetConnection;
    protected ISaveSystem<T> _saveSystem;


    /*protected async Task<bool> CheckHasInternetAsync()
    {
        await Task.Delay(100);

        return false;
    }*/

    public void Save(T data)
    {
        _saveSystem.Save(data);
    }

    public T Load()
    {
        return _saveSystem.Load();
    }

}
