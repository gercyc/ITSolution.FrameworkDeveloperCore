using System.Linq;
using System.Text;
using ITSolution.Framework.Core.Common.BaseClasses.EnvironmentConfig;

namespace ITSolution.Framework.Core.Server.BaseClasses
{
    public static class UtilsParameters
    {
        public static string GetStringOperator(Operator @operator)
        {
            string op = string.Empty;
            switch (@operator)
            {
                case Operator.Equals:
                    op = " = ";
                    break;
                case Operator.Between:
                    op = " BETWEEN ";
                    break;
                case Operator.In:
                    op = " IN ";
                    break;
                case Operator.Like:
                    op = " LIKE ";
                    break;
                case Operator.NotEquals:
                    op = " != ";
                    break;
                case Operator.NotIn:
                    op = " NOT IN ";
                    break;
                case Operator.NotLike:
                    op = " NOT LIKE ";
                    break;
                default:
                    op = " = ";
                    break;
            }

            return op;
        }

        public static string GetStringCondition(Condition condition)
        {
            string op = string.Empty;
            switch (condition)
            {
                case Condition.And:
                    op = " AND ";
                    break;
                case Condition.Or:
                    op = " OR ";
                    break;
                default:
                    op = " ";
                    break;
            }

            return op;
        }

        public static string GetWhereCondition(ParameterList parameterList)
        {
            if (parameterList == null)
                return string.Empty;
            
            string separator = EnvironmentInformation.DatabaseType == DatabaseType.Oracle ? ":" : "@";
            StringBuilder sbCommandText = new StringBuilder();

            sbCommandText.AppendFormat(@" WHERE  ");

            foreach (CustomDbParameter parameter in parameterList)
            {
                sbCommandText.AppendFormat(@" {0} {1} {2} {3}",
                    parameter.ParameterName.Replace(separator, ""),
                    GetStringOperator(parameter.Operator),
                    string.Format("{0}{1}", separator, parameter.ParameterName),
                    GetStringCondition(parameter.Condition));
            }

            return sbCommandText.ToString();
        }
    }
}