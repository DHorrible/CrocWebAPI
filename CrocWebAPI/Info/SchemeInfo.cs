using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Info
{
    public static class SchemeInfo
    {
        #region Schemes

        public partial class InstanceTypesScheme
        {
            private readonly string name = "InstanceTypes";

            public readonly string ID0 = "ID0";
            public readonly string Name = "Name";

            public static implicit operator string(InstanceTypesScheme obj)
            {
                return obj.ToString();
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public partial class InstanceBatchesScheme
        {
            private readonly string name = "InstanceBatches";

            public readonly string ID0 = "ID0";
            public readonly string Batch = "Batch";

            public static implicit operator string(InstanceBatchesScheme obj)
            {
                return obj.ToString();
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public partial class InstanceReleasesScheme
        {
            private readonly string name = "InstanceReleases";

            public readonly string ID0 = "ID0";
            public readonly string Produced = "Produced";
            public readonly string Place = "Place";

            public static implicit operator string(InstanceReleasesScheme obj)
            {
                return obj.ToString();
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public partial class InstanceSerialsScheme
        {
            private readonly string name = "InstanceSerials";

            public readonly string ID0 = "ID0";
            public readonly string Number = "Number";

            public static implicit operator string(InstanceSerialsScheme obj)
            {
                return obj.ToString();
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        public partial class InstancesScheme
        {
            private readonly string name = "Instances";

            public readonly string ID0 = "ID0";
            public readonly string ID1 = "ID1";
            public readonly string ID2 = "ID2";
            public readonly string TypeID0 = "TypeID0";
            public readonly string SerialID0 = "SerialID0";
            public readonly string ReleaseID0 = "ReleaseID0";
            public readonly string BatchID0 = "BatchID0";

            public static implicit operator string(InstancesScheme obj)
            {
                return obj.ToString();
            }

            public override string ToString()
            {
                return this.name;
            }
        }

        #endregion

        #region Share

        public static InstanceBatchesScheme InstanceBatches = new InstanceBatchesScheme();
        public static InstanceReleasesScheme InstanceReleases = new InstanceReleasesScheme();
        public static InstanceSerialsScheme InstanceSerials = new InstanceSerialsScheme();
        public static InstanceTypesScheme InstanceTypes = new InstanceTypesScheme();
        public static InstancesScheme Instances = new InstancesScheme();

        #endregion
    }
}
