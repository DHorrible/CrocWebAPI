using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using CrocWebAPI.Data;
using CrocWebAPI.Data.DataBaseTypes;
using CrocWebAPI.Data.Extensions;
using CrocWebAPI.Info;
using CrocWebAPI.Npgsql.Manager;
using Npgsql;

namespace CrocWebAPI.Npgsql.Helper
{
    public static class NpgsHelper
    {
        public static Instance GetInstance(Guid[] ids)
        {
            var vals = NpgsHelper.GetValuesByIDs(
                ids,
                SchemeInfo.Instances,
                SchemeInfo.Instances.TypeID0,
                SchemeInfo.Instances.ReleaseID0,
                SchemeInfo.Instances.SerialID0,
                SchemeInfo.Instances.BatchID0);

            if (vals == null)
            {
                return null;
            }

            return new Instance(
                new[] { ids[0], ids[1], ids[2] },
                vals.Get<Guid>(SchemeInfo.Instances.TypeID0),
                vals.Get<Guid>(SchemeInfo.Instances.ReleaseID0),
                vals.Get<Guid>(SchemeInfo.Instances.SerialID0),
                vals.Get<Guid>(SchemeInfo.Instances.BatchID0)
                );
        }

        public static InstanceType GetInstanceType(Guid id)
        {
            var vals = GetValuesByIDs(
                id,
                SchemeInfo.InstanceTypes,
                SchemeInfo.InstanceTypes.ID0,
                SchemeInfo.InstanceTypes.Name
                );

            if (vals == null)
            {
                return null;
            }

            return new InstanceType(
                vals.Get<Guid>(SchemeInfo.InstanceTypes.ID0),
                vals.Get<string>(SchemeInfo.InstanceTypes.Name)
                );
        }

        public static Dictionary<string, object> GetValuesByIDs(
            Guid id,
            string tableName,
            params string[] fields)
        {
            return GetValuesByIDs(
                new[] { id },
                tableName,
                fields);
        }

        public static Dictionary<string, Dictionary<string, object>> GetJoinValuesByIDs(
            Guid id0,
            Guid id1,
            Guid id2,
            params (string table, Guid id, string[] fields)[] tuples)
        {
            var query = $"SELECT{Environment.NewLine}\t";

            foreach (var tuple in tuples)
            {
                foreach (var field in tuple.fields)
                {
                    query += $"{Environment.NewLine}\t\"{tuple.table}\".\"{field}\" AS \"{tuple.table}_{field}\",";
                }
            }

            query = query.Remove(query.Length - 1) + $@"
FROM ""{SchemeInfo.Instances}"" AS ""i""
";
            foreach (var tuple in tuples)
            {
                query += $@"
INNER JOIN ""{tuple.table}""
    ON ""{tuple.table}"".""ID0"" = '{tuple.id}'
";
            }

            query += $@"
WHERE ""i"".""ID0"" = '{id0}'
    AND ""i"".""ID1"" = '{id1}'
    AND ""i"".""ID2"" = '{id2}'
";

            if (NpgsManager.Open())
            {
                using (var reader = NpgsManager.SetCommand(query).ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        var vals = new Dictionary<string, Dictionary<string, object>>();
                        foreach (DbDataRecord dbDataRecord in reader)
                        {
                            foreach (var tuple in tuples)
                            {
                                var currentVals = vals[tuple.table] = new Dictionary<string, object>();
                                foreach (var field in tuple.fields)
                                {
                                    currentVals[field] = dbDataRecord[$"{tuple.table}_{field}"];
                                }
                            }

                            NpgsManager.Close();

                            return vals;
                        }
                    }
                }

                NpgsManager.Close();

                return null;
            }

            return null;
        }

        public static Dictionary<string, object> GetValuesByIDs(
            Guid[] ids,
            string tableName,
            params string[] fields)
        {
            var query = $"SELECT{Environment.NewLine}\t";
            
            if (!fields.Any())
            {
                return null;
            }
            else
            {
                foreach(var field in fields)
                {
                    query += $"\t\"t\".\"{field}\",";
                }

            }

            query = query.Remove(query.Length - 1) + $@"
FROM ""{tableName}"" AS ""t""
    WHERE TRUE";

            int i = 0;

            foreach (var id in ids)
            {
                query += $" AND \"t\".\"ID{i++}\" = '{id}'";
            }

            return GetValuesByQuery(query, fields);
        }

        public static Dictionary<string, object> GetValuesByQuery(
            string query,
            string[] fields)
        {
            if (NpgsManager.Open())
            {
                using (var reader = NpgsManager.SetCommand(query).ExecuteReader())
                {

                    if (reader.HasRows)
                    {
                        var vals = new Dictionary<string, object>();
                        foreach (DbDataRecord dbDataRecord in reader)
                        {
                            foreach (var field in fields)
                            {
                                vals[field] = dbDataRecord[field];
                            }

                            NpgsManager.Close();

                            return vals;
                        }
                    }
                }

                NpgsManager.Close();

                return null;
            }

            return null;
        }
    }
}
