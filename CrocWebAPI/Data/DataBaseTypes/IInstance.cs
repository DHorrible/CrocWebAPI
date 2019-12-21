using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Data.DataBaseTypes
{
    public interface IInstance
    {
        Guid[] PrimaryKey { get; }
    }
}
