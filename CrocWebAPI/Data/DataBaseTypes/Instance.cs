using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Data.DataBaseTypes
{
    public sealed class Instance : InstanceBase
    {
        #region Properties

        public Guid ID0 { get; private set; }
        public Guid ID1 { get; private set; }
        public Guid ID2 { get; private set; }
        public Guid TypeID0 { get; private set; }
        public Guid ReleaseID0 { get; private set; }
        public Guid SerialID0 { get; private set; }
        public Guid BatchID0 { get; private set; }

        #endregion

        #region Constructors

        public Instance(
            Guid[] ids, 
            Guid typeID0, 
            Guid releaseID0, 
            Guid serialID0,
            Guid batchID0)
                : base(ids)
        {
            this.TypeID0 = typeID0;
            this.ReleaseID0 = releaseID0;
            this.SerialID0 = serialID0;
            this.BatchID0 = batchID0;
        }

        #endregion

        #region Public methods


        #endregion

        #region Operators



        #endregion
    }
}
