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
        string sTagTipoDocumento = Settings.Default.sTagTipoDocumento;
        int nPosicionTipoDocumento = Settings.Default.nPosicionTagTipoDocumento;
        string sTagTipoOperacionAduanera = Settings.Default.sTagTipoOperacionAduanera;
        int nPosicionTipoOperacionAduanera = Settings.Default.nPosicionTagTipoOperacionAduanera;
        string sTagValorAduana = Settings.Default.sTagValorAduana;
        int nPosicionValorAduana = Settings.Default.nPosicionTagValorAduana;
        string sTagValorComercial = Settings.Default.sTagValorComercial;
        int nPosicionComercial = Settings.Default.nPosicionTagValorComercial;
        int nValorMercanciaPrevioDeConso = Settings.Default.nValorMercanciaPrevioDeConso;
        string[] aCamposRenglon;
        char[] aSeparador = {Settings.Default.cSeparadorDosPuntos,' '};
        decimal nValorMercancia;
        private Pedimento oPedimento;
        private CRP oCRP;
        List<Pedimento> LPedimentos;
        public IOUtils()
        {
            this.bEncontrado = false;
            this.sResult = "";
            //this.aSeparador = new char[1];
            this.nCodificacion = Encoding.Default;
            LPedimentos = new List<Pedimento>();
        }
        public List<Pedimento> SetPedimento(string patente, string numeroPedimento)
        {
            oPedimento = new Pedimento();
            oPedimento.SetPatente(patente);
            oPedimento.SetPedimento(numeroPedimento);
            oCRP = new CRP();
            oCRP.SetRutaBase();
            oCRP.SetPattern(oPedimento.GetPatente(), oPedimento.GetPedimento());
            oCRP.SetRutaCompleta(oCRP.GetPattern());
            oCRP.SetCopia(CopyFrom(oCRP.GetRutaCompleta()));
            oPedimento.SetTipoDocumento(GetTag(oCRP.GetCopia(), sTagTipoDocumento, aSeparador[0], nPosicionTipoDocumento));
            oPedimento.SetOperacionAduanera(GetTag(oCRP.GetCopia(), sTagTipoOperacionAduanera, aSeparador[0], nPosicionTipoOperacionAduanera));
            if (oPedimento.GetTipoDocumento().Equals("NORMAL"))
            {
                if (oPedimento.GetOperacionAduanera().Equals("IMPORTACIÓN"))
                {
                    oPedimento.SetValorMercancia(DecimalTryParse(GetTag(oCRP.GetCopia(), sTagValorAduana, aSeparador[0], nPosicionValorAduana)));

                }
                else
                {
                    oPedimento.SetValorMercancia(DecimalTryParse(GetTag(oCRP.GetCopia(), sTagValorComercial, aSeparador[1], nPosicionComercial)));
                }
            }
            else // Previo de consolidado
            {
                oPedimento.SetValorMercancia(nValorMercanciaPrevioDeConso);
            }
            LPedimentos.Add(oPedimento);
            return LPedimentos;

        }
        public Pedimento[] GetPedimentos(string patente, string numeroPedimento)
        {
            bEncontrado = false;
            Pedimento[] aPedimentos=null;
            //oPedimento = new Pedimento();
            oCRP = new CRP();
            oCRP.SetRutaBase();
            oCRP.SetPattern(patente,numeroPedimento);
            string[] aRutas = GetRutas(oCRP.GetRutaBase(), oCRP.GetPattern());
            string[] aCRP = new string[aRutas.Length];
            aPedimentos = new Pedimento[aRutas.Length];
            for (int i=0;i<aRutas.Length;i++)
            {
                oPedimento = new Pedimento();
                oPedimento.SetPatente(patente);
                oPedimento.SetPedimento(numeroPedimento);
                aCRP[i] = CopyFrom(aRutas[i]);
                oCRP.SetCopia(aCRP[i]);
                oPedimento.SetTipoDocumento(GetTag(oCRP.GetCopia(),"Tipo Doc",':',1));
                oPedimento.SetOperacionAduanera(GetTag(oCRP.GetCopia(), "Tipo de ope", ':', 2));
                if (oPedimento.GetTipoDocumento().Equals("NORMAL"))
                {
                    if (oPedimento.GetOperacionAduanera().Equals("IMPORTACIÓN"))
                    {
                        oPedimento.SetValorMercancia(DecimalTryParse(GetTag(oCRP.GetCopia(),"Valor Aduana",' ',3)));
                        
                    }
                    else // Exportacion
                    {
                        oPedimento.SetValorMercancia(DecimalTryParse(GetTag(oCRP.GetCopia(), "Valor comercial", ' ', 25)));
                    }
                }
                else //Previo de consolidado
                {
                    oPedimento.SetValorMercancia(1);
                }
                //Termina if Normal
                aPedimentos[i] = oPedimento;
            }
            return aPedimentos;
        }
        /// <summary>
        /// Metodo que busca un patter en la ruta base
        /// incluidos todos los sub-directorios
        /// </summary>
        /// <param name="rutaBase"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public string[] GetRutas(string rutaBase,string pattern)
        {
            return Directory.GetFiles(rutaBase,pattern,SearchOption.AllDirectories);
        }
        public decimal DecimalTryParse(string valorMercancia)
        {
            decimal.TryParse(valorMercancia, out decimal nValorMercancia);
            return nValorMercancia;
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
        //Redefinir como boolean 
        public Boolean isValid(string valorMercancia)
        {
            
            return decimal.TryParse(valorMercancia, out nValorMercancia);
        }
        public decimal TryParse(string valorMercancia)
        {
            decimal.TryParse(valorMercancia,out nValorMercancia);
            return nValorMercancia;
        }
        
        public void BenchmarkGetAllCRPs()
        {
            _ = GetAllCRPs(@"C:\Users\jaguilar\Documents\CRPs");
        }

        //Metodo que obtiene todos los CRPs en la ruta base
        public string[] GetAllCRPs(string rutaBase)
        {
            return Directory.GetFiles(rutaBase);
        }
    }
}
