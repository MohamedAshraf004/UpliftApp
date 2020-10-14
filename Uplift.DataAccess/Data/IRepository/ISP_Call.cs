using Dapper;
using System;
using System.Collections.Generic;

namespace Uplift.DataAccess.Data.IRepository
{
    public interface ISP_Call : IDisposable
    {
        T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null);
        void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null);
        IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null);
    }
}