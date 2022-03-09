using System;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Configuration;
using System.Web.Security;

namespace KS.SimuladorPrecos.DataEntities.Utility
{
    /// <summary>
    /// Classe responsável por armazenar informações comuns a todo o projeto
    /// </summary>
    public static class Utility
    {
        #region :: Propriedades ::

        /// <summary>
        /// Verifica se deve-se ou não gravar logs de erro da aplicação
        /// </summary>
        internal static bool ISTOSAVELOG
        {
            get
            {
                if (WebConfigurationManager.AppSettings["SALVAR_LOG"] == null)
                    return false;
                else if (String.IsNullOrEmpty(WebConfigurationManager.AppSettings["SALVAR_LOG"].ToString()))
                    return false;
                else
                {
                    switch (WebConfigurationManager.AppSettings["SALVAR_LOG"].ToString().ToUpper())
                    {
                        case "SIM":
                            return true;
                        default:
                            return false;
                    }
                }
            }
        }

        #endregion

        #region :: Constantes ::

        /// <summary>
        /// Caminho da Página de Erro Genérica
        /// <remarks>Caminho Relativo da Página de Erro Genérica</remarks>
        /// </summary>
        public const string ERRORPAGEPATH = @"~/AppPaginaErro/ErrorPage.aspx";
        /// <summary>
        /// Expressão Regular para Validação de E-Mail
        /// </summary>
        public const string REVALIDAMAIL = @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*";
        /// <summary>
        /// Expressão Regular para Validação de Data
        /// <remarks>Expressão Regular para Validação de Data</remarks>
        /// </summary>
        public const string REVALIDADATA = @"^((0?[1-9]|[12]\d)\/(0?[1-9]|1[0-2])|30\/(0?[13-9]|1[0-2])|31\/(0?[13578]|1[02]))\/(19|20)?\d{2}$";
        /// <summary>
        /// Expressão Regular para Validação de Hora
        /// <remarks>Expressão Regular para Validação de Hora</remarks>
        /// </summary>
        public const string REVALIDAHORA = @"^([0-2]{1})([0-3]{1}):([0-5]{1})([0-9]{1})$";
        /// <summary>
        /// Validação de valores numéricos
        /// </summary>
        public const string REVALIDANUMERO = @"^([0-9]+)$";
        /// <summary>
        /// Valida um valor decimal
        /// </summary>
        public const string REVALIDADECIMAL = @"^([0-9]{1,2},[0-9]{1,4})$";
        /// <summary>
        /// cliente status cadastro não aprovado
        /// </summary>
        public const string StatusCadastroNaoAprovado = "Cadastro / Não Aprovado";
        /// <summary>
        /// cliente status apoio não aprovado
        /// </summary>
        public const string StatusApoioNaoAprovado = "Apoio / Não Aprovado";
        /// <summary>
        /// cliente status fiscal não aprovado
        /// </summary>
        public const string StatusFiscalNaoAprovado = "Fiscal / Não Aprovado";
        
        #endregion

        #region :: Campos Estáticos ::

        /// <summary>
        /// Caminho no Servidor Onde Serão Gravados os Arquivos de Log do Sistema
        /// <remarks>Caminho Relativo no Servidor Onde Serão Gravados os Arquivos de Log do Sistema</remarks>
        /// </summary>
        public static string FILELOGPATH = WebConfigurationManager.AppSettings["LOG"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["LOG"].ToString()) ? WebConfigurationManager.AppSettings["LOG"].ToString() + "\\{0}\\KRAFTSALESLOG_{1}.txt" : HttpContext.Current.Server.MapPath("~/AppLogFiles/{0}/KRAFTSALESLOG_{1}.txt") : HttpContext.Current.Server.MapPath("~/AppLogFiles/{0}/KRAFTSALESLOG_{1}.txt");
        /// <summary>
        /// Caminho no Servidor Onde Serão Gravados os Arquivos de Log do Sistema
        /// <remarks>Caminho Relativo no Servidor Onde Serão Gravados os Arquivos de Log do Sistema</remarks>
        /// </summary>
        public static string FILELOGPATHDIRECTORY = WebConfigurationManager.AppSettings["LOG"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["LOG"].ToString()) ? WebConfigurationManager.AppSettings["LOG"].ToString() : HttpContext.Current.Server.MapPath("~/AppLogFiles") : HttpContext.Current.Server.MapPath("~/AppLogFiles");
        /// <summary>
        /// Caminho no Servidor Onde Serão Gravados os Arquivos de Log do Sistema por data
        /// </summary>
        public static string FILELOGPATHDIRECTORYDATE = WebConfigurationManager.AppSettings["LOG"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["LOG"].ToString()) ? WebConfigurationManager.AppSettings["LOG"].ToString() + "\\{0}" : HttpContext.Current.Server.MapPath("~/AppLogFiles/{0}") : HttpContext.Current.Server.MapPath("~/AppLogFiles/{0}");
        /// <summary>
        /// Caminho no Servidor Onde Serão Gravados os Arquivos de Log do Sistema
        /// <remarks>Caminho Relativo no Servidor Onde Serão Gravados os Arquivos de Log do Sistema</remarks>
        /// </summary>
        public static string FILELOG = WebConfigurationManager.AppSettings["FILELOG"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["FILELOG"].ToString()) ? WebConfigurationManager.AppSettings["FILELOG"].ToString() + "\\{0}\\KRAFTSALESLOGTRACK_{1}.txt" : HttpContext.Current.Server.MapPath("~/AppLogFilesTrack/{0}/KRAFTSALESLOGTRACK_{1} {2}.txt") : HttpContext.Current.Server.MapPath("~/AppLogFilesTrack/{0}/KRAFTSALESLOGTRACK_{1} {2}.txt");
        /// <summary>
        /// Caminho no Servidor Onde Serão Gravados os Arquivos de Log do Sistema
        /// <remarks>Caminho Relativo no Servidor Onde Serão Gravados os Arquivos de Log do Sistema</remarks>
        /// </summary>
        public static string FILELOGDIRECTORY = WebConfigurationManager.AppSettings["FILELOGDIRECTORY"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["FILELOGDIRECTORY"].ToString()) ? WebConfigurationManager.AppSettings["FILELOGDIRECTORY"].ToString() : HttpContext.Current.Server.MapPath("~/AppLogFilesTrack") : HttpContext.Current.Server.MapPath("~/AppLogFilesTrack");
        /// <summary>
        /// 
        /// </summary>
        public static string FILELOGTRACKPATHDIRECTORYDATE = WebConfigurationManager.AppSettings["FILELOGDIRECTORY"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["FILELOGDIRECTORY"].ToString()) ? WebConfigurationManager.AppSettings["FILELOGDIRECTORY"].ToString() + "\\{0}" : HttpContext.Current.Server.MapPath("~/AppLogFilesTrack/{0}") : HttpContext.Current.Server.MapPath("~/AppLogFilesTrack/{0}");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos
        /// </summary>
        public static string FILEUPLOADDIRECTORY = WebConfigurationManager.AppSettings["ANEXOS_PRECLIENTE"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_PRECLIENTE"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_PRECLIENTE"].ToString() : HttpContext.Current.Server.MapPath("~/Anexos_PreCliente") : HttpContext.Current.Server.MapPath("~/Anexos_PreCliente");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos
        /// </summary>
        public static string FILEUPLOADDIRECTORYPATH = WebConfigurationManager.AppSettings["ANEXOS_PRECLIENTE"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_PRECLIENTE"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_PRECLIENTE"].ToString() + "\\{0}" : HttpContext.Current.Server.MapPath("~/Anexos_PreCliente/{0}") : HttpContext.Current.Server.MapPath("~/Anexos_PreCliente/{0}");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos
        /// </summary>
        public static string FILEUPLOADDIRECTORYEMPENHO = WebConfigurationManager.AppSettings["ANEXOS_EMPENHO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_EMPENHO"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_EMPENHO"].ToString() : HttpContext.Current.Server.MapPath("~/Anexos_Empenho") : HttpContext.Current.Server.MapPath("~/Anexos_Empenho");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos
        /// </summary>
        public static string FILEUPLOADDIRECTORYEMPENHOPATH = WebConfigurationManager.AppSettings["ANEXOS_EMPENHO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_EMPENHO"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_EMPENHO"].ToString() + "\\{0}" : HttpContext.Current.Server.MapPath("~/Anexos_Empenho/{0}") : HttpContext.Current.Server.MapPath("~/Anexos_Empenho/{0}");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos receita
        /// </summary>
        public static string FILEUPLOADDIRECTORYRECEITA= WebConfigurationManager.AppSettings["ANEXOS_RECEITA"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_RECEITA"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_RECEITA"].ToString() : HttpContext.Current.Server.MapPath("~/Anexos_Receita") : HttpContext.Current.Server.MapPath("~/Anexos_Receita");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos receita
        /// </summary>
        public static string FILEUPLOADDIRECTORYRECEITAPATH = WebConfigurationManager.AppSettings["ANEXOS_RECEITA"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_RECEITA"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_RECEITA"].ToString() + "\\{0}" : HttpContext.Current.Server.MapPath("~/Anexos_Receita/{0}") : HttpContext.Current.Server.MapPath("~/Anexos_Receita/{0}");
        /// <summary>
        /// 
        /// </summary>
        public static string FILEUPLOADDIRECTORYCONTRATO = WebConfigurationManager.AppSettings["ANEXOS_CONTRATO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_CONTRATO"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_CONTRATO"].ToString() : HttpContext.Current.Server.MapPath("~/Anexos_Contrato") : HttpContext.Current.Server.MapPath("~/Anexos_Contrato");
        /// <summary>
        /// Caminho para a gravação temporária de arquivos
        /// </summary>
        public static string FILEUPLOADDIRECTORYCONTRATOPATH = WebConfigurationManager.AppSettings["ANEXOS_CONTRATO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["ANEXOS_CONTRATO"].ToString()) ? WebConfigurationManager.AppSettings["ANEXOS_CONTRATO"].ToString() + "\\{0}" : HttpContext.Current.Server.MapPath("~/Anexos_Contrato/{0}") : HttpContext.Current.Server.MapPath("~/Anexos_Contrato/{0}");
        /// <summary>
        /// Recupera a Mensagem de Erro Genérica
        /// <remarks>Recupera a Mensagem de Erro Genérica</remarks>
        /// </summary>
        public static string ERRORMESSAGE = HttpContext.GetGlobalResourceObject("Resource", "msgErro").ToString();
        /// <summary>
        /// Mensagem padrão de observações sobre a inclusão do pré-cadastro do cliente
        /// </summary>
        public static string DEFAULTWFMESSAGE = HttpContext.GetGlobalResourceObject("Resource", "msgDefaultWorkFowMessage").ToString();
        /// <summary>
        /// Recupera a string de conexão da aplicação
        /// </summary>
        public static string CONNECTIONSTRING = WebConfigurationManager.ConnectionStrings["defaultConnection"].ToString();

        #region :: Alíquotas ::

        /// <summary>
        /// Recupera o valor padrão para o cálculo do ICMSe do custo padrão
        /// </summary>
        public static decimal VLR_CUSTOPADRAOICMSE = WebConfigurationManager.AppSettings["VLR_CUSTO_PADRAO_ICMSE"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_CUSTO_PADRAO_ICMSE"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_CUSTO_PADRAO_ICMSE"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_CUSTO_PADRAO_ICMSE"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Recupera o valor base para o cálculo do ajuste do regime fiscal
        /// </summary>
        public static decimal VLR_BASEAJUSTEREGIMEFISCAL = WebConfigurationManager.AppSettings["VLR_BASE_AJUSTEREGIMEFISCAL"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_BASE_AJUSTEREGIMEFISCAL"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_BASE_AJUSTEREGIMEFISCAL"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_BASE_AJUSTEREGIMEFISCAL"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Recupera o valor base para cálculo de ST para produtos de categoria SIMILAR para o RS.
        /// </summary>
        public static decimal VLR_STRSSIMILAR = WebConfigurationManager.AppSettings["VLR_STRSSIMILAR"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSIMILAR"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSIMILAR"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSIMILAR"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Recupera o valor base para cálculo de ST para produtos de categoria GENÉRICO para o RS.
        /// </summary>
        public static decimal VLR_STRSGENERICO = WebConfigurationManager.AppSettings["VLR_STRSGENERICO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSGENERICO"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSGENERICO"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSGENERICO"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Recupera o valor base para cálculo de ST para produtos de categoria NÃO SIMILAR para o RS.
        /// </summary>
        public static decimal VLR_STRSNAOSIMILAR = WebConfigurationManager.AppSettings["VLR_STRSNAOSIMILAR"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNAOSIMILAR"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNAOSIMILAR"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNAOSIMILAR"].ToString()) : new decimal() : new decimal() : new decimal();
        
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSPOSITIVASIMILAR = WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILAR"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILAR"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILAR"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILAR"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSPOSITIVASIMILARPRC = WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILARPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILARPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILARPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVASIMILARPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSPOSITIVAGENERICO = WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICO"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICO"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICO"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSPOSITIVAGENERICOPRC = WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICOPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICOPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICOPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAGENERICOPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSPOSITIVAOUTROS = WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROS"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROS"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROS"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROS"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSPOSITIVAOUTROSPRC = WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROSPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROSPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROSPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSPOSITIVAOUTROSPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEGATIVASIMILAR = WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILAR"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILAR"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILAR"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILAR"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEGATIVASIMILARPRC = WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILARPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILARPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILARPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVASIMILARPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEGATIVAGENERICO = WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICO"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICO"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICO"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEGATIVAGENERICOPRC = WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICOPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICOPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICOPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAGENERICOPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEGATIVAOUTROS = WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROS"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROS"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROS"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROS"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEGATIVAOUTROSPRC = WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROSPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROSPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROSPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEGATIVAOUTROSPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEUTRASIMILAR = WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILAR"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILAR"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILAR"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILAR"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEUTRASIMILARPRC = WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILARPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILARPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILARPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEUTRASIMILARPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEUTRAGENERICO = WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICO"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICO"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICO"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEUTRAGENERICOPRC = WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICOPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICOPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICOPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAGENERICOPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEUTRAOUTROS = WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROS"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROS"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROS"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROS"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSNEUTRAOUTROSPRC = WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROSPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROSPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROSPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSNEUTRAOUTROSPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSSEMLISTASIMILAR = WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILAR"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILAR"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILAR"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILAR"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSSEMLISTASIMILARPRC = WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILARPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILARPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILARPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTASIMILARPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSSEMLISTAGENERICO = WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICO"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICO"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICO"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICO"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSSEMLISTAGENERICOPRC = WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICOPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICOPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICOPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAGENERICOPRC"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSSEMLISTAOUTROS = WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROS"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROS"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROS"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROS"].ToString()) : new decimal() : new decimal() : new decimal();
        /// <summary>
        /// 
        /// </summary>
        public static decimal VLR_STRSSEMLISTAOUTROSPRC = WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROSPRC"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROSPRC"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROSPRC"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRSSEMLISTAOUTROSPRC"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Valor base para o cálculo de ST para a LISTA POSITIVA para o RJ.
        /// </summary>
        public static decimal VLR_STRJLISTAPOSITIVA = WebConfigurationManager.AppSettings["VLR_STRJLISTAPOSITIVA"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRJLISTAPOSITIVA"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRJLISTAPOSITIVA"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRJLISTAPOSITIVA"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Valor base para o cálculo de ST para a LISTA NEGATIVA para o RJ
        /// </summary>
        public static decimal VLR_STRJLISTANEGATIVA = WebConfigurationManager.AppSettings["VLR_STRJLISTANEGATIVA"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRJLISTANEGATIVA"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRJLISTANEGATIVA"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRJLISTANEGATIVA"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Valor base para o cálculo de ST para a LISTA NEUTRA para o RJ
        /// </summary>
        public static decimal VLR_STRJLISTANEUTRA = WebConfigurationManager.AppSettings["VLR_STRJLISTANEUTRA"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRJLISTANEUTRA"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRJLISTANEUTRA"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRJLISTANEUTRA"].ToString()) : new decimal() : new decimal() : new decimal();

        /// <summary>
        /// Valor base para o cálculo de ST para a SEM LISTA para o RJ
        /// </summary>
        public static decimal VLR_STRJLISTASEM = WebConfigurationManager.AppSettings["VLR_STRJLISTASEM"] != null ? !String.IsNullOrEmpty(WebConfigurationManager.AppSettings["VLR_STRJLISTASEM"].ToString()) ? new Regex(Utility.REVALIDADECIMAL).IsMatch(WebConfigurationManager.AppSettings["VLR_STRJLISTASEM"].ToString()) ? decimal.Parse(WebConfigurationManager.AppSettings["VLR_STRJLISTASEM"].ToString()) : new decimal() : new decimal() : new decimal();

        #endregion

        #endregion

        #region :: Enum ::

        public enum TipoTelefone
        {
            /// <summary>
            /// TELEFONE
            /// </summary>
            TELEFONE,
            /// <summary>
            /// Cnpj
            /// </summary>
            CELULAR
        }
        

        /// <summary>
        /// Tipo do documento pra formatação
        /// </summary>
        public enum TipoDocumento
        {
            /// <summary>
            /// Cpf
            /// </summary>
            CPF,
            /// <summary>
            /// Cnpj
            /// </summary>
            CNPJ,
            /// <summary>
            /// Documento RG
            /// </summary>
            RG,
            /// <summary>
            /// Frete
            /// </summary>
            FRETE,
            /// <summary>
            /// Cartão de crédito
            /// </summary>
            CARTAOCREDITO,
            /// <summary>
            /// Formato do CEP
            /// </summary>
            CEP
        }

        /// <summary>
        /// Define ou remove a máscara
        /// </summary>
        public enum FormatOption
        {
            /// <summary>
            /// Aplica a máscara
            /// </summary>
            ApplyMask,
            /// <summary>
            /// Remove a máscara
            /// </summary>
            RemoveMask
        }

        #endregion

        #region :: Métodos ::


        public static bool IsNumber(string _value)
        {
            try
            {
                return
                    new Regex(Utility.REVALIDANUMERO)
                        .IsMatch(_value);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return false;
            }
        }
        /// <summary>
        /// Faz a Chamada ao Método de Gravação de Log de Erro
        /// <remarks>Chama o Método de Gravação de Arquivos de Log do Sistema</remarks>
        /// </summary>
        /// <param name="oEx">Objeto Exceção</param>
        public static void WriteLog(Exception oEx)
        {
            LogManager.CallLogWriter(oEx);
        }

        /// <summary>
        /// Faz a Chamada ao Método de Gravação de Log de Acompanhamento
        /// </summary>
        /// <param name="sMessage">Mensagem a ser exibida</param>
        /// <returns></returns>
        public static bool WriteLog(string sMessage)
        {
            try
            {
                LogManager.CallLogWriter(sMessage);
            }
            catch
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Criptografa a Senha
        /// </summary>
        /// <remarks>Criptografa a Senha Informada</remarks>
        /// <param name="sPassWd">Senha</param>
        /// <returns>Retorna a Senha Criptografada</returns>
        public static string EncryptPassword(string sPassWd)
        {
            return FormsAuthentication.HashPasswordForStoringInConfigFile(sPassWd, "SHA1");
        }

        /// <summary>
        /// Retorna o Tempo Total da Operação de Recebimento e/ou Conferência
        /// Na Posição 0: Informar a Data Final. Na Posição 1: Informar a Data Inicial
        /// </summary>
        /// <param name="oDr">Na Posição 0: Informar a Data Final. Na Posição 1: Informar a Data Inicial</param>
        /// <returns>Retorna o Tempo Total Formatado { DD HH:MM:SS }</returns>
        public static string DuracaoOperacao(string[] oDr)
        {
            oDr[0] = String.IsNullOrEmpty(oDr[0]) || DateTime.Parse(oDr[0]).Equals(DateTime.MinValue) ? DateTime.Now.ToString() : oDr[0];
            oDr[1] = String.IsNullOrEmpty(oDr[1]) || DateTime.Parse(oDr[1]).Equals(DateTime.MinValue) ? DateTime.Now.ToString() : oDr[1];

            return string.Format("{0}D {1}:{2}:{3}", (DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).Days > 0 ?
                                                    ((DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).ToString().Split('.'))[0].ToString().PadLeft(2, '0') :
                                                      (0).ToString().PadLeft(2, '0'),

                                                     (DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).Days > 0 ?
                                                  ((((DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).ToString().Split(':'))[0]).Split('.'))[1].ToString().PadLeft(2, '0') :
                                                    ((DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).ToString().Split(':'))[0].ToString().PadLeft(2, '0'),

                                                    ((DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).ToString().Split(':'))[1].ToString().PadLeft(2, '0'),

                                                    ((DateTime.Parse(oDr[0].ToString())
                                                        .Subtract(DateTime.Parse(oDr[1].ToString()))
                                                            .Duration()).ToString().Split(':'))[2].ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// Formata a string para a pesquisa na base de dados
        /// </summary>
        /// <param name="sValue">String a ser tratada</param>
        /// <returns>String formatada</returns>
        public static string FormataStringPesquisa(string sValue)
        {
            try
            {
                Regex regex = new Regex(@"[aáàâãeéêèiíìîoóôõòuúùûcçnñ]", RegexOptions.IgnoreCase);
                MatchCollection matches = regex.Matches(sValue);
                bool a = false, e = false, i = false, o = false, u = false, n = false, c = false;

                foreach (Match match in matches)
                {
                    switch (match.Value.ToLower())
                    {
                        case "a":
                        case "á":
                        case "à":
                        case "â":
                        case "ã":

                            if (a) continue;
                            sValue = new Regex(@"[aáàâã]", RegexOptions.IgnoreCase).Replace(sValue, "[aáàâã]");
                            sValue = sValue.TrimEnd();
                            a = true;
                            break;

                        case "e":
                        case "é":
                        case "ê":
                        case "è":

                            if (e) continue;
                            sValue = new Regex(@"[eéêè]", RegexOptions.IgnoreCase).Replace(sValue, "[eéêè]");
                            sValue = sValue.TrimEnd();
                            e = true;
                            break;

                        case "i":
                        case "í":
                        case "ì":
                        case "î":

                            if (i) continue;
                            sValue = new Regex(@"[iíìî]", RegexOptions.IgnoreCase).Replace(sValue, "[iíìî]");
                            sValue = sValue.TrimEnd();
                            i = true;
                            break;

                        case "o":
                        case "ó":
                        case "ô":
                        case "õ":
                        case "ò":

                            if (o) continue;
                            sValue = new Regex(@"[oóôõò]", RegexOptions.IgnoreCase).Replace(sValue, "[oóôõò]");
                            sValue = sValue.TrimEnd();
                            o = true;
                            break;

                        case "u":
                        case "ú":
                        case "ù":
                        case "û":

                            if (u) continue;
                            sValue = new Regex(@"[uúùû]", RegexOptions.IgnoreCase).Replace(sValue, "[uúùû]");
                            sValue = sValue.TrimEnd();
                            u = true;
                            break;

                        case "c":
                        case "ç":

                            if (c) continue;
                            sValue = new Regex(@"[cç]", RegexOptions.IgnoreCase).Replace(sValue, "[cç]");
                            sValue = sValue.TrimEnd();
                            c = true;
                            break;

                        case "n":
                        case "ñ":

                            if (n) continue;
                            sValue = new Regex(@"[nñ]", RegexOptions.IgnoreCase).Replace(sValue, "[nñ]");
                            sValue = sValue.TrimEnd();
                            n = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return sValue;
            }

            return sValue;
        }

        /// <summary>
        /// Caso o valor do parâmetro seja nulo, retorna o valor DBNull.Value para o banco de dados
        /// </summary>
        /// <param name="value">Valor a ser verificado</param>
        /// <returns></returns>
        public static object GetDataValue(object value)
        {
            if (value is DateTime)
            {
                if (DateTime.Parse(value.ToString()).Equals(DateTime.MinValue))
                    return DBNull.Value;
            }
            else if (value == null)
            {
                return DBNull.Value;
            }

            return value;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sValue"></param>
        /// <param name="_option"></param>
        /// <param name="tpFone"></param>
        /// <returns></returns>
        public static string FormataTelefone(string sValue, FormatOption _option, TipoTelefone tpFone = TipoTelefone.TELEFONE)
        {
            try
            {
                if (string.IsNullOrEmpty(sValue))
                    return string.Empty;

                MaskedTextProvider msk = null;

                switch (_option)
                {
                    case FormatOption.ApplyMask:

                        switch (tpFone)
                        {
                            case TipoTelefone.TELEFONE:
                                msk = new MaskedTextProvider(@"\(00\) 0000\-0000");
                                msk.Set(sValue);
                                break;

                            case TipoTelefone.CELULAR:

                                if (!String.IsNullOrEmpty(sValue))
                                {
                                    string ddd = sValue.Substring(0, 2);

                                    if (ddd == "11") // SP
                                    {
                                        msk = new MaskedTextProvider(@"\(00\) 00000\-0000");
                                        msk.Set(sValue);
                                        break;
                                    }
                                    else
                                    {
                                        sValue = sValue.Substring(0, 10);
                                        msk = new MaskedTextProvider(@"\(00\) 0000\-0000");
                                        msk.Set(sValue);
                                        break;
                                    }
                                }
                                else
                                    break;
                        }

                        return sValue = msk.ToString();

                    default:

                        return sValue = new Regex(@"[\s.()/-]+").Replace(sValue, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return sValue;
            }
        }

        /// <summary>
        /// Formata a saída de CNPJ e/ou CPF
        /// </summary>
        /// <param name="sValue">Valor do documento</param>
        /// <param name="_option">Tipo de saída</param>
        /// <param name="tpDoc">Tipo do documento para a formatação</param>
        /// <returns>String formatada</returns>
        public static string FormataDocumentos(string sValue, FormatOption _option, TipoDocumento tpDoc = TipoDocumento.CNPJ)
        {
            try
            {
                MaskedTextProvider msk = null;

                switch (_option)
                {
                    case FormatOption.ApplyMask:

                        switch (tpDoc)
                        {
                            case TipoDocumento.CPF:
                                msk = new MaskedTextProvider(@"000\.000\.000-00");

                                if (sValue.Length < 11)
                                    sValue = sValue.PadLeft(11, '0');

                                msk.Set(sValue);
                                break;

                            case TipoDocumento.CNPJ:
                                msk = new MaskedTextProvider(@"00\.000\.000/0000-00");

                                if (sValue.Length < 14)
                                    sValue = sValue.PadLeft(14, '0');

                                msk.Set(sValue);
                                break;

                            case TipoDocumento.RG:
                                msk = new MaskedTextProvider(@"00\.000\.000-0");

                                if (sValue.Length < 9)
                                    sValue = sValue.PadLeft(9, '0');

                                msk.Set(sValue);
                                break;

                            case TipoDocumento.CEP:
                                msk = new MaskedTextProvider(@"00000-000");

                                if (sValue.Length < 8)
                                    sValue = sValue.PadLeft(8, '0');

                                msk.Set(sValue);
                                break;
                        }

                        return sValue = msk.ToString();

                    default:
                        return sValue = new Regex(@"[\./-]+").Replace(sValue, string.Empty);
                }
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return sValue;
            }
        }

        /// <summary>
        /// Seta todos os valores string para UpperCase
        /// <param name="_class">Classe na qual serão formatadas as propriedades</param>
        /// </summary>
        public static void SetStringToUpper(object _class)
        {
            try
            {
                foreach (PropertyInfo prop in _class.GetType()
                                                    .GetProperties()
                                                        .Cast<PropertyInfo>()
                                                            .Where(x => x.PropertyType.Name.Equals("String") &&
                                                                        x.CanWrite))
                    if (prop.GetValue(_class, null) != null)
                        if (new Regex(Utility.REVALIDAMAIL).IsMatch(prop.GetValue(_class, null).ToString()))
                            prop.SetValue(_class, prop.GetValue(_class, null).ToString().ToLower(), null);
                        else
                            prop.SetValue(_class, prop.GetValue(_class, null).ToString().ToUpper(), null);
            }
            catch (Exception ex)
            {
                Utility.WriteLog(ex);
                return;
            }
        }

        #endregion
    }
}
