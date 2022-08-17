using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Persistencia
{
    public class Mensaje <T>
    {
        private List<T> data;
        private string messaje;

        public List<T> Data { get => data; set => data = value; }
        public string Messaje { get => messaje; set => messaje = value; }
    }
}