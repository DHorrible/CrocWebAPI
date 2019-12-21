using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Data.DataBaseTypes
{
    public sealed class InstanceType : InstanceBase
    {
        #region Properties

        public Guid ID0 { get; private set; }
        public string Name { get; private set; }
        
        #endregion

        #region Constructors

        public InstanceType(Guid id, string name)
                : base(id)
        {
            this.Name = name;
        }

        #endregion

        #region Public methods



        #endregion

        #region Operators



        #endregion
    }
}
