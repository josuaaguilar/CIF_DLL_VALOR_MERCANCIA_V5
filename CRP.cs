using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CIF_VALOR_MERCANCIA.Properties;

namespace CIF_VALOR_MERCANCIA
{
    /// <summary>
    /// Esta clase contiene la copia del CRP que se busca en el directorio base (sRutaBase) definido
    /// en la configuración de la DLL
    /// </summary>
    public class CRP
    {
         //Sigleton ? para evitar construir una instancia nueva;
        private string sRutaBase = Settings.Default.sRutaBase;
        private string sCopia { get; set; }
        private string sRutaCompleta { get; set; }
        private string sPattern { get; set; }

        public CRP(string pattern)
        {
            //Pasar el patron?
            this.sPattern = pattern;

        }
        public void setCopia(string patron)
        {
            IOUtils oUtilidades = new IOUtils();

            this.sCopia = oUtilidades.CopyFrom(this.sRutaCompleta);
        }
        public void setRutaCompleta()
        {
            IOUtils oUtilidades = new IOUtils();
            this.sRutaCompleta = oUtilidades.CombinePaths(this.sRutaBase,this.sPattern);
        }
        public string GetCopia()
        {
            return this.sCopia;
        }
    }
}
