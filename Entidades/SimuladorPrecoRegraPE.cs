using DataBaseAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    [Serializable]
    public class SimuladorPrecoRegraPE
    {
        public SQLAutomator sa = null;

        public SimuladorPrecoRegraPE()
        {
            sa = new SQLAutomator(this, "KsSimuladorPrecoRegraPE", "estabelecimentoId", null, null);
        }

        public string estabelecimentoId { get; set; }
        public string codigoItem { get; set; }
        public string uFOrigemFornec { get; set; }
        public string ufDestinoCliente { get; set; }
        public string contribuinte { get; set; }
        public string encargos { get; set; }
        public DateTime dataImportacaoInicio { get; set; }
        public DateTime dataImportacaoFim { get; set; }

        public string usuarioId { get; set; }

        public DataTable ObterTodos()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT * from KsSimuladorPrecoRegraPE (nolock)");

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

        public DataTable ObterRegrasSPINRAZA()
        {
            DataBaseAccess da = new DataBaseAccess();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT *
                                 FROM KSSIMULADORPRECOREGRAPE (nolock) where 1=1  {0}  {1} {2} {3}",
                                 !string.IsNullOrEmpty(codigoItem) ? "and codigoItem = " + codigoItem : string.Empty,
                                 !string.IsNullOrEmpty(estabelecimentoId) ? "and estabelecimentoId  = " + estabelecimentoId : string.Empty,
                                 !string.IsNullOrEmpty(ufDestinoCliente) ? "and ufDestinoCliente  = '" + ufDestinoCliente + "'" : string.Empty,
                                  !string.IsNullOrEmpty(contribuinte) ? "and contribuinte  = '" + contribuinte + "'" : string.Empty
                                 );

                DataTable dt = da.getDataTable(sSQL, this);
                if (dt != null)
                {
                    return dt;
                }
                else
                {
                    return null;
                }

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

        public DataTable ObterTodosFiltro()
        {
            DataBaseAccess da = new DataBaseAccess();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select top 10 
                                    estabelecimentoId
                                    ,codigoItem
                                    ,ufOrigemFornec
                                    ,ufDestinoCliente
                                    ,contribuinte
                                    ,encargos
                                    ,dataImportacao
                                    ,usuarioId

                                 from KsSimuladorPrecoRegraPE (nolock) where 1=1 {0} {1} {2} {3} {4} {5} {6}  order by dataImportacao desc ",
                             !string.IsNullOrEmpty(estabelecimentoId) ? "and estabelecimentoId =         " + estabelecimentoId : string.Empty
                            , !string.IsNullOrEmpty(codigoItem) ? "and codigoItem =   '" + codigoItem + "'" : string.Empty
                             , !string.IsNullOrEmpty(contribuinte) ? "and contribuinte =   '" + contribuinte + "'" : string.Empty
                            , !string.IsNullOrEmpty(uFOrigemFornec) ? "and uFOrigemFornec  = '" + uFOrigemFornec + "'" : string.Empty
                            , !string.IsNullOrEmpty(ufDestinoCliente) ? "and ufDestinoCliente =  '" + ufDestinoCliente + "'" : string.Empty
                            , !string.IsNullOrEmpty(encargos) ? "and encargos =          " + encargos : string.Empty
                            , !dataImportacaoInicio.Equals(DateTime.MinValue) ? "and dataImportacao between '" + dataImportacaoInicio.ToString("yyyy-MM-dd HH:mm:ss") + "' and '" + dataImportacaoFim.ToString("yyyy-MM-dd HH:mm:ss") + "'" : string.Empty
                            , !string.IsNullOrEmpty(usuarioId) ? "and usuarioId ='" + usuarioId + "'" : string.Empty);

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

        public bool InserirPrecoRegraTempDePETemp()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {


                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Query Update ::

                string sSQL =
                        @" INSERT INTO KsSimuladorPrecoRegraPE
                                select * from KsSimuladorPrecoRegraPETemp";

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

        public bool InserirPrecoRegraTempDePE()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {


                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Query Update ::

                string sSQL =
                        @" INSERT INTO KsSimuladorPrecoRegraPETemp
                                select * from KsSimuladorPrecoRegraPE";

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

        public bool DeletarSimuladorRegraPE()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE KSSimuladorPrecoRegraPE";

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

        public bool DeletarSimuladorRegraPETemp()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE KSSimuladorPrecoRegraPETemp";

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
    }
}
