using System;
using DataBaseAccessLib;

namespace KS.SimuladorPrecos.DataEntities
{
    /// <summary>
    /// Objeto de Acessos do Usuário ao sistema
    /// </summary>
    [Serializable]
    public class UsuarioAcesso
    {
        #region :: Globais ::

        public SQLAutomator sa = null;

        #endregion

        #region :: Campos ::

        /// <summary>
        /// Campo que provê acessoa o objeto Usuario
        /// </summary>
        private Usuario _usuario = new Usuario();

        #endregion

        #region :: Propriedades ::

        /// <summary>
        /// Id do objeto UsuarioAcesso
        /// </summary>
        public int usuarioAcessoId { get; set; }

        /// <summary>
        /// Id do usuário
        /// </summary>
        public string usuarioId { get; set; }

        /// <summary>
        /// Data em que o usuário acessou o sistema
        /// </summary>
        public DateTime usuarioAcessoData { get; set; }

        /// <summary>
        /// Endereço IP da máquina onde o usuário acessou o sistema pela última vez
        /// </summary>
        public string usuarioAcessoNroIP
        {
            get
            {
                return (!(String.IsNullOrEmpty(System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]))) ?
                                               System.Web.HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"] :
                                               System.Web.HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"];
            }
        }

        #endregion

        #region :: Objetos ::

        /// <summary>
        /// Objeto Usuario
        /// </summary>
        public Usuario Usuario
        {
            get { return this._usuario; }
            set { this._usuario = value; }
        }

        #endregion

        #region :: Métodos ::

        void CreateSA()
        {
            sa = new SQLAutomator(this, "KsUsuarioAcesso", "usuarioAcessoId", "usuarioAcessoId", "Usuario");
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public UsuarioAcesso()
        {
            CreateSA();
        }

        /// <summary>
        /// Inclui os dados informados na base de dados
        /// </summary>
        /// <returns>bool</returns>
        public bool Incluir()
        {
            DataBaseAccess da = new DataBaseAccess();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"IF EXISTS(
	                                            SELECT usuarioAcessoId FROM KsUsuarioAcesso WHERE usuarioId = '{0}'
                                              )
	                                            BEGIN
		                                            DELETE FROM KsUsuarioAcesso WHERE usuarioId = '{0}'
	                                            END
	                                            INSERT INTO KsUsuarioAcesso VALUES('{0}', GETDATE(), '{1}')", 
                                                usuarioId, 
                                                usuarioAcessoNroIP);

                if (!da.executeNonQuery(sSQL, this))
                    throw new Exception(da.LastMessage);

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                da.close();
            }
        }

        #endregion
    }
}
