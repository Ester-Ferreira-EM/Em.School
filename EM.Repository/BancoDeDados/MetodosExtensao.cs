using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;

namespace EM.Domain.ExtensionMethods
{
    public static class MetodosExtensao
    {
        public static void CreateParameter(this DbParameterCollection dbParameter, string parameterName, object value) =>
        dbParameter.Add(new FbParameter(parameterName, value));
    }
}