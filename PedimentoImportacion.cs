using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIF_VALOR_MERCANCIA
{
    public class PedimentoImportacion : PedimentoDecorador
    {
        public PedimentoImportacion(IPedimento pedimento) : base(pedimento) { }
        /// <summary>
        /// Apartir de este punto, se utiliza override para sobrescribir los metodos del pedimentobase
        /// </summary>
        /// <returns></returns>

        public override decimal GetValorMercancia()
        {
            IOUtils oUtilidades = new IOUtils();
            CRP oCRP = new CRP(oUtilidades.GetPattern(base.GetPatente(), base.GetPedimento()));
            //Sustituir por busqueda de VALOR ADUANA.
            string sCRP = oCRP.GetCopia();
            char[] separador = { ':' };
            string sTag = "Valor Aduana";
            int posicion = 2;
            oUtilidades.GetTag(sCRP,sTag, separador, posicion);
            return base.GetValorMercancia();
        }
    }
}
