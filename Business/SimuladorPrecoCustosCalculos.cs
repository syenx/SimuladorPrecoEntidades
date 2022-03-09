using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace KS.SimuladorPrecos.DataEntities.Business
{
    public class SimuladorPrecoCustosCalculos
    {

        public string GetCalculoById(string find, DataListItemEventArgs e, CalculoSobreVenda _calculoSobreVenda, CalculoDeCustoPadrao _calculoDeCustoPadrao)
        {
            switch (find)
            {

                case "ltrMargemVlrValor":
                    return string.Format("{0:n2}",
                          _calculoSobreVenda.GetValorMagem(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                                DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                                DataBinder.Eval(e.Item.DataItem, "tipo").ToString(), _calculoSobreVenda.rblPerfilCliente,
                                                                DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                                DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                            _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                   decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString()),
                                                                DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                                DataBinder.Eval(e.Item.DataItem, "exclusivoHospitalar").ToString(),
                                                                DataBinder.Eval(e.Item.DataItem, "listaDescricao").ToString(),
                                                                DataBinder.Eval(e.Item.DataItem, "categoria").ToString(),
                                                      int.Parse(DataBinder.Eval(e.Item.DataItem, "itemCodigoOrigem").ToString()),
                                                                DataBinder.Eval(e.Item.DataItem, "tratamentoICMSEstab").ToString()));


                case "ltrCustoPadraoVendaValor":
                    return string.Format("{0:n2}",
                                            _calculoDeCustoPadrao.GetValorCustoPadrao(
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                                              DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                                         _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString()),
                                                                              DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                                              DataBinder.Eval(e.Item.DataItem, "tipo").ToString(),
                                                                    int.Parse(DataBinder.Eval(e.Item.DataItem, "itemCodigoOrigem").ToString()),
                                                                              DataBinder.Eval(e.Item.DataItem, "tratamentoICMSEstab").ToString(),
                                                                              DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                                              DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString()));


                case "ltrPrecoVendaLiquidoValor":
                    return string.Format("{0:n2}",
                                               _calculoSobreVenda.GetPrecoVendaLiquido(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                                                               DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                                                               DataBinder.Eval(e.Item.DataItem, "tipo").ToString(), _calculoDeCustoPadrao.rblPerfilCliente,
                                                                                               DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                                                               DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                                                        _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                                                               DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                                                               DataBinder.Eval(e.Item.DataItem, "exclusivoHospitalar").ToString(),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                                                  decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString()),
                                                                                               DataBinder.Eval(e.Item.DataItem, "listaDescricao").ToString(),
                                                                                               DataBinder.Eval(e.Item.DataItem, "categoria").ToString()));

                case "ltrPISCofinsSobreVendaValor":
                    return string.Format("{0:n2}",
                 _calculoSobreVenda.GetPISCofinsSobreVenda(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString()),
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString())));

                case "ltrAjusteRegimeFiscalValor":
                    return string.Format("{0:n2}",
               _calculoSobreVenda.GetAjusteRegimeFiscalSobreVenda(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString()),
                                                                       DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                                       DataBinder.Eval(e.Item.DataItem, "tipo").ToString(), _calculoSobreVenda.rblPerfilCliente,
                                                                       DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                                       DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                                   _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                                       DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                                       DataBinder.Eval(e.Item.DataItem, "exclusivoHospitalar").ToString(),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString())
                                                                    ));
                case "ltrICMSSTSobreVendaValor":
                    return string.Format("{0:n2}",
                             _calculoSobreVenda.GetICMSSTSobreVenda(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString()),
                                                                          DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                                          DataBinder.Eval(e.Item.DataItem, "tipo").ToString(),
                                                                            _calculoSobreVenda.rblPerfilCliente,
                                                                          DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                                                          DataBinder.Eval(e.Item.DataItem, "listaDescricao").ToString(),
                                                                          DataBinder.Eval(e.Item.DataItem, "categoria").ToString(),
                                                                          DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                                          DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                                 _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString()),
                                                                          DataBinder.Eval(e.Item.DataItem, "exclusivoHospitalar").ToString(),
                                                    false,
                                                    null, true
                                                    ));

                case "ltrICMSSobreVendaValor":
                    return string.Format("{0:n2}",
                                    _calculoSobreVenda.GetICMSSobreVenda(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString()),
                                        DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                        DataBinder.Eval(e.Item.DataItem, "tipo").ToString(),
                                   _calculoSobreVenda.rblPerfilCliente,
                                        DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                        DataBinder.Eval(e.Item.DataItem, "exclusivoHospitalar").ToString(),
                                        DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                            _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString())
                                                        ));
                case "ltrPrecoVendaValor":
                    return string.Format("{0:n2}", _calculoSobreVenda.GetPrecoVenda(_calculoSobreVenda.GetPrecoVendaDesconto(DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString())));

                case "ltrCustoPadraoValor":
                    return string.Format("{0:n2}",
                            _calculoDeCustoPadrao.GetValorCustoPadrao(
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                              DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                         _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString()),
                                                              DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                              DataBinder.Eval(e.Item.DataItem, "tipo").ToString(),
                                                    int.Parse(DataBinder.Eval(e.Item.DataItem, "itemCodigoOrigem").ToString()),
                                                              DataBinder.Eval(e.Item.DataItem, "tratamentoICMSEstab").ToString(),
                                                              DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                              DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString()
                                                ));

                case "ltrAliquotaICMSValor":
                    return string.Format("{0:n2}",
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()) < 1 ?
                                           ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()) * 100) :
                                                          DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS"));
                case "ltrICMSSTValor":
                    return string.Format("{0:#.00}",
                           _calculoDeCustoPadrao.GetValorICMS_ST(DataBinder.Eval(e.Item.DataItem, "descST").ToString(),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "aliquotaInternaICMS").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                        _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "valorICMSST").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "reducaoST_MVA").ToString())  
                                                ));

                case "ltrMVAValor":
                    return string.Format("{0:n2}%",
                                         decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()) < 1 ?
                                        ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()) * 100) :
                                         decimal.Parse(DataBinder.Eval(e.Item.DataItem, "mva").ToString()) * 100);
                case "ltrDescParaSTValor":
                    return DataBinder.Eval(e.Item.DataItem, "descST").ToString().Equals("-") ?
                                            DataBinder.Eval(e.Item.DataItem, "descST").ToString() :
                                            string.Format("{0:n2}%",
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descST").ToString()) < 1 ?
                                                           ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descST").ToString()) * 100) :
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descST").ToString()));

                case "ltrPMCValor":
                    return string.Format("{0:n2}",
                                     decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()) < 1 ?
                                   ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()) * 100) :
                                     decimal.Parse(DataBinder.Eval(e.Item.DataItem, "pmc17").ToString()
                                ));

                case "ltrPMC":
                    return string.Format("PMC {0:f0}%",
                         decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPmc").ToString()) < 1 ?
                       ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPmc").ToString()) * 100) :
                         decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPmc").ToString()));

                case "ltrPISCofinsVlrValor":
                    return string.Format("{0:n2}",
                                  _calculoDeCustoPadrao.GetValorPISCofins( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()),
                                                       decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                                _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                                       decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                                       decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString())
                                                        ));
                case "ltrPISCofinsPrcValor":
                    return string.Format("{0:n2}%",
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()) < 1 ?
                                           ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()) * 100) :
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percPisCofins").ToString()));
                case "ltrIPIVlrValor":
                    return string.Format("{0:n2}",
                            _calculoDeCustoPadrao.GetValorIPI( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()),
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                      _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString())
                                            ));


                case "ltrIPIPrcValor":
                    return string.Format("{0:n2}%",
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()) < 1 ?
                                               ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()) * 100) :
                                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percIPI").ToString()
                                            ));


                case "ltrCreditoICMSValor":
                    return string.Format("{0:n2}",
                 _calculoDeCustoPadrao.GetCreditoICMS( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()),
                                     decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                     decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                                     decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                                     decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()),
                                        int.Parse(DataBinder.Eval(e.Item.DataItem, "itemCodigoOrigem").ToString()),
                                                    DataBinder.Eval(e.Item.DataItem, "tratamentoICMSEstab").ToString(),
                                                    DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                                    DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                                    DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                                    DataBinder.Eval(e.Item.DataItem, "tipo").ToString()
                                    ));
                case "ltrICMSeValor":
                    return string.Format("{0:n2}%",
                    _calculoDeCustoPadrao.GetPercICMSe( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percICMSe").ToString()),
                                    DataBinder.Eval(e.Item.DataItem, "estabelecimentoId").ToString(),
                                    DataBinder.Eval(e.Item.DataItem, "ufIdOrigem").ToString(),
                                    DataBinder.Eval(e.Item.DataItem, "resolucao13").ToString(),
                                    DataBinder.Eval(e.Item.DataItem, "tipo").ToString()));

                case "ltrReducaoBaseValor":
                    return string.Format("{0:n2}%",
                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()) < 1 ?
                                ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()) * 100) :
                                 decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percReducaoBase").ToString()));

                case "ltrPrecoAquisicaoValor":
                    return string.Format("{0:n2}%",
                  _calculoDeCustoPadrao.GetPrecoAquisicao( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "precoFabrica").ToString()),
                       _calculoDeCustoPadrao.GetDescontoComercial( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())),
                         decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()),
                         decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString())
                    ).ToString());
                case "ltrRepasseValor":
                    return string.Format("{0:n2}%",
                        ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()) < 1 ?
                        ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString()) * 100) :
                          decimal.Parse(DataBinder.Eval(e.Item.DataItem, "percRepasse").ToString())).ToString());

                case "ltrDescontoAdicionalValor":
                    return string.Format("{0:n2}%",
                      _calculoDeCustoPadrao.GetDescontoAdicional( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()) < 1 ?
                                (_calculoDeCustoPadrao.GetDescontoAdicional( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString())) * 100) :
                                _calculoDeCustoPadrao.GetDescontoAdicional( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoAdicional").ToString()))).ToString());

                case "ltrDescontoComercialValor":
                    return string.Format("{0:n2}%",_calculoDeCustoPadrao.GetDescontoComercial
                                                            (
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString()) < 1 ?
                                                            ( decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString()) * 100) :
                                                             decimal.Parse(DataBinder.Eval(e.Item.DataItem, "descontoComercial").ToString())
                                                            ));

                case "":




                default:
                    return "";

            }
        }
    }
}
