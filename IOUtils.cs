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
    public class IOUtils
    {
        StreamReader oReader;
        StringReader srReader;
        Encoding nCodificacion;
        Boolean bEncontrado;
        string sRenglon, sResult;
        string[] aCamposRenglon;
        char[] aSeparador;
        decimal nValorMercancia;
        public IOUtils()
        {
            this.bEncontrado = false;
            this.sResult = "";
            this.aSeparador = new char[1];
            this.nCodificacion = Encoding.Default;
        }
        public string GetTag(string crp, string tag,char separador,int posicion)
        {
            //Validar con exportación! -> ok
            this.bEncontrado = false;
            //Agregar un try catch para ArgumentNullException
            try
            {
                this.srReader = new StringReader(crp);
            }
            catch (ArgumentNullException e)
            {
                Console.WriteLine(e.Message);
            }
            
            this.sRenglon = this.srReader.ReadLine();
            this.aSeparador[0] = separador;
            while(!this.bEncontrado && this.sRenglon != null)
            {
                if (this.sRenglon.Contains(tag))
                {
                    this.bEncontrado = true;
                    this.aCamposRenglon = this.sRenglon.Split(this.aSeparador);
                    this.sResult = this.aCamposRenglon[posicion];
                }
                else
                {
                    this.sRenglon = this.srReader.ReadLine();
                }
            }
            return this.sResult.Trim();
        }
        /// <summary>
        /// Validar si es necesario implementar el formateo
        /// </summary>
        /// <param name="patente"></param>
        /// <returns></returns>
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
        public Boolean Exist(string rutaBse)
        {
            return Directory.Exists(rutaBse);
        }
        public string PathCombine(string rutaBase, string pattern)
        {
            return Path.Combine(rutaBase, pattern);
        }
        public string CopyFrom(string rutaCompleta)
        {
            this.oReader = new StreamReader(rutaCompleta, nCodificacion);
            return this.oReader.ReadToEnd();
        }
        public decimal TryParse(string valorMercancia)
        {
            decimal.TryParse(valorMercancia,out nValorMercancia);
            return nValorMercancia;
        }
        //Metodo que obtiene todos los CRPs en la ruta base
        public string[] GetAllCRPs(string rutaBase)
        {
            return Directory.GetFiles(rutaBase);
        }
    }
}
