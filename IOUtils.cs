using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using CIF_VALOR_MERCANCIA.Properties;

namespace CIF_VALOR_MERCANCIA
{
    /// <summary>
    /// Esta clase utiliza System.IO para acceder a la información
    /// manifestada en el archivos .txt
    /// </summary>
    class IOUtils
    {
        
        StreamReader oReader;
        StringReader sReader;
        
        public IOUtils()
        {

        }
        public string GetTag(string sCRP, string tag,char[] separador,int posicion)
        {
            //Validar con exportación! 
            Boolean bEncontrado;
            bEncontrado = false;
            sReader = new StringReader(sCRP);
            string sRenglon, sResult="";
            sRenglon = sReader.ReadLine();
            string[] sCamposRenglon;
            while(!bEncontrado && sRenglon != null)
            {
                if (sRenglon.Contains(tag))
                {
                    bEncontrado = true;
                    sCamposRenglon = sRenglon.Split(separador);
                    sResult = sCamposRenglon[posicion];
                }
                else
                {
                    sRenglon = sReader.ReadLine();
                }
            }
            return sResult;
        }
        public string FormatearPatente(string patente)
        {
            //meter a un ciclo while hasta que patente.length == 4
            if (patente.Length == 4)
                return patente;
            else
            {
                //En el caso que la cadena sea menor a 4 caracteres hay que evaluarla para decidir si se insertan
                //0's al inicio pj:
                //patente = "323" -> FormatearPatente("323") => patente.prepend("0) : patente = "0323"
                Console.WriteLine(patente);
            }
            return patente;
        }
        public Boolean Exist(string sRutaBse)
        {
            return Directory.Exists(sRutaBse);
        }
        public string GetPattern(string patente, string numeroPedimento)
        {
            return patente + "-" + numeroPedimento + "*";
        }
        public string CombinePaths(string rutaBase, string pattern)
        {
            return Path.Combine(rutaBase, pattern);
        }
        public string CopyFrom(string sRutaCompleta)
        {
            oReader = new StreamReader(sRutaCompleta);
            return oReader.ReadToEnd();
        }
    }
}
