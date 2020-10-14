using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using Uplift.DataAccess.Data.IRepository;
using UpliftApp.DataAccess.Data;

namespace Uplift.DataAccess.Data.Repository
{
    public class SP_Call : ISP_Call
    {
        private readonly ApplicationDbContext _db;
        private IDbConnection Db;

        public SP_Call(ApplicationDbContext db)
        {
            this._db = db;
            Db = new SqlConnection(db.Database.GetDbConnection().ConnectionString);
        }

        public T ExecuteReturnScaler<T>(string procedureName, DynamicParameters param = null)
        {

            return (T)Convert.ChangeType(Db.ExecuteScalar<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure), typeof(T));

        }

        public void ExecuteWithoutReturn(string procedureName, DynamicParameters param = null)
        {

            Db.Execute(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

        }

        public IEnumerable<T> ReturnList<T>(string procedureName, DynamicParameters param = null)
        {

            return Db.Query<T>(procedureName, param, commandType: System.Data.CommandType.StoredProcedure);

        }

        public void Dispose()
        {
            _db.Dispose();
        }
    }
}
