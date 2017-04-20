using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SearchTelefonos
{
    public enum ParamSerach
    {
        /// <summary>
        /// RUC o Dni
        /// </summary>
        ByDocument,
        /// <summary>
        /// Nombre o Razon Social
        /// </summary>
        ByNames
    }

    public interface IPhoneSearch
    {
        /// <summary>
        /// Dominio de la Pagina Web
        /// </summary>
        string Domain { get; }

        /// <summary>
        /// Nombre abreviado de la Pagina.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Realiza la busqueda de telefonos.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEnumerable<string> SearchTelefonos(string value);

        /// <summary>
        /// Obtiene los metodos que soporta la página.
        /// </summary>
        ParamSerach[] Support { get; }
    }
}
