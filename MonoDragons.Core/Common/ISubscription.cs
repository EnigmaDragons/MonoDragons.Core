namespace MonoDragons
{
    public interface ISubscription<in T>
    {
        void Update(T change);
    }
}
