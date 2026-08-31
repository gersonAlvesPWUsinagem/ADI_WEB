using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Helpers;

public static class Servidores
{
    // Dicionário com todos os seus endereços
    private static readonly Dictionary<string, string> Lista = new Dictionary<string, string>
        {
            { "REDE ZANE",      "http://192.168.0.12:5253/api/" },
            { "REDE PAI",       "http://192.168.15.5:5253/api/" },
            { "IP CASA",        "http://192.168.0.30:5253/api/" },
            { "REDE VIZINHO",   "https://192.168.15.26:7287/api/" },
            { "SERVER MEU PW",  "http://192.168.1.252:5253/api/" },
            { "SERVER MEU PW NEW NIP",  "https://192.168.1.252:7287/api/" },
            { "PRODUÇÃO",       "http://192.168.0.235:5253/api/" },


            { "PRODUÇÃO 2",     "https://192.168.0.235:5260/api/" },
            { "IP CASA 2",        "https://192.168.0.30:7287/api/" },
        };

#if DEBUG
    // Obtém a URL base definida no seu dicionário de Servidores
     //private const string ChaveWindows = "IP CASA 2";
     //private const string ChaveWindows = "REDE VIZINHO";
  private const string ChaveWindows = "SERVER MEU PW NEW NIP";
#else
        //private const string ChaveWindows = "IP CASA 2";
        private const string ChaveWindows = "PRODUÇÃO 2";
#endif
    // Altere APENAS estas chaves quando mudar de ambiente
    //private const string ChaveAndroid = "REDE ZANE";
    //private const string ChaveWindows = "SERVER MEU PW";
    //private const string ChaveWindows = "REDE VIZINHO";
    // private const string ChaveWindows = "IP CASA 2";
    //private const string ChaveWindows = "PRODUÇÃO 2";
    // private const string ChaveWindows = "SERVER MEU PW NEW NIP";

    public static string GetBaseUrl()
    {
        return Lista[ChaveWindows];
    }
}