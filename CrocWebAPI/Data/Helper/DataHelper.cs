using CrocWebAPI.Data.DataBaseTypes;
using CrocWebAPI.Data.Extensions;
using CrocWebAPI.Info;
using CrocWebAPI.Npgsql.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Data.Helper
{
    public static class DataHelper
    {
        #region Static members



        #endregion

        #region Public methods

        public static string ToJson(Instance instance)
        {
            var vals = NpgsHelper.GetJoinValuesByIDs(
                instance.ID0,
                instance.ID1,
                instance.ID2,
                (SchemeInfo.InstanceReleases, instance.ReleaseID0, new[] {SchemeInfo.InstanceReleases.Produced, SchemeInfo.InstanceReleases.Place}),
                (SchemeInfo.InstanceSerials, instance.SerialID0, new[] {SchemeInfo.InstanceSerials.Number}),
                (SchemeInfo.InstanceBatches, instance.BatchID0, new[] {SchemeInfo.InstanceBatches.Batch}),
                (SchemeInfo.InstanceTypes, instance.TypeID0, new[] { SchemeInfo.InstanceTypes.Name }));

            if (vals == null)
            {
                return null;
            }

            if (!vals.TryGetValue(SchemeInfo.InstanceReleases, out var release) ||
                !vals.TryGetValue(SchemeInfo.InstanceSerials, out var serial) ||
                !vals.TryGetValue(SchemeInfo.InstanceBatches, out var batch) ||
                !vals.TryGetValue(SchemeInfo.InstanceTypes, out var type))
            {
                return null;
            }

            var content = GetImageContent(instance.TypeID0);
            var contentString = content == null
                ? string.Empty
                : Convert.ToBase64String(content); //string.Join("\",\"", content);
            
            return $@"
{{

    ""TypeName"": ""{type.Get<string>(SchemeInfo.InstanceTypes.Name)}"",
    ""Produced"": ""{release.Get<DateTime>(SchemeInfo.InstanceReleases.Produced)}"",
    ""Place"": ""{release.Get<string>(SchemeInfo.InstanceReleases.Place)}"",
    ""Number"": ""{serial.Get<string>(SchemeInfo.InstanceSerials.Number)}"",
    ""Batch"": ""{batch.Get<string>(SchemeInfo.InstanceBatches.Batch)}"",
    ""Image"": ""{contentString}"" 
}}
";
        }

        public static byte[] GetImageContent(Guid typeID)
        {
            var path = $"images/{typeID}.png";

            return File.Exists(path) ? File.ReadAllBytes(path) : null;
        }

        #endregion

        #region Private methods



        #endregion
    }
}
