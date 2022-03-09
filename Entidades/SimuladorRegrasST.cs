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
    public class SimuladorRegrasST
    {
        public SQLAutomator sa = null;

        public SimuladorRegrasST()
        {
            sa = new SQLAutomator(this, "ksSimuladorCargaRegrasST", "cargaRegraStId", null, null);
        }

        #region Propriedades

        public string estabelecimentoId { get; set; }
        public string itemId { get; set; }
        public string classeFiscal { get; set; }
        public string perfilCliente { get; set; }
        public string estadoDestino { get; set; }
        public string aliquota { get; set; }
        public string PMC { get; set; }
        public string PMPF { get; set; }
        public string icmsInterno { get; set; }
        public string dataImportacao { get; set; }
        public string usuarioId { get; set; }

        #endregion

        #region Métodos

        public DataTable ObterTodos()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select top 100 
                    estabelecimentoId
                    ,itemId
                    ,classeFiscal
                    ,perfilCliente
                    ,estadoDestino
                    ,aliquota
                    ,PMC
                    ,PMC_CHEIO
                    ,PMPF
                    ,icmsInterno
                    ,dataImportacao
                    ,usuarioId

                    from KSSimuladorCargaRegrasST (nolock)  order by dataImportacao desc");

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

        public DataTable ObterTodosTemp()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select 
	                                cargaRegraStId ,
	                                itemId ,
	                                classeFiscal ,
	                                perfilCliente ,
	                                estadoDestino ,
	                                aliquota ,
	                                PMC,
                                    PMPF,
	                                icmsInterno ,
	                                dataImportacao,
	                                usuarioId 
                                 from KSSimuladorCargaRegasST (nolock)");

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

        public DataTable ObterTodosComFiltro()
        {
            DataBaseAccess da = new DataBaseAccess();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select 
                                            estabelecimentoId
                                            ,itemId
                                            ,classeFiscal
                                            ,perfilCliente
                                            ,estadoDestino
                                            ,aliquota
                                            ,PMC
                                            ,icmsInterno
                                            ,PMC_CHEIO
                                            ,PMPF
                                            ,dataImportacao
                                            ,usuarioId
                                 from KSSimuladorCargaRegrasST (nolock) where 1=1 {0} {1} {2} {3} {4} {5} {6} {7} {8} ",
                             !string.IsNullOrEmpty(itemId) ? "and itemId ='" + itemId + "'" : string.Empty
                            , !string.IsNullOrEmpty(estabelecimentoId) ? "and estabelecimentoId ='" + estabelecimentoId + "'" : string.Empty
                            , !string.IsNullOrEmpty(classeFiscal) ? "and classeFiscal ='" + classeFiscal + "'" : string.Empty
                            , !string.IsNullOrEmpty(perfilCliente) ? "and perfilCliente  ='" + perfilCliente + "'" : string.Empty
                            , !string.IsNullOrEmpty(estadoDestino) ? "and estadoDestino ='" + estadoDestino + "'" : string.Empty
                            , !string.IsNullOrEmpty(aliquota) ? "and aliquota =       " + aliquota : string.Empty
                            , !string.IsNullOrEmpty(PMC) ? "and PMC =          " + PMC : string.Empty
                            , !string.IsNullOrEmpty(icmsInterno) ? "and icmsInterno =    " + icmsInterno : string.Empty
                            , !string.IsNullOrEmpty(dataImportacao) ? "and dataImportacao like '%" + dataImportacao.Trim() + "%' " : string.Empty
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

        public DataTable ObterTodosTempFiltro()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select 
	                                cargaRegraStId ,
	                                itemId ,
	                                classeFiscal ,
	                                perfilCliente ,
	                                estadoDestino ,
	                                aliquota ,
	                                PMC,
                                    PMPF,
	                                icmsInterno ,
	                                dataImportacao,
	                                usuarioId 
                                     from KSSimuladorCargaRegasSTtemp (nolock) where 1=1 ",
                             !itemId.Equals(0) ? "and itemId =         " + itemId : string.Empty
                            , !string.IsNullOrEmpty(classeFiscal) ? "and classeFiscal =   " + classeFiscal : string.Empty
                            , !string.IsNullOrEmpty(perfilCliente) ? "and perfilCliente  = " + perfilCliente : string.Empty
                            , !string.IsNullOrEmpty(estadoDestino) ? "and estadoDestino =  " + estadoDestino : string.Empty
                            , !aliquota.Equals(0) ? "and aliquota =       " + aliquota : string.Empty
                            , !string.IsNullOrEmpty(PMC) ? "and PMC =            " + PMC : string.Empty
                            , !icmsInterno.Equals(0) ? "and icmsInterno =    " + icmsInterno : string.Empty
                            , !dataImportacao.Equals(0) ? "and dataImportacao = " + dataImportacao : string.Empty
                            , !string.IsNullOrEmpty(usuarioId) ? "and usuarioId =);    " + usuarioId : string.Empty);

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

            SqlConnection _conn = new SqlConnection();
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

                    foreach (DataColumn c in dt.Columns)
                    {
                        copy.ColumnMappings.Add(c.ColumnName, c.ColumnName);
                    }
                    copy.WriteToServer(dt);

                    _transaction.Commit();

                }
            }
            catch
            {
                _transaction.Commit();

            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public bool DeletarSimuladorRegrasST()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE KSSimuladorCargaRegrasST";

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

        public bool DeletarSimuladorRegrasSTTemp()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE FROM KSSimuladorCargaRegasSTTemp";

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
