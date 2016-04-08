﻿using Dapper;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System;

namespace SystemChecker.Model.Data.Repositories
{
    public interface ICheckToPerformRepository : IBaseRepository<CheckToPerform>
    {
        List<CheckToPerform> GetEnabledChecks();
    }

    public class CheckToPerformRepository : BaseRepository<CheckToPerform>, ICheckToPerformRepository
    {
        public CheckToPerformRepository(IDbConnection connection)
            : base(
                 connection,
                 "tblChecksToPerform",
                "CheckId",
                "CheckTypeId, SystemName, Settings, Outcomes, CheckSuiteId, Disabled, Updated",
                "@CheckTypeId, @SystemName, @Settings, @Outcomes, @CheckSuiteId, @Disabled, @Updated"
                 )
        { }
        
        public List<CheckToPerform> GetEnabledChecks()
        {
            return Connection.Query<CheckToPerform>(
                $@"SELECT {Columns}
                FROM [{TableName}] 
                WHERE Disabled is null")
                .ToList();
        }
    }
}
