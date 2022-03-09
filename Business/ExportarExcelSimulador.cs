using KS.SimuladorPrecos.DataEntities.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Text;

namespace KS.SimuladorPrecos.DataEntities.Business
{
    public class ExportarExcelSimulador
    {

    
        public string item { get; set; }
        public string descricao { get; set; }
        public string ufOrigem { get; set; }
        public string fornecedor { get; set; }
        public string lista { get; set; }
        public string categoria { get; set; }
        public string classFiscal { get; set; }
        public string ncm { get; set; }
        public string medicamentoControlado { get; set; }
        public string usoExclusivoH { get; set; }
        public string resolucao { get; set; }
        public string perfil { get; set; }
        public string estadoDestino { get; set; }
        public decimal capAplicado { get; set; }
        public string Estab { get; set; }
        public decimal mgMinimaPercent { get; set; }
        public decimal mgMinimaValor { get; set; }
        public decimal mgMaximoValor { get; set; }


        public static DataTable ConvertToDataTable<T>(IList<T> data)
        {
            PropertyDescriptorCollection properties =
               TypeDescriptor.GetProperties(typeof(T));
            DataTable table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
                table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
            foreach (T item in data)
            {
                DataRow row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                table.Rows.Add(row);
            }
            return table;

        }

        public static List<ExportarExcelSimulador>  BuscarConsultaExportar(List<SimuladorPrecoCustos> lstCustos, string ufDestino, string perfilCliente, decimal margem)
        {
            List<ExportarExcelSimulador> lista = new List<ExportarExcelSimulador>();
         
            foreach (var item in lstCustos)
            {
                


                lista.Add(new ExportarExcelSimulador()
                {
                    item = item.itemId,
                    capAplicado = item.capDescontoPrc,
                    categoria = item.categoria,
                    classFiscal = item.tipo,
                    descricao = item.itemDescricao,
                    Estab = item.estabelecimentoUf,
                    mgMaximoValor = item.precoFabrica,
                    estadoDestino = ufDestino,
                    perfil = perfilCliente,
                    mgMinimaPercent = margem,
                    resolucao = item.resolucao13, 
                    fornecedor = item.laboratorioNome,
                    ufOrigem = item.ufIdOrigem,
                    lista = item.listaDescricao,
                    ncm = item.NCM,
                   usoExclusivoH = item.exclusivoHospitalar,
                   medicamentoControlado = (item.itemControlado.Equals(false)? "NÃO": "SIM" ),
                   mgMinimaValor = (item.precoFabrica) / (1+ margem / 100) 
                });
            }
            return lista;
        }
    }
}
