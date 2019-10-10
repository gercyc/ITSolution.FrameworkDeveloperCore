using System.Data;
using System.Data.Common;
using System.Threading;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public class CustomDbParameter : DbParameter
    {
        public override void ResetDbType()
        {
            this.DbType = DbType.String;
        }

        public CustomDbParameter(string parameterName, object parameterValue)
        {
            this.ParameterName = parameterName;
            this.Value = parameterValue;
            
        }
        
        public override DbType DbType { get; set; }
        public override ParameterDirection Direction { get; set; }
        public override bool IsNullable { get; set; }
        public override string ParameterName { get; set; }
        public override string SourceColumn { get; set; }
        public override object Value { get; set; }
        public override bool SourceColumnNullMapping { get; set; }
        public override int Size { get; set; }
        public Operator Operator { get; set; }
        public Condition Condition { get; set; }
    }
}