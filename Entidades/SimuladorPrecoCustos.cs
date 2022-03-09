using DataBaseAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.UI.WebControls;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    [Serializable]
    public class SimuladorPrecoCustos
    {
        [NonSerialized]
        public SQLAutomator sa = (SQLAutomator)null;
        public string pathArquivo;
        public bool itemAtualizado;
        public bool itemExcluido;
        public bool itemInserido;
      
        public string estabelecimentoId { get; set; }

        public string itemId { get; set; }

        public string ufIdOrigem { get; set; }

        public string itemDescricao { get; set; }

        public string laboratorioNome { get; set; }

        public string tipo { get; set; }

        public string exclusivoHospitalar { get; set; }

        public string NCM { get; set; }

        public string listaDescricao { get; set; }

        public string categoria { get; set; }

        public string resolucao13 { get; set; }

        public string tratamentoICMSEstab { get; set; }

        public string aplicacaoRepasse { get; set; }

        public string reducaoST_MVA { get; set; }

        public Decimal precoFabrica { get; set; }

        public Decimal descontoComercial { get; set; }

        public Decimal descontoAdicional { get; set; }

        public Decimal percRepasse { get; set; }

        public Decimal precoAquisicao { get; set; }

        public Decimal percReducaoBase { get; set; }

        public Decimal percICMSe { get; set; }

        public Decimal valorCreditoICMS { get; set; }

        public Decimal percIPI { get; set; }

        public Decimal valorIPI { get; set; }

        public Decimal percPisCofins { get; set; }

        public Decimal valorPisCofins { get; set; }

        public Decimal pmc17 { get; set; }

        public string descST { get; set; }

        public Decimal mva { get; set; }

        public Decimal valorICMSST { get; set; }

        public string estabelecimentoNome { get; set; }

        public string estabelecimentoUf { get; set; }

        public Decimal custoPadrao { get; set; }

        public Decimal aliquotaInternaICMS { get; set; }

        public Decimal percPmc { get; set; }

        public bool itemControlado { get; set; }

        public string unidadeNegocioId { get; set; }

        public bool capAplicado { get; set; }

        public Decimal capDescontoPrc { get; set; }

        public string usuarioId { get; set; }

        public bool _isAdm { get; set; }

        public bool itemObsoleto { get; set; }

        public int itemCodigoOrigem { get; set; }

        public Decimal transfEs { get; set; }

        public bool VendaComST { get; set; }

        public string NomeArquivo { get; set; }

        public DateTime DataAlteracao { get; set; }

        public Decimal transfDf { get; set; }

        public string classificFiscal { get; set; }

        private void CreateSA() => this.sa = new SQLAutomator((object)this, "ksSimuladorPrecoCustos", "estabelecimentoId,itemId,ufIdOrigem", (string)null, (string)null);

        public SimuladorPrecoCustos() => this.CreateSA();

        public bool InserirLogCustos(
          string myFileName,
          string fileName,
          DateTime dataAtualiza,
          string usuarioLogado)
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                string str = dataAtualiza.Year.ToString() + "-" + dataAtualiza.Month.ToString() + "-" + dataAtualiza.Day.ToString() + " " + dataAtualiza.Hour.ToString() + ":" + dataAtualiza.Minute.ToString();
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format(" INSERT INTO ksSimuladorPrecoCustosLog\r\n                           ( NomeArquivo, usuarioId, pathArquivo, DataAlteracao  )\r\n                            VALUES\r\n                           ( '{0}' , '{1}' , '{2}' , '{3}')\r\n\r\n                         ", (object)fileName, (object)usuarioLogado, (object)myFileName, (object)str);
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public string GetConvenioTabelaCusto(string itemId)
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("select top 1 tipo from ksSimuladorPrecoCustos where itemid = '{0}'", (object)itemId.PadLeft(5, '0'));
                return dataBaseAccess.getDataTable(sql, (object)this).Rows[0]["tipo"].ToString();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public DataTable GetPrecoCustosLog()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("SELECT TOP 100 [DataAlteracao] ,[usuarioId] ,[pathArquivo] ,[NomeArquivo]\r\n                                FROM ksSimuladorPrecoCustosLog WITH (NOLOCK) order by DataAlteracao desc\r\n                                            ", !string.IsNullOrEmpty(this.estabelecimentoId) ? (object)("AND estabelecimentoId = '" + this.estabelecimentoId + "'") : (object)string.Empty, !string.IsNullOrEmpty(this.itemId) ? (object)("AND itemId = '" + this.itemId + "'") : (object)string.Empty, !string.IsNullOrEmpty(this.usuarioId) ? (object)("AND usuarioId LIKE '%" + this.usuarioId + "%'") : (object)string.Empty);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public DataTable GetPrecoCustosLogFiltro(DateTime de, DateTime ate)
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string str;
                if (string.IsNullOrEmpty(de.ToString()))
                    str = string.Empty;
                else
                    str = "AND DataAlteracao between '" + de.ToString("yyyy-MM-dd") + "' and '" + ate.ToString("yyyy-MM-dd") + "'";
                string sql = string.Format("SELECT TOP 100 [DataAlteracao] ,[usuarioId] ,[pathArquivo] ,[NomeArquivo]\r\n                                FROM ksSimuladorPrecoCustosLog WITH (NOLOCK) where 1=1 {0} order by DataAlteracao desc\r\n                                            ", (object)str);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public DataTable GetPrecoCustos()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format(@"
                    SELECT  
                                              estabelecimentoId
                                              ,itemId
                                              ,ufIdOrigem
                                              ,itemDescricao
                                              ,laboratorioNome
                                              ,tipo
                                              ,exclusivoHospitalar
                                              ,NCM
                                              ,listaDescricao
                                              ,categoria
                                              ,resolucao13
                                              ,tratamentoICMSEstab
                                              ,precoFabrica
                                              ,descontoComercial
                                              ,descontoAdicional
                                              ,percRepasse
                                              ,precoAquisicao
                                              ,percReducaoBase
                                              ,percICMSe
                                              ,valorCreditoICMS
                                              ,percIPI
                                              ,valorIPI
                                              ,percPisCofins
                                              ,valorPisCofins
                                              ,pmc17
                                              ,descST
                                              ,mva
                                              ,valorICMSST
                                              ,aplicacaoRepasse
                                              ,reducaoST_MVA
                                              ,estabelecimentoNome
                                              ,estabelecimentoUf
                                              ,custoPadrao
                                              ,aliquotaInternaICMS
                                              ,percPmc
                                              ,itemControlado
                                              ,capAplicado
                                              ,capDescontoPrc
                                              ,TransfEs
                                              ,vendaComST
                                              ,TransfDF
											  ,classificFiscal
                                            FROM ksSimuladorPrecoCustos WITH (NOLOCK) 
                                            WHERE 1=1  {0} {1}       ", !string.IsNullOrEmpty(this.estabelecimentoId) ? (object)("AND estabelecimentoId = '" + this.estabelecimentoId + "'") : (object)string.Empty, !string.IsNullOrEmpty(this.itemId) ? (object)("AND itemId = '" + this.itemId + "'") : (object)string.Empty);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool DeletarPrecoCustos()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "DELETE ksSimuladorPrecoCustos";
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool DeletarPrecoCustosTemp()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "DELETE FROM ksSimuladorPrecoCustosTemp";
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public DataTable GetItemSelecionado()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                string sql = string.Format("SELECT  top 10    itemId,\r\n                                                itemDescricao \r\n                                    FROM\t    ksSimuladorPrecoCustos\r\n                                    WHERE\t    {0}\r\n                                    GROUP BY itemId, itemDescricao ", !string.IsNullOrEmpty(this.itemId) ? (object)("itemId = " + this.itemId) : (object)string.Empty);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public DataTable GetDescricao()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                string sql = string.Format("SELECT   top 10   itemId,\r\n                                                itemDescricao \r\n                                    FROM\t    ksSimuladorPrecoCustos\r\n                                    WHERE\t    {0} \r\n                                    GROUP BY itemId,itemDescricao", !string.IsNullOrEmpty(this.itemDescricao) ? (object)("itemDescricao LIKE '" + this.itemDescricao + "%'") : (object)string.Empty);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public DataTable GetItensIdOuDescri() => string.IsNullOrEmpty(this.itemId) ? this.GetDescricao() : this.GetItemSelecionado();

        public DataTable GetItemDataDescricao()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                string sql = string.Format("SELECT      itemId,\r\n                                                itemDescricao \r\n                                    FROM\t    ksSimuladorPrecoCustos\r\n                                    WHERE\t    itemDescricao LIKE '{0}%'\r\n                                    GROUP BY\r\n\t\t                                        itemId,\r\n\t\t                                        itemDescricao", (object)this.itemDescricao);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        private DataTable GetItemData()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format(@"SELECT	    SI.itemId,

                                                ufIdOrigem,
                                                ITM.itemDescricao,
                                                laboratorioNome,
                                                listaDescricao,
                                                categoria,
                                                NCM,
                                                tipo,
                                                SI.classificFiscal,
                                                exclusivoHospitalar,
                                                resolucao13,
                                                descontoComercial,
                                                SI.itemControlado,
                                                SI.precoFabrica,
                                                pmc17,
                                                capAplicado,
                                                capDescontoPrc,
                                                ISNULL(ITM.itemObsoleto, 0) AS itemObsoleto



                                    FROM        ksSimuladorPrecoCustos AS SI

                                    INNER JOIN  KsItem  AS ITM ON(SI.itemId = ITM.itemId)
                                    WHERE       SI.itemId = '{0}'
                                    GROUP BY

                                                SI.itemId,
                                                ufIdOrigem,
                                                ITM.itemDescricao,
                                                laboratorioNome,
                                                listaDescricao,
                                                categoria,
                                                NCM,
                                                tipo,
                                                SI.classificFiscal,
                                                exclusivoHospitalar,
                                                resolucao13,
                                                descontoComercial,
                                                SI.itemControlado,
                                                SI.precoFabrica,
                                                pmc17,
                                                capAplicado,
                                                capDescontoPrc,
                                                ITM.itemObsoleto", (object)this.itemId);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public SimuladorPrecoCustos GetItem()
        {
            SimuladorPrecoCustos simuladorPrecoCustos = (SimuladorPrecoCustos)null;
            try
            {
                DataTable itemData = this.GetItemData();
                if (itemData != null)
                {
                    if (itemData.Rows.Count > 0)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)itemData.Rows)
                            simuladorPrecoCustos = new SimuladorPrecoCustos()
                            {
                                itemId = row["itemId"].ToString(),
                                itemDescricao = row["itemDescricao"].ToString(),
                                ufIdOrigem = row["ufIdOrigem"].ToString(),
                                laboratorioNome = row["laboratorioNome"].ToString(),
                                listaDescricao = row["listaDescricao"].ToString(),
                                categoria = row["categoria"].ToString(),
                                tipo = row["tipo"].ToString(),
                                classificFiscal = row["classificFiscal"].ToString(),
                                NCM = row["NCM"].ToString(),
                                exclusivoHospitalar = row["exclusivoHospitalar"].ToString(),
                                resolucao13 = row["resolucao13"].ToString(),
                                descontoComercial = !Convert.IsDBNull(row["descontoComercial"]) ? Decimal.Parse(row["descontoComercial"].ToString()) : 0M,
                                itemControlado = !Convert.IsDBNull(row["itemControlado"]) && bool.Parse(row["itemControlado"].ToString()),
                                precoFabrica = !Convert.IsDBNull(row["precoFabrica"]) ? Decimal.Parse(row["precoFabrica"].ToString()) : 0M,
                                pmc17 = !Convert.IsDBNull(row["pmc17"]) ? Decimal.Parse(row["pmc17"].ToString()) : 0M,
                                capDescontoPrc = !Convert.IsDBNull(row["capDescontoPrc"]) ? Decimal.Parse(row["capDescontoPrc"].ToString()) : 0M,
                                capAplicado = !Convert.IsDBNull(row["capAplicado"]) && bool.Parse(row["capAplicado"].ToString()),
                                itemObsoleto = !string.IsNullOrEmpty(row["itemObsoleto"].ToString()) && bool.Parse(row["itemObsoleto"].ToString())
                            };
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return simuladorPrecoCustos;
        }

        private DataTable GetCustosData()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("SELECT estabelecimentoId,\r\n                                   P.itemId,\r\n                                   ufIdOrigem,\r\n                                   P.itemDescricao,\r\n                                   laboratorioNome,\r\n                                   tipo,\r\n                                   exclusivoHospitalar,\r\n                                   NCM,\r\n                                   listaDescricao,\r\n                                   categoria,\r\n                                   resolucao13,\r\n                                   tratamentoICMSEstab,\r\n                                   P.precoFabrica,\r\n                                   descontoComercial,\r\n                                   descontoAdicional,\r\n                                   percRepasse,\r\n                                   precoAquisicao,\r\n                                   percReducaoBase,\r\n                                   percICMSe,\r\n                                   valorCreditoICMS,\r\n                                   percIPI,\r\n                                   valorIPI,\r\n                                   percPisCofins,\r\n                                   valorPisCofins,\r\n                                   pmc17,\r\n                                   descST,\r\n                                   mva,\r\n                                   valorICMSST,\r\n                                   aplicacaoRepasse,\r\n                                   reducaoST_MVA,\r\n                                   estabelecimentoNome + ' - ' + ' Estab. ' + estabelecimentoId AS estabelecimentoNome,\r\n                                   estabelecimentoUf,\r\n                                   custoPadrao,\r\n                                   aliquotaInternaICMS,\r\n                                   percPmc,\r\n                                   I.itemCodigoOrigem,\r\n                                   P.transfEs,\r\n                                   P.vendaComSt,\r\n                                   P.transfDf\r\n                            FROM ksSimuladorPrecoCustos P\r\n                            INNER JOIN KSITEM I ON I.itemId = P.itemId\r\n                            WHERE\t    P.itemId = '{0}' {2} \r\n                            AND         estabelecimentoId \r\n                                        IN(\r\n                                            SELECT  ESU.estabelecimentoId\r\n\t\t   \r\n                                            FROM    KsEstabelecimentoUnidadeNegocio ESU\r\n                                            INNER JOIN KsEstabelecimento EST ON  EST.estabelecimentoId = ESU.estabelecimentoId\r\n                                            INNER JOIN ksUnidadeNegocio UNG ON   UNG.unidadeNegocioId = ESU.unidadeNegocioId\r\n                                            INNER JOIN ksUf Uf ON uf.ufId = EST.ufId\r\n                                            INNER JOIN ksPais P ON P.paisId = Uf.paisId\r\n                                            {1}\r\n                                            ) \r\n                                    order by CAST( estabelecimentoId as numeric)    ", (object)this.itemId, !this._isAdm ? (object)("WHERE   ESU.unidadeNegocioId IN(SELECT unidadeNegocioId FROM KsUsuarioUnidadeNegocio WHERE usuarioId = 'thiagoa') GROUP BY ESU.estabelecimentoId") : (object)string.Empty, !string.IsNullOrEmpty(this.estabelecimentoId) ? (object)string.Format("AND estabelecimentoId = '{0}'", (object)this.estabelecimentoId) : (object)string.Empty);
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public List<SimuladorPrecoCustos> GetCustos()
        {
            List<SimuladorPrecoCustos> simuladorPrecoCustosList1 = new List<SimuladorPrecoCustos>();
            try
            {
                DataTable custosData = this.GetCustosData();
                if (custosData != null)
                {
                    if (custosData.Rows.Count > 0)
                    {
                        foreach (DataRow row in (InternalDataCollectionBase)custosData.Rows)
                        {
                            List<SimuladorPrecoCustos> simuladorPrecoCustosList2 = simuladorPrecoCustosList1;
                            SimuladorPrecoCustos simuladorPrecoCustos = new SimuladorPrecoCustos();
                            simuladorPrecoCustos.estabelecimentoId = row["estabelecimentoId"].ToString();
                            simuladorPrecoCustos.itemId = row["itemId"].ToString();
                            simuladorPrecoCustos.ufIdOrigem = row["ufIdOrigem"].ToString();
                            simuladorPrecoCustos.itemDescricao = row["itemDescricao"].ToString();
                            simuladorPrecoCustos.laboratorioNome = row["laboratorioNome"].ToString();
                            simuladorPrecoCustos.tipo = row["tipo"].ToString();
                            simuladorPrecoCustos.exclusivoHospitalar = row["exclusivoHospitalar"].ToString();
                            simuladorPrecoCustos.NCM = row["NCM"].ToString();
                            simuladorPrecoCustos.listaDescricao = row["listaDescricao"].ToString();
                            simuladorPrecoCustos.categoria = row["categoria"].ToString();
                            simuladorPrecoCustos.resolucao13 = row["resolucao13"].ToString();
                            simuladorPrecoCustos.tratamentoICMSEstab = row["tratamentoICMSEstab"].ToString();
                            simuladorPrecoCustos.precoFabrica = !Convert.IsDBNull(row["precoFabrica"]) ? Decimal.Parse(row["precoFabrica"].ToString()) : 0M;
                            simuladorPrecoCustos.descontoComercial = !Convert.IsDBNull(row["descontoComercial"]) ? Decimal.Parse(row["descontoComercial"].ToString()) : 0M;
                            simuladorPrecoCustos.descontoAdicional = !Convert.IsDBNull(row["descontoAdicional"]) ? Decimal.Parse(row["descontoAdicional"].ToString()) : 0M;
                            simuladorPrecoCustos.percRepasse = !Convert.IsDBNull(row["percRepasse"]) ? Decimal.Parse(row["percRepasse"].ToString()) : 0M;
                            simuladorPrecoCustos.precoAquisicao = !Convert.IsDBNull(row["precoAquisicao"]) ? Decimal.Parse(row["precoAquisicao"].ToString()) : 0M;
                            simuladorPrecoCustos.percReducaoBase = !Convert.IsDBNull(row["percReducaoBase"]) ? Decimal.Parse(row["percReducaoBase"].ToString()) : 0M;
                            simuladorPrecoCustos.percICMSe = !Convert.IsDBNull(row["percICMSe"]) ? Decimal.Parse(row["percICMSe"].ToString()) : 0M;
                            simuladorPrecoCustos.valorCreditoICMS = !Convert.IsDBNull(row["valorCreditoICMS"]) ? Decimal.Parse(row["valorCreditoICMS"].ToString()) : 0M;
                            simuladorPrecoCustos.percIPI = !Convert.IsDBNull(row["percIPI"]) ? Decimal.Parse(row["percIPI"].ToString()) : 0M;
                            simuladorPrecoCustos.valorIPI = !Convert.IsDBNull(row["valorIPI"]) ? Decimal.Parse(row["valorIPI"].ToString()) : 0M;
                            simuladorPrecoCustos.percPisCofins = !Convert.IsDBNull(row["percPisCofins"]) ? Decimal.Parse(row["percPisCofins"].ToString()) : 0M;
                            simuladorPrecoCustos.valorPisCofins = !Convert.IsDBNull(row["valorPisCofins"]) ? Decimal.Parse(row["valorPisCofins"].ToString()) : 0M;
                            simuladorPrecoCustos.pmc17 = !Convert.IsDBNull(row["pmc17"]) ? Decimal.Parse(row["pmc17"].ToString()) : 0M;
                            simuladorPrecoCustos.descST = !Convert.IsDBNull(row["descST"]) ? (string.IsNullOrEmpty(row["descST"].ToString()) || row["descST"].ToString().Equals("-") || Decimal.Parse(row["descST"].ToString()).Equals(0M) ? "-" : row["descST"].ToString()) : "-";
                            simuladorPrecoCustos.mva = !Convert.IsDBNull(row["mva"]) ? Decimal.Parse(row["mva"].ToString()) : 0M;
                            simuladorPrecoCustos.aliquotaInternaICMS = !Convert.IsDBNull(row["aliquotaInternaICMS"]) ? Decimal.Parse(row["aliquotaInternaICMS"].ToString()) : 0M;
                            simuladorPrecoCustos.valorICMSST = !Convert.IsDBNull(row["valorICMSST"]) ? Decimal.Parse(row["valorICMSST"].ToString()) : 0M;
                            simuladorPrecoCustos.aplicacaoRepasse = row["aplicacaoRepasse"].ToString();
                            int num;
                            string str;
                            if (Convert.IsDBNull(row["reducaoST_MVA"]))
                            {
                                num = 0;
                                str = num.ToString();
                            }
                            else if (string.IsNullOrEmpty(row["reducaoST_MVA"].ToString()))
                            {
                                num = 0;
                                str = num.ToString();
                            }
                            else
                                str = row["reducaoST_MVA"].ToString();
                            simuladorPrecoCustos.reducaoST_MVA = str;
                            simuladorPrecoCustos.estabelecimentoNome = row["estabelecimentoNome"].ToString();
                            simuladorPrecoCustos.estabelecimentoUf = row["estabelecimentoUf"].ToString();
                            simuladorPrecoCustos.custoPadrao = !Convert.IsDBNull(row["custoPadrao"]) ? Decimal.Parse(row["custoPadrao"].ToString()) : 0M;
                            simuladorPrecoCustos.transfEs = Decimal.Parse(row["transfEs"].ToString());
                            simuladorPrecoCustos.VendaComST = row["vendaComSt"].ToString().ToUpper().Equals("TRUE");
                            simuladorPrecoCustos.transfDf = Decimal.Parse(row["transfDf"].ToString());
                            simuladorPrecoCustosList2.Add(simuladorPrecoCustos);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return simuladorPrecoCustosList1;
        }

        public DataTable GetDataUltimoUpdate()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "\r\n\t\t                    SELECT  'Carga de Custos' as Tipo ,DataAlteracao as DataUpdate\r\n                            FROM \r\n                                ksSimuladorPrecoCustosLog SU\r\n                            WHERE \r\n                            DataAlteracao = (SELECT MAX(DataAlteracao) as DataAlteracao  \r\n\t\t\t\t\t                            FROM ksSimuladorPrecoCustosLog \r\n\t\t\t\t\t                        )\r\n                            UNION \r\n                                SELECT  'Carga de Regras Gerais' as Tipo  ,dataAtualizacao as DataUpdate\r\n                            FROM \r\n                                ksSimuladorPrecoRegrasGeraisLog SU\r\n                            WHERE \r\n                            dataAtualizacao = (SELECT MAX(dataAtualizacao) as dataAtualizacao  \r\n\t\t\t\t\t                            from ksSimuladorPrecoRegrasGeraisLog \r\n\t\t\t\t\t                            )";
                return dataBaseAccess.getDataTable(sql, (object)this);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool DeletarItemPrecoCustos()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "DELETE FROM ksSimuladorPrecoCustos where itemId=@itemId and estabelecimentoId=@estabelecimentoId";
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool InserirKsSimuladorPrecoCustoDaTemp()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "insert into ksSimuladorPrecoCustosTemp select * from  ksSimuladorPrecoCustos (nolock)";
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool InserirSimuladorPrecoCustosTempDaSimuladorPrecoCustos()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "insert ksSimuladorPrecoCustosTemp\r\n                            select * from ksSimuladorPrecoCustos";
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool InserirSimuladorPrecoCustosRollBack()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "insert ksSimuladorPrecoCustos\r\n                            select * from ksSimuladorPrecoCustosTemp";
                if (!dataBaseAccess.executeNonQuery(sql, (object)this))
                    throw new Exception(dataBaseAccess.LastMessage);
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                dataBaseAccess.close();
            }
        }

        public bool InsertBulkCopy(DataTable dt, string Tabela)
        {
            SqlConnection connection = new SqlConnection(KS.SimuladorPrecos.DataEntities.Utility.Utility.CONNECTIONSTRING);
            connection.Open();
            SqlTransaction externalTransaction = connection.BeginTransaction();
            try
            {
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.Default, externalTransaction);
                sqlBulkCopy.DestinationTableName = Tabela;
                foreach (DataColumn column in (InternalDataCollectionBase)dt.Columns)
                    sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                sqlBulkCopy.WriteToServer(dt);
            }
            catch (Exception ex)
            {
                externalTransaction.Rollback();
                return false;
            }
            finally
            {
                connection.Close();
            }
            externalTransaction.Commit();
            return true;
        }

        public bool WriteToDatabase(DataTable dt, string Tabela)
        {
            using (SqlConnection connection = new SqlConnection(KS.SimuladorPrecos.DataEntities.Utility.Utility.CONNECTIONSTRING))
            {
                SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.TableLock | SqlBulkCopyOptions.FireTriggers | SqlBulkCopyOptions.UseInternalTransaction, (SqlTransaction)null);
                sqlBulkCopy.DestinationTableName = Tabela;
                connection.Open();
                sqlBulkCopy.WriteToServer(dt);
                connection.Close();
            }
            return true;
        }
    }
}
