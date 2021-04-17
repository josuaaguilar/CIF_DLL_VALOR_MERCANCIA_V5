using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIF_VALOR_MERCANCIA
{
    public class PedimentoDecorador : IPedimento
        
    {
        /// <summary>
        /// Clase que decora en función de la interfaz IPedimento
        /// </summary>
        private IPedimento Pedimento;
        public PedimentoDecorador(IPedimento pedimento)
        {
            this.Pedimento = pedimento;
        }

        public virtual string GetTipoDocumento()
        {
            return this.Pedimento.GetTipoDocumento();
        }
        public virtual string GetOperacionAduanera()
        {
            return this.Pedimento.GetOperacionAduanera();
        }
        public virtual decimal GetValorMercancia()
        {
            return this.Pedimento.GetValorMercancia();
        }
        public virtual string GetPedimento()
        {
            return "";
        }
        public virtual string GetPatente()
        {
            return "";
        }
    }
}
