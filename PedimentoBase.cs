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
        private string sPatente;
        private string sNumeroPedimento;
        private string sTipoDocumento;
        private string sOperacionAduanera;
        private decimal nValorMercancia;
        /// <summary>
        /// Constructor PedimentoBase
        /// </summary>
        /// <returns></returns>
        public PedimentoBase()
        {
            this.nValorMercancia = 0 ;
        }
        public void SetTipoDocumento(string tipoDocumento)
        {
            this.sTipoDocumento = tipoDocumento;
        }
        public string GetTipoDocumento()
        {
            return this.sTipoDocumento;
        }
        public void SetOperacionAduanera(string operacionAduanera)
        {
            this.sOperacionAduanera = operacionAduanera;
        }
        public string GetOperacionAduanera()
        {
            return this.sOperacionAduanera;
        }
        public void SetPatente(string patente)
        {
            this.sPatente = patente;
        }
        public string GetPatente()
        {
            return this.sPatente;
        }
        public void SetPedimento(string pedimento)
        {
            this.sNumeroPedimento = pedimento;
        }
        public string GetPedimento()
        {
            return this.sNumeroPedimento;
        }
        public void SetValorMercancia(decimal valorMercancia)
        {
            this.nValorMercancia = valorMercancia;
        }
        public decimal GetValorMercancia()
        {
            return this.nValorMercancia;
        }
    }
}
