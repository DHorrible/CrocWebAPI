using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Threading.Tasks;

namespace CrocWebAPI.Data.DataBaseTypes
{
    public abstract class InstanceBase : IInstance, IEquatable<InstanceBase>
    {
        #region Properties

        public Guid[] PrimaryKey { get; }

        #endregion

        #region Constructors

        public InstanceBase(Guid[] ids)
        {
            this.PrimaryKey = ids;

            var properties = this.GetType().GetProperties().Where(t => t.Name.StartsWith("ID"));

            int i = 0;

            foreach(var id in ids)
            {
                var propertie = properties.FirstOrDefault(t => t.Name == $"ID{i}");
                
                i++;

                propertie.SetValue(this, id);
            }
        }

        public InstanceBase(Guid id)
        {
            this.PrimaryKey = new[] { id };

            this.GetType().GetProperties().FirstOrDefault(t => t.Name == $"ID0").SetValue(this, id);
        }

        #endregion

        #region Operators

        public static bool operator ==(InstanceBase right, InstanceBase left)
        {
            return right?.PrimaryKey == left?.PrimaryKey;
        }

        public static bool operator !=(InstanceBase right, InstanceBase left)
        {
            return right?.PrimaryKey != left?.PrimaryKey;
        }

        #endregion

        #region Public methods

        public override bool Equals(object obj)
        {
            return this.Equals(obj as InstanceBase);
        }

        public bool Equals([AllowNull] InstanceBase other)
        {
            return other != null &&
                   this.PrimaryKey.Equals(other.PrimaryKey);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(this.PrimaryKey);
        }

        #endregion
    }
}
