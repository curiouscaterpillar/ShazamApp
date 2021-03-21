using System;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace ShazamApp.Core
{
    public interface IRecordContext<T>
    {
        Task<T> InsertAsync(T record);

        Task<IEnumerable<T>> GetAsync();

        Task CommitAsync();
    }
}
