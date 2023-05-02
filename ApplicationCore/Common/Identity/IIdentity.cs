namespace Application_Core.Common.Repository;

public interface IIdentity<T> : IEquatable<T> where T: IEquatable<T>
{
    public T Id { get; set; }

    bool IEquatable<T>.Equals(T? other)
    {
        return this.Equals(other);
    }
}