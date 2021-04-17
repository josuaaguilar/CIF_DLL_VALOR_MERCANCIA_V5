using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CIF_VALOR_MERCANCIA
{
    public interface IPedimento
    {
        /// <summary>
        /// Todas las clases de Pedimento incluyen los metodos de extracción
        /// la función GetValorMercía varía en función del tipo de Operación.
        /// </summary>
        /// <returns></returns>
        string GetTipoDocumento();
        string GetOperacionAduanera();
        decimal GetValorMercancia();
        string GetPedimento();
        string GetPatente();

    }
}
