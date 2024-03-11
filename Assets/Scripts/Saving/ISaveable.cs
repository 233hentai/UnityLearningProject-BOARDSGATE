namespace BOARDSGATE.Saving
{
    public interface ISaveable
    {
        object GetStates();
        void RestoreStates(object state);
    }
}