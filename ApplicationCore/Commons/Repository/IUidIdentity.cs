﻿namespace Application_Core.Commons.Repository
{
    public interface IUidIdentity<T> : IIdentity<T> where T: IComparable<T>
    {
        public Guid Guid { get; set; }
    }
}

