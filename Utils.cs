using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CIF_VALOR_MERCANCIA.Properties;

namespace CIF_VALOR_MERCANCIA
{
    static class Utils
    {
        public static decimal GetValorMercancia(string CRP, string tipoDocumento, string operacionAduanera)
        {
            if (tipoDocumento.Equals("IMPORATACIÓN"))
            {
                if (operacionAduanera.Equals("IMPORTACIÓN"))
                {
                    decimal.TryParse(GetTag(CRP,Settings.Default.sTagValorAduana,Settings.Default.cSeparadorEspacioEnBlanco,Settings.Default.nPosicionTagValorAduana), out decimal nValorAduana);
                    return nValorAduana;
                }
                else //Es Expo
                {
                    decimal.TryParse(GetTag(CRP, Settings.Default.sTagValorComercial, Settings.Default.cSeparadorEspacioEnBlanco, Settings.Default.nPosicionTagValorComercial), out decimal nValorComercial);
                    return nValorComercial;
                }
            }
            else // Es previo de consolidado
            {
                return 1;
            }
        }
        public static string CopyFrom(string rutaCompleta)
        {
            StreamReader oReader = new StreamReader(rutaCompleta, Encoding.Default);
            return oReader.ReadToEnd();
        }
        public static string GetTipoDoc(string CRP)
        {
            return GetTag(CRP,Settings.Default.sTagTipoDocumento,Settings.Default.cSeparadorDosPuntos,Settings.Default.nPosicionTagTipoDocumento);
        }
        public static string GetOperacionAduanera(string CRP)
        {
            return GetTag(CRP,Settings.Default.sTagTipoOperacionAduanera,Settings.Default.cSeparadorDosPuntos,Settings.Default.nPosicionTagTipoOperacionAduanera);
        }
        public static string GetValorAduana(string CRP)
        {
            return GetTag(CRP,Settings.Default.sTagValorAduana,Settings.Default.cSeparadorEspacioEnBlanco,Settings.Default.nPosicionTagValorAduana);
        }
        public static string GetValorComercial(string CRP)
        {
            return GetTag(CRP, Settings.Default.sTagValorComercial, Settings.Default.cSeparadorEspacioEnBlanco, Settings.Default.nPosicionTagValorComercial);
        }
        public static string GetTag(string CRP, string tag, char separador, int posicion)
        {
            Boolean bEncontrado = false;
            string aux, sTagResult="";
            string[] aCampos;
            StringReader oReader = new StringReader(CRP);
            aux = oReader.ReadLine();

            while (!bEncontrado && aux != null)
            {
                if (aux.Contains(tag))
                {
                    bEncontrado = true;
                    aCampos = aux.Split();
                    sTagResult = aCampos[posicion];
                }
                else
                {
                    aux = oReader.ReadLine();
                }

            }
            return sTagResult;
        }
    }
}
