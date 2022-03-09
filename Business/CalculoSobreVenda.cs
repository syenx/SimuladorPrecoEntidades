using KS.SimuladorPrecos.DataEntities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;

namespace KS.SimuladorPrecos.DataEntities.Business
{
    public class CalculoSobreVenda
    {

        #region propriedades
        CalculoDeCustoPadrao _CalculoDeCustoPadrao = new CalculoDeCustoPadrao();

        public List<SimuladorPrecoCustos> lstCustos { get; set; }

        public string txtItem { get; set; }

        public string ddlEstadoDestino { get; set; }

        public string txtPrecoObjetivo { get; set; }

        public string txtDescontoObjetivo { get; set; }

        public string txtMargemObjetivo { get; set; }

        public string rblPerfilCliente { get; set; }

        #endregion

        public string GetPrecoVendaDesconto(string _estabelecimentoId)
        {
            try
            {
                if (!String.IsNullOrEmpty(this.txtDescontoObjetivo))
                {
                    decimal _descontoObjetivo =
                        decimal.Parse(this.txtDescontoObjetivo) > 1 ?
                            ((decimal.Parse(this.txtDescontoObjetivo)) / 100) :
                                decimal.Parse(this.txtDescontoObjetivo);

                    SimuladorPrecoCustos oCst =
                        lstCustos
                            .Where(x =>
                                    x.itemId.Equals(this.txtItem.PadLeft(5, '0')) &&
                                    x.estabelecimentoId.Equals(_estabelecimentoId)
                                  )
                            .SingleOrDefault();

                    switch (_estabelecimentoId)
                    {
                        #region :: Distribuidoras ::

                        case "2":
                        case "3":
                        case "5":
                        case "10":
                        case "11":
                        case "20":
                        case "30":
                        case "31":
                        case "40":
                        case "90":

                            return (
                                    (oCst.precoFabrica)
                                        -
                                    (
                                        (oCst.precoFabrica)
                                            *
                                        (_descontoObjetivo)
                                    )
                                   ).ToString();

                        #endregion

                        #region :: Drogarias ::

                        case "4":
                        case "7":
                        case "8":
                        case "91":

                            return (
                                    (oCst.pmc17)
                                        -
                                    (
                                        (oCst.pmc17)
                                            *
                                        (_descontoObjetivo)
                                    )
                                   ).ToString();

                        #endregion

                        default:
                            return this.txtPrecoObjetivo;
                    }
                }
                else
                    return this.txtPrecoObjetivo;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return this.txtPrecoObjetivo;
            }
        }

        public decimal GetPrecoVenda(string _valorVenda)
        {
            try
            {
                if (!String.IsNullOrEmpty(_valorVenda))
                    return decimal.Parse(_valorVenda);

                return 0;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_valorVenda"></param>
        /// <param name="_estabelecimento"></param>
        /// <param name="_convenioId"></param>
        /// <param name="_perfil"></param>
        /// <param name="_resolucao"></param>
        /// <param name="_usoExclusivo"></param>
        /// <param name="_descST"></param>
        /// <param name="_mva"></param>
        /// <param name="_aliquotaIntICMS"></param>
        /// <param name="_pmc17"></param>
        /// <param name="_precoFabrica"></param>
        /// <param name="_descontoComercial"></param>
        /// <param name="_descontoAdicional"></param>
        /// <param name="_repasse"></param>
        /// <param name="_valorICMSST"></param>
        /// <param name="_percIPI"></param>
        /// <param name="_reducaoST_MVA"></param>
        /// <returns></returns>
        public decimal GetICMSSobreVenda(string _valorVenda, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, string _usoExclusivo, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, decimal _valorICMSST, decimal _percIPI, decimal _reducaoST_MVA)
        {
            try
            {
                #region :: Versão anterior ::

                /*
                SimuladorPrecoRegrasGerais oRg =
                    new SimuladorPrecoRegrasGerais
                        {
                            estabelecimentoId = _estabelecimento,
                            convenioId = _convenioId,
                            perfilCliente = _perfil,
                            resolucaoId = _resolucao,
                            ufDestino = this.ddlEstadoDestino.SelectedValue,
                            usoExclusivoHospitalar = Utility.Utility.FormataStringPesquisa(_usoExclusivo)

                        }.GetRegras();

                if (oRg != null)
                    return ((oRg.icmsSobreVenda > 1 ? (oRg.icmsSobreVenda / 100) : oRg.icmsSobreVenda) * GetPrecoVenda(_valorVenda));
                else
                    return 0;
                */

                #endregion

                List<SimuladorPrecoRegrasGerais> oRg =
                    new SimuladorPrecoRegrasGerais
                    {
                        estabelecimentoId = _estabelecimento,
                        convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                        perfilCliente = _perfil,
                        resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                        ufDestino = this.ddlEstadoDestino,
                        usoExclusivoHospitalar = Utility.Utility.FormataStringPesquisa(_usoExclusivo)

                    }.GetRegrasICMSVenda();

                if (oRg != null)
                {

                    if (_estabelecimento.Equals("20"))
                    {

                        if (_convenioId.Equals("Convênio 118"))
                        {
                        }
                    }


                    SimuladorPrecoRegrasGerais _oRg = null;

                    #region :: Valida o cálculo de ICMS de saída de acordo com a ST de entreda ::

                    if (
                        _CalculoDeCustoPadrao.GetValorICMS_ST(_descST,
                                        _mva,
                                        _aliquotaIntICMS,
                                        _pmc17,
                                        _precoFabrica,
                                        _descontoComercial,
                                        _descontoAdicional,
                                        _repasse,
                                        _valorICMSST,
                                        _percIPI,
                                        _reducaoST_MVA
                                       ) > 0
                       )
                        _oRg = oRg.Where(x => x._icmsStValor.GetValueOrDefault(-1).Equals(0)).SingleOrDefault();
                    else
                        _oRg = oRg.Where(x => x._icmsStValor == null).SingleOrDefault();

                    #endregion

                    return ((_oRg.icmsSobreVenda > 1 ? (_oRg.icmsSobreVenda / 100) : _oRg.icmsSobreVenda) * GetPrecoVenda(_valorVenda));
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o ICMS-ST sobre venda
        /// </summary>
        /// <param name="_valorVenda">Valor venda</param>
        /// <param name="_estabelecimento">Estabelecimento</param>
        /// <param name="_convenioId">Convênio</param>
        /// <param name="_perfil">Perfil</param>
        /// <param name="_resolucao">Resolução 13</param>
        /// <param name="_aliquotaIntICMS">/// </param>
        /// <param name="_categoria"></param>
        /// <param name="_descontoAdicional"></param>
        /// <param name="_descontoComercial"></param>
        /// <param name="_descST"></param>
        /// <param name="_ICMSe"></param>
        /// <param name="_lista"></param>
        /// <param name="_mva"></param>
        /// <param name="_percIPI"></param>
        /// <param name="_percPISCofins"></param>
        /// <param name="_pmc17"></param>
        /// <param name="_precoFabrica"></param>
        /// <param name="_reducaoBase"></param>
        /// <param name="_reducaoST_MVA"></param>
        /// <param name="_repasse"></param>
        /// <param name="_uf"></param>
        /// <param name="_usoExclusivo"></param>
        /// <param name="_valorICMSST"></param>
        /// <returns>Cálculo formatado</returns>
        public decimal GetICMSSTSobreVenda(string _valorVenda, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, string _lista, string _categoria, string _uf, decimal _pmc17, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, decimal _percPISCofins, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, decimal _valorICMSST, decimal _reducaoST_MVA, string _usoExclusivo, bool _hasData, SimuladorPrecoRegrasGerais _oRg, bool embutirICMSST)
        {
            try
            {
                SimuladorPrecoRegrasGerais oRg = null;

                if (!_hasData)
                {
                    oRg =
                        new SimuladorPrecoRegrasGerais
                        {
                            estabelecimentoId = _estabelecimento,
                            convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                            perfilCliente = _perfil,
                            resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                            ufDestino = this.ddlEstadoDestino

                        }.GetRegras();
                }
                else
                    oRg = _oRg;

                if (oRg != null)
                {
                    #region :: Estabelecimento 02 ::

                    if (_estabelecimento.PadLeft(2, '0').Equals("02"))
                    {
                        switch (this.ddlEstadoDestino.ToUpper())
                        {
                            case "RS":

                                if (IsEquals(_convenioId.Trim(), "REGIME NORMAL") &&
                                       IsEquals(GetPerfil(_perfil.Trim()), "CONTRIBUINTE"))
                                {
                                    #region :: PMC17 > 0 ::

                                    if (_pmc17 > 0)
                                    {
                                        switch (GetEquality(_categoria.Trim(), "SIMILAR", "GENÉRICO"))
                                        {
                                            case "SIMILAR":

                                                if (oRg.icmsStSobreVenda != 0)
                                                {
                                                    return (
                                                            (_pmc17
                                                                *
                                                             Utility.Utility.VLR_STRSSIMILAR
                                                                *
                                                             (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                            )
                                                                -
                                                            GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                           );
                                                }
                                                else
                                                    return oRg.icmsStSobreVenda;
                                            case "GENÉRICO":
                                                if (oRg.icmsStSobreVenda != 0)
                                                {
                                                    return (
                                                            (_pmc17
                                                                *
                                                             Utility.Utility.VLR_STRSGENERICO
                                                                *
                                                             (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                            )
                                                                -
                                                            GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                           );
                                                }
                                                else
                                                    return oRg.icmsStSobreVenda;
                                            default:
                                                if (oRg.icmsStSobreVenda != 0)
                                                {
                                                    return (
                                                            (_pmc17
                                                                *
                                                             Utility.Utility.VLR_STRSNAOSIMILAR
                                                                *
                                                             (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                            )
                                                                -
                                                            GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                           );
                                                }
                                                else
                                                    return oRg.icmsStSobreVenda;

                                        }
                                    }

                                    #endregion

                                    #region :: PMC17 = 0 ::

                                    else
                                    {
                                        #region :: Positiva ::

                                        if (IsEquals(_lista.Trim(), "POSITIVA"))
                                        {
                                            switch (GetEquality(_categoria.Trim(), "SIMILAR", "GENÉRICO"))
                                            {
                                                case "SIMILAR":
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {
                                                        decimal valor1 = 0;

                                                        valor1 = (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSPOSITIVASIMILAR
                                                                        *
                                                                    Utility.Utility.VLR_STRSPOSITIVASIMILARPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                );

                                                        decimal icmsSobVenda = GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA);
                                                        return (
                                                               valor1
                                                                        -
                                                                   icmsSobVenda
                                                               );
                                                    }
                                                    else
                                                        return oRg.icmsStSobreVenda;

                                                case "GENÉRICO":
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {
                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSPOSITIVAGENERICO
                                                                        *
                                                                    Utility.Utility.VLR_STRSPOSITIVAGENERICOPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else
                                                        return oRg.icmsStSobreVenda;

                                                default:
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {

                                                        decimal valor = (
                                                                 (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                     GetPrecoVenda(_valorVenda)
                                                                         *
                                                                     Utility.Utility.VLR_STRSPOSITIVAOUTROS
                                                                         *
                                                                     Utility.Utility.VLR_STRSPOSITIVAOUTROSPRC
                                                                         *
                                                                     (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                 )
                                                                         -
                                                                     GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                                );


                                                        return valor;
                                                    }

                                                    else
                                                        return oRg.icmsStSobreVenda;


                                                //(
                                                //    (
                                                ////GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                //        GetPrecoVenda(_valorVenda)
                                                //            *
                                                //        Utility.VLR_STRSPOSITIVAOUTROS
                                                //            *
                                                //        Utility.VLR_STRSPOSITIVAOUTROSPRC
                                                //            *
                                                //        (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                //    )
                                                //            -
                                                //        GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                //   );
                                            }
                                        }

                                        #endregion

                                        #region :: Negativa ::

                                        else if (IsEquals(_lista.Trim(), "NEGATIVA"))
                                        {
                                            switch (GetEquality(_categoria.Trim(), "SIMILAR", "GENÉRICO"))
                                            {
                                                case "SIMILAR":
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {

                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSNEGATIVASIMILAR
                                                                        *
                                                                    Utility.Utility.VLR_STRSNEGATIVASIMILARPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else
                                                        return oRg.icmsStSobreVenda;

                                                case "GENÉRICO":
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {
                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSNEGATIVAGENERICO
                                                                        *
                                                                    Utility.Utility.VLR_STRSNEGATIVAGENERICOPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else
                                                        return oRg.icmsStSobreVenda;

                                                default:
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {
                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSNEGATIVAOUTROS
                                                                        *
                                                                    Utility.Utility.VLR_STRSNEGATIVAOUTROSPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else return oRg.icmsStSobreVenda;
                                            }
                                        }

                                        #endregion

                                        #region :: Neutra ::

                                        else if (IsEquals(_lista.Trim(), "NEUTRA"))
                                        {
                                            switch (GetEquality(_categoria.Trim(), "SIMILAR", "GENÉRICO"))
                                            {
                                                case "SIMILAR":
                                                    return (
                                                            (
                                                        //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                GetPrecoVenda(_valorVenda)
                                                                    *
                                                                Utility.Utility.VLR_STRSNEUTRASIMILAR
                                                                    *
                                                                Utility.Utility.VLR_STRSNEUTRASIMILARPRC
                                                                    *
                                                                (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                            )
                                                                    -
                                                                GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                           );

                                                case "GENÉRICO":
                                                    return (
                                                            (
                                                        //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                GetPrecoVenda(_valorVenda)
                                                                    *
                                                                Utility.Utility.VLR_STRSNEUTRAGENERICO
                                                                    *
                                                                Utility.Utility.VLR_STRSNEUTRAGENERICOPRC
                                                                    *
                                                                (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                            )
                                                                    -
                                                                GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                           );

                                                default:
                                                    return (
                                                            (
                                                        //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                GetPrecoVenda(_valorVenda)
                                                                    *
                                                                Utility.Utility.VLR_STRSNEUTRAOUTROS
                                                                    *
                                                                Utility.Utility.VLR_STRSNEUTRAOUTROSPRC
                                                                    *
                                                                (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                            )
                                                                    -
                                                                GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                           );
                                            }
                                        }

                                        #endregion

                                        #region :: Sem Lista ::

                                        else
                                        {
                                            switch (GetEquality(_categoria.Trim(), "SIMILAR", "GENÉRICO"))
                                            {
                                                case "SIMILAR":
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {
                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSSEMLISTASIMILAR
                                                                        *
                                                                    Utility.Utility.VLR_STRSSEMLISTASIMILARPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else return oRg.icmsStSobreVenda;

                                                case "GENÉRICO":
                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {
                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSSEMLISTAGENERICO
                                                                        *
                                                                    Utility.Utility.VLR_STRSSEMLISTAGENERICOPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else
                                                        return oRg.icmsStSobreVenda;
                                                default:

                                                    if (oRg.icmsStSobreVenda != 0)
                                                    {

                                                        return (
                                                                (
                                                            //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                                    GetPrecoVenda(_valorVenda)
                                                                        *
                                                                    Utility.Utility.VLR_STRSSEMLISTAOUTROS
                                                                        *
                                                                    Utility.Utility.VLR_STRSSEMLISTAOUTROSPRC
                                                                        *
                                                                    (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                                )
                                                                        -
                                                                    GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                               );
                                                    }
                                                    else
                                                        return oRg.icmsStSobreVenda;

                                            }
                                        }

                                        #endregion
                                    }

                                    #endregion
                                }
                                else
                                    return new decimal();

                            default:
                                return new decimal();
                        }
                    }

                    #endregion

                    #region :: Estabelecimento 31 ::

                    else if (_estabelecimento.Equals("31"))
                    {
                        if (embutirICMSST)
                        {
                            switch (this.ddlEstadoDestino.ToUpper())
                            {
                                case "RJ":

                                    if ((IsEquals(_convenioId.Trim(), "REGIME NORMAL", "DERMOCOSMÉTICOS"))
                                                &&
                                            IsEquals(GetPerfil(_perfil.Trim()), "CONTRIBUINTE"))
                                    {
                                        if (IsEquals(_lista.Trim(), "POSITIVA"))

                                            if (oRg.icmsStSobreVenda != 0)
                                            {
                                                return (
                                                        (
                                                    //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                         GetPrecoVenda(_valorVenda)
                                                            *
                                                         Utility.Utility.VLR_STRJLISTAPOSITIVA
                                                            *
                                                         (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                        )
                                                            -
                                                        GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                       );
                                            }
                                            else
                                                return oRg.icmsStSobreVenda;

                                        else if (IsEquals(_lista.Trim(), "NEGATIVA"))
                                            if (oRg.icmsStSobreVenda != 0)
                                            {

                                                return (
                                                        (
                                                    //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                         GetPrecoVenda(_valorVenda)
                                                            *
                                                         Utility.Utility.VLR_STRJLISTANEGATIVA
                                                            *
                                                         (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                        )
                                                            -
                                                        GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                       );
                                            }
                                            else
                                                return oRg.icmsStSobreVenda;

                                        else if (IsEquals(_lista.Trim(), "NEUTRA"))
                                            if (oRg.icmsStSobreVenda != 0)
                                            {
                                                return (
                                                        (
                                                    //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                         GetPrecoVenda(_valorVenda)
                                                            *
                                                         Utility.Utility.VLR_STRJLISTANEUTRA
                                                            *
                                                         (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                        )
                                                            -
                                                        GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                       );
                                            }
                                            else
                                                return oRg.icmsStSobreVenda;

                                        else
                                        {
                                            if (oRg.icmsStSobreVenda != 0)
                                            {
                                                return (
                                                        (
                                                    //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                                         GetPrecoVenda(_valorVenda)
                                                            *
                                                         Utility.Utility.VLR_STRJLISTASEM
                                                            *
                                                         (oRg.icmsStSobreVenda > 1 ? (oRg.icmsStSobreVenda / 100) : oRg.icmsStSobreVenda)
                                                        )
                                                            -
                                                        GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _usoExclusivo, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                                       );
                                            }
                                            else
                                                return oRg.icmsStSobreVenda;

                                        }
                                    }
                                    else
                                        return new decimal();

                                default:
                                    return new decimal();
                            }
                        }

                        else
                        {
                            return new decimal();
                        }
                    }

                    #endregion

                    else
                        return new decimal();
                }
                else
                    return new decimal();

                #region :: Regras de cálculo de ST ::

                /*
                 * ST somente para estabelecimentos 2 e 31
                 *
                 *   regras estabelecimento 2
                 *
                 *   Somente RS
                 *
                 *   saida = RS
                 *   - regime normal 
                 *   - contribuinte
                 *   - produto similar
                 *   - pmc17 * 0,75 * icmsst
                 *
                 *   saida = RS
                 *   - regime normal 
                 *   - contribuinte
                 *   - produto NAO similar
                 *   - pmc17 * 0,80 * icmsst
                 *
                 *   default = 0
                 *
                 *   Regras estabelecimento 31
                 *
                 *   Somente RJ
                 *
                 *   Saida = RJ
                 *   - Regime normal e/ou Dermocosmeticos
                 *   - contribuinte
                 *   - lista Positiva
                 *   - custoPadrao * 1,3824(variável) * icmsst
                 *
                 *   Saida = RJ
                 *   - Regime normal e/ou Dermocosmeticos
                 *   - contribuinte
                 *   - lista Negativa
                 *   - custoPadrao * 1,3293(variável) * icmsst
                 *
                 *   Saida = RJ
                 *   - Regime normal e/ou Dermocosmeticos
                 *   - contribuinte
                 *   - lista Neutra
                 *   - custoPadrao * 1,4142(variável) * icmsst
                 *
                 *   Saida = RJ
                 *   - Regime normal e/ou Dermocosmeticos
                 *   - contribuinte
                 *   - lista Sem Lista
                 *   - custoPadrao * 1,4142(variável) * icmsst
                 *
                 *   default 0
                 *
                 */

                #endregion
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o ajuste do regime fiscal sobre venda
        /// </summary>
        /// <param name="_valorVenda">Valor venda</param>
        /// <param name="_estabelecimento">Estabelecimento</param>
        /// <param name="_convenioId">Convênio</param>
        /// <param name="_perfil">Perfil</param>
        /// <param name="_resolucao">Resolução 13</param>
        /// <returns>Cálculo formatado</returns>
        public decimal GetAjusteRegimeFiscalSobreVenda(string _valorVenda, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, decimal _percPISCofins, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, string _uf, string _exclusivoHospitalar, decimal _valorICMSST, decimal _reducaoST_MVA)
        {
            try
            {
                SimuladorPrecoRegrasGerais oRg =
                    new SimuladorPrecoRegrasGerais
                    {
                        estabelecimentoId = _estabelecimento,
                        convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                        perfilCliente = _perfil,
                        resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                        ufDestino = this.ddlEstadoDestino,
                        usoExclusivoHospitalar = Utility.Utility.FormataStringPesquisa(_exclusivoHospitalar)

                    }.GetRegras();



                if (oRg != null)
                {
                    switch (GetEquality(_convenioId.Trim(), "DERMOCOSMETICOS", "REGIME NORMAL"))
                    {
                        case "DERMOCOSMETICOS":
                        case "REGIME NORMAL":

                            return
                                    !_estabelecimento.Equals("11") ?

                                    (
                                //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                       GetPrecoVenda(_valorVenda)

                                            *
                                //(Utility.VLR_BASEAJUSTEREGIMEFISCAL)
                                //  *
                                        (oRg.ajusteRegimeFiscal > 1 ? (oRg.ajusteRegimeFiscal / 100) : oRg.ajusteRegimeFiscal)
                                    )
                                        :
                                    (
                                        (
                                //GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPISCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId)
                                            GetPrecoVenda(_valorVenda)
                                                *
                                //(Utility.VLR_BASEAJUSTEREGIMEFISCAL)
                                //   *
                                            (oRg.ajusteRegimeFiscal > 1 ? (oRg.ajusteRegimeFiscal / 100) : oRg.ajusteRegimeFiscal)
                                        )
                                            *
                                        (-1)
                                    );
                        default:
                            return ((oRg.ajusteRegimeFiscal > 1 ? (oRg.ajusteRegimeFiscal / 100) : oRg.ajusteRegimeFiscal) * GetPrecoVenda(_valorVenda)); //return 0;
                    }
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o PIS/Cofins sobre venda
        /// </summary>
        /// <param name="_valorVenda">Valor venda</param>
        /// <param name="_percPisCofins">Percentual do PIS/Cofins</param>
        /// <returns>Cálculo formatado</returns>
        public decimal GetPISCofinsSobreVenda(string _valorVenda, decimal _percPisCofins)
        {
            try
            {
                return (
                        (_percPisCofins > 1 ? (_percPisCofins / 100) : _percPisCofins)
                            *
                        GetPrecoVenda(_valorVenda)
                       );
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calculo do preço líquido de venda
        /// </summary>
        /// <param name="_valorVenda">Valor venda</param>
        /// <param name="_percPisCofins">PIS/Cofins</param>
        /// <param name="_estabelecimento">Estabelecimento</param>
        /// <param name="_convenioId">Convênio</param>
        /// <param name="_perfil">Perfil</param>
        /// <param name="_resolucao">Resolução</param>
        /// <returns>Cálculo formatado</returns>
        public decimal GetPrecoVendaLiquido(string _valorVenda, decimal _percPisCofins, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, string _uf, string _exclusivoHospitalar, decimal _valorICMSST, decimal _reducaoST_MVA, string _lista, string _categoria)
        {
            try
            {
                SimuladorPrecoRegrasGerais oRg =
                    new SimuladorPrecoRegrasGerais
                    {
                        estabelecimentoId = _estabelecimento,
                        convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                        perfilCliente = _perfil,
                        resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                        ufDestino = this.ddlEstadoDestino

                    }.GetRegras();

                if (oRg != null)
                {
                    return (
                            GetPrecoVenda(_valorVenda)
                                -
                            GetICMSSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _exclusivoHospitalar, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA)
                                -
                            GetICMSSTSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _lista, _categoria, _uf, _pmc17, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _exclusivoHospitalar, false, null, false)
                                +
                            GetAjusteRegimeFiscalSobreVenda(_valorVenda, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA)
                                -
                            ((_percPisCofins / 100) * GetPrecoVenda(_valorVenda))
                           );
                }
                else
                    return 0;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o valor da margem
        /// </summary>
        /// <param name="_valorVenda">Valor venda</param>
        /// <param name="_percPisCofins">Percentual do PIS/Cofins</param>
        /// <param name="_estabelecimento">Estabelecimento</param>
        /// <param name="_convenioId">Convênio</param>
        /// <param name="_perfil">Perfil</param>
        /// <param name="_resolucao">Resolução</param>
        /// <param name="_reducaoBase">Redução base</param>
        /// <param name="_ICMSe">ICMSe</param>
        /// <param name="_percIPI">Percentual IPI</param>
        /// <param name="_descST">Desc ST</param>
        /// <param name="_mva">MVA</param>
        /// <param name="_aliquotaIntICMS">Alíquota interna ICMS</param>
        /// <param name="_pmc17">PMC</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto Adicional</param>
        /// <param name="_repasse">Valor do repasse</param>
        /// <returns>Cálculo formatado</returns>
        public decimal GetValorMagem(string _valorVenda, decimal _percPisCofins, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, decimal _valorICMSST, decimal _reducaoST_MVA, string _uf, string _exclusivoHospitalar, string _lista, string _categoria, int CodigoOrigem, string tratamentoICMSEstab)
        {
            try
            {
                return (
                        (GetPrecoVendaLiquido(_valorVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria))
                            -
                        (_CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, CodigoOrigem, tratamentoICMSEstab, _uf, _resolucao))
                       );
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        /// <summary>
        /// Calcula o valor percentual da margem
        /// </summary>
        /// <param name="_valorVenda">Valor venda</param>
        /// <param name="_percPisCofins">Percentual do PIS/Cofins</param>
        /// <param name="_estabelecimento">Estabelecimento</param>
        /// <param name="_convenioId">Convênio</param>
        /// <param name="_perfil">Perfil</param>
        /// <param name="_resolucao">Resolução</param>
        /// <param name="_reducaoBase">Redução base</param>
        /// <param name="_ICMSe">ICMSe</param>
        /// <param name="_percIPI">Percentual IPI</param>
        /// <param name="_descST">Desc ST</param>
        /// <param name="_mva">MVA</param>
        /// <param name="_aliquotaIntICMS">Alíquota interna ICMS</param>
        /// <param name="_pmc17">PMC</param>
        /// <param name="_precoFabrica">Preço fábrica</param>
        /// <param name="_descontoComercial">Desconto comercial</param>
        /// <param name="_descontoAdicional">Desconto Adicional</param>
        /// <param name="_repasse">Valor do repasse</param>
        /// <returns>Cálculo formatado</returns>
        public decimal? GetPercentualMargem1(string _valorVenda, decimal _percPisCofins, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, DataListItemEventArgs e, string _lista, string _categoria, string _uf, string _exclusivoHospitalar, decimal _valorICMSST, decimal _reducaoST_MVA, int codigoOrigem, string tratamentoICMSEstab)
        {
            try
            {
                #region :: Valor Assumido ::

                if (String.IsNullOrEmpty(this.txtMargemObjetivo))
                    return (
                            (
                                (GetValorMagem(_valorVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _uf, _exclusivoHospitalar, _lista, _categoria, codigoOrigem, tratamentoICMSEstab))
                                    /
                                (GetPrecoVendaLiquido(_valorVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria))
                            )
                                    *
                                100
                           );
                #endregion

                #region :: Valor Calculado ::

                else
                {
                    #region :: Declaração de Auxiliares ::

                    decimal _margemObjetivo = decimal.Parse(this.txtMargemObjetivo) > 1 ? (decimal.Parse(this.txtMargemObjetivo) / 100) : decimal.Parse(this.txtMargemObjetivo),
                            _vlrVenda = 0,
                            _vlrInicialSaida = _CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, codigoOrigem, tratamentoICMSEstab, _uf, _resolucao),
                            _vlrIcms = 0;

                    decimal? _margem = null;
                    SimuladorPrecoRegrasGerais oRg = null;

                    #endregion

                    #region :: Loop Objetivo ::

                    for (int i = 0; (!_margem.HasValue ? 0 : _margem.Value) != _margemObjetivo; i++)
                    {
                        #region :: Valida Margem ::

                        if (_margem.HasValue)
                            if (_margem.Value > _margemObjetivo)
                                break;


                        #endregion

                        #region :: Valor de Incremento ::

                        _vlrVenda = ((_vlrInicialSaida) += decimal.Parse("0,05"));

                        #endregion

                        #region :: Get Regra ::

                        if (_margem == null)
                        {
                            #region :: Regras ::

                            oRg =
                                new SimuladorPrecoRegrasGerais
                                {
                                    estabelecimentoId = _estabelecimento,
                                    convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                                    perfilCliente = _perfil,
                                    resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                                    ufDestino = this.ddlEstadoDestino

                                }.GetRegras();

                            #endregion

                            #region :: Bloco ICMS Sobre Venda ::

                            List<SimuladorPrecoRegrasGerais> oRgSv =
                                new SimuladorPrecoRegrasGerais
                                {
                                    estabelecimentoId = _estabelecimento,
                                    convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                                    perfilCliente = _perfil,
                                    resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                                    ufDestino = this.ddlEstadoDestino,
                                    usoExclusivoHospitalar = Utility.Utility.FormataStringPesquisa(_exclusivoHospitalar)

                                }.GetRegrasICMSVenda();

                            if (oRgSv != null)
                            {
                                SimuladorPrecoRegrasGerais _oRg = null;

                                #region :: Valida o cálculo de ICMS de saída de acordo com a ST de entreda ::

                                if (
                                    _CalculoDeCustoPadrao.GetValorICMS_ST(_descST,
                                                    _mva,
                                                    _aliquotaIntICMS,
                                                    _pmc17,
                                                    _precoFabrica,
                                                    _descontoComercial,
                                                    _descontoAdicional,
                                                    _repasse,
                                                    _valorICMSST,
                                                    _percIPI,
                                                    _reducaoST_MVA
                                                   ) > 0
                                   )
                                    _oRg = oRgSv.Where(x => x._icmsStValor.GetValueOrDefault(-1).Equals(0)).SingleOrDefault();
                                else
                                    _oRg = oRgSv.Where(x => x._icmsStValor == null).SingleOrDefault();

                                #endregion

                                _vlrIcms = ((_oRg.icmsSobreVenda > 1 ? (_oRg.icmsSobreVenda / 100) : _oRg.icmsSobreVenda));
                            }
                            else
                                _vlrIcms = 0;

                            #endregion
                        }

                        #endregion

                        #region :: Valor Margem ::

                        _margem =
                              (
                                    (
                                        (GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms))
                                            -
                                        (_CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, codigoOrigem, tratamentoICMSEstab, _uf, _resolucao))
                                    )
                                        /
                                    (GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms))
                                );

                        #endregion
                    }

                    #endregion

                    #region :: Valor venda ::

                    ((Literal)e.Item.FindControl("ltrPrecoVendaValor")).Text = string.Format("{0:n2}", _vlrVenda);

                    #endregion

                    #region :: ICMS sobre o valor de venda ::

                    ((Literal)e.Item.FindControl("ltrICMSSobreVendaValor")).Text =
                        string.Format("{0:n2}",
                            GetICMSSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _exclusivoHospitalar, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA));

                    #endregion

                    #region :: ICMS-ST sobre o valor da venda ::

                    ((Literal)e.Item.FindControl("ltrICMSSTSobreVendaValor")).Text =
                        string.Format("{0:n2}",
                            GetICMSSTSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _lista, _categoria, _uf, _pmc17, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _exclusivoHospitalar, true, oRg, true));

                    #endregion

                    #region :: Valor do Ajuste Fiscal ::

                    ((Literal)e.Item.FindControl("ltrAjusteRegimeFiscalValor")).Text =
                        string.Format("{0:n2}",
                                       GetAjusteRegimeFiscalSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA));

                    #endregion

                    #region :: PIS/Cofins sobre venda ::

                    ((Literal)e.Item.FindControl("ltrPISCofinsSobreVendaValor")).Text =
                        string.Format("{0:n2}",
                            GetPISCofinsSobreVenda(_vlrVenda.ToString(), _percPisCofins));

                    #endregion

                    #region :: Valor da venda líquida ::

                    ((Literal)e.Item.FindControl("ltrPrecoVendaLiquidoValor")).Text =
                        string.Format("{0:n2}",
                                       GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms));

                    #endregion

                    #region :: Valor Margem ::

                    ((Literal)e.Item.FindControl("ltrMargemVlrValor")).Text =
                        string.Format("{0:n2}",
                                       (
                                        GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms)
                                            -
                                        _CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, codigoOrigem, tratamentoICMSEstab, _uf, _resolucao)
                                       )
                                     );

                    #endregion

                    return (_margem * 100);
                }

                #endregion
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }

        public decimal? GetPercentualMargem(string _valorVenda, decimal _percPisCofins, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, DataListItemEventArgs e, string _lista, string _categoria, string _uf, string _exclusivoHospitalar, decimal _valorICMSST, decimal _reducaoST_MVA, int codigoOrigem, string tratamentoICMSEstab)
        {
            try
            {
                #region :: Valor Assumido ::

                if (String.IsNullOrEmpty(this.txtMargemObjetivo))
                    return (
                            (
                                (GetValorMagem(_valorVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _uf, _exclusivoHospitalar, _lista, _categoria, codigoOrigem, tratamentoICMSEstab))
                                    /
                                (GetPrecoVendaLiquido(_valorVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria))
                            )
                                    *
                                100
                           );
                #endregion

                #region :: Valor Calculado ::

                else
                {

                    #region :: Declaração de Auxiliares ::

                    decimal _margemObjetivo = decimal.Parse(this.txtMargemObjetivo) > 1 ? (decimal.Parse(this.txtMargemObjetivo) / 100) : decimal.Parse(this.txtMargemObjetivo),
                            _vlrVenda = 0,
                            _vlrInicialSaida = _CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, codigoOrigem, tratamentoICMSEstab, _uf, _resolucao),
                            _vlrIcms = 0;

                    decimal? _margem = null;
                    SimuladorPrecoRegrasGerais oRg = null;

                    #endregion

                    #region :: Get Regra ::

                    if (_margem == null)
                    {
                        #region :: Regras ::

                        oRg =
                            new SimuladorPrecoRegrasGerais
                            {
                                estabelecimentoId = _estabelecimento,
                                convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                                perfilCliente = _perfil,
                                resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                                ufDestino = this.ddlEstadoDestino

                            }.GetRegras();

                        #endregion

                        #region :: Bloco ICMS Sobre Venda ::

                        List<SimuladorPrecoRegrasGerais> oRgSv =
                            new SimuladorPrecoRegrasGerais
                            {
                                estabelecimentoId = _estabelecimento,
                                convenioId = Utility.Utility.FormataStringPesquisa(_convenioId),
                                perfilCliente = _perfil,
                                resolucaoId = Utility.Utility.FormataStringPesquisa(_resolucao),
                                ufDestino = this.ddlEstadoDestino,
                                usoExclusivoHospitalar = Utility.Utility.FormataStringPesquisa(_exclusivoHospitalar)

                            }.GetRegrasICMSVenda();

                        if (oRgSv != null)
                        {
                            SimuladorPrecoRegrasGerais _oRg = null;

                            #region :: Valida o cálculo de ICMS de saída de acordo com a ST de entreda ::

                            if (
                                _CalculoDeCustoPadrao.GetValorICMS_ST(_descST,
                                                _mva,
                                                _aliquotaIntICMS,
                                                _pmc17,
                                                _precoFabrica,
                                                _descontoComercial,
                                                _descontoAdicional,
                                                _repasse,
                                                _valorICMSST,
                                                _percIPI,
                                                _reducaoST_MVA
                                               ) > 0
                               )
                                _oRg = oRgSv.Where(x => x._icmsStValor.GetValueOrDefault(-1).Equals(0)).SingleOrDefault();
                            else
                                _oRg = oRgSv.Where(x => x._icmsStValor == null).SingleOrDefault();

                            #endregion

                            _vlrIcms = ((_oRg.icmsSobreVenda > 1 ? (_oRg.icmsSobreVenda / 100) : _oRg.icmsSobreVenda));
                        }
                        else
                            _vlrIcms = 0;

                        #endregion

                        #region :: Valor Margem ::

                        //if (_vlrIcms > 1 && _margemObjetivo > 1)
                        //{
                        _vlrVenda = (_vlrInicialSaida / (1 - _vlrIcms) / (1 - _margemObjetivo));
                        //}
                        //else if (_vlrIcms == 0 && _margemObjetivo > 1)
                        //{
                        //    _vlrVenda = (_vlrInicialSaida  / (1 - _margemObjetivo));
                        //}
                        //else if (_vlrIcms > 1 && _margemObjetivo == 0)
                        //{
                        //    _vlrVenda = (_vlrInicialSaida / (1 - _vlrIcms));
                        //}
                        //else if (_vlrIcms == 0 && _margemObjetivo <= 1)
                        //{
                        //    _vlrVenda = _vlrInicialSaida ;
                        //}

                        _margem =
                                  (
                                        (
                                            (GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms))
                                                -
                                            (_CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, codigoOrigem, tratamentoICMSEstab, _uf, _resolucao))
                                        )
                                            /
                                        (GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms))
                                    );

                        #endregion
                    }

                    #endregion


                    #region :: Valor venda ::

                    ((Literal)e.Item.FindControl("ltrPrecoVendaValor")).Text = string.Format("{0:n2}", _vlrVenda);

                    #endregion

                    #region :: ICMS sobre o valor de venda ::

                    ((Literal)e.Item.FindControl("ltrICMSSobreVendaValor")).Text =
                        string.Format("{0:n2}",
                            GetICMSSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _exclusivoHospitalar, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _percIPI, _reducaoST_MVA));

                    #endregion

                    #region :: ICMS-ST sobre o valor da venda ::

                    ((Literal)e.Item.FindControl("ltrICMSSTSobreVendaValor")).Text =
                        string.Format("{0:n2}",
                            GetICMSSTSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _lista, _categoria, _uf, _pmc17, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _exclusivoHospitalar, true, oRg, true));

                    #endregion

                    #region :: Valor do Ajuste Fiscal ::

                    ((Literal)e.Item.FindControl("ltrAjusteRegimeFiscalValor")).Text =
                        string.Format("{0:n2}",
                                       GetAjusteRegimeFiscalSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA));

                    #endregion

                    #region :: PIS/Cofins sobre venda ::

                    ((Literal)e.Item.FindControl("ltrPISCofinsSobreVendaValor")).Text =
                        string.Format("{0:n2}",
                            GetPISCofinsSobreVenda(_vlrVenda.ToString(), _percPisCofins));

                    #endregion

                    #region :: Valor da venda líquida ::

                    ((Literal)e.Item.FindControl("ltrPrecoVendaLiquidoValor")).Text =
                        string.Format("{0:n2}",
                                       GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms));

                    #endregion

                    #region :: Valor Margem ::

                    ((Literal)e.Item.FindControl("ltrMargemVlrValor")).Text =
                        string.Format("{0:n2}",
                                       (
                                        GetPrecoVendaLiquidoMemoria(oRg, _vlrVenda, _percPisCofins, _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA, _lista, _categoria, _exclusivoHospitalar, _vlrIcms)
                                            -
                                        _CalculoDeCustoPadrao.GetValorCustoPadrao(_reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _estabelecimento, _convenioId, codigoOrigem, tratamentoICMSEstab, _uf, _resolucao)
                                       )
                                     );

                    #endregion



                    return (_margem * 100);
                }

                #endregion
            }
            catch (Exception ex)
            {

                Utility.Utility.WriteLog(ex);
                return 0;
            }


        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="oRg"></param>
        /// <returns></returns>
        private decimal GetPrecoVendaLiquidoMemoria(SimuladorPrecoRegrasGerais oRg, decimal _vlrVenda, decimal _percPisCofins, string _estabelecimento, string _convenioId, string _perfil, string _resolucao, decimal _reducaoBase, decimal _ICMSe, decimal _percIPI, string _descST, decimal _mva, decimal _aliquotaIntICMS, decimal _pmc17, decimal _precoFabrica, decimal _descontoComercial, decimal _descontoAdicional, decimal _repasse, string _uf, string _exclusivoHospitalar, decimal _valorICMSST, decimal _reducaoST_MVA, string _lista, string _categoria, string _usoExclusivo, decimal _vlrIcms)
        {
            try
            {
                if (!oRg._AliquotasCalculadas)
                {
                    oRg._AliquotasCalculadas = true;
                    oRg._ajusteRegimeFiscal = GetAjusteRegimeFiscalSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _pmc17, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _uf, _exclusivoHospitalar, _valorICMSST, _reducaoST_MVA);
                }

                return (
                        GetPrecoVenda(_vlrVenda.ToString())
                            -
                        (_vlrIcms * GetPrecoVenda(_vlrVenda.ToString()))
                            -
                        GetICMSSTSobreVenda(_vlrVenda.ToString(), _estabelecimento, _convenioId, _perfil, _resolucao, _lista, _categoria, _uf, _pmc17, _reducaoBase, _ICMSe, _percIPI, _percPisCofins, _descST, _mva, _aliquotaIntICMS, _precoFabrica, _descontoComercial, _descontoAdicional, _repasse, _valorICMSST, _reducaoST_MVA, _exclusivoHospitalar, true, oRg, false)
                            +
                        oRg._ajusteRegimeFiscal
                            -
                        ((_percPisCofins / 100) * GetPrecoVenda(_vlrVenda.ToString()))
                       );
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return 0;
            }
        }


        public bool IsEquals(string _value, params string[] _compare)
        {
            try
            {
                if (String.IsNullOrEmpty(_value))
                    return false;

                foreach (string s in _compare)
                    if (new Regex(Utility.Utility.FormataStringPesquisa(_value), RegexOptions.IgnoreCase).IsMatch(s))
                        return true;
            }
            catch (Exception ex)
            {
                Utility.Utility.WriteLog(ex);
                return false;
            }

            return false;
        }

        public string GetEquality(string _value, params string[] _compare)
        {
            string _returnValue = "Não Consta";

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

        public string GetPerfil(string _perfil)
        {
            switch (this.rblPerfilCliente.ToUpper())
            {
                case "C":
                    return "contribuinte";

                default:
                    return "não contribuinte";
            }
        }


    }
}
