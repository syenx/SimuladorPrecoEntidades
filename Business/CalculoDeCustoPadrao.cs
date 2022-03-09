using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI;

namespace KS.SimuladorPrecos.DataEntities.Business
{
    public class CalculoDeCustoPadrao 
    {
        public string txtDescontoAdicional { get; set; }
        public bool IsSetorPublico { get; set; }
        public bool CapAplicado { get; set; }
        public string txtCapAplicado { get; set; }
        public string ddlEstadoDestino { get; set; }
        public string rblPerfilCliente { get; set; }

        public bool IsContribuinte { get; set; }

        /// <summary>
        /// Recupera o valor do desconto comercial
        /// </summary>
        /// <param name="_descontoComercial">Valor do desconto comercial</param>
        /// <returns></returns>
        public  decimal GetDescontoComercial( decimal _descontoComercial)
        {
            try
            {
                 decimal _descontoInformado =
                    !String.IsNullOrEmpty(this.txtDescontoAdicional) ?
                         decimal.Parse(this.txtDescontoAdicional) :
                            new  decimal();
                //return ((_descontoComercial > 1 ? _descontoComercial : (_descontoComercial + _descontoInformado)));
                return ((_descontoComercial > 1 ? _descontoComercial : (_descontoComercial * 100)) + _descontoInformado);
            }
            catch (Exception ex)
            {
                Utility.Utility. WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Realiza o cálculo dos descontos adicionais
        /// </summary>
        /// <param name="_descontoAdicional">Valor recuperado na carga</param>
        /// <returns>Valor formatado</returns>
        public  decimal GetDescontoAdicional( decimal _descontoAdicional)
        {
            try
            {
                 decimal _descontoCap = new  decimal();

                if (IsSetorPublico)
                    _descontoCap =
                        !CapAplicado ? new  decimal() :
                            !String.IsNullOrEmpty(this.txtCapAplicado) ?
                                 decimal.Parse(this.txtCapAplicado) : new  decimal();

                return ((_descontoAdicional > 1 ? _descontoAdicional : (_descontoAdicional * 100)) + ((_descontoCap > 1 ? _descontoCap : (_descontoCap * 100))));
                //return ((_descontoAdicional > 1 ? _descontoAdicional : (_descontoAdicional * 100)) + (_descontoCap / 100));
            }
            catch (Exception ex)
            {
                Utility.Utility. WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Realiza o cálculo do preço de aquisição
        /// </summary>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto adicional</param>
        /// <param name="_repasse">Valor do repasse</param>
        /// <returns>Cálculo com o valor formatado</returns>
        public  decimal GetPrecoAquisicao( decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse)
        {
            try
            {
                return (_precoFabrica * (1 - (_descontoComercial > 1 ? (_descontoComercial / 100) : _descontoComercial)) * (1 - (GetDescontoAdicional(_descontoAdicional) / 100)) * (1 - (_repasse > 1 ? (_repasse / 100) : _repasse)));
            }
            catch (Exception ex)
            {
                Utility.Utility. WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o valor do crédito de ICMS
        /// </summary>
        /// <param name="_reducaoBase">Redução base</param>
        /// <param name="_ICMSe">ICMSe</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto adicional</param>
        /// <param name="_repasse">Repasse</param>
        /// <returns>Cálculo formato</returns>
        //public  decimal GetCreditoICMS( decimal _reducaoBase,  decimal _ICMSe,  decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse)
        //{
        //    try
        //    {
        //        return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

        //    }
        //    catch (Exception ex)
        //    {
        //        Utility.WriteLog(ex);
        //        return 0;
        //    }
        //}

        /// <summary>
        /// Calcula o valor do crédito de ICMS
        /// </summary>
        /// <param name="_reducaoBase">Redução base</param>
        /// <param name="_ICMSe">ICMSe</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto adicional</param>
        /// <param name="_repasse">Repasse</param>
        /// <returns>Cálculo formato</returns>
        public  decimal GetCreditoICMS( decimal _reducaoBase,  decimal _ICMSe,  decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse, int CodigoOrigem, string tratamentoICMSEstab, string _estabelecimento, string UFOrigem, string _resolucao, string ConvenioId)
        {
            try
            {
                if (_estabelecimento == "12" || _estabelecimento == "13")
                {
                    if (this.ddlEstadoDestino.ToUpper().Equals("ES"))
                    {

                        _resolucao = RemoveSpecialCharactersAccents(_resolucao);

                        bool regiaoSUL = false;
                        bool regiaoNO = false;
                        switch (UFOrigem)
                        {
                            case "RS":
                            case "SC":
                            case "PR":
                            case "SP":
                            case "RJ":
                            case "ES":
                            case "MG":
                                regiaoSUL = true;
                                break;
                            default:
                                regiaoNO = true;
                                break;
                        }

                        if (tratamentoICMSEstab.Equals("Sem ICMS"))
                        {
                            return new  decimal();
                        }

                        if (this.ddlEstadoDestino.ToUpper().Equals(UFOrigem))
                        {

                            _ICMSe = 17;
                            return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                        }

                        else if (_resolucao.Equals("SIM") && regiaoNO)
                        {
                            _ICMSe = 4;
                            return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                        }
                        else if (_resolucao.Equals("NAO") && regiaoNO)
                        {
                            _ICMSe = 12;
                            return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                        }

                        else if (_resolucao.Equals("SIM") && regiaoSUL)
                        {
                            _ICMSe = 4;
                            return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                        }
                        else if (_resolucao.Equals("NAO") && regiaoSUL)
                        {
                            _ICMSe = 7;
                            return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                        }

                    }
                }
                else
                {

                    if (_estabelecimento == "20")
                    {
                        if (this.ddlEstadoDestino.ToUpper().Equals("DF"))
                        {

                            if (ConvenioId.ToUpper().Equals("REGIME NORMAL"))
                            {

                                _ICMSe = 12;
                                return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                            }
                            else
                            {
                                return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                            }
                        }
                        else if (!this.ddlEstadoDestino.ToUpper().Equals("DF"))
                        {



                            // 0 - Mercado Público                            1 - Mercado Privado Não Contribuinte
                            if (rblPerfilCliente.Equals("0") || rblPerfilCliente.Equals("1") || rblPerfilCliente.Equals("2") || rblPerfilCliente.Equals("C"))
                            {

                                if (ConvenioId.Equals("Convênio 118") || ConvenioId.Equals("Convênio 57"))
                                {
                                    _ICMSe = 0;
                                    return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                                }
                                else
                                {
                                    _ICMSe = 12;
                                    return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                                }


                                //if (_resolucao.Equals("SIM"))
                                //{

                                //    _ICMSe = 12;
                                //    return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                                //}
                                //else
                                //{
                                //    return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                                //    //_ICMSe = 4;
                                //    //return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

                                //}

                            }
                            else
                            {
                                return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                                //_ICMSe = 4;
                                //return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                            }

                        }
                        else
                        {
                            return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                            //_ICMSe = 4;
                            //return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                        }


                    }
                    else
                    {
                        return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));
                    }

                }
                return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (1 - (_reducaoBase > 1 ? (_reducaoBase / 100) : _reducaoBase)) * (_ICMSe > 1 ? (_ICMSe / 100) : _ICMSe));

            }
            catch (Exception ex)
            {
                Utility.Utility. WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o valor do IPI
        /// </summary>
        /// <param name="_percIPI">Percentual IPI</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto Adicional</param>
        /// <param name="_repasse">Valor do repasse</param>
        /// <returns>Cálculo formatado</returns>
        public  decimal GetValorIPI( decimal _percIPI,  decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse)
        {
            try
            {
                return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (_percIPI > 1 ? (_percIPI / 100) : _percIPI));
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o valor do PIS/Cofins
        /// </summary>
        /// <param name="_percPISCofins">Percentual do PIS/Cofins</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto adicional</param>
        /// <param name="_repasse">Valor do repase</param>
        /// <returns>Cálculo formatado</returns>
        public  decimal GetValorPISCofins( decimal _percPISCofins,  decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse)
        {
            try
            {
                return (GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) * (_percPISCofins > 1 ? (_percPISCofins / 100) : _percPISCofins));
            }
            catch (Exception ex)
            {
                Utility.Utility. WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calculo do valor do ICMS-ST
        /// </summary>
        /// <param name="_descST">Descrição ST</param>
        /// <param name="_mva">Valor MVA</param>
        /// <param name="_aliquotaIntICMS">Aliquota do ICMS</param>
        /// <param name="_pmc17">PMC</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto Adicional</param>
        /// <param name="_repasse">Valor do repasse</param>
        /// <param name="_valorICMSST">Valor do IcmsSt</param>
        /// <returns>Cálculo formatado</returns>
        public   decimal GetValorICMS_ST(string _descST,  decimal _mva,  decimal _aliquotaIntICMS,  decimal _pmc17,  decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse,  decimal _valorICMSST,  decimal _percIPI,  decimal _reducaoST_MVA)
        {
            try
            {
                if (_descST.Trim().Equals("-") && _mva.Equals(0))
                    return new  decimal();
                else if (_descST.Trim().Equals("-") && !(_mva.Equals(0)))
                    return ((GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse) + GetValorIPI(_percIPI, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse)) * (1 + (_mva > 1 ? (_mva / 100) : _mva)) * (1 - (_reducaoST_MVA > 1 ? (_reducaoST_MVA / 100) : _reducaoST_MVA)) * (_aliquotaIntICMS > 1 ? (_aliquotaIntICMS / 100) : _aliquotaIntICMS));
                else
                    return (_pmc17 * (1 - ( decimal.Parse(_descST) > 1 ? ( decimal.Parse(_descST) / 100) :  decimal.Parse(_descST))) * (_aliquotaIntICMS > 1 ? (_aliquotaIntICMS / 100) : _aliquotaIntICMS));
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o valor do custo padrão
        /// </summary>
        /// <param name="_reducaoBase">Redução base</param>
        /// <param name="_ICMSe">ICMSe</param>
        /// <param name="_percIPI">Percentual IPI</param>
        /// <param name="_percPISCofins">Percentual PIS/Cofins</param>
        /// <param name="_descST">DescST</param>
        /// <param name="_mva">MVA</param>
        /// <param name="_aliquotaIntICMS">Alíquota ICMS</param>
        /// <param name="_pmc17">PMC</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto Adicional</param>
        /// <param name="_repasse">Valor repasse</param>
        /// <param name="_valorICMSST">Valor IcmsSt</param>
        /// <param name="_reducaoST_MVA"></param>
        /// <param name="_estabelecimento"></param>
        /// <param name="_convenio"></param>
        /// <returns>Cálculo formatado</returns>
        public  decimal GetValorCustoPadrao( decimal _reducaoBase,  decimal _ICMSe,  decimal _percIPI,  decimal _percPISCofins, string _descST,  decimal _mva,  decimal _aliquotaIntICMS,  decimal _pmc17,  decimal _precoFabrica,  decimal _descontoComercial,  decimal _descontoAdicional,  decimal _repasse,  decimal _valorICMSST,  decimal _reducaoST_MVA, string _estabelecimento, string _convenioId, int CodigoOrigem, string tratamentoICMSEstab, string uforigem, string resolucao)
        {
            try
            {
                #region :: Aplicação da Regra ::

                string _convenio = GetEquality(_convenioId.Trim(), "CONVÊNIO 118", "CONVÊNIO 57");

                switch (_convenio.ToUpper())
                {
                    case "CONVÊNIO 118":

                        if (_estabelecimento.Equals("20"))
                        {
                            if (!this.ddlEstadoDestino.ToUpper().Equals("DF"))
                                if (!IsContribuinte)
                                    _ICMSe = _ICMSe.Equals(0) ? new  decimal() : Utility.Utility. VLR_CUSTOPADRAOICMSE;
                            //_ICMSe = Utility.VLR_CUSTOPADRAOICMSE;
                        }

                        break;

                    default:

                        if (_convenio.ToUpper().Equals("CONVÊNIO 57"))
                            break;

                        //if (_estabelecimento.Equals("20"))
                        //{
                        //    if (this.ddlEstadoDestino.SelectedValue.ToUpper().Equals("DF"))
                        //    {
                        //        _ICMSe = Utility.VLR_CUSTOPADRAOICMSE;


                        //    }
                        //    else
                        //    {
                        //        if (!IsContribuinte)
                        //            _ICMSe = Utility.VLR_CUSTOPADRAOICMSE;
                        //    }
                        //}

                        break;
                }


                #endregion

                if (_estabelecimento == "12" || _estabelecimento == "13")
                {
                    if (this.ddlEstadoDestino.ToUpper().Equals("ES"))
                    {
                        resolucao = RemoveSpecialCharactersAccents(resolucao);
                        return (
                               GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse)
                                   -
                               GetCreditoICMS(_reducaoBase, _ICMSe, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, CodigoOrigem, tratamentoICMSEstab, _estabelecimento, uforigem, resolucao, _convenioId)
                                   +
                               GetValorIPI(_percIPI, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse)
                                   -
                               GetValorPISCofins(_percPISCofins, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse)
                                   +
                               GetValorICMS_ST(_descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                              );
                        //-
                        //GetCreditoICMS(_reducaoBase,
                        //              Utility.VLR_CUSTOPADRAOICMSE,
                        //              _precoFabrica,
                        //              GetDescontoComercial(_descontoComercial),
                        //              _descontoAdicional,
                        //              _repasse,
                        //              CodigoOrigem,
                        //              tratamentoICMSEstab,
                        //              _estabelecimento,
                        //              uforigem, resolucao
                        //              );

                    }
                }






                return (
                        GetPrecoAquisicao(_precoFabrica, _descontoComercial, _descontoAdicional, _repasse)
                            -
                        GetCreditoICMS(_reducaoBase, _ICMSe, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, CodigoOrigem, tratamentoICMSEstab, _estabelecimento, uforigem, resolucao, _convenioId)
                            +
                        GetValorIPI(_percIPI, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse)
                            -
                        GetValorPISCofins(_percPISCofins, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse)
                            +
                        GetValorICMS_ST(_descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                       );

                #region :: Definição das Regras de cálculo ::

                /*
                 * 
                 * Convênio 57
                 *  - Normal
                 *  
                 * Convênio 118
                 *  - Loja <> 20
                 *   - Normal
                 *                 
                 * Convênio 118
                 *  - Loja 20
                 *   - Destino <> DF
                 *    - Não Contribuinte
                 *     - Se tiver ICMS
                 *      - ICMSe Sempre 12%
                 *
                 * * Convênio 118
                 *  - Loja 20
                 *   - Destino <> DF
                 *    - Não Contribuinte
                 *     - Se NÃO tiver ICMS
                 *      - 0
                 * 
                 * Convênio 118
                 *  - Loja 20
                 *   - Destino <> DF
                 *    - Contribuinte
                 *     - Normal
                 *     
                 * Convênio 118
                 *  - Loja 20
                 *   - Destino = DF
                 *    - Normal
                 *
                 * Default
                 *  - Loja <> 20
                 *   - Normal
                 *  
                 * Default
                 *  - Loja 20
                 *   - Destino = DF
                 *    - ICMSe Sempre 12%
                 *    
                 * Default
                 *  - Loja 20
                 *   - Destino <> DF
                 *    - Não Contribuinte
                 *     - ICMSe Sempre 12%
                 *     
                 * Default
                 *  - Loja 20
                 *   - Destino <> DF
                 *    - Contribuinte
                 *     - Normal
                 */

                #endregion
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        public  decimal GetPercICMSe( decimal percICMSe, string estabelecimento, string UFOrigem, string _resolucao, string ConvenioId)
        {
            if (estabelecimento == "12" || estabelecimento == "13")
            {
                if (this.ddlEstadoDestino.ToUpper().Equals("ES"))
                {
                    _resolucao = RemoveSpecialCharactersAccents(_resolucao);
                    bool regiaoSUL = false;
                    bool regiaoNO = false;
                    switch (UFOrigem)
                    {
                        case "RS":
                        case "SC":
                        case "PR":
                        case "SP":
                        case "RJ":
                        case "ES":
                        case "MG":
                            regiaoSUL = true;
                            break;
                        default:
                            regiaoNO = true;
                            break;
                    }


                    if (this.ddlEstadoDestino.ToUpper().Equals(UFOrigem))
                    {

                        percICMSe = 17;
                        return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                    }

                    else if (_resolucao.Equals("SIM") && regiaoNO)
                    {
                        percICMSe = 4;
                        return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                    }
                    else if (_resolucao.Equals("NAO") && regiaoNO)
                    {
                        percICMSe = 12;
                        return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                    }

                    else if (_resolucao.Equals("SIM") && regiaoSUL)
                    {
                        percICMSe = 4;
                        return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                    }
                    else if (_resolucao.Equals("NAO") && regiaoSUL)
                    {
                        percICMSe = 7;
                        return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                    }

                }
            }
            else
            {


                if (estabelecimento == "20")
                {
                    if (this.ddlEstadoDestino.ToUpper().Equals("DF"))
                    {
                        if (ConvenioId.ToUpper().Equals("REGIME NORMAL"))
                        {
                            percICMSe = 12;
                            return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                        }
                        else
                        {

                            return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                        }
                    }
                    else if (!this.ddlEstadoDestino.ToUpper().Equals("DF"))
                    {



                        // 0 - Mercado Público                            1 - Mercado Privado Não Contribuinte
                        if (rblPerfilCliente.Equals("0") || rblPerfilCliente.Equals("1") || rblPerfilCliente.Equals("2") || rblPerfilCliente.Equals("C"))
                        {

                            if (ConvenioId.Equals("Convênio 118") || ConvenioId.Equals("Convênio 57"))
                            {
                                percICMSe = 0;
                                return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                            }
                            else
                            {
                                percICMSe = 12;
                                return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                            }

                            //if (_resolucao.Equals("SIM"))
                            //{
                            //percICMSe = 12;
                            //return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                            //}
                            //else
                            //{
                            //    return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                            //    //percICMSe = 4;
                            //    //return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

                            //}

                        }
                        else
                        {
                            return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                            //percICMSe = 4;
                            //return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                        }

                    }
                    else
                    {
                        return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                        //percICMSe = 4;
                        //return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                    }


                }
                else
                {
                    return percICMSe < 1 ? (percICMSe * 100) : percICMSe;
                }
            }

            return percICMSe < 1 ? (percICMSe * 100) : percICMSe;

        }




        public static string RemoveSpecialCharactersAccents(string texto, bool allowSpace = true)
        {

            string ret;

            if (allowSpace)
                ret = System.Text.RegularExpressions.Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ\s,]+?", string.Empty);
            else
                ret = System.Text.RegularExpressions.Regex.Replace(texto, @"[^0-9a-zA-ZéúíóáÉÚÍÓÁèùìòàÈÙÌÒÀõãñÕÃÑêûîôâÊÛÎÔÂëÿüïöäËYÜÏÖÄçÇ]+?", string.Empty);


            if (string.IsNullOrEmpty(ret))
                return String.Empty;
            else
            {
                byte[] bytes = System.Text.Encoding.GetEncoding("iso-8859-8").GetBytes(ret);
                ret = System.Text.Encoding.UTF8.GetString(bytes);
            }


            return ret;

        }


        /// <summary>
        /// Compara a igualdade das strings, independente de Case e/ou acentuação e retorna o valor validado.
        /// </summary>
        /// <param name="_value">Valor à ser comparado</param>
        /// <param name="_compare">Valores com o qual se comparar</param>
        /// <returns>Retorna a chave na qual o valor se enquadra</returns>
        public string GetEquality(string _value, params string[] _compare)
        {
            string _returnValue = "Não consta";

            try
            {
                if (String.IsNullOrEmpty(_value))
                    return _returnValue;

                foreach (string s in _compare)
                    if (new Regex(Utility.Utility.FormataStringPesquisa(_value), RegexOptions.IgnoreCase).IsMatch(s))
                        return s;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return _returnValue;
            }

            return _returnValue;
        }
   
    }
}
