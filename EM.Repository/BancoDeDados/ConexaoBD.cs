using FirebirdSql.Data.FirebirdClient;
using System.Data.Common;

namespace EM.Repository.ConexaoBancoDeDados
{
    public static class ConexaoBD
    {
        private static readonly string servidor = "localhost" ;
        private static readonly int port = 3054;
        private static readonly string bancoDeDados = "C:\\Workspace Ester\\BD\\ESCOLA.FDB";
        private static readonly string user = "SYSDBA";
        private static readonly string password = "masterkey";
        private static readonly DbConnectionStringBuilder stringBuilder = [];

        public static FbConnection CrieConexao()
        {
            stringBuilder["DataSource"] = servidor;
            stringBuilder["Port"] = port;
            stringBuilder["Database"] = bancoDeDados;
            stringBuilder["UserID"] = user;
            stringBuilder["Password"] = password;
            stringBuilder["Charset"] = "UTF8";

            string connectionString = stringBuilder.ToString();
		    FbConnection connection = new(connectionString);
            connection.Open();
            return connection;
		}
    }

}