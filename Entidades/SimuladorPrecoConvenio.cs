using DataBaseAccessLib;
using System;
using System.Data;
using System.Data.SqlClient;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    [Serializable]
    public class SimuladorPrecoConvenio
    {
        [NonSerialized]
        public SQLAutomator sa = (SQLAutomator)null;
        public string usuarioId;

        public string item { get; set; }

        public string origem { get; set; }

        public string destino { get; set; }

        public string convenio { get; set; }

        private void CreateSA() => this.sa = new SQLAutomator((object)this, "KsSimuladorPrecoCargaConvenio", "item,origem,desitno,convenio", (string)null, (string)null);

        public SimuladorPrecoConvenio() => this.CreateSA();

        public DataTable ObterDadosPorFiltro()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("\r\n                                            SELECT usuario, dataAtualizacao, nomeArquivo\r\n                                              FROM [dbo].[KsSimuladorPrecoCargaConvenio_LOG] order by  dataAtualizacao desc ");
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

        public bool VerificarSeItensExisteNaLista()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("\r\n                                            select item\t,origem,\tdestino,\tconvenio from  KsSimuladorPrecoCargaConvenio where item = '{0}' ", (object)int.Parse(this.item));
                DataTable dataTable = dataBaseAccess.getDataTable(sql, (object)this);
                return dataTable != null && dataTable.Rows.Count > 0;
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

        public DataTable ObterListaConvenio()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("\r\n                                            select item\t,origem,\tdestino,\tconvenio from  KsSimuladorPrecoCargaConvenio where item = '{0}' and destino = '{1}'", (object)int.Parse(this.item), (object)this.destino);
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

        public bool Deletar()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = "DELETE KsSimuladorPrecoCargaConvenio";
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

        public bool Inserir()
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("insert into KsSimuladorPrecoCargaConvenio values ({0}, {1}, {2}, {3})", !string.IsNullOrEmpty(this.item) ? (object)("'" + this.item + "'") : (object)string.Empty, !string.IsNullOrEmpty(this.origem) ? (object)("'" + this.origem + "'") : (object)string.Empty, !string.IsNullOrEmpty(this.destino) ? (object)("'" + this.destino + "'") : (object)string.Empty, !string.IsNullOrEmpty(this.convenio) ? (object)("'" + this.convenio + "'") : (object)string.Empty);
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

        public bool InsertBulkCopy(DataTable dt, string Tabela)
        {
            SqlConnection connection = new SqlConnection();
            SqlTransaction externalTransaction = (SqlTransaction)null;
            try
            {
                KS.SimuladorPrecos.DataEntities.Utility.Utility.SetStringToUpper((object)this);
                using (connection = new SqlConnection(KS.SimuladorPrecos.DataEntities.Utility.Utility.CONNECTIONSTRING))
                {
                    connection.Open();
                    externalTransaction = connection.BeginTransaction();
                    SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(connection, SqlBulkCopyOptions.KeepIdentity, externalTransaction);
                    sqlBulkCopy.DestinationTableName = Tabela;
                    foreach (DataColumn column in (InternalDataCollectionBase)dt.Columns)
                        sqlBulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                    sqlBulkCopy.WriteToServer(dt);
                    externalTransaction.Commit();
                }
            }
            catch
            {
                externalTransaction.Rollback();
            }
            finally
            {
                connection.Close();
            }
            return true;
        }

        public bool InsertLog(string nomeArquivo)
        {
            DataBaseAccess dataBaseAccess = new DataBaseAccess();
            try
            {
                if (!dataBaseAccess.open())
                    throw new Exception(dataBaseAccess.LastMessage);
                string sql = string.Format("insert into [dbo].[KsSimuladorPrecoCargaConvenio_LOG]\r\n                                               ( [nomeArquivo], [usuario]) values ('{1}', {0})", !string.IsNullOrEmpty(this.usuarioId) ? (object)("'" + this.usuarioId + "'") : (object)string.Empty, (object)nomeArquivo);
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
    }
}
