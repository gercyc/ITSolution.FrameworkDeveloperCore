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
                case  Operator.Between:
                    op = " BETWEEN ";
                    break;
                case  Operator.In:
                    op = " IN ";
                    break;
                case  Operator.Like:
                    op = " LIKE ";
                    break;
                case  Operator.NotEquals:
                    op = " != ";
                    break;
                case  Operator.NotIn:
                    op = " NOT IN ";
                    break;
                case  Operator.NotLike:
                    op = " NOT LIKE ";
                    break;
                default:
                    op = " = ";
                    break;
            }

            return op;
        }
    }
}