using DataBaseAccessLib;
using System.Data;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    /// <summary>
    /// 
    /// </summary>
    [Serializable]
    public class SimuladorPrecoRegrasGerais
    {
        #region :: Globais ::

        /// <summary>
        /// 
        /// </summary>
        [NonSerialized]
        public SQLAutomator sa = null;

        #endregion

        #region :: Campos ::

        /// <summary>
        /// Verifica se já foi feito o cálculo do ICMS-ST e Ajuste de Regime Fiscal
        /// </summary>
        public bool _AliquotasCalculadas = false;
        public string usuarioId;
        public string pathArquivo;
        public string NomeArquivo;

        #endregion

        #region :: Propriedades ::

        /// <summary>
        /// 
        /// </summary>
        public string estabelecimentoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string convenioId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ufDestino { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string perfilCliente { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string Perfil { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string resolucaoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string usoExclusivoHospitalar { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal icmsStValor { get; set; }
        /// <summary>
        /// Usado para o cálculo de ICMS de saída
        /// </summary>
        public decimal? _icmsStValor { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal icmsSobreVenda { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal icmsStSobreVenda { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public decimal ajusteRegimeFiscal { get; set; }
        /// <summary>
        /// St já calculada
        /// </summary>
        public decimal _icmsStSobreVenda { get; set; }
        /// <summary>
        /// Ajuste já calculado
        /// </summary>
        public decimal _ajusteRegimeFiscal { get; set; }

        #endregion

        #region :: Métodos ::

        /// <summary>
        /// 
        /// </summary>
        void CreateSA()
        {
            sa = new SQLAutomator(this, "ksSimuladorPrecoRegrasGerais", null, null, null);
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public SimuladorPrecoRegrasGerais()
        {
            CreateSA();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private DataTable GetRegrasData()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL =
                    string.Format(@"SELECT	estabelecimentoId,
		                                    convenioId,
		                                    perfilCliente,
		                                    resolucaoId,
		                                    icmsStValor,
		                                    usoExclusivoHospitalar,
		                                    icmsSobreVenda,
		                                    icmsStSobreVenda,
		                                    ajusteRegimeFiscal,
                                            ufDestino
                                    FROM	ksSimuladorPrecoRegrasGerais
                                    WHERE 1=1	{0} {1} {2} {3} {4} {5}
                                    ORDER BY
                                            CAST(estabelecimentoId AS INT)",
                                             !string.IsNullOrEmpty(estabelecimentoId) ? "AND	estabelecimentoId = '" + estabelecimentoId + "'" : string.Empty,
                                             !string.IsNullOrEmpty(convenioId) ?  "AND convenioId LIKE '" + convenioId + "'" : string.Empty,
                                             !string.IsNullOrEmpty(perfilCliente) ?      "AND perfilCliente = '" + GetPerfil(perfilCliente) + "'" : string.Empty,
                                             !string.IsNullOrEmpty(resolucaoId) ? "AND resolucaoId LIKE '" + resolucaoId + "'" : string.Empty,
                                             !string.IsNullOrEmpty(ufDestino) ?   "AND ufDestino = '" + ufDestino + "'" : string.Empty,
                                             !string.IsNullOrEmpty(usoExclusivoHospitalar) ? "AND usoExclusivoHospitalar LIKE '" + usoExclusivoHospitalar + "'" : string.Empty);

                DataTable dt = da.getDataTable(sSQL, this);

                return dt;
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

        protected string GetPerfil(string _perfil)
        {
            switch (_perfil)
            {
                case "C":
                    return "contribuinte";

                default:
                    return "não contribuinte";
            }
        }

        private DataTable GetRegrasDataSimulaALL()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL =
                    string.Format(@"SELECT	estabelecimentoId,
		                                    convenioId,
		                                    perfilCliente,
		                                    resolucaoId,
		                                    icmsStValor,
		                                    usoExclusivoHospitalar,
		                                    icmsSobreVenda,
		                                    icmsStSobreVenda,
		                                    ajusteRegimeFiscal,
                                            ufDestino
                                    FROM	ksSimuladorPrecoRegrasGerais
                                    WHERE 1=1	{0} {1} {2} {3} {4} {5}
                                    ORDER BY
                                            CAST(estabelecimentoId AS INT)",
                                             !string.IsNullOrEmpty(estabelecimentoId) ? "AND	estabelecimentoId = '" + estabelecimentoId + "'" : string.Empty,
                                             !string.IsNullOrEmpty(convenioId) ? "AND convenioId LIKE '" + convenioId + "'" : string.Empty,
                                             !string.IsNullOrEmpty(perfilCliente) ? "AND perfilCliente = '" + perfilCliente + "'" : string.Empty,
                                             !string.IsNullOrEmpty(resolucaoId) ? "AND resolucaoId LIKE '" + resolucaoId + "'" : string.Empty,
                                             !string.IsNullOrEmpty(ufDestino) ? "AND ufDestino = '" + ufDestino + "'" : string.Empty,
                                             !string.IsNullOrEmpty(usoExclusivoHospitalar) ? "AND usoExclusivoHospitalar LIKE '" + usoExclusivoHospitalar + "'" : string.Empty);

                DataTable dt = da.getDataTable(sSQL, this);

                return dt;
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

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public SimuladorPrecoRegrasGerais GetRegras()
        {
            SimuladorPrecoRegrasGerais oRg = new SimuladorPrecoRegrasGerais();

            try
            {
                DataTable oDt = this.GetRegrasData();

                if (oDt != null)
                {
                    if (oDt.Rows.Count > 0)
                    {
                        foreach (DataRow row in oDt.Rows)
                        {
                            oRg.estabelecimentoId = row["estabelecimentoId"].ToString();
                            oRg.convenioId = row["convenioId"].ToString();
                            oRg.perfilCliente = row["perfilCliente"].ToString();
                            oRg.resolucaoId = row["resolucaoId"].ToString();
                            oRg.icmsStValor = !Convert.IsDBNull(row["icmsStValor"]) ? decimal.Parse(row["icmsStValor"].ToString()) : new decimal();
                            oRg.usoExclusivoHospitalar = row["usoExclusivoHospitalar"].ToString();
                            oRg.icmsSobreVenda = !Convert.IsDBNull(row["icmsSobreVenda"]) ? decimal.Parse(row["icmsSobreVenda"].ToString()) : new decimal();
                            oRg.icmsStSobreVenda = !Convert.IsDBNull(row["icmsStSobreVenda"]) ? decimal.Parse(row["icmsStSobreVenda"].ToString()) : new decimal();
                            oRg.ajusteRegimeFiscal = !Convert.IsDBNull(row["ajusteRegimeFiscal"]) ? decimal.Parse(row["ajusteRegimeFiscal"].ToString()) : new decimal();
                            oRg.ufDestino = row["ufDestino"].ToString();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRg;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public List<SimuladorPrecoRegrasGerais> GetRegrasICMSVenda()
        {
            List<SimuladorPrecoRegrasGerais> oRg = null;

            try
            {
                DataTable oDt = this.GetRegrasData();

                if (oDt != null)
                {
                    if (oDt.Rows.Count > 0)
                    {
                        oRg = new List<SimuladorPrecoRegrasGerais>();

                        foreach (DataRow row in oDt.Rows)
                        {
                            oRg.Add(
                                new SimuladorPrecoRegrasGerais
                                {
                                    estabelecimentoId = row["estabelecimentoId"].ToString(),
                                    convenioId = row["convenioId"].ToString(),
                                    perfilCliente = row["perfilCliente"].ToString(),
                                    resolucaoId = row["resolucaoId"].ToString(),
                                    //icmsStValor = !Convert.IsDBNull(row["icmsStValor"]) ?  decimal.Parse(row["icmsStValor"].ToString()) : new  decimal(),
                                    _icmsStValor = !Convert.IsDBNull(row["icmsStValor"]) ? decimal.Parse(row["icmsStValor"].ToString()) : new Nullable<decimal>(),
                                    usoExclusivoHospitalar = row["usoExclusivoHospitalar"].ToString(),
                                    icmsSobreVenda = !Convert.IsDBNull(row["icmsSobreVenda"]) ? decimal.Parse(row["icmsSobreVenda"].ToString()) : new decimal(),
                                    icmsStSobreVenda = !Convert.IsDBNull(row["icmsStSobreVenda"]) ? decimal.Parse(row["icmsStSobreVenda"].ToString()) : new decimal(),
                                    ajusteRegimeFiscal = !Convert.IsDBNull(row["ajusteRegimeFiscal"]) ? decimal.Parse(row["ajusteRegimeFiscal"].ToString()) : new decimal(),
                                    ufDestino = row["ufDestino"].ToString()
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRg;
        }

        public List<SimuladorPrecoRegrasGerais> GetRegrasICMSVendaSimlularALL()
        {
            List<SimuladorPrecoRegrasGerais> oRg = null;

            try
            {
                DataTable oDt = this.GetRegrasDataSimulaALL();

                if (oDt != null)
                {
                    if (oDt.Rows.Count > 0)
                    {
                        oRg = new List<SimuladorPrecoRegrasGerais>();

                        foreach (DataRow row in oDt.Rows)
                        {
                            oRg.Add(
                                new SimuladorPrecoRegrasGerais
                                {
                                    estabelecimentoId = row["estabelecimentoId"].ToString(),
                                    convenioId = row["convenioId"].ToString(),
                                    perfilCliente = row["perfilCliente"].ToString(),
                                    resolucaoId = row["resolucaoId"].ToString(),
                                    //icmsStValor = !Convert.IsDBNull(row["icmsStValor"]) ?  decimal.Parse(row["icmsStValor"].ToString()) : new  decimal(),
                                    _icmsStValor = !Convert.IsDBNull(row["icmsStValor"]) ? decimal.Parse(row["icmsStValor"].ToString()) : new Nullable<decimal>(),
                                    usoExclusivoHospitalar = row["usoExclusivoHospitalar"].ToString(),
                                    icmsSobreVenda = !Convert.IsDBNull(row["icmsSobreVenda"]) ? decimal.Parse(row["icmsSobreVenda"].ToString()) : new decimal(),
                                    icmsStSobreVenda = !Convert.IsDBNull(row["icmsStSobreVenda"]) ? decimal.Parse(row["icmsStSobreVenda"].ToString()) : new decimal(),
                                    ajusteRegimeFiscal = !Convert.IsDBNull(row["ajusteRegimeFiscal"]) ? decimal.Parse(row["ajusteRegimeFiscal"].ToString()) : new decimal(),
                                    ufDestino = row["ufDestino"].ToString()
                                });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oRg;
        }


        public bool InserirPrecoRegraTempDePrecoRegra()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {


                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Query Update ::

                string sSQL =
                        @" INSERT INTO ksSimuladorPrecoRegrasGeraisTemp
                                select * from ksSimuladorPrecoRegrasGerais";

                #endregion

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

        public bool InserirLog()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {


                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Query Update ::

                string sSQL =
                        string.Format(@" INSERT INTO ksSimuladorPrecoRegrasGeraisLog
                       (dataAtualizacao
                       ,usuarioId
                       ,pathArquivo
                       ,NomeArquivo)
                 VALUES
                       (getdate()
                       ,'{0}'
                       ,'{1}'
                       ,'{2}')", usuarioId, pathArquivo, NomeArquivo);


                #endregion              
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

        public bool InsertBulkCopy(DataTable dt, string Tabela)
        {
            SqlConnection _conn = null;

            SqlTransaction _transaction = null;
            try
            {
                Utility.Utility.SetStringToUpper(this);



                using (_conn = new SqlConnection(Utility.Utility.CONNECTIONSTRING))
                {
                    _conn.Open();

                    _transaction = _conn.BeginTransaction();
                    SqlBulkCopy copy = new SqlBulkCopy(_conn, SqlBulkCopyOptions.KeepIdentity, _transaction);
                    copy.DestinationTableName = Tabela;
                    copy.WriteToServer(dt);

                    _transaction.Commit();

                }


            }
            catch
            {
                _transaction.Rollback();
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public DataTable GetPrecoRegrasGerais()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT estabelecimentoId
                                              ,convenioId
                                              ,ufDestino
                                              ,perfilCliente
                                              ,resolucaoId
                                              ,icmsStValor
                                              ,usoExclusivoHospitalar
                                              ,icmsSobreVenda
                                              ,icmsStSobreVenda
                                              ,ajusteRegimeFiscal
                                          FROM ksSimuladorPrecoRegrasGerais WITH (NOLOCK) 
                                       
                                            WHERE 1=1 {0} {1}  {2} {3}  {4} {5} {6}
                                            ",
                                             !String.IsNullOrEmpty(estabelecimentoId) ? "AND estabelecimentoId = '" + estabelecimentoId + "'" : string.Empty,
                                             !String.IsNullOrEmpty(convenioId) ? "AND convenioId = '" + convenioId + "'" : string.Empty,
                                             !String.IsNullOrEmpty(ufDestino) ? "AND ufDestino = '" + ufDestino + "'" : string.Empty,
                                              !String.IsNullOrEmpty(perfilCliente) ? "AND perfilCliente = '" + perfilCliente + "'" : string.Empty,
                                              !String.IsNullOrEmpty(resolucaoId) ? "AND perfilCliente = '" + perfilCliente + "'" : string.Empty,
                                              !icmsStValor.Equals(null) ? "AND icmsStValor = " + icmsStValor : "AND icmsStValor IS NULL",
                                              !String.IsNullOrEmpty(usoExclusivoHospitalar) ? "AND usoExclusivoHospitalar = '" + usoExclusivoHospitalar + "'" : string.Empty);

                DataTable dt = da.getDataTable(sSQL, this);

                return dt;
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

        public DataTable GetPrecoRegrasGeraisTemp()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT estabelecimentoId
                                            ,convenioId
                                            ,ufDestino
                                            ,perfilCliente
                                            ,resolucaoId
                                            ,icmsStValor
                                            ,usoExclusivoHospitalar
                                            ,icmsSobreVenda
                                            ,icmsStSobreVenda
                                            ,ajusteRegimeFiscal
                                           FROM ksSimuladorPrecoRegrasGeraisTemp WITH (NOLOCK) 
                                            WHERE 1=1  {0} {1}  {2} {3}  {4} {5} {6}
                                            ",
                                            !String.IsNullOrEmpty(estabelecimentoId) ? " AND estabelecimentoId = '" + estabelecimentoId + "'" : string.Empty,
                                            !String.IsNullOrEmpty(convenioId) ? " AND convenioId = '" + convenioId + "'" : string.Empty,
                                            !String.IsNullOrEmpty(ufDestino) ? " AND ufDestino = '" + ufDestino + "'" : string.Empty,
                                            !String.IsNullOrEmpty(perfilCliente) ? " AND perfilCliente = '" + perfilCliente + "'" : string.Empty,
                                            !String.IsNullOrEmpty(resolucaoId) ? " AND perfilCliente = '" + perfilCliente + "'" : string.Empty,
                                            !icmsStValor.Equals(null) ? " AND icmsStValor = " + icmsStValor : "AND icmsStValor IS NULL",
                                            !String.IsNullOrEmpty(usoExclusivoHospitalar) ? " AND usoExclusivoHospitalar = '" + usoExclusivoHospitalar + "'" : string.Empty);

                DataTable dt = da.getDataTable(sSQL, this);

                return dt;
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

        public bool DeletarPrecoCustos()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE FROM ksSimuladorPrecoRegrasGerais";

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

        public bool DeletarPrecoCustosTemp()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE FROM ksSimuladorPrecoRegrasGeraisTemp";

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

        public bool DeletarItemPrecoRegrasGerais()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = @"DELETE FROM ksSimuladorPrecoRegrasGerais where estabelecimentoId=@estabelecimentoId AND convenioId=@convenioId  AND ufDestino=@ufDestino AND perfilCliente=@perfilCliente AND resolucaoId=@resolucaoId  AND icmsStValor=@icmsStValor";

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




        public DataTable GetPrecoRegrasGeraisLog()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT TOP 100 
                                                  dataAtualizacao
                                                  ,usuarioId
                                                  ,pathArquivo
                                                  ,NomeArquivo
                                FROM ksSimuladorPrecoRegrasGeraisLog WITH (NOLOCK) where 1=1 order by dataAtualizacao desc");

                DataTable dt = da.getDataTable(sSQL, this);

                return dt;
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
