using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;

namespace KS.SimuladorPrecos.DataEntities.Utility
{
    internal class LogManager
    {
        /// <summary>
        /// Chama o Método Escritor de Log
        /// </summary>
        /// <remarks>Chamada ao Método Escritor de Log</remarks>
        /// <param name="oEx">Objeto Exceção</param>
        public static void CallLogWriter(Exception oEx)
        {
            WriteLog(oEx);
        }

        /// <summary>
        /// Chama o Método Escritor de Log de Acompanhamento
        /// </summary>
        /// <param name="sMessage">Mensagem a ser exibida</param>
        public static void CallLogWriter(string sMessage)
        {
            WriteLog(sMessage);
        }

        /// <summary>
        /// Chama o Método de Gravação de Log
        /// </summary>
        /// <remarks>Chamada ao Método de Gravação de Log</remarks>
        /// <param name="oEx">Objeto Exception</param>
        private static void WriteLog(Exception oEx)
        {
            LogTemplate(oEx);
        }

        /// <summary>
        /// Chama o Método de Gravação de Log
        /// </summary>
        /// <param name="sMessage">Mensagem a ser exibida</param>
        private static void WriteLog(string sMessage)
        {
            LogTemplate(sMessage);
        }

        /// <summary>
        /// Formata a Saída e Grava o Log de Erro Ocorrido
        /// </summary>
        /// <remarks>Template do Arquivo de Log de Erro do Sistema</remarks>
        /// <param name="oEx">Objeto Exceção</param>
        private static void LogTemplate(Exception oEx)
        {
            try
            {
                if (!Directory.Exists(Utility.FILELOGPATHDIRECTORY))
                    Directory.CreateDirectory(Utility.FILELOGPATHDIRECTORY);

                if (!Utility.ISTOSAVELOG)
                    return;

                if (!Directory.Exists(string.Format(Utility.FILELOGPATHDIRECTORYDATE, DateTime.Now.ToString("dd-MM-yyyy"))))
                    Directory.CreateDirectory(string.Format(Utility.FILELOGPATHDIRECTORYDATE, DateTime.Now.ToString("dd-MM-yyyy")));

                using (StreamWriter sWriter = new StreamWriter(string.Format(Utility.FILELOGPATH,
                                                               DateTime.Now.ToString("dd-MM-yyyy"),
                                                               ((object)HttpContext.Current.Session["USUARIO"])
                                                                    .GetType().GetProperties().Cast<PropertyInfo>()
                                                                        .Where(x => x.Name.Equals("UserID")).SingleOrDefault()
                                                                            .GetValue(((object)HttpContext.Current.Session["USUARIO"]), null)
                                                                                .ToString().ToUpper()),
                                                               true,
                                                               Encoding.Default))
                {                    
                    sWriter.WriteLine(Environment.NewLine);
                    sWriter.WriteLine("Log de Erro em " + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"));
                    sWriter.WriteLine(Environment.NewLine);
                    sWriter.WriteLine(string.Format("IP[{0}]", !String.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ?
                                                                    HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString() :
                                                                        HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString()));
                    sWriter.WriteLine(Environment.NewLine);
                    sWriter.WriteLine("Classe Onde Ocorreu: \r\n" + oEx.TargetSite.DeclaringType.ToString());
                    sWriter.WriteLine(Environment.NewLine);
                    sWriter.WriteLine("Método que Lançou a Exceção: \r\n" + oEx.TargetSite.ToString());
                    sWriter.WriteLine(Environment.NewLine);
                    sWriter.WriteLine("Erro Ocorrido: \r\n" + oEx.Message.ToString());
                    sWriter.WriteLine(Environment.NewLine);
                    sWriter.WriteLine("########################################");
                    sWriter.Flush();
                }
            }
            catch 
            {
                HttpCookie ErroAplicacao = new HttpCookie("ErroAplicacao");
                ErroAplicacao["Data"] = DateTime.Now.ToLongDateString();
                ErroAplicacao["Classe"] = oEx.TargetSite.DeclaringType.ToString();
                ErroAplicacao["Metodo"] = oEx.TargetSite.ToString();
                ErroAplicacao["InnerException"] = oEx.InnerException != null ? oEx.InnerException.Message : string.Empty;
                ErroAplicacao["MensagemErro"] = oEx.Message;
                HttpContext.Current.Response.Cookies.Add(ErroAplicacao);

                Error();
            }
        }

        /// <summary>
        /// Formata a Saída e Grava o Log de Acompanhamento
        /// </summary>
        /// <param name="sMessage">Mensagem a ser exibida</param>
        private static void LogTemplate(string sMessage)
        {
            try
            {
                if (!Directory.Exists(Utility.FILELOGDIRECTORY))
                    Directory.CreateDirectory(Utility.FILELOGDIRECTORY);

                if (!Directory.Exists(string.Format(Utility.FILELOGTRACKPATHDIRECTORYDATE, DateTime.Now.ToString("dd-MM-yyyy"))))
                    Directory.CreateDirectory(string.Format(Utility.FILELOGTRACKPATHDIRECTORYDATE, DateTime.Now.ToString("dd-MM-yyyy")));

                using (StreamWriter sWriter = new StreamWriter(string.Format(Utility.FILELOG,
                                                               DateTime.Now.ToString("dd-MM-yyyy"),
                                                               ((object)HttpContext.Current.Session["USUARIO"])
                                                                    .GetType().GetProperties().Cast<PropertyInfo>()
                                                                        .Where(x => x.Name.Equals("UserID")).SingleOrDefault()
                                                                            .GetValue(((object)HttpContext.Current.Session["USUARIO"]), null)
                                                                                .ToString().ToUpper(),
                                                               DateTime.Now.ToString("dd-MM-yyyy")),
                                                               true,
                                                               Encoding.Default))
                {

                    sWriter.WriteLine(string.Format("\r\n[{0}] IP[{1}]: {2}",
                                                    DateTime.Now.ToString("HH:mm:ss"),
                                                    !String.IsNullOrEmpty(HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"]) ? 
                                                        HttpContext.Current.Request.ServerVariables["HTTP_X_FORWARDED_FOR"].ToString() : 
                                                            HttpContext.Current.Request.ServerVariables["REMOTE_ADDR"].ToString(),
                                                    sMessage));
                    sWriter.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Redireciona o Servidor para a Página de Erro Padrão
        /// <remarks>Método Chamado Caso Ocorra Algum Erro Durante a Criação do Arquivo de Log Impossibilitando a Criação do Mesmo</remarks>
        /// </summary>
        private static void Error()
        {
            HttpContext.Current.Response.Redirect(Utility.ERRORPAGEPATH);
        }
    }
}
