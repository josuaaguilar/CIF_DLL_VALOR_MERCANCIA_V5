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
        private string sCopia;
        private string sRutaCompleta;
        private string sPattern;
        public CRP() { }
        public void SetPattern(string patente, string numeroPedimento)
        {
            this.sPattern = patente + "-" + numeroPedimento+"-430.txt";
        }
        public string GetPattern()
        {
            return this.sPattern;
        }
        public void SetCopia()
        {
            IOUtils oUtilidades = new IOUtils();
            this.sCopia = oUtilidades.CopyFrom(this.sRutaCompleta);
        }
        public void SetRutaCompleta(string pattern)
        {
            IOUtils oUtilidades = new IOUtils();
            this.sRutaCompleta = oUtilidades.PathCombine(this.sRutaBase, pattern);
        }
        public string GetCopia()
        {
            return this.sCopia;
        }
        public string GetRutaBase()
        {
            return this.sRutaBase;
        }
    }
}
