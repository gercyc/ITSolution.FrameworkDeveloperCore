using ITSolution.Framework.BaseClasses;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses
{
    public class AppConnectionString
    {
        #region Private fields

        //Chaves p/montar a string
        private const string DataSource_Key = "Data Source=";
        private const string Server_Key = "Server=";
        private const string InitialCatalog_Key = "Initial Catalog=";
        private const string UserID_Key = "User ID=";
        private const string Password_Key = "Password=";
        private const string IntegratedSecurity_Key = "Integrated Security=";

        #endregion

        #region Public properties

        [Required]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "Nome da Conexão inválido")]
        public string ConnectionName { get; set; }

        /// <summary>
        /// Datasource
        /// </summary>
        [Required(ErrorMessage = "Servidor não informado")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "Servidor inválido")]
        public string ServerName { get; set; }

        /// <summary>
        /// Nome do banco
        /// </summary>
        [Required(ErrorMessage = "Banco de dados não informado")]
        [StringLength(int.MaxValue, MinimumLength = 2, ErrorMessage = "Banco de dados inválido")]
        public string Database { get; set; }

        /// <summary>
        /// Usuário de acesso
        /// </summary>
        [StringLength(int.MaxValue, MinimumLength = 0, ErrorMessage = "Usuário inválido")]
        public string User { get; set; }

        /// <summary>
        /// Senha de acesso ao banco
        /// </summary>
        public string Password { get; set; }

        public List<string> DatabaseList { get; set; }

        //When false, User ID and Password are specified in the connection.
        //When true, the current Windows account credentials are used for authentication.
        //Recognized values are true, false, yes, no, and sspi (strongly recommended), which is equivalent to true.
        public string IntegratedSecurity { get; set; }

        /// <summary>
        /// Tipo de conexão utilizada
        /// </summary>
        public DatabaseType ServerType { get; set; }

        /// <summary>
        /// String de conexão utilizada pelo DbContext
        /// </summary>
        public string ConnectionString { get; set; }
        public string Default { get; set; }
        public object ServerPort { get; internal set; }
        public string ServerHost { get; internal set; }
        public string ConnectData { get; internal set; }
        public string TnsAtp { get; internal set; }
        public string WalletLocation { get; set; }
        public string MinPoolSize { get; set; }
        public string MaxPoolSize { get; set; }
        public string ConnectionTimeOut { get; set; }

        #endregion

        #region Constructors

        public AppConnectionString()
        {
            this.ConnectionName = "";
            this.IntegratedSecurity = "true";
            this.DatabaseList = new List<string>();
        }

        /// <summary>
        /// Constrói um arquivo de conexão a partir da string
        /// </summary>
        /// <param name="name"></param>Nome da string de conexão
        /// <param name="connectionString"></param>
        public AppConnectionString(string name, string connectionString)
        {
            this.ServerType = DatabaseType.MSSQL;
            this.ConnectionName = name;
            this.ConnectionString = buildConnectionString(connectionString);
            this.DatabaseList = new List<string>();
        }

        /// <summary>
        /// Constrói um arquivo de conexão a partir da string
        /// Name da conexão padrão é ITS
        /// </summary>
        /// <param name="connectionString"></param>
        public AppConnectionString(string connectionString)
            : this("ITS", connectionString)
        {
        }

        public AppConnectionString(string connectionName, string serverName, string user, string pw, string database)
        {
            this.ServerType = DatabaseType.MSSQL;
            this.ConnectionName = connectionName;
            this.ServerName = serverName;
            this.Database = database;
            this.User = user;
            this.Password = pw;
            this.DatabaseList = new List<string>();
            this.ConnectionString = buildConnectionString().ToString();
        }

        #endregion

        #region Methods

        public AppConnectionString Clone()
        {
            return (AppConnectionString)this.MemberwiseClone();
        }

        /// <summary>
        /// Recria a string de conexão
        /// </summary>
        public void RebuildConnectionString()
        {
            StringBuilder builderConn = new StringBuilder();
            ;

            if (ServerType == DatabaseType.MSSQL)
                //constroi a string de conexão base
                builderConn = buildConnectionString();
            else if (ServerType == DatabaseType.Oracle)
            {
                string tnsbase =
                    "(description= (address=(protocol=tcps)(port={0})(host={1}))(connect_data=({2}))(security=(my_wallet_directory={3})))";
                this.TnsAtp = string.Format(tnsbase, ServerPort, ServerHost, ConnectData, WalletLocation);
                builderConn.AppendFormat("User Id={0};Password={1};Data Source={2};", User, Password, ConnectionName);
            }
            else if (ServerType == DatabaseType.SQLITE)
            {
                builderConn.AppendFormat("Data Source={0}", Database);
                this.ConnectionString = string.Format("Data Source={0}", Database);
            }

            this.ConnectionString = builderConn.ToString();
        }

        #endregion

        #region Interno

        /// <summary>
        /// Constrói o AppConfig com base na string de conexão informada
        /// </summary>
        /// <param name="connectionString"></param>
        /// <returns></returns>
        private string buildConnectionString(string connectionString)
        {
            //quebrando e montando a string
            //Data Source=(local);Initial Catalog=dbBalcao_DEV;Integrated Security=True;User ID=user1;Password=1234;
            var split = connectionString.Split(';');
            var sb = new StringBuilder();
            for (int i = 0; i < split.Length; i++)
            {
                var value = split[i];
                if (value.Contains(DataSource_Key))
                    this.ServerName = split[i].Replace(DataSource_Key, "");

                else if (value.Contains(Server_Key))
                    this.ServerName = split[i].Replace(Server_Key, "");

                else if (value.Contains(InitialCatalog_Key))
                    this.Database = split[i].Replace(InitialCatalog_Key, "");

                else if (value.Contains(IntegratedSecurity_Key))
                    this.IntegratedSecurity = split[i].Replace(IntegratedSecurity_Key, "");

                else if (value.Contains(UserID_Key))
                {
                    this.User = split[i].Replace(UserID_Key, "");
                    this.IntegratedSecurity = "False";
                }

                else if (value.Contains(Password_Key))
                    this.Password = split[i].Replace(Password_Key, "");

                else
                {
                    if (i < split.Length - 1)
                        //use o q tem na string de conexao
                        sb.Append(split[i]).Append(";");
                    else //o ultimo nao precisa de ;
                        sb.Append(split[i]);
                }
            }

            //se nao tem usuário
            if (this.User.IsNullOrEmpty())
                this.IntegratedSecurity = "True";


            //constroi a string de conexão
            var builderConn = buildConnectionString();

            //add o que nao esta padronizado
            builderConn.Append(sb);

            return builderConn.ToString();
        }

        /// <summary>
        /// Utiliza os dados da classe para gerar a string de conexão
        /// </summary>
        /// <returns></returns>
        private StringBuilder buildConnectionString()
        {
            //constroi a string de conexão
            var builderConn = new StringBuilder();

            builderConn.Append(DataSource_Key);
            builderConn.Append(this.ServerName);
            builderConn.Append(";");

            builderConn.Append(InitialCatalog_Key);
            builderConn.Append(this.Database);
            builderConn.Append(";");

            if (!string.IsNullOrWhiteSpace(this.User))
            {
                builderConn.Append("User ID=");
                builderConn.Append(this.User);
                builderConn.Append(";");

                //se nao tem usuario nao faz sentido ter senha
                //se tem usuario pode ou nao ter senha
                if (!this.Password.IsNullOrEmpty())
                {
                    builderConn.Append("Password=");
                    builderConn.Append(this.Password);
                    builderConn.Append(";");
                    builderConn.Append("Integrated Security=False;");
                }
            }
            else
            {
                builderConn.Append("Integrated Security=True;");
            }

            return builderConn;
        }

        #endregion

        public override string ToString()
        {
            return this.ConnectionName;
        }
    }
}