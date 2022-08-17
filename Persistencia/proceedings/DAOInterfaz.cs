using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistencia.proceedings
{
    interface DAOInterfaz <T>
    {
        List<T> Listar(int id);
        T  Get(int id);

        bool set(T data);

        bool set(List<T> data);

        bool update(int id);

        bool update(List<T> data);

        T setUltiomo();
    }
}
