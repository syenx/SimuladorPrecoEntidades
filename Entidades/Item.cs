using DataBaseAccessLib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;

namespace KS.SimuladorPrecos.DataEntities.Entidades
{
    [Serializable]
    public class Item
    {


        #region :: Globais ::

        public SQLAutomator sa = null;

        #endregion

        #region :: Propriedades ::

        /// <summary>
        /// Id do objeto Item
        /// </summary>
        public string Id { get { return this.itemId; } }

        /// <summary>
        /// Descrição do Item
        /// </summary>
        public string Descricao { get { return this.itemDescricao; } }

        /// <summary>
        /// Id do item
        /// </summary>
        public string itemId { get; set; }

        /// <summary>
        /// Descrição do item
        /// </summary>
        public string itemDescricao { get; set; }

        /// <summary>
        /// Nome abreviado do Item
        /// </summary>
        public string itemInfComplementar { get; set; }

        /// <summary>
        /// Peso bruto do Item
        /// </summary>
        public  decimal itemPesoBruto { get; set; }

        /// <summary>
        /// Valor do Item
        /// </summary>
        public  decimal itemPesoLiquido { get; set; }

        /// <summary>
        /// Verifica se item refrigerado
        /// </summary>
        public bool itemRefrigerado { get; set; }

        /// <summary>
        /// Verifica se item controlado
        /// </summary>
        public bool itemControlado { get; set; }

        /// <summary>
        /// Id da unidade de medida
        /// </summary>
        public string unidadeMedidaId { get; set; }

        /// <summary>
        /// Sigla da unidade de medida
        /// </summary>
        public string unidadeMedidaSigla { get; set; }

        /// <summary>
        /// Id da família comercial
        /// </summary>
        public string familiaComercialId { get; set; }

        /// <summary>
        /// Descrição da família comercial
        /// </summary>
        public string familiaComercialDescricao { get; set; }

        /// <summary>
        /// Id da família material
        /// </summary>
        public string familiaMaterialId { get; set; }

        /// <summary>
        /// Descrição da família material
        /// </summary>
        public string familiaMaterialDescricao { get; set; }

        /// <summary>
        /// Id do grupo de estoque
        /// </summary>
        public string grupoEstoqueId { get; set; }

        /// <summary>
        /// Descrição do grupo de estoque
        /// </summary>
        public string grupoEstoqueDescricao { get; set; }

        /// <summary>
        /// Id do fabricante
        /// </summary>
        public string fabricanteId { get; set; }

        /// <summary>
        /// Nome do fabricante
        /// </summary>
        public string fabricanteNome { get; set; }

        /// <summary>
        /// Nome abreviado do fabricante
        /// </summary>
        public string fabricanteNomeAbreviado { get; set; }

        /// <summary>
        /// Id da classificação fiscal
        /// </summary>
        public string classificacaoFiscalId { get; set; }

        /// <summary>
        /// Descrição da classificação fiscal
        /// </summary>
        public string classificacaoFiscalDescricao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string empenhoPregao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unidadeNegocioId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string unidadeNegocioDescricao { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string estabelecimentoId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string estabelecimentoRazaoSocial { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  decimal tabelaPrecoVlrMinimo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  decimal tabelaPrecoVlrTabela { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  decimal tabelaPrecoVlrMaximo { get; set; }
        /// <summary>
        /// Verifica se foi informado valor
        /// </summary>
        public bool informado { get; set; }
        /// <summary>
        /// Verifica se o registro está na base
        /// </summary>
        public bool gravado { get; set; }
        /// <summary>
        /// Quando a unidade de negócio for PJ-Público utilizar o depósito LIC. Para as demais unidades de negócio utilizar o depósito ALM.
        /// </summary>
        public string tipoUsuario { get; set; }
        /// <summary>
        /// variavel usada como parametro para listar dados da tabela de preço
        /// </summary>
        public string clienteIdERP { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int Saldo { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int clienteId { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  decimal precoFabrica { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime precoFabricaValidadeInicio { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public DateTime precoFabricaValidadeFim { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string itemCodigoOrigem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string itemCodTributacaoICMS { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  decimal itemAliquotaIPI { get; set; }
        /// <summary>
        ///
        /// </summary>
        public string ufIdOrigem { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public string ufIdDestino { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public bool contribuinte { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public  decimal coeficienteDesconto { get; set; }
        /// <summary>
        /// 
        /// </summary>
        public int pedidoId { get; set; }

        /// <summary>
        /// Informa se é pedido existente
        /// </summary>
        public bool IsPedido
        {
            get { return !pedidoId.Equals(0); }
        }


        public bool itemConv118 { get; set; }
        public bool itemIsentoIcms { get; set; }

        public string codidentific { get; set; }
        #endregion

        #region :: Métodos ::

        void CreateSA()
        {
            sa = new SQLAutomator(this, "KsItem", "itemId", null, "Id,Descricao,unidadeMedidaSigla,familiaComercialDescricao,familiaMaterialDescricao,grupoEstoqueDescricao,fabricanteNome,fabricanteNomeAbreviado,classificacaoFiscalDescricao,empenhoPregao,unidadeNegocioId,estabelecimentoId,tabelaPrecoVlrMinimo,tabelaPrecoVlrTabela,tabelaPrecoVlrMaximo,unidadeNegocioDescricao,estabelecimentoRazaoSocial,gravado,informado,tipoUsuario,clienteIdERP,Saldo,clienteId,precoFabrica,precoFabricaValidadeInicio,precoFabricaValidadeFim,itemCodigoOrigem,itemCodTributacaoICMS,itemAliquotaIPI,contribuinte,ufIdDestino,ufIdOrigem,coeficienteDesconto");
        }

        /// <summary>
        /// Construtor
        /// </summary>
        public Item()
        {
            CreateSA();
        }

        /// <summary>
        /// Recupera uma lista com os departamentos cadastrados
        /// </summary>
        /// <returns>DataTable</returns>
        public DataTable Listar()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                //string sSQL = sa.getSelectAllSQL("itemDescricao");
                string sSQL = string.Format("SELECT * FROM ksItem  WITH (NOLOCK) WHERE 1 = 1 AND (itemObsoleto <> 1  or itemObsoleto is null)  {0} {1} ORDER BY itemDescricao",
                                            !string.IsNullOrEmpty(familiaMaterialId) ? " AND familiaMaterialId = '" + familiaMaterialId + "'" : string.Empty,
                                            !string.IsNullOrEmpty(fabricanteId) ? " AND fabricanteId = '" + fabricanteId + "'" : string.Empty);

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

        public DataTable ListarFiltroPorId()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT ITM.itemId, ITM.itemDescricao,FMT.fabricanteId, FMT.fabricanteNome  FROM	KsItem ITM   WITH(NOLOCK)
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT  WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES  WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              INNER JOIN ksFabricante FBR WITH(NOLOCK) ON
		                                                FBR.fabricanteId = ITM.fabricanteId
                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              WHERE	 1=1   {0}  AND (itemObsoleto <> 1  or itemObsoleto is null) AND  itemDiferenciado  IS NULL",
                                                       !String.IsNullOrEmpty(itemId) ? "AND ITM.itemId  LIKE '%" + itemId + "%' " : string.Empty);


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

        public DataTable ListarFiltroIn(List<string> ids)
        {
            DataBaseAccess da = new DataBaseAccess();

            foreach (var item in ids)
            {
                itemId += "'" + item + "',";
            }

            itemId = itemId.Substring(0, itemId.Length - 1);
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT    *  FROM	KsItem ITM   WITH(NOLOCK)
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT  WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES  WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              INNER JOIN ksFabricante FBR WITH(NOLOCK) ON
		                                                FBR.fabricanteId = ITM.fabricanteId
                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              WHERE	    {0}",

                                                         !String.IsNullOrEmpty(itemId) ? "ITM.itemId in ("


                                                         + itemId + ")" : string.Empty);


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


        public DataTable ListarFiltro()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT    *  FROM	KsItem ITM   WITH(NOLOCK)
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT  WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES  WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              INNER JOIN ksFabricante FBR WITH(NOLOCK) ON
		                                                FBR.fabricanteId = ITM.fabricanteId
                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              WHERE	    {0} {1} {2} {3} {4} {5} {6} {7} 
                                                        ITM.itemDescricao LIKE '%" + itemDescricao + "%'    and (itemObsoleto <> 1  or itemObsoleto is null)",
                                                        !String.IsNullOrEmpty(itemId) ? "ITM.itemId  LIKE '%" + itemId + "%' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(unidadeMedidaId) ? "ITM.unidadeMedidaId = '" + unidadeMedidaId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaComercialId) ? "ITM.familiaComercialId = '" + familiaComercialId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaMaterialId) ? "ITM.familiaMaterialId = '" + familiaMaterialId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(grupoEstoqueId) ? "ITM.grupoEstoqueId = '" + grupoEstoqueId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(fabricanteId) ? "ITM.fabricanteId = '" + fabricanteId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(classificacaoFiscalId) ? "ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(itemInfComplementar) ? "ITM.itemInfComplementar = '" + itemInfComplementar + "' AND" : string.Empty);


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

        public DataTable ListarItemEan()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT    *  FROM	KsItem ITM   WITH(NOLOCK)
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT  WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES  WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              INNER JOIN ksFabricante FBR WITH(NOLOCK) ON
		                                                FBR.fabricanteId = ITM.fabricanteId
                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              WHERE	 1=1   {0} {1} {2} {3} {4} {5} {6} {7}  AND (itemObsoleto <> 1  or itemObsoleto is null) AND  itemDiferenciado  IS NULL",
                                                        !String.IsNullOrEmpty(itemId) ? "AND ITM.itemId  LIKE '%" + itemId + "%' " : string.Empty,
                                                        !String.IsNullOrEmpty(unidadeMedidaId) ? "AND ITM.unidadeMedidaId = '" + unidadeMedidaId + "'" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaComercialId) ? "AND ITM.familiaComercialId = '" + familiaComercialId + "'" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaMaterialId) ? " AND ITM.familiaMaterialId = '" + familiaMaterialId + "'" : string.Empty,
                                                        !String.IsNullOrEmpty(grupoEstoqueId) ? " AND ITM.grupoEstoqueId = '" + grupoEstoqueId + "'" : string.Empty,
                                                        !String.IsNullOrEmpty(fabricanteId) ? " AND ITM.fabricanteId = '" + fabricanteId + "'" : string.Empty,
                                                        !String.IsNullOrEmpty(classificacaoFiscalId) ? " AND ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "'" : string.Empty,
                                                         !String.IsNullOrEmpty(itemInfComplementar) ? " AND ITM.itemInfComplementar = '" + itemInfComplementar + "'" : string.Empty);


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


        public DataTable ListarFiltroItemPedido(bool IsContrato = false)
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Bloco Reserva ::

                string _blocoReserva =
                    string.Format(
                        @"ISNULL
		                    ( 
			                    (
			                    SELECT	
					                    SUM(PedIt.pedidoItemQuantidade) 
			                    FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
					                    INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
			                    WHERE 
					                    PedIt.itemId				=		ITM.itemId
					                    AND Ped.pedidoSituacao	IN		('Reserva') 
					                    AND Ped.estabelecimentoId	= '{0}'
                                        AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
			                    )
		                    , 0)
		                    - ",
                            estabelecimentoId),

                       _pedidoDiferente =
                            string.Format(
                                @"
                                    AND Ped.pedidoId <> {0}
                                 ",
                                 pedidoId);

                #endregion

                #region :: Bloco Desconto ::

                string _descontoJoin =
                    string.Format(
                        @"
                          INNER JOIN ksCliente CLI WITH(NOLOCK) ON
                                    CLI.clienteId = {2}

                          LEFT JOIN ksItemDesconto DSC WITH(NOLOCK) ON
                                DSC.itemId = ITM.itemId
                          AND   DSC.ufIdOrigem = '{0}'
                          AND   ((DSC.ufIdDestino = '{1}') OR (LTRIM(RTRIM(DSC.ufIdDestino)) = ''))                          
                          AND   ((DSC.contribuinte = CLI.clienteContribuinteICMS) OR (CLI.clienteContribuinteICMS IS NULL))

                          LEFT JOIN ksItemAdicional ADC WITH(NOLOCK) ON
                                ADC.itemId = DSC.itemId

                          LEFT JOIN ksItemAdicionalCF ACF WITH(NOLOCK) ON
                                ACF.classeFiscalId = ADC.classeFiscal
                         ",
                          ufIdOrigem,
                          ufIdDestino,
                          clienteId),

                        _descontoCampos =
                            @",ISNULL(DSC.coeficienteDesconto, 0) coeficienteDesconto
                              ,contribuinte
                              ,ISNULL(DSC.ufIdDestino, '') ufIdDestino
                              ,ISNULL(ADC.resolucao13, 0) resolucao13
                              ,ISNULL(ADC.convenio87, 0) convenio87
                              ,ISNULL(ADC.classeFiscal, 1) classeFiscalId
                              ,ACF.classeFiscal AS classeFiscalACF
                             ",
                        _descontoGroup =
                            @",coeficienteDesconto,contribuinte,ufIdDestino,resolucao13
                              ,convenio87,classeFiscalId,ACF.classeFiscal,ADC.classeFiscal";

                #endregion

                #region :: Bloco Contrato ::

                string _BlocoCampos =
                            @",ISNULL(IFRC.precoFabrica, 0) precoFabrica
                              ,IFRC.dataInicio AS precoFabricaValidadeInicio
                              ,IFRC.dataTermino AS precoFabricaValidadeFim
                              ,IFRC.estabelecimentoID
                              ,ISNULL(IFRC.fornecedorID, ITM.fabricanteId) fornecedorID
                              ,EST.estabelecimentoRazaoSocial
							  ,EST.ufId",

                        _BlocoJoin =
                            string.Format(
                                @"LEFT JOIN ksItemFornec IFRC WITH(NOLOCK) ON
                                        IFRC.ItemID = ITM.itemId 
                                  AND   IFRC.estabelecimentoID = '{0}' 
                                  --AND   IFRC.fornecedorID = FBR.fabricanteId

                                 LEFT JOIN ksEstabelecimento EST WITH(NOLOCK) ON
								        EST.estabelecimentoId = IFRC.estabelecimentoID	
                                 ", estabelecimentoId),

                       _BlocoCondicao = string.Format(
                                @"(('{0}' BETWEEN IFRC.dataInicio AND IFRC.dataTermino) OR (IFRC.dataInicio IS NULL)) AND ",
                                  DateTime.Now.Date),

                       _BlocoGroup = @",IFRC.precoFabrica,IFRC.dataInicio,IFRC.dataTermino,IFRC.estabelecimentoID,IFRC.fornecedorID
                                       ,EST.estabelecimentoRazaoSocial,EST.ufId",

                       _BlocoJoinFabricante = @"FBR.fabricanteId = IFRC.fornecedorID";

                #endregion

                #region :: Query ::

                string sSQL = string.Format(@"SET DATEFORMAT DMY
                                              SELECT    ITM.itemId
                                                        ,ITM.unidadeMedidaId
                                                        ,ITM.itemDescricao
                                                        ,ITM.itemInfComplementar
                                                        ,ITM.itemPesoBruto
                                                        ,ITM.itemPesoLiquido
                                                        ,ITM.familiaComercialId
                                                        ,ITM.familiaMaterialId
                                                        ,ITM.grupoEstoqueId
                                                        ,ITM.classificacaoFiscalId
                                                        ,ITM.itemRefrigerado
                                                        ,ITM.itemControlado
                                                        ,ITM.tipoProdutoId
                                                        ,ITM.dataImplant
                                                        ,ITM.itemCodEMS
                                                        ,ITM.itemCodigoOrigem
                                                        ,ITM.codidentific
                                                        ,ISNULL(ITM.itemConv118,0) AS itemConv118
                                                        ,ISNULL(ITM.itemIsentoIcms,0) AS itemIsentoIcms  
                                                        ,ISNULL(ITM.AutorizaVacinas,0) AS AutorizaVacinas 
                                                        ,ISNULL(ITM.AutorizaRetinoide,0) AS AutorizaRetinoide 
                                                        ,ISNULL(ITM.BloqVacinas,0) AS BloqVacinas 
                                                        ,ISNULL(ITM.BloqRetinoide,0) AS BloqRetinoide 
                                                        ,ISNULL(ITM.BloqMisoprostol,0) AS BloqMisoprostol         
                                                        ,ISNULL(ITM.itemObsoleto,0) AS itemObsoleto                                                  
                                                        ,UNM.unidadeMedidaId                                                         
                                                        ,UNM.unidadeMedidaSigla
                                                        ,FCM.familiaComercialDescricao
		                                                ,FMT.familiaMaterialDescricao
		                                                ,GES.grupoEstoqueDescricao
		                                                ,ISNULL(FBR.fabricanteNome, FB.fabricanteNome) fabricanteNome
                		                                ,ISNULL(FBR.fabricanteNomeAbreviado, FB.fabricanteNomeAbreviado) fabricanteNomeAbreviado
                                                        ,CFS.classificacaoFiscalDescricao
                                                        ,FMT.familiaMaterialId
                                                        ,ISNULL(FBR.fabricanteId, ITM.fabricanteId) fabricanteId
                                                        --,ITM.precoFabrica
                                                        --,ITM.precoFabricaValidadeInicio
                                                        --,ITM.precoFabricaValidadeFim
                                                        {12}
                                                        {17}
                                                        ,
                                                        -- valor minimo 
														CASE
															 ISNULL((SELECT TOP 1 PC.tabelaPrecoVlrMinimo 
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (    -- PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
                                                                                                                     PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PC.UFdestino					 = TAB_IMP.UFdestino 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                            INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PC.itemId = ITM.itemId       AND 
																	PC.unidadeNegocioId = '{7}'  AND 
																	PC.estabelecimentoId = '{8}' AND 
			                                                        TAB_IMP.UFdestino =	'{22}'  AND
																	CAST(PC.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
                                                                     
                                                         WHEN -99999 THEN                                                         
															 ISNULL((SELECT top 1 PC.tabelaPrecoVlrMinimo 
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PC.UFdestino					 = TAB_IMP.UFdestino 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND 
                                                                     PC.itemId = ITM.itemId      AND 
																	 PC.unidadeNegocioId = '{7}' AND 
                                                                     PC.clienteIdERP = '' AND
			                                                         TAB_IMP.UFdestino =	'{22}'  AND
																	 PC.estabelecimentoId = '{8}'), 0)
														ELSE 
                                                              ISNULL((SELECT TOP 1 PC.tabelaPrecoVlrMinimo 
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                             INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP  WITH(NOLOCK) ON ( --    PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PC.UFdestino					 = TAB_IMP.UFdestino 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PC.itemId = ITM.itemId       AND 
																	 PC.unidadeNegocioId = '{7}'  AND 
																	 PC.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
																	 CAST(PC.clienteIdErp AS INT) =  CAST('{10}' AS INT)), 0)
														 END AS tabelaPrecoVlrMinimo,

                                                         -- valor tabela
														CASE 
                                                         ISNULL((SELECT TOP 1 PR.tabelaPrecoVlrTabela 
                                                          FROM   KsTabelaPrecoKS PR  WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PR.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                            PR.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PR.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PR.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PR.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PR.UFdestino					 = TAB_IMP.UFdestino     
														                                                        AND  PR.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                          INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                 PR.itemId = ITM.itemId       AND 
                                                                 PR.unidadeNegocioId = '{7}'  AND 
                                                                 PR.estabelecimentoId = '{8}' AND 
                                                                TAB_IMP.UFdestino =	'{22}'  AND
				                                                 CAST(PR.clienteIdErp AS INT) = CAST('{10}' AS INT)
                                                         ), -99999)
														 WHEN -99999 THEN														 
														  ISNULL((SELECT top 1 PR.tabelaPrecoVlrTabela 
																  FROM   KsTabelaPrecoKS PR  WITH(NOLOCK)
                                                                   INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PR.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                                     PR.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                                AND  PR.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                                AND  PR.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                                AND  PR.itemId					 = TAB_IMP.itemId
                                                                                                                        AND  PR.UFdestino					 = TAB_IMP.UFdestino  
														                                                                AND  PR.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                               )
																  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                         PR.itemId = ITM.itemId      AND 
                                                                         PR.unidadeNegocioId = '{7}' AND 
                                                                         PR.clienteIdERP = '' AND
			                                                             TAB_IMP.UFdestino =	'{22}'  AND
                                                                         PR.estabelecimentoId = '{8}'), 0)
														 ELSE
															 ISNULL((SELECT TOP 1 PR.tabelaPrecoVlrTabela 
															  FROM   KsTabelaPrecoKS PR  WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PR.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PR.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PR.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PR.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PR.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PR.UFdestino					 = TAB_IMP.UFdestino 
														                                                        AND  PR.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PR.itemId = ITM.itemId       AND 
																	 PR.unidadeNegocioId = '{7}'  AND 
																	 PR.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
																	 CAST(PR.clienteIdErp AS INT) = CAST('{10}' AS INT)), 0)
														 END AS tabelaPrecoVlrTabela,

                                                        -- valor maximo 
														CASE
                                                          ISNULL((SELECT TOP 1 PO.tabelaPrecoVlrMaximo 
                                                          FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (    -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PO.UFdestino					 = TAB_IMP.UFdestino             
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                          INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                 PO.itemId = ITM.itemId       AND 
                                                                 PO.unidadeNegocioId = '{7}'  AND 
                                                                 TAB_IMP.UFdestino =	'{22}'  AND
                                                                 PO.estabelecimentoId = '{8}' AND 
				                                                 CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
														WHEN -99999 THEN  ISNULL((SELECT top 1 PO.tabelaPrecoVlrMaximo 
																		  FROM   KsTabelaPrecoKS PO  WITH (NOLOCK)
                                                                          INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                                           PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                                      AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                                      AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
                                                                                                                              AND  PO.UFdestino					 = TAB_IMP.UFdestino 														                                                               
                                                                                                                              AND  PO.itemId				 = TAB_IMP.itemId 
														                                                                      AND  PO.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId
														                                                                      )
																		  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                                 PO.itemId = ITM.itemId     AND 
																				 PO.unidadeNegocioId = '{7}' AND 
                                                                                 PO.clienteIdERP = '' AND
                                                                                 TAB_IMP.UFdestino =	'{22}'  AND
																				 PO.estabelecimentoId = '{8}'),0)
														ELSE 
															ISNULL((SELECT TOP 1 PO.tabelaPrecoVlrMaximo 
															 FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PO.UFdestino					 = TAB_IMP.UFdestino         
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															 INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PO.itemId = ITM.itemId       AND 
                                                                     PO.unidadeNegocioId = '{7}'  AND 
                                                                     PO.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
				                                                     CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), 0)
														END AS tabelaPrecoVlrMaximo,
    

                                                        -- retorna codigo da tabela utilizada 

														CASE
															 ISNULL((SELECT TOP 1 PC.tabelaPrecoImportacaoId
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = TAB_IMP.estabelecimentoidPara
														                                                        AND  PC.clienteIdErp 			 = TAB_IMP.clienteIdErp
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PC.UFdestino					 = TAB_IMP.UFdestino             
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															        INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PC.itemId = ITM.itemId       AND 
																	PC.unidadeNegocioId = '{7}'  AND 
																	PC.estabelecimentoId = '{8}' AND 
                                                                    TAB_IMP.UFdestino =	'{22}'  AND
																	CAST(PC.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
                                                                     
                                                         WHEN -99999 THEN                                                         
															 ISNULL((SELECT top 1 PC.tabelaPrecoImportacaoId
															  FROM   KsTabelaPrecoKS PC  WITH (NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (  --   PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.UFdestino					 = TAB_IMP.UFdestino 
                                                                                                                AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															 INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PC.itemId = ITM.itemId      AND 
																	 PC.unidadeNegocioId = '{7}' AND 
                                                                     PC.clienteIdERP = '' AND
                                                                      TAB_IMP.UFdestino =	'{22}'  AND
																	 PC.estabelecimentoId = '{8}'), 0)
														ELSE 
                                                              ISNULL((SELECT TOP 1 PC.tabelaPrecoImportacaoId
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                             INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.UFdestino					 = TAB_IMP.UFdestino 
                                                                                                                AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PC.itemId = ITM.itemId       AND 
																	 PC.unidadeNegocioId = '{7}'  AND 
																	 PC.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
																	 CAST(PC.clienteIdErp AS INT) =  CAST('{10}' AS INT)), 0)
														 END AS tabelaPrecoImportacaoId,
                                        -- valor  Preço Fabrica 
														CASE
                                                          ISNULL((SELECT TOP 1 TAB_IMP.tabelaPrecoFabrica
                                                          FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (    -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
														                                                        AND  PO.UFdestino					 = TAB_IMP.UFdestino 
                                                                                                                AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                          INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                 PO.itemId = ITM.itemId       AND 
                                                                 PO.unidadeNegocioId = '{7}'  AND 
                                                                 PO.estabelecimentoId = '{8}' AND 
                                                                 TAB_IMP.UFdestino =	'{22}'  AND
				                                                 CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
														WHEN -99999 THEN  ISNULL((SELECT top 1 TAB_IMP.tabelaPrecoFabrica 
																		  FROM   KsTabelaPrecoKS PO  WITH (NOLOCK)
                                                                          INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                                           PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                                      AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                                      AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                                      AND  PO.itemId				 = TAB_IMP.itemId
                                                                                                                              AND  PO.UFdestino					 = TAB_IMP.UFdestino    
														                                                                      AND  PO.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId
														                                                                      )
																		  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                                 PO.itemId = ITM.itemId     AND 
																				 PO.unidadeNegocioId = '{7}' AND 
                                                                                 PO.clienteIdERP = '' AND
                                                                                 TAB_IMP.UFdestino =	'{22}'  AND
																				 PO.estabelecimentoId = '{8}'),0)
														ELSE 
															ISNULL((SELECT TOP 1 TAB_IMP.tabelaPrecoFabrica 
															 FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
                                                                                                                AND  PO.UFdestino					 = TAB_IMP.UFdestino             
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															 INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PO.itemId = ITM.itemId       AND 
                                                                     PO.unidadeNegocioId = '{7}'  AND 
                                                                     PO.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
				                                                     CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), 0)
														END AS tabelaPrecoFabrica,
                                                        -------------- 
                                                        'true' AS gravado,
                                                        'false' AS informado,

                                                ISNULL((SELECT 
	                                                SUM(SE.saldoEstoqueQuantidadeAtual) 
		                                                -
		                                                --ISNULL
		                                                --( 
			                                            --    (
			                                            --    SELECT	
					                                    --            SUM(PedIt.pedidoItemQuantidade) 
			                                            --    FROM	KsPedidoItem AS PedIt 
					                                    --            INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
			                                            --    WHERE 
					                                    --            PedIt.itemId				=		ITM.itemId
					                                    --            AND Ped.pedidoSituacao	IN		('Reserva') 
					                                    --            AND Ped.estabelecimentoId	= '{8}'
                                                        --            AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
			                                            --    )
		                                                --, 0)
		                                                ---
                                                        {20}
		                                                ISNULL
		                                                ( 
			                                                (
			                                                SELECT
					                                                SUM(PedIt.pedidoItemQuantidade) 
			                                                FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
					                                                INNER JOIN  ksPedido Ped WITH(NOLOCK) ON  (ped.pedidoId = PedIt.pedidoId)
			                                                WHERE 
					                                                PedIt.itemId						=		ITM.itemId
					                                                AND NOT Ped.pedidoSituacao			IN		('Reprovado','Faturado','Cancelado','Reserva','Integrado','Orcamento')
					                                                AND Ped.estabelecimentoId			= '{8}'
					
					                                                AND 
					                                                (
						                                                PedIt.embarque					IS		NULL
						                                                OR PedIt.embarque				=		''
						                                                OR CAST(PedIt.embarque AS INT)	=		0
					                                                )
                                                                    AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
                                                                    {21}
			                                                )
		                                                , 0)
		                                                -
		                                                ISNULL
		                                                (
			                                                (
				                                                SELECT	
						                                                SUM(PedIt.pedidoItemQuantidade) 
				                                                FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
						                                                INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
				                                                WHERE 
						                                                PedIt.itemId						= ITM.itemId
						                                                AND NOT Ped.pedidoSituacao			IN ('Reprovado','FATURADO','Cancelado','Orcamento') -- Faturado já possui NOTA, portanto não é mais alocado
						                                                AND Ped.estabelecimentoId			= '{8}'
						                                                AND 
						                                                (
							                                                NOT PedIt.embarque				IS		NULL
							                                                AND PedIt.embarque				<>		''
							                                                AND CAST(PedIt.embarque AS INT)	>		0
							                                                --AND PedIt.atualizaSaldoEstoque	=		1
						                                                )
                                                                        AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
                                                                        {21}
			                                                )
		                                                , 0)	
		                                                FROM    ksSaldoEstoque SE  WITH(NOLOCK)
                                                        INNER JOIN ksGrupoClienteDeposito DP WITH(NOLOCK) ON
				                                                DP.depositoId = SE.saldoEstoqueDeposito
			                                            INNER JOIN ksCliente C WITH(NOLOCK) ON
				                                                C.grupoComercialId = DP.clienteGrupoComercialId AND
				                                                C.clienteId = {11}
			                                            WHERE   SE.itemId = ITM.itemId AND 
                                                                SE.estabelecimentoId = '{8}' 
                                                                --AND SE.saldoEstoqueDeposito = {9}
			                                            GROUP BY 
                                                                SE.estabelecimentoId, 
                                                                SE.saldoEstoqueDeposito
		                                                ),0)		
		                                                AS Saldo

		                                                ,(CASE
		                                                WHEN  IIF.itemId IS NULL THEN
			                                                'false'
		                                                else	
			                                                'true'
		                                                end
		                                                )AS IsentoFrete
                                                        ,0 as pedidoItemValorDescontoBoleto   

                                              FROM	KsItem ITM WITH(NOLOCK)
                                              --LEFT JOIN ksSaldoEstoque AS SE ON	  (SE.itemId	=	ITM.itemid AND SE.saldoEstoqueDeposito = {9} ) 
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              LEFT JOIN ksFabricante FB WITH(NOLOCK) ON	
                                                        FB.fabricanteId = ITM.fabricanteId
                                              {13}

                                              LEFT JOIN ksFabricante FBR WITH(NOLOCK) ON		                                                
                                                        {16}

                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              LEFT JOIN ksItemIsentoFrete AS IIF WITH(NOLOCK) ON	
                                                        IIF.itemId	=	ITM.itemid    
                                              {18}

                                              WHERE	    {0} {1} {2} {3} {4} {5} {6} {14}

                                                        ITM.itemDescricao LIKE '%" + itemDescricao + "%' "
                                                     + @"GROUP BY ITM.itemId,ITM.unidadeMedidaId,ITM.itemDescricao,
				                                        ITM.itemInfComplementar,ITM.itemPesoBruto,ITM.itemPesoLiquido,
				                                        ITM.familiaComercialId,ITM.familiaMaterialId,ITM.grupoEstoqueId,
				                                        ITM.fabricanteId,ITM.classificacaoFiscalId,ITM.itemRefrigerado, ITM.codidentific,
				                                        ITM.itemControlado,ITM.tipoProdutoId,
				                                        UNM.unidadeMedidaSigla,FCM.familiaComercialDescricao
				                                        ,FMT.familiaMaterialDescricao,GES.grupoEstoqueDescricao,FBR.fabricanteNome
                                                        ,FBR.fabricanteNomeAbreviado,CFS.classificacaoFiscalDescricao,ITM.fabricanteId
                                                        ,UNM.unidadeMedidaId,IIF.itemId,FMT.familiaMaterialId,FBR.fabricanteId,ITM.dataImplant,ITM.itemCodEMS
                                                        ,FB.fabricanteNomeAbreviado,FB.fabricanteNome,ITM.itemCodigoOrigem,ITM.itemConv118,ITM.itemIsentoIcms,ITM.AutorizaVacinas,ITM.AutorizaRetinoide ,ITM.BloqMisoprostol,ITM.BloqVacinas
                                                        ,ITM.BloqRetinoide,ITM.itemObsoleto
                                                        {15}{19}",
                                                         !String.IsNullOrEmpty(itemId) ? "ITM.itemId  = '" + itemId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(unidadeMedidaId) ? "ITM.unidadeMedidaId = '" + unidadeMedidaId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(familiaComercialId) ? "ITM.familiaComercialId = '" + familiaComercialId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(familiaMaterialId) ? "ITM.familiaMaterialId = '" + familiaMaterialId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(grupoEstoqueId) ? "ITM.grupoEstoqueId = '" + grupoEstoqueId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(fabricanteId) ? "ITM.fabricanteId = '" + fabricanteId + "' AND" : string.Empty,
                                                         !String.IsNullOrEmpty(classificacaoFiscalId) ? "ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "' AND" : string.Empty,
                                                         unidadeNegocioId,
                                                         estabelecimentoId,
                                                         tipoUsuario == "PJP" ? "'LIC'" : "'ALM'",
                                                         clienteIdERP,
                                                         clienteId,
                                                         IsContrato ? _BlocoCampos : string.Empty,
                                                         IsContrato ? _BlocoJoin : string.Empty,
                                                         IsContrato ? _BlocoCondicao : string.Empty,
                                                         IsContrato ? _BlocoGroup : string.Empty,
                                                         IsContrato ? _BlocoJoinFabricante : "FBR.fabricanteId = ITM.fabricanteId",
                                                         !IsContrato ? _descontoCampos : string.Empty,
                                                         !IsContrato ? _descontoJoin : string.Empty,
                                                         !IsContrato ? _descontoGroup : string.Empty,
                                                         IsPedido ? string.Empty : _blocoReserva,
                                                         IsPedido ? _pedidoDiferente : string.Empty,
                                                            ufIdDestino
                                                          // string.Empty 
                                                          );

                #endregion

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
   

        public DataTable ListarFiltroItem(bool IsContrato = false)
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Bloco Reserva ::

                string _blocoReserva =
                    string.Format(
                        @"ISNULL
		                    ( 
			                    (
			                    SELECT	
					                    SUM(PedIt.pedidoItemQuantidade) 
			                    FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
					                    INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
			                    WHERE 
					                    PedIt.itemId				=		ITM.itemId
					                    AND Ped.pedidoSituacao	IN		('Reserva') 
					                    AND Ped.estabelecimentoId	= '{0}'
                                        AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
			                    )
		                    , 0)
		                    - ",
                            estabelecimentoId),

                       _pedidoDiferente =
                            string.Format(
                                @"
                                    AND Ped.pedidoId <> {0}
                                 ",
                                 pedidoId);

                #endregion

                #region :: Bloco Desconto ::

                string _descontoJoin =
                    string.Format(
                        @"
                          INNER JOIN ksCliente CLI WITH(NOLOCK) ON
                                    CLI.clienteId = {2}

                          LEFT JOIN ksItemDesconto DSC WITH(NOLOCK) ON
                                DSC.itemId = ITM.itemId
                          AND   DSC.ufIdOrigem = '{0}'
                          AND   ((DSC.ufIdDestino = '{1}') OR (LTRIM(RTRIM(DSC.ufIdDestino)) = ''))                          
                          AND   ((DSC.contribuinte = CLI.clienteContribuinteICMS) OR (CLI.clienteContribuinteICMS IS NULL))

                          LEFT JOIN ksItemAdicional ADC WITH(NOLOCK) ON
                                ADC.itemId = DSC.itemId

                          LEFT JOIN ksItemAdicionalCF ACF WITH(NOLOCK) ON
                                ACF.classeFiscalId = ADC.classeFiscal
                         ",
                          ufIdOrigem,
                          ufIdDestino,
                          clienteId),

                        _descontoCampos =
                            @",ISNULL(DSC.coeficienteDesconto, 0) coeficienteDesconto
                              ,contribuinte
                              ,ISNULL(DSC.ufIdDestino, '') ufIdDestino
                              ,ISNULL(ADC.resolucao13, 0) resolucao13
                              ,ISNULL(ADC.convenio87, 0) convenio87
                              ,ISNULL(ADC.classeFiscal, 1) classeFiscalId
                              ,ACF.classeFiscal AS classeFiscalACF
                             ",
                        _descontoGroup =
                            @",coeficienteDesconto,contribuinte,ufIdDestino,resolucao13
                              ,convenio87,classeFiscalId,ACF.classeFiscal,ADC.classeFiscal";

                #endregion

                #region :: Bloco Contrato ::

                string _BlocoCampos =
                            @",ISNULL(IFRC.precoFabrica, 0) precoFabrica
                              ,IFRC.dataInicio AS precoFabricaValidadeInicio
                              ,IFRC.dataTermino AS precoFabricaValidadeFim
                              ,IFRC.estabelecimentoID
                              ,ISNULL(IFRC.fornecedorID, ITM.fabricanteId) fornecedorID
                              ,EST.estabelecimentoRazaoSocial
							  ,EST.ufId",

                        _BlocoJoin =
                            string.Format(
                                @"LEFT JOIN ksItemFornec IFRC WITH(NOLOCK) ON
                                        IFRC.ItemID = ITM.itemId 
                                  AND   IFRC.estabelecimentoID = '{0}' 
                                  --AND   IFRC.fornecedorID = FBR.fabricanteId

                                 LEFT JOIN ksEstabelecimento EST WITH(NOLOCK) ON
								        EST.estabelecimentoId = IFRC.estabelecimentoID	
                                 ", estabelecimentoId),

                       _BlocoCondicao =
                            string.Format(
                                @"(('{0}' BETWEEN IFRC.dataInicio AND IFRC.dataTermino) OR (IFRC.dataInicio IS NULL)) AND ",
                                  DateTime.Now.Date),

                       _BlocoGroup = @",IFRC.precoFabrica,IFRC.dataInicio,IFRC.dataTermino,IFRC.estabelecimentoID,IFRC.fornecedorID
                                       ,EST.estabelecimentoRazaoSocial,EST.ufId",

                       _BlocoJoinFabricante = @"FBR.fabricanteId = IFRC.fornecedorID";

                #endregion

                #region :: Query ::

                string sSQL = string.Format(@"SET DATEFORMAT DMY
                                              SELECT    ITM.itemId
                                                        ,ITM.unidadeMedidaId
                                                        ,ITM.itemDescricao
                                                        ,ITM.itemInfComplementar
                                                        ,ITM.itemPesoBruto
                                                        ,ITM.itemPesoLiquido
                                                        ,ITM.familiaComercialId
                                                        ,ITM.familiaMaterialId
                                                        ,ITM.grupoEstoqueId
                                                        ,ITM.classificacaoFiscalId
                                                        ,ITM.itemRefrigerado
                                                        ,ITM.itemControlado
                                                        ,ITM.tipoProdutoId
                                                        ,ITM.dataImplant
                                                        ,ITM.itemCodEMS
                                                        ,ITM.itemCodigoOrigem
                                                        ,ITM.codidentific
                                                        ,ISNULL(ITM.itemConv118,0) AS itemConv118
                                                        ,ISNULL(ITM.itemIsentoIcms,0) AS itemIsentoIcms  
                                                        ,ISNULL(ITM.AutorizaVacinas,0) AS AutorizaVacinas 
                                                        ,ISNULL(ITM.AutorizaRetinoide,0) AS AutorizaRetinoide 
                                                        ,ISNULL(ITM.BloqMisoprostol,0) AS BloqMisoprostol                                                         
                                                        ,UNM.unidadeMedidaId                                                         
                                                        ,UNM.unidadeMedidaSigla
                                                        ,FCM.familiaComercialDescricao
		                                                ,FMT.familiaMaterialDescricao
		                                                ,GES.grupoEstoqueDescricao
		                                                ,ISNULL(FBR.fabricanteNome, FB.fabricanteNome) fabricanteNome
                		                                ,ISNULL(FBR.fabricanteNomeAbreviado, FB.fabricanteNomeAbreviado) fabricanteNomeAbreviado
                                                        ,CFS.classificacaoFiscalDescricao
                                                        ,FMT.familiaMaterialId
                                                        ,ISNULL(FBR.fabricanteId, ITM.fabricanteId) fabricanteId
                                                        --,ITM.precoFabrica
                                                        --,ITM.precoFabricaValidadeInicio
                                                        --,ITM.precoFabricaValidadeFim
                                                        {12}
                                                        {17}
                                                        ,
                                                        -- valor minimo 
														CASE
															 ISNULL((SELECT TOP 1 PC.tabelaPrecoVlrMinimo 
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (    -- PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
                                                                                                                     PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                            INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PC.itemId = ITM.itemId       AND 
																	PC.unidadeNegocioId = '{7}'  AND 
																	PC.estabelecimentoId = '{8}' AND 
			                                                        TAB_IMP.UFdestino =	'{22}'  AND
																	CAST(PC.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
                                                                     
                                                         WHEN -99999 THEN                                                         
															 ISNULL((SELECT top 1 PC.tabelaPrecoVlrMinimo 
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND 
                                                                     PC.itemId = ITM.itemId      AND 
																	 PC.unidadeNegocioId = '{7}' AND 
                                                                     PC.clienteIdERP = '' AND
			                                                         TAB_IMP.UFdestino =	'{22}'  AND
																	 PC.estabelecimentoId = '{8}'), 0)
														ELSE 
                                                              ISNULL((SELECT TOP 1 PC.tabelaPrecoVlrMinimo 
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                             INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP  WITH(NOLOCK) ON ( --    PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PC.itemId = ITM.itemId       AND 
																	 PC.unidadeNegocioId = '{7}'  AND 
																	 PC.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
																	 CAST(PC.clienteIdErp AS INT) =  CAST('{10}' AS INT)), 0)
														 END AS tabelaPrecoVlrMinimo,

                                                         -- valor tabela
														CASE 
                                                         ISNULL((SELECT TOP 1 PR.tabelaPrecoVlrTabela 
                                                          FROM   KsTabelaPrecoKS PR  WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PR.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                            PR.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PR.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PR.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PR.itemId					 = TAB_IMP.itemId 
														                                                        AND  PR.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                          INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                 PR.itemId = ITM.itemId       AND 
                                                                 PR.unidadeNegocioId = '{7}'  AND 
                                                                 PR.estabelecimentoId = '{8}' AND 
                                                                TAB_IMP.UFdestino =	'{22}'  AND
				                                                 CAST(PR.clienteIdErp AS INT) = CAST('{10}' AS INT)
                                                         ), -99999)
														 WHEN -99999 THEN														 
														  ISNULL((SELECT top 1 PR.tabelaPrecoVlrTabela 
																  FROM   KsTabelaPrecoKS PR  WITH(NOLOCK)
                                                                   INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PR.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                                     PR.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                                AND  PR.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                                AND  PR.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                                AND  PR.itemId					 = TAB_IMP.itemId 
														                                                                AND  PR.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                               )
																  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                         PR.itemId = ITM.itemId      AND 
                                                                         PR.unidadeNegocioId = '{7}' AND 
                                                                         PR.clienteIdERP = '' AND
			                                                             TAB_IMP.UFdestino =	'{22}'  AND
                                                                         PR.estabelecimentoId = '{8}'), 0)
														 ELSE
															 ISNULL((SELECT TOP 1 PR.tabelaPrecoVlrTabela 
															  FROM   KsTabelaPrecoKS PR  WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PR.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PR.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PR.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PR.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PR.itemId					 = TAB_IMP.itemId 
														                                                        AND  PR.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PR.itemId = ITM.itemId       AND 
																	 PR.unidadeNegocioId = '{7}'  AND 
																	 PR.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
																	 CAST(PR.clienteIdErp AS INT) = CAST('{10}' AS INT)), 0)
														 END AS tabelaPrecoVlrTabela,

                                                        -- valor maximo 
														CASE
                                                          ISNULL((SELECT TOP 1 PO.tabelaPrecoVlrMaximo 
                                                          FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (    -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                          INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                 PO.itemId = ITM.itemId       AND 
                                                                 PO.unidadeNegocioId = '{7}'  AND 
                                                                 TAB_IMP.UFdestino =	'{22}'  AND
                                                                 PO.estabelecimentoId = '{8}' AND 
				                                                 CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
														WHEN -99999 THEN  ISNULL((SELECT top 1 PO.tabelaPrecoVlrMaximo 
																		  FROM   KsTabelaPrecoKS PO  WITH (NOLOCK)
                                                                          INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                                           PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                                      AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                                      AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                                      AND  PO.itemId				 = TAB_IMP.itemId 
														                                                                      AND  PO.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId
														                                                                      )
																		  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                                 PO.itemId = ITM.itemId     AND 
																				 PO.unidadeNegocioId = '{7}' AND 
                                                                                 PO.clienteIdERP = '' AND
                                                                                 TAB_IMP.UFdestino =	'{22}'  AND
																				 PO.estabelecimentoId = '{8}'),0)
														ELSE 
															ISNULL((SELECT TOP 1 PO.tabelaPrecoVlrMaximo 
															 FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															 INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PO.itemId = ITM.itemId       AND 
                                                                     PO.unidadeNegocioId = '{7}'  AND 
                                                                     PO.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
				                                                     CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), 0)
														END AS tabelaPrecoVlrMaximo,
    

                                                        -- retorna codigo da tabela utilizada 

														CASE
															 ISNULL((SELECT TOP 1 PC.tabelaPrecoImportacaoId
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = TAB_IMP.estabelecimentoidPara
														                                                        AND  PC.clienteIdErp 			 = TAB_IMP.clienteIdErp
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															        INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PC.itemId = ITM.itemId       AND 
																	PC.unidadeNegocioId = '{7}'  AND 
																	PC.estabelecimentoId = '{8}' AND 
                                                                    TAB_IMP.UFdestino =	'{22}'  AND
																	CAST(PC.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
                                                                     
                                                         WHEN -99999 THEN                                                         
															 ISNULL((SELECT top 1 PC.tabelaPrecoImportacaoId
															  FROM   KsTabelaPrecoKS PC  WITH (NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (  --   PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															 INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PC.itemId = ITM.itemId      AND 
																	 PC.unidadeNegocioId = '{7}' AND 
                                                                     PC.clienteIdERP = '' AND
                                                                      TAB_IMP.UFdestino =	'{22}'  AND
																	 PC.estabelecimentoId = '{8}'), 0)
														ELSE 
                                                              ISNULL((SELECT TOP 1 PC.tabelaPrecoImportacaoId
															  FROM   KsTabelaPrecoKS PC WITH(NOLOCK)
                                                             INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     --PC.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PC.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PC.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PC.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PC.itemId					 = TAB_IMP.itemId 
														                                                        AND  PC.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                     PC.itemId = ITM.itemId       AND 
																	 PC.unidadeNegocioId = '{7}'  AND 
																	 PC.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
																	 CAST(PC.clienteIdErp AS INT) =  CAST('{10}' AS INT)), 0)
														 END AS tabelaPrecoImportacaoId,
                                        -- valor  Preço Fabrica 
														CASE
                                                          ISNULL((SELECT TOP 1 TAB_IMP.tabelaPrecoFabrica
                                                          FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (    -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
                                                          INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                 PO.itemId = ITM.itemId       AND 
                                                                 PO.unidadeNegocioId = '{7}'  AND 
                                                                 PO.estabelecimentoId = '{8}' AND 
                                                                 TAB_IMP.UFdestino =	'{22}'  AND
				                                                 CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), -99999)
														WHEN -99999 THEN  ISNULL((SELECT top 1 TAB_IMP.tabelaPrecoFabrica 
																		  FROM   KsTabelaPrecoKS PO  WITH (NOLOCK)
                                                                          INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                                           PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                                      AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                                      AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                                      AND  PO.itemId				 = TAB_IMP.itemId 
														                                                                      AND  PO.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId
														                                                                      )
																		  INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                                 PO.itemId = ITM.itemId     AND 
																				 PO.unidadeNegocioId = '{7}' AND 
                                                                                 PO.clienteIdERP = '' AND
                                                                                 TAB_IMP.UFdestino =	'{22}'  AND
																				 PO.estabelecimentoId = '{8}'),0)
														ELSE 
															ISNULL((SELECT TOP 1 TAB_IMP.tabelaPrecoFabrica 
															 FROM   KsTabelaPrecoKS PO WITH(NOLOCK)
                                                            INNER JOIN ksTabelaPrecoImportacaoItem	TAB_IMP WITH(NOLOCK) ON (     -- PO.unidadeNegocioId		 = TAB_IMP.unidadeNegocioId AND  
														                                                             PO.estabelecimentoId		 = TAB_IMP.estabelecimentoId
														                                                        AND  PO.estabelecimentoidPara	 = ISNULL(TAB_IMP.estabelecimentoidPara,'')
														                                                        AND  PO.clienteIdErp 			 = ISNULL(TAB_IMP.clienteIdErp ,'')
														                                                        AND  PO.itemId					 = TAB_IMP.itemId 
														                                                        AND  PO.tabelaPrecoImportacaoId  = TAB_IMP.tabelaPrecoImportacaoId
														                                                        )
															 INNER JOIN KStabelaprecoimportacao TAB_CAPA WITH(NOLOCK) ON (TAB_CAPA.tabelaPrecoImportacaoId = TAB_IMP.tabelaPrecoImportacaoId)
															  WHERE  
                                                                    TAB_IMP.tabelaPrecoImportacaoFimVal > GETDATE() AND 
                                                                    TAB_CAPA.tabelaPrecoImportacaoStatus = 'Aprovada' AND
                                                                    PO.itemId = ITM.itemId       AND 
                                                                     PO.unidadeNegocioId = '{7}'  AND 
                                                                     PO.estabelecimentoId = '{8}' AND 
                                                                     TAB_IMP.UFdestino =	'{22}'  AND
				                                                     CAST(PO.clienteIdErp AS INT) = CAST('{10}' AS INT)), 0)
														END AS tabelaPrecoFabrica,
                                                        -------------- 
                                                        'true' AS gravado,
                                                        'false' AS informado,

                                                ISNULL((SELECT 
	                                                SUM(SE.saldoEstoqueQuantidadeAtual) 
		                                                -
		                                                --ISNULL
		                                                --( 
			                                            --    (
			                                            --    SELECT	
					                                    --            SUM(PedIt.pedidoItemQuantidade) 
			                                            --    FROM	KsPedidoItem AS PedIt 
					                                    --            INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
			                                            --    WHERE 
					                                    --            PedIt.itemId				=		ITM.itemId
					                                    --            AND Ped.pedidoSituacao	IN		('Reserva') 
					                                    --            AND Ped.estabelecimentoId	= '{8}'
                                                        --            AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
			                                            --    )
		                                                --, 0)
		                                                ---
                                                        {20}
		                                                ISNULL
		                                                ( 
			                                                (
			                                                SELECT
					                                                SUM(PedIt.pedidoItemQuantidade) 
			                                                FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
					                                                INNER JOIN  ksPedido Ped WITH(NOLOCK) ON  (ped.pedidoId = PedIt.pedidoId)
			                                                WHERE 
					                                                PedIt.itemId						=		ITM.itemId
					                                                AND NOT Ped.pedidoSituacao			IN		('Reprovado','Faturado','Cancelado','Reserva','Integrado','Orcamento')
					                                                AND Ped.estabelecimentoId			= '{8}'
					
					                                                AND 
					                                                (
						                                                PedIt.embarque					IS		NULL
						                                                OR PedIt.embarque				=		''
						                                                OR CAST(PedIt.embarque AS INT)	=		0
					                                                )
                                                                    AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
                                                                    {21}
			                                                )
		                                                , 0)
		                                                -
		                                                ISNULL
		                                                (
			                                                (
				                                                SELECT	
						                                                SUM(PedIt.pedidoItemQuantidade) 
				                                                FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
						                                                INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
				                                                WHERE 
						                                                PedIt.itemId						= ITM.itemId
						                                                AND NOT Ped.pedidoSituacao			IN ('Reprovado','FATURADO','Cancelado','Orcamento') -- Faturado já possui NOTA, portanto não é mais alocado
						                                                AND Ped.estabelecimentoId			= '{8}'
						                                                AND 
						                                                (
							                                                NOT PedIt.embarque				IS		NULL
							                                                AND PedIt.embarque				<>		''
							                                                AND CAST(PedIt.embarque AS INT)	>		0
							                                                --AND PedIt.atualizaSaldoEstoque	=		1
						                                                )
                                                                        AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
                                                                        {21}
			                                                )
		                                                , 0)	
		                                                FROM    ksSaldoEstoque SE  WITH(NOLOCK)
                                                        INNER JOIN ksGrupoClienteDeposito DP WITH(NOLOCK) ON
				                                                DP.depositoId = SE.saldoEstoqueDeposito
			                                            INNER JOIN ksCliente C WITH(NOLOCK) ON
				                                                C.grupoComercialId = DP.clienteGrupoComercialId AND
				                                                C.clienteId = {11}
			                                            WHERE   SE.itemId = ITM.itemId AND 
                                                                SE.estabelecimentoId = '{8}' 
                                                                --AND SE.saldoEstoqueDeposito = {9}
			                                            GROUP BY 
                                                                SE.estabelecimentoId, 
                                                                SE.saldoEstoqueDeposito
		                                                ),0)		
		                                                AS Saldo

		                                                ,(CASE
		                                                WHEN  IIF.itemId IS NULL THEN
			                                                'false'
		                                                else	
			                                                'true'
		                                                end
		                                                )AS IsentoFrete
                                                        ,0 as pedidoItemValorDescontoBoleto   

                                              FROM	KsItem ITM WITH(NOLOCK)
                                              --LEFT JOIN ksSaldoEstoque AS SE ON	  (SE.itemId	=	ITM.itemid AND SE.saldoEstoqueDeposito = {9} ) 
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              LEFT JOIN ksFabricante FB WITH(NOLOCK) ON	
                                                        FB.fabricanteId = ITM.fabricanteId
                                              {13}

                                              LEFT JOIN ksFabricante FBR WITH(NOLOCK) ON		                                                
                                                        {16}

                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              LEFT JOIN ksItemIsentoFrete AS IIF WITH(NOLOCK) ON	
                                                        IIF.itemId	=	ITM.itemid    
                                              {18}

                                              WHERE	    {0} {1} {2} {3} {4} {5} {6} {14}

                                                        ITM.itemDescricao LIKE '%" + itemDescricao + "%' "
                                                    + @"GROUP BY ITM.itemId,ITM.unidadeMedidaId,ITM.itemDescricao,
				                                        ITM.itemInfComplementar,ITM.itemPesoBruto,ITM.itemPesoLiquido,
				                                        ITM.familiaComercialId,ITM.familiaMaterialId,ITM.grupoEstoqueId,
				                                        ITM.fabricanteId,ITM.classificacaoFiscalId,ITM.itemRefrigerado,
				                                        ITM.itemControlado,ITM.tipoProdutoId ,ITM.codidentific, 
				                                        UNM.unidadeMedidaSigla,FCM.familiaComercialDescricao
				                                        ,FMT.familiaMaterialDescricao,GES.grupoEstoqueDescricao,FBR.fabricanteNome
                                                        ,FBR.fabricanteNomeAbreviado,CFS.classificacaoFiscalDescricao,ITM.fabricanteId
                                                        ,UNM.unidadeMedidaId,IIF.itemId,FMT.familiaMaterialId,FBR.fabricanteId,ITM.dataImplant,ITM.itemCodEMS
                                                        ,FB.fabricanteNomeAbreviado,FB.fabricanteNome,ITM.itemCodigoOrigem,ITM.itemConv118,ITM.itemIsentoIcms,ITM.AutorizaVacinas,ITM.AutorizaRetinoide ,ITM.BloqMisoprostol
                                                        {15}{19}",
                                                        !String.IsNullOrEmpty(itemId) ? "ITM.itemId  = '" + itemId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(unidadeMedidaId) ? "ITM.unidadeMedidaId = '" + unidadeMedidaId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaComercialId) ? "ITM.familiaComercialId = '" + familiaComercialId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaMaterialId) ? "ITM.familiaMaterialId = '" + familiaMaterialId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(grupoEstoqueId) ? "ITM.grupoEstoqueId = '" + grupoEstoqueId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(fabricanteId) ? "ITM.fabricanteId = '" + fabricanteId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(classificacaoFiscalId) ? "ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "' AND" : string.Empty,
                                                        unidadeNegocioId,
                                                        estabelecimentoId,
                                                        tipoUsuario == "PJP" ? "'LIC'" : "'ALM'",
                                                        clienteIdERP,
                                                        clienteId,
                                                        IsContrato ? _BlocoCampos : string.Empty,
                                                        IsContrato ? _BlocoJoin : string.Empty,
                                                        IsContrato ? _BlocoCondicao : string.Empty,
                                                        IsContrato ? _BlocoGroup : string.Empty,
                                                        IsContrato ? _BlocoJoinFabricante : "FBR.fabricanteId = ITM.fabricanteId",
                                                        !IsContrato ? _descontoCampos : string.Empty,
                                                        !IsContrato ? _descontoJoin : string.Empty,
                                                        !IsContrato ? _descontoGroup : string.Empty,
                                                        IsPedido ? string.Empty : _blocoReserva,
                                                        IsPedido ? _pedidoDiferente : string.Empty,
                                                           ufIdDestino
                                                         // string.Empty 
                                                         );

                #endregion

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

        public DataTable GetItem(bool IsContrato = false)
        {


            return new DataTable();

        }

        public DataTable CheckOut(string Estabelecimento, int ClienteId, string ItemID)
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                //string sSQL = sa.getSelectAllSQL("itemDescricao");
                string sSQL = string.Format(@"SELECT  estabelecimentoId 
                                          ,clienteId
                                          ,itemId
                                          ,Mensagem
                                          FROM Oc_RastroItem  WITH (NOLOCK) where estabelecimentoId='{0}' AND clienteId='{1}' AND itemId='{2}' ", Estabelecimento, clienteId, ItemID);

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
        /// Recupera uma lista de item para carregar um DropDownList
        /// </summary>
        /// <returns></returns>
        public List<Item> ListaDrop()
        {
            List<Item> oLstItem = new List<Item>();

            try
            {
                DataTable oDt = this.Listar();

                if (oDt != null)
                {
                    if (oDt.Rows.Count > 0)
                    {
                        foreach (DataRow row in oDt.Rows)
                        {
                            oLstItem.Add(new Item
                            {
                                itemId = row["itemId"].ToString(),
                                itemDescricao = row["itemDescricao"].ToString()
                            });
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oLstItem;
        }

        /// <summary>
        /// Verifica se o registro já existe na base de dados
        /// </summary>
        /// <returns>bool</returns>
        public bool ExisteRegistro()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Empty;

                sSQL = string.Format("SELECT * FROM KsItem  WITH (NOLOCK) WHERE itemDescricao LIKE '{0}' AND itemId <> '{1}'", Utility.Utility.FormataStringPesquisa(itemDescricao), itemId);

                DataTable dt = da.getDataTable(sSQL, this);

                if (dt.Rows.Count > 0)
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

        /// <summary>
        /// Verifica se o registro já existe na base de dados
        /// </summary>
        /// <returns>bool</returns>
        public bool IdJaInformado()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format("SELECT * FROM KsItem  WITH (NOLOCK) WHERE itemId = '{0}'", itemId);

                DataTable dt = da.getDataTable(sSQL, this);

                if (dt.Rows.Count > 0)
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

        /// <summary>
        /// Inclui os dados informados na base de dados
        /// </summary>
        /// <returns>bool</returns>
        public bool Incluir()
        {
            DataBaseAccess da = new DataBaseAccess();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = sa.getInsertSQL();

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

        /// <summary>
        /// Exclui o registro de departamento
        /// </summary>
        /// <returns>bool</returns>
        public bool Deletar()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = sa.getDeleteSQL();

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

        /// <summary>
        /// Realiza a alteração dos registros de departamento
        /// </summary>
        /// <returns>bool</returns>
        public bool Alterar()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = sa.getUpdateSQL();

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

        /// <summary>
        /// ao tentar integrar pedido verifica se todos os itens tem saldo disponivel
        /// </summary>
        public bool VerificaSaldoItem()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"SELECT SUM(SE.saldoEstoqueQuantidadeAtual)	AS Estoque
                                                FROM ksSaldoEstoque SE   WITH (NOLOCK)
                                                WHERE SE.itemId = '{0}'", itemId);

                DataTable dt = da.getDataTable(sSQL, this);

                 decimal resultado =  decimal.Parse(dt.Rows[0]["Estoque"].ToString());


                if (resultado > 0)
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

        /// <summary>
        /// Recupera os dados do item adicional
        /// </summary>
        /// <returns></returns>
        public DataTable GetItemAdicional()
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL =
                    string.Format(
                        @"
                            SELECT	itemId, 
		                            resolucao13, 
		                            convenio87, 
		                            ICF.classeFiscal

                            FROM	ksItemAdicional ADC  WITH (NOLOCK)
                            
                            LEFT JOIN ksItemAdicionalCF ICF WITH (NOLOCK) ON
                                    ICF.classeFiscalId = ADC.classeFiscal

                            WHERE	itemId = '{0}'
                         ",
                          itemId);

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

        public DataTable listarItensIn(string codigosItens)
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format("SELECT itemId from ksItem  WITH (NOLOCK) where itemId in ({0})", codigosItens);

                DataTable dt = da.getDataTable(sSQL, this);

                if (dt == null)
                    throw new Exception("Falha ao tentar recuperar os dados dos itens!");

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
        /// Utilizado para a tela de pedidos Agrupados
        /// </summary>
        /// <returns></returns>
        //        public DataTable ListarFiltroItemPedidoAgrupado()
        //        {
        //            DataBaseAccess da = new DataBaseAccess();

        //            try
        //            {
        //                if (!da.open())
        //                    throw new Exception(da.LastMessage);



        //                #region :: Query ::

        //                string sSQL = string.Format(@"SET DATEFORMAT DMY
        //                                              SELECT  
        //		                                         ITM.itemId
        //		                                        ,ITM.unidadeMedidaId
        //		                                        ,ITM.itemDescricao
        //		                                        ,ITM.itemInfComplementar
        //		                                        ,ITM.itemPesoBruto
        //		                                        ,ITM.itemPesoLiquido
        //		                                        ,ITM.familiaComercialId
        //		                                        ,ITM.familiaMaterialId
        //		                                        ,ITM.grupoEstoqueId
        //		                                        ,ITM.classificacaoFiscalId
        //		                                        ,ITM.itemRefrigerado
        //		                                        ,ITM.itemControlado
        //		                                        ,ITM.tipoProdutoId
        //		                                        ,ITM.dataImplant
        //		                                        ,ITM.itemCodEMS
        //                                                ,ITM.itemCodigoOrigem
        //												--,ITM.itemConv118
        //												--,ITM.itemIsentoIcms
        //                                                ,ISNULL(ITM.itemConv118,0) AS itemConv118
        //												,ISNULL(ITM.itemIsentoIcms,0) AS itemIsentoIcms
        //		                                        ,UNM.unidadeMedidaId
        //		                                        ,UNM.unidadeMedidaSigla
        //		                                        ,FCM.familiaComercialDescricao
        //		                                        ,FMT.familiaMaterialDescricao
        //		                                        ,GES.grupoEstoqueDescricao
        //		                                        ,FBR.fabricanteNome
        //		                                        ,FBR.fabricanteNomeAbreviado
        //		                                        ,CFS.classificacaoFiscalDescricao
        //		                                        ,FMT.familiaMaterialId
        //		                                        ,FBR.fabricanteId
        //                                                , 0 coeficienteDesconto
        //		                                        --,ISNULL(DSC.coeficienteDesconto, 0) coeficienteDesconto
        //		                                         ,'false' contribuinte
        //		                                        , '' ufIdDestino
        //		                                        ,0 resolucao13
        //		                                        ,'false' convenio87
        //		                                        ,1 classeFiscalId
        //		                                        ,0 classeFiscalACF
        //		                                        ,0 tabelaPrecoVlrMinimo
        //		                                        ,0 tabelaPrecoVlrTabela
        //		                                        ,0 tabelaPrecoVlrMaximo
        //		                                        ,0 tabelaPrecoImportacaoId
        //		                                        ,'true' AS gravado
        //		                                        ,'false' AS informado
        //		                                        ,0 Saldo
        //		                                        ,'false' AS IsentoFrete
        //                                                ,0 as pedidoItemValorDescontoBoleto   
        //
        //                                        FROM KsItem  ITM WITH(NOLOCK)
        //                                        INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
        //			                                        UNM.unidadeMedidaId = ITM.unidadeMedidaId
        //                                        INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
        //			                                        FCM.familiaComercialId = ITM.familiaComercialId
        //                                        INNER JOIN ksFamiliaMaterial FMT WITH(NOLOCK) ON
        //			                                        FMT.familiaMaterialId = ITM.familiaMaterialId
        //                                        INNER JOIN ksGrupoEstoque GES WITH(NOLOCK) ON
        //			                                        GES.grupoEstoqueId = ITM.grupoEstoqueId
        //                                        LEFT JOIN ksFabricante FB WITH(NOLOCK) ON	
        //			                                        FB.fabricanteId = ITM.fabricanteId
        //                                        LEFT JOIN ksFabricante FBR WITH(NOLOCK) ON		                                                
        //			                                        FBR.fabricanteId = ITM.fabricanteId
        //                                        INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
        //			                                        CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
        //                                        LEFT JOIN ksItemIsentoFrete AS IIF WITH(NOLOCK) ON	
        //			                                        IIF.itemId	=	ITM.itemid      
        //                                        --LEFT JOIN ksItemDesconto DSC WITH(NOLOCK) ON
        //			                                     --   DSC.itemId = ITM.itemId 
        //                                        WHERE 1=1 {0} {1} {2} {3} {4} {5} {6}
        //                                        ",
        //                                       !String.IsNullOrEmpty(itemId) ? "AND ITM.itemId = '" + itemId + "'" : string.Empty,
        //                                        !String.IsNullOrEmpty(unidadeMedidaId) ? "AND ITM.unidadeMedidaId = '" + unidadeMedidaId + "'" : string.Empty,
        //                                        !String.IsNullOrEmpty(familiaComercialId) ? "AND ITM.familiaComercialId = '" + familiaComercialId + "'" : string.Empty,
        //                                        !String.IsNullOrEmpty(familiaMaterialId) ? "AND ITM.familiaMaterialId = '" + familiaMaterialId + "'" : string.Empty,
        //                                        !String.IsNullOrEmpty(grupoEstoqueId) ? "AND ITM.grupoEstoqueId = '" + grupoEstoqueId + "'" : string.Empty,
        //                                        !String.IsNullOrEmpty(fabricanteId) ? "AND ITM.fabricanteId = '" + fabricanteId + "'" : string.Empty,
        //                                        !String.IsNullOrEmpty(classificacaoFiscalId) ? "AND ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "'" : string.Empty);

        //                #endregion

        //                DataTable dt = da.getDataTable(sSQL, this);

        //                return dt;
        //            }
        //            catch (Exception ex)
        //            {
        //                throw ex;
        //            }
        //            finally
        //            {
        //                da.close();
        //            }
        //        }

        public DataTable ListarFiltroItemPedidoAgrupado(bool IsContrato = false)
        {
            DataBaseAccess da = new DataBaseAccess();

            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                #region :: Bloco Reserva ::

                string _blocoReserva =
                    string.Format(
                        @"ISNULL
		                    ( 
			                    (
			                    SELECT	
					                    SUM(PedIt.pedidoItemQuantidade) 
			                    FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
					                    INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
			                    WHERE 
					                    PedIt.itemId				=		ITM.itemId
					                    AND Ped.pedidoSituacao	IN		('Reserva') 
					                    AND Ped.estabelecimentoId	= '{0}'
                                        AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
			                    )
		                    , 0)
		                    - ",
                            estabelecimentoId),

                       _pedidoDiferente =
                            string.Format(
                                @"
                                    AND Ped.pedidoId <> {0}
                                 ",
                                 pedidoId);

                #endregion

                #region :: Bloco Desconto ::

                string _descontoJoin =
                    string.Format(
                        @"
                          INNER JOIN ksCliente CLI WITH(NOLOCK) ON
                                    CLI.clienteId = {2}

                          LEFT JOIN ksItemDesconto DSC WITH(NOLOCK) ON
                                DSC.itemId = ITM.itemId
                          AND   DSC.ufIdOrigem = '{0}'
                          AND   ((DSC.ufIdDestino = '{1}') OR (LTRIM(RTRIM(DSC.ufIdDestino)) = ''))                          
                          AND   ((DSC.contribuinte = CLI.clienteContribuinteICMS) OR (CLI.clienteContribuinteICMS IS NULL))

                          LEFT JOIN ksItemAdicional ADC WITH(NOLOCK) ON
                                ADC.itemId = DSC.itemId

                          LEFT JOIN ksItemAdicionalCF ACF WITH(NOLOCK) ON
                                ACF.classeFiscalId = ADC.classeFiscal
                         ",
                          ufIdOrigem,
                          ufIdDestino,
                          clienteId),

                        _descontoCampos =
                            @",ISNULL(DSC.coeficienteDesconto, 0) coeficienteDesconto
                              ,contribuinte
                              ,ISNULL(DSC.ufIdDestino, '') ufIdDestino
                              ,ISNULL(ADC.resolucao13, 0) resolucao13
                              ,ISNULL(ADC.convenio87, 0) convenio87
                              ,ISNULL(ADC.classeFiscal, 1) classeFiscalId
                              ,ACF.classeFiscal AS classeFiscalACF
                             ",
                        _descontoGroup =
                            @",coeficienteDesconto,contribuinte,ufIdDestino,resolucao13
                              ,convenio87,classeFiscalId,ACF.classeFiscal,ADC.classeFiscal";

                #endregion

                #region :: Bloco Contrato ::

                string _BlocoCampos =
                            @",ISNULL(IFRC.precoFabrica, 0) precoFabrica
                              ,IFRC.dataInicio AS precoFabricaValidadeInicio
                              ,IFRC.dataTermino AS precoFabricaValidadeFim
                              ,IFRC.estabelecimentoID
                              ,ISNULL(IFRC.fornecedorID, ITM.fabricanteId) fornecedorID
                              ,EST.estabelecimentoRazaoSocial
							  ,EST.ufId",

                        _BlocoJoin =
                            string.Format(
                                @"LEFT JOIN ksItemFornec IFRC WITH(NOLOCK) ON
                                        IFRC.ItemID = ITM.itemId 
                                  AND   IFRC.estabelecimentoID = '{0}' 
                                  --AND   IFRC.fornecedorID = FBR.fabricanteId

                                 LEFT JOIN ksEstabelecimento EST WITH(NOLOCK) ON
								        EST.estabelecimentoId = IFRC.estabelecimentoID	
                                 ", estabelecimentoId),

                       _BlocoCondicao =
                            string.Format(
                                @"(('{0}' BETWEEN IFRC.dataInicio AND IFRC.dataTermino) OR (IFRC.dataInicio IS NULL)) AND ",
                                  DateTime.Now.Date),

                       _BlocoGroup = @",IFRC.precoFabrica,IFRC.dataInicio,IFRC.dataTermino,IFRC.estabelecimentoID,IFRC.fornecedorID
                                       ,EST.estabelecimentoRazaoSocial,EST.ufId",

                       _BlocoJoinFabricante = @"FBR.fabricanteId = IFRC.fornecedorID";

                #endregion

                #region :: Query ::

                string sSQL = string.Format(@"SET DATEFORMAT DMY
                                              SELECT  top 1   ITM.itemId
                                                        ,ITM.unidadeMedidaId
                                                        ,ITM.itemDescricao
                                                        ,ITM.itemInfComplementar
                                                        ,ITM.itemPesoBruto
                                                        ,ITM.itemPesoLiquido
                                                        ,ITM.familiaComercialId
                                                        ,ITM.familiaMaterialId
                                                        ,ITM.grupoEstoqueId
                                                        ,ITM.classificacaoFiscalId
                                                        ,ITM.itemRefrigerado
                                                        ,ITM.itemControlado
                                                        ,ITM.tipoProdutoId
                                                        ,ITM.dataImplant
                                                        ,ITM.itemCodEMS
                                                        ,ITM.itemCodigoOrigem
                                                        ,ISNULL(ITM.BloqVacinas, 0) AS BloqVacinas
                                                        ,ISNULL(ITM.BloqRetinoide, 0) AS BloqRetinoide
                                                        ,ISNULL(ITM.BloqMisoprostol, 0) AS BloqMisoprostol
                                                         ,ITM.codidentific
                                                        ,ISNULL(ITM.itemConv118,0) AS itemConv118
												        ,ISNULL(ITM.itemIsentoIcms,0) AS itemIsentoIcms                                                        
                                                        ,UNM.unidadeMedidaId                                                         
                                                        ,UNM.unidadeMedidaSigla
                                                        ,FCM.familiaComercialDescricao
		                                                ,FMT.familiaMaterialDescricao
		                                                ,GES.grupoEstoqueDescricao
		                                                ,ISNULL(FBR.fabricanteNome, FB.fabricanteNome) fabricanteNome
                		                                ,ISNULL(FBR.fabricanteNomeAbreviado, FB.fabricanteNomeAbreviado) fabricanteNomeAbreviado
                                                        ,CFS.classificacaoFiscalDescricao
                                                        ,FMT.familiaMaterialId
                                                        ,ISNULL(FBR.fabricanteId, ITM.fabricanteId) fabricanteId
                                                        --,ITM.precoFabrica
                                                        --,ITM.precoFabricaValidadeInicio
                                                        --,ITM.precoFabricaValidadeFim
                                                        {12}
                                                        {17}
                                                        ,
                                                       -- valor minimo 
														0 as tabelaPrecoVlrMinimo,

                                                         -- valor tabela
													0 AS tabelaPrecoVlrTabela,

                                                        -- valor maximo 
														0 AS tabelaPrecoVlrMaximo,
    

                                                        -- retorna codigo da tabela utilizada 

														'' AS tabelaPrecoImportacaoId,

                                                        -------------- 
                                                        'true' AS gravado,
                                                        'false' AS informado,

                                                ISNULL((SELECT 
	                                                SUM(SE.saldoEstoqueQuantidadeAtual) 
		                                                -
		                                                --ISNULL
		                                                --( 
			                                            --    (
			                                            --    SELECT	
					                                    --            SUM(PedIt.pedidoItemQuantidade) 
			                                            --    FROM	KsPedidoItem AS PedIt 
					                                    --            INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
			                                            --    WHERE 
					                                    --            PedIt.itemId				=		ITM.itemId
					                                    --            AND Ped.pedidoSituacao	IN		('Reserva') 
					                                    --            AND Ped.estabelecimentoId	= '{8}'
                                                        --            AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
			                                            --    )
		                                                --, 0)
		                                                ---
                                                        {20}
		                                                ISNULL
		                                                ( 
			                                                (
			                                                SELECT
					                                                SUM(PedIt.pedidoItemQuantidade) 
			                                                FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
					                                                INNER JOIN  ksPedido Ped WITH(NOLOCK) ON  (ped.pedidoId = PedIt.pedidoId)
			                                                WHERE 
					                                                PedIt.itemId						=		ITM.itemId
					                                                AND NOT Ped.pedidoSituacao			IN		('Reprovado','Faturado','Cancelado','Reserva','Integrado','Orcamento')
					                                                AND Ped.estabelecimentoId			= '{8}'
					
					                                                AND 
					                                                (
						                                                PedIt.embarque					IS		NULL
						                                                OR PedIt.embarque				=		''
						                                                OR CAST(PedIt.embarque AS INT)	=		0
					                                                )
                                                                    AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
                                                                    {21}
			                                                )
		                                                , 0)
		                                                -
		                                                ISNULL
		                                                (
			                                                (
				                                                SELECT	
						                                                SUM(PedIt.pedidoItemQuantidade) 
				                                                FROM	KsPedidoItem AS PedIt  WITH(NOLOCK)
						                                                INNER JOIN  ksPedido Ped WITH(NOLOCK) ON (ped.pedidoId =	PedIt.pedidoId)
				                                                WHERE 
						                                                PedIt.itemId						= ITM.itemId
						                                                AND NOT Ped.pedidoSituacao			IN ('Reprovado','FATURADO','Cancelado','Orcamento') -- Faturado já possui NOTA, portanto não é mais alocado
						                                                AND Ped.estabelecimentoId			= '{8}'
						                                                AND 
						                                                (
							                                                NOT PedIt.embarque				IS		NULL
							                                                AND PedIt.embarque				<>		''
							                                                AND CAST(PedIt.embarque AS INT)	>		0
							                                                --AND PedIt.atualizaSaldoEstoque	=		1
						                                                )
                                                                        AND PedIt.pedidoItemDeposito = se.saldoEstoqueDeposito
                                                                        {21}
			                                                )
		                                                , 0)	
		                                                FROM    ksSaldoEstoque SE  WITH(NOLOCK)
                                                        INNER JOIN ksGrupoClienteDeposito DP WITH(NOLOCK) ON
				                                                DP.depositoId = SE.saldoEstoqueDeposito
			                                            INNER JOIN ksCliente C WITH(NOLOCK) ON
				                                                C.grupoComercialId = DP.clienteGrupoComercialId AND
				                                                C.clienteId = {11}
			                                            WHERE   SE.itemId = ITM.itemId AND 
                                                                SE.estabelecimentoId = '{8}' 
                                                                --AND SE.saldoEstoqueDeposito = {9}
			                                            GROUP BY 
                                                                SE.estabelecimentoId, 
                                                                SE.saldoEstoqueDeposito
		                                                ),0)		
		                                                AS Saldo

		                                                ,(CASE
		                                                WHEN  IIF.itemId IS NULL THEN
			                                                'false'
		                                                else	
			                                                'true'
		                                                end
		                                                )AS IsentoFrete
                                                        ,0 as pedidoItemValorDescontoBoleto   

                                              FROM	KsItem ITM WITH(NOLOCK)
                                              --LEFT JOIN ksSaldoEstoque AS SE ON	  (SE.itemId	=	ITM.itemid AND SE.saldoEstoqueDeposito = {9} ) 
                                              INNER JOIN KsUnidadeMedida UNM WITH(NOLOCK) ON
		                                                UNM.unidadeMedidaId = ITM.unidadeMedidaId
                                              INNER JOIN KsFamiliaComercial FCM WITH(NOLOCK) ON
		                                                FCM.familiaComercialId = ITM.familiaComercialId
                                              INNER JOIN ksFamiliaMaterial FMT WITH(NOLOCK) ON
		                                                FMT.familiaMaterialId = ITM.familiaMaterialId
                                              INNER JOIN ksGrupoEstoque GES WITH(NOLOCK) ON
		                                                GES.grupoEstoqueId = ITM.grupoEstoqueId
                                              LEFT JOIN ksFabricante FB WITH(NOLOCK) ON	
                                                        FB.fabricanteId = ITM.fabricanteId
                                              {13}

                                              LEFT JOIN ksFabricante FBR WITH(NOLOCK) ON		                                                
                                                        {16}

                                              INNER JOIN ksClassificacaoFiscal CFS WITH(NOLOCK) ON
		                                                CFS.classificacaoFiscalId = ITM.classificacaoFiscalId
                                              LEFT JOIN ksItemIsentoFrete AS IIF WITH(NOLOCK) ON	
                                                        IIF.itemId	=	ITM.itemid    
                                              {18}

                                              WHERE	    {0} {1} {2} {3} {4} {5} {6} {14}

                                                        ITM.itemDescricao LIKE '%" + itemDescricao + "%' "
                                                    + @"GROUP BY ITM.itemId,ITM.unidadeMedidaId,ITM.itemDescricao,
				                                        ITM.itemInfComplementar,ITM.itemPesoBruto,ITM.itemPesoLiquido,
				                                        ITM.familiaComercialId,ITM.familiaMaterialId,ITM.grupoEstoqueId,
				                                        ITM.fabricanteId,ITM.classificacaoFiscalId,ITM.itemRefrigerado,
				                                        ITM.itemControlado,ITM.tipoProdutoId, ITM.codidentific,
				                                        UNM.unidadeMedidaSigla,FCM.familiaComercialDescricao
				                                        ,FMT.familiaMaterialDescricao,GES.grupoEstoqueDescricao,FBR.fabricanteNome
                                                        ,FBR.fabricanteNomeAbreviado,CFS.classificacaoFiscalDescricao,ITM.fabricanteId
                                                        ,UNM.unidadeMedidaId,IIF.itemId,FMT.familiaMaterialId,FBR.fabricanteId,ITM.dataImplant,ITM.itemCodEMS
                                                        ,FB.fabricanteNomeAbreviado,FB.fabricanteNome,ITM.itemCodigoOrigem,ITM.itemConv118,ITM.itemIsentoIcms,ITM.BloqVacinas,ITM.BloqRetinoide,ITM.BloqMisoprostol 
                                                        {15}{19}",
                                                        !String.IsNullOrEmpty(itemId) ? "ITM.itemId  = '" + itemId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(unidadeMedidaId) ? "ITM.unidadeMedidaId = '" + unidadeMedidaId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaComercialId) ? "ITM.familiaComercialId = '" + familiaComercialId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(familiaMaterialId) ? "ITM.familiaMaterialId = '" + familiaMaterialId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(grupoEstoqueId) ? "ITM.grupoEstoqueId = '" + grupoEstoqueId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(fabricanteId) ? "ITM.fabricanteId = '" + fabricanteId + "' AND" : string.Empty,
                                                        !String.IsNullOrEmpty(classificacaoFiscalId) ? "ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "' AND" : string.Empty,
                                                        unidadeNegocioId,
                                                        estabelecimentoId,
                                                        tipoUsuario == "PJP" ? "'LIC'" : "'ALM'",
                                                        clienteIdERP,
                                                        clienteId,
                                                        IsContrato ? _BlocoCampos : string.Empty,
                                                        IsContrato ? _BlocoJoin : string.Empty,
                                                        IsContrato ? _BlocoCondicao : string.Empty,
                                                        IsContrato ? _BlocoGroup : string.Empty,
                                                        IsContrato ? _BlocoJoinFabricante : "FBR.fabricanteId = ITM.fabricanteId",
                                                        !IsContrato ? _descontoCampos : string.Empty,
                                                        !IsContrato ? _descontoJoin : string.Empty,
                                                        !IsContrato ? _descontoGroup : string.Empty,
                                                        IsPedido ? string.Empty : _blocoReserva,
                                                        IsPedido ? _pedidoDiferente : string.Empty);

                #endregion

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

        #region Métodos Retorno Item

        public DataTable GetItemProc(bool IsContrato = false)
        {

            SqlConnection conn = null;
            SqlCommand comm = null;
            SqlDataAdapter adapter = null;
            DataSet oDs = null;

            try
            {

                #region Monta Parametros dinamicos
                string strunidadeMedidaId = !String.IsNullOrEmpty(unidadeMedidaId) ? " AND ITM.unidadeMedidaId = '" + unidadeMedidaId + "' " : string.Empty;
                string strfamiliaComercialId = !String.IsNullOrEmpty(familiaComercialId) ? " AND ITM.familiaComercialId = '" + familiaComercialId + "' " : string.Empty;
                string strfamiliaMaterialId = !String.IsNullOrEmpty(familiaMaterialId) ? " AND ITM.familiaMaterialId = '" + familiaMaterialId + "' " : string.Empty;
                string strgrupoEstoqueId = !String.IsNullOrEmpty(grupoEstoqueId) ? " AND ITM.grupoEstoqueId = '" + grupoEstoqueId + "' " : string.Empty;
                string strfabricanteId = !String.IsNullOrEmpty(fabricanteId) ? " AND ITM.fabricanteId = '" + fabricanteId + "' " : string.Empty;
                string strclassificacaoFiscalId = !String.IsNullOrEmpty(classificacaoFiscalId) ? " AND ITM.classificacaoFiscalId = '" + classificacaoFiscalId + "' " : string.Empty;


                string Parametros = strunidadeMedidaId +
                                    strfamiliaComercialId +
                                    strfamiliaMaterialId +
                                    strgrupoEstoqueId +
                                    strfabricanteId +
                                    strclassificacaoFiscalId;

                #endregion


                #region :: Bloco de execução ::

                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString))
                {
                    oDs = new DataSet();
                    conn.Open();

                    comm =
                        new SqlCommand
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandText = "dbo.SP_RETURNINFOITEM",
                            Connection = conn
                        };

                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@itemId", itemId).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@PARAMETROS", Parametros).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@CLIENTEID ", clienteId).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@UFORIGEM", ufIdOrigem).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@UFDESTINO", ufIdDestino).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@ISCONTRATO", IsContrato).DbType = DbType.Boolean;
                    adapter = new SqlDataAdapter(comm);
                    //Thread.Sleep(6000);
                    adapter.Fill(oDs);
                }

                #endregion

                #region :: Retorno ::

                if (oDs != null)
                {
                    if (oDs.Tables[0].Rows.Count > 0)
                        return oDs.Tables[0];

                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return new DataTable();
        }

     public DataTable GetTabelaPreco(Item item)
        {
            SqlConnection conn = null;
            SqlCommand comm = null;
            SqlDataAdapter adapter = null;
            DataSet oDs = null;

            try
            {


                #region :: Bloco de execução ::

                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString))
                {
                    oDs = new DataSet();
                    conn.Open();

                    comm =
                        new SqlCommand
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandText = "dbo.SP_RETURN_TABELAPRECO2",
                            Connection = conn
                        };

                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@UNIDADENEGOCIOID", item.unidadeNegocioId).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@UFDESTINO", item.ufIdDestino).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@ESTABELECIMENTOID", item.estabelecimentoId).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@CLIENTEIDERP", item.clienteIdERP).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@itemId", item.itemId).DbType = DbType.String;
                    adapter = new SqlDataAdapter(comm);
                    //Thread.Sleep(6000);
                    adapter.Fill(oDs);
                }

                #endregion

                #region :: Retorno ::

                if (oDs != null)
                {
                    if (oDs.Tables[0].Rows.Count > 0)
                        return oDs.Tables[0];

                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return new DataTable();
        }


        public DataTable GetSaldoEstoque(Item item)
        {


            SqlConnection conn = null;
            SqlCommand comm = null;
            SqlDataAdapter adapter = null;
            DataSet oDs = null;

            try
            {


                #region :: Bloco de execução ::

                using (conn = new SqlConnection(ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString))
                {
                    oDs = new DataSet();
                    conn.Open();

                    comm =
                        new SqlCommand
                        {
                            CommandType = CommandType.StoredProcedure,
                            CommandText = "dbo.SP_RETURN_SALDOESTOQUE",
                            Connection = conn
                        };

                    comm.Parameters.Clear();
                    comm.Parameters.AddWithValue("@CLIENTEID", item.clienteId).DbType = DbType.Int32;
                    comm.Parameters.AddWithValue("@ESTABELECIMENTOID", item.estabelecimentoId).DbType = DbType.String;
                    comm.Parameters.AddWithValue("@ITEMID", item.itemId).DbType = DbType.String;
                    adapter = new SqlDataAdapter(comm);
                    //Thread.Sleep(6000);
                    adapter.Fill(oDs);
                }

                #endregion

                #region :: Retorno ::

                if (oDs != null)
                {
                    if (oDs.Tables[0].Rows.Count > 0)
                        return oDs.Tables[0];

                }

                #endregion
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);

            }
            finally
            {
                conn.Close();
            }

            return new DataTable();

        }


        #endregion



        #endregion


        public Item ItemPorIdOuDescricao()
        {
            DataBaseAccess da = new DataBaseAccess();
            Item itens = new Item();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select distinct 
                        a.itemId, 
                        a.itemDescricao, 
                        b.fabricanteNomeAbreviado,
                        a.itemConv118, 
                        c.PrFabrica, c.ufDestino
                        from KsItem (nolock) as a 
                        inner join ksFabricante (nolock) as b on a.fabricanteId = b.fabricanteId
                        inner join KsPrecoCmed  (nolock) c on a.itemInfComplementar = c.EAN    where 1=1 {0} {1} {2}",
                     !String.IsNullOrEmpty(itemId) ? "AND a.itemId  LIKE '%" + itemId + "%' " : string.Empty,
                     !String.IsNullOrEmpty(itemDescricao) ? "AND itemDescricao  LIKE '%" + itemDescricao + "%' " : string.Empty,
                     !String.IsNullOrEmpty(ufIdDestino) ? "AND UfDestino  = '" + ufIdDestino + "'" : string.Empty);

                foreach (var item in da.getDataTable(sSQL, this).AsEnumerable())
                {
                    itens = new Item()
                    {
                        itemId = item.ItemArray[0].ToString(),
                        itemDescricao = item.ItemArray[1].ToString(),
                        fabricanteNomeAbreviado = item.ItemArray[2].ToString(),
                        itemConv118 = Convert.ToBoolean(item.ItemArray[3]),
                        tabelaPrecoVlrTabela =  decimal.Parse(item.ItemArray[4].ToString())
                    };

                }



                if (itens == null)
                    throw new Exception("Falha ao tentar recuperar os dados dos itens!");

                return itens;
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

        public List<Item> listarItensPorIdOuDescricao()
        {
            DataBaseAccess da = new DataBaseAccess();
            List<Item> list = new List<Item>();
            Item itens = new Item();
            try
            {
                if (!da.open())
                    throw new Exception(da.LastMessage);

                string sSQL = string.Format(@"select distinct 
                        a.itemId, 
                        a.itemDescricao, 
                        b.fabricanteNomeAbreviado,
                        a.itemConv118, 
                        c.PrFabrica, c.ufDestino
                        from KsItem (nolock) as a 
                        inner join ksFabricante (nolock) as b on a.fabricanteId = b.fabricanteId
                        inner join KsPrecoCmed  (nolock) c on a.itemInfComplementar = c.EAN    where 1=1 {0} {1} {2}",
                     !String.IsNullOrEmpty(itemId) ? "AND a.itemId  LIKE '%" + itemId + "%' " : string.Empty,
                     !String.IsNullOrEmpty(itemDescricao) ? "AND itemDescricao  LIKE '%" + itemDescricao + "%' " : string.Empty,
                     !String.IsNullOrEmpty(ufIdDestino) ? "AND UfDestino  = '" + ufIdDestino + "'" : string.Empty);

                foreach (var item in da.getDataTable(sSQL, this).AsEnumerable())
                {
                    itens = new Item()
                    {
                        itemId = item.ItemArray[0].ToString(),
                        itemDescricao = item.ItemArray[1].ToString(),
                        fabricanteNomeAbreviado = item.ItemArray[2].ToString(),
                        itemConv118 = !string.IsNullOrEmpty(item.ItemArray[3].ToString()) ? Convert.ToBoolean(item.ItemArray[3].ToString()) : false,
                        tabelaPrecoVlrTabela = !string.IsNullOrEmpty(item.ItemArray[4].ToString()) ? decimal.Parse(item.ItemArray[4].ToString()) : 0
                    };
                    list.Add(itens);

                }



                if (list == null)
                    throw new Exception("Falha ao tentar recuperar os dados dos itens!");

                return list;
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
