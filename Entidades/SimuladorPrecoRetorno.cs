// Decompiled with JetBrains decompiler
// Type: KS.SimuladorPrecos.DataEntities.Entidades.SimuladorPrecoRetorno
// Assembly: KS.SimuladorPrecos.DataEntities, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null
// MVID: C9FA5186-8E58-431C-8E29-065087E9C413
// Assembly location: C:\Users\redbu\OneDrive\Área de Trabalho\SimuladorPrecosHomolog\bin\KS.SimuladorPrecos.DataEntities.dll

using System;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    [Serializable]
    public class SimuladorPrecoRetorno
    {
        public string ESTABELECIMENTO_DESTINO { get; set; }

        public string ITEM { get; set; }

        public string DESCRICAO { get; set; }

        public string UF_ORIGEM { get; set; }

        public string FORNECEDOR { get; set; }

        public string LISTA { get; set; }

        public string CATEGORIA { get; set; }

        public string CLASS_FISCAL { get; set; }

        public string NCM { get; set; }

        public string MED_CONTROLADO_USO { get; set; }

        public string EX_HOSPITALAR { get; set; }

        public string RESOLUCAO_13 { get; set; }

        public string PERFIL_DE_CLIENTE { get; set; }

        public string ESTADO_DESTINO { get; set; }

        public Decimal MARGEM_OBJETIVO_PER { get; set; }

        public Decimal MARGEM_OBJETIVO_VAL { get; set; }

        public Decimal PRECO_DE_VENDA_COM_ST { get; set; }

        public Decimal PRECO_DE_VENDA { get; set; }

        public Decimal CUSTO_PADRAO { get; set; }

        public Decimal PRECO_FABRICA { get; set; }

        public Decimal DESCONTO_COMERCIAL { get; set; }

        public Decimal PMC { get; set; }

        public Decimal PIS_COFINS_VAL { get; set; }

        public Decimal PIS_COFINS_PER { get; set; }

        public Decimal IPI_VAL { get; set; }

        public string DESCONTO_ADICIONAL { get; set; }

        public string REPASSE { get; set; }

        public Decimal CREDITO_ICMS_PER { get; set; }

        public Decimal CREDITO_ICMS_VAL { get; set; }

        public Decimal MVA { get; set; }

        public Decimal ICMS_ST { get; set; }

        public string TRANSF_ES { get; set; }

        public Decimal ICMS_ST_SOBRE_VENDA { get; set; }

        public Decimal ICMS_SOBRE_VENDA { get; set; }

        public string HORA_PROCESS { get; set; }
    }
}
