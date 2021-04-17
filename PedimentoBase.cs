using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIF_VALOR_MERCANCIA
{
    public class PedimentoBase : IPedimento
    {
        /// <summary>
        /// En el mejor de los casos,
        /// estos metodos retornan de la siguiente forma:
        /// Operacion Aduanera = Importación
        /// Tipo de Documento = Normal
        /// Valor Mercancia = Valor Aduana
        /// </summary>
        /// <returns></returns>

        private string sPatente { get; set; }
        private string sNumeroPedimento { get; set; }
        private string sTipoDocumento { get; set; }
        private string sOperacionAduanera { get; set; }
        private decimal ValorMercancia { get; set; }
        public PedimentoBase(string Patente, string NumeroPedimento)
        {
            IOUtils oUtilidades = new IOUtils();
            oUtilidades.FormatearPatente(Patente);
            this.sPatente = oUtilidades.FormatearPatente(Patente);
            this.sNumeroPedimento = NumeroPedimento;
            //CRP oCRP = new CRP(oUtilidades.GetPattern(this.sPatente,this.sNumeroPedimento));
        }
        public string GetOperacionAduanera()
        {
            return "IMPORTACION";
        }

        public string GetTipoDocumento()
        {
            return "NORMAL";
        }
        /// <summary>
        /// Se inicializa en 0 para comodidad.
        /// </summary>
        /// <returns></returns>
        public decimal GetValorMercancia()
        {
            return 0;
        }
        public string GetPatente()
        {
            return this.sPatente;
        }
        public string GetPedimento()
        {
            return this.sNumeroPedimento;
        }

    }
}
