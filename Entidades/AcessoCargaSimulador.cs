
using DataBaseAccessLib;
using System;
using System.Data;


namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    [Serializable]
    public class AcessoCargaSimulador
    {
        #region :: Globais ::
        public SQLAutomator sa = null;

        #endregion



        public int simuladorPrecoAcessoCargaId { get; set; }
        public string usuarioId { get; set; }
        public string usuarioNome { get; set; }
        public string tipoAcesso { get; set; }


        public bool VerificaAcessoCarga()
        {

            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL =
                    string.Format(@"select * from KsSimuladorPrecoAcessoMenu where 1=1 {0}", "and usuarioId = '" + usuarioId.ToUpper() + "'");

                DataTable dt = da.getDataTable(sSQL, this);

                if (dt.Rows.Count >= 1)
                    return true;
                else
                    return false;

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


        //        --	simuladorPrecoAcessoCargaId int identity,
        //--	usuarioId varchar(30),
        //--usuarioNome varchar(50)
        //--	tipoAcesso varchar(30)
    }
}