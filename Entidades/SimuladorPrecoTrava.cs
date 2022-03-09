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
    public class SimuladorPrecoTrava
    {
        [NonSerialized]
        public SQLAutomator sa = null;
        public string usuarioId;

        public string categoria { get; set; }
        public string lista { get; set; }
        public string trava { get; set; }
        public string mva_st { get; set; }



        void CreateSA()
        {
            sa = new SQLAutomator(this, "KsSimuladorPrecoCargaTrava", "categoria,lista", null, null);
        }

        public SimuladorPrecoTrava()
        {
            CreateSA();
        }


        #region Metodos 

        public DataTable ObterDadosPorFiltro()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"
                                            SELECT [categoria]
                                                  ,[lista]
                                                  ,[trava]
                                                  ,[mva_st]
                                                  ,mva_ts_reduzido
                                                  ,dataAtualizacao
                                                  ,usuarioId
                                              FROM [dbo].[KsSimuladorPrecoCargaTrava]
                                                WHERE  1=1 {0} {1}
                                            ",
                                              !String.IsNullOrEmpty(categoria) ? "AND categoria = '" + categoria + "'" : string.Empty,
                                              !String.IsNullOrEmpty(lista) ? "AND lista = '" + lista + "'" : string.Empty);


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

        public bool Deletar()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = "DELETE KsSimuladorPrecoCargaTrava";

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

        public bool Inserir()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"insert into KsSimuladorPrecoCargaTrava values ({0}, {1}, {2}, {3})",
                     !String.IsNullOrEmpty(categoria) ? "'" + categoria + "'" : string.Empty,
                     !String.IsNullOrEmpty(lista) ? "'" + lista + "'" : string.Empty,
                     !String.IsNullOrEmpty(trava) ? "'" + trava + "'" : string.Empty,
                     !String.IsNullOrEmpty(mva_st) ? "'" + mva_st + "'" : string.Empty);

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
                _transaction.Rollback();
            }
            finally
            {
                _conn.Close();
            }
            return true;
        }

        public bool UpdateLog()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"UPDATE [dbo].[KsSimuladorPrecoCargaTrava]
                                               SET [dataAtualizacao] = getdate()
                                                  ,[usuarioId] = {0} ",
                     !String.IsNullOrEmpty(usuarioId) ? "'" + usuarioId + "'" : string.Empty);

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
