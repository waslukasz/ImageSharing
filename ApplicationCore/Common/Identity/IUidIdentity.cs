namespace Application_Core.Common.Identity
{
    public interface IUidIdentity<T> : IIdentity<T> where T: IEquatable<T>
    {
        public Guid Guid { get; set; }
    }
}

