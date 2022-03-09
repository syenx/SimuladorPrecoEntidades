using DataBaseAccessLib;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    public class SimuladorPrecoPMC
    {
        [NonSerialized]
        public SQLAutomator sa = null;
        public string codigoOnco { get; set; }
        public string cheio { get; set; }
        public string reduzido { get; set; }
     
        void CreateSA()
        {
            sa = new SQLAutomator(this, "KsSimuladorPrecoPMC", "codigoOnco,cheio,reduzido ", null, null);
        }

        public SimuladorPrecoPMC()
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
                                            SELECT [codigoOnco]
                                                  ,[cheio]
                                                  ,[reduzido]
                                             
                                              FROM [dbo].[KsSimuladorPrecoPMC]
                                                WHERE  1=1 {0} ",
                                              !String.IsNullOrEmpty(codigoOnco) ? "AND codigoOnco = '" + codigoOnco + "'" : string.Empty);


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

                string sSQL = "DELETE KsSimuladorPrecoPMC";

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

                string sSQL = string.Format(@"insert into KsSimuladorPrecoPMC values ({0}, {1}, {2})",
                     !String.IsNullOrEmpty(codigoOnco) ? "'" + codigoOnco + "'" : string.Empty,
                     !String.IsNullOrEmpty(cheio) ? "'" + cheio + "'" : string.Empty,
                     !String.IsNullOrEmpty(reduzido) ? "'" + reduzido + "'" : string.Empty);

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

        #endregion


    }
}
