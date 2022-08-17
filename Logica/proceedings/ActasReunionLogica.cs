using Entidades.Generales;
using Entidades.PlanAccion;
using Persistencia.Generales;
using Persistencia.proceedings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Persistencia.proceedings;

namespace Logica.proceedings
{
    [Serializable]
    public class ActasReunionLogica 
    {

        #region METODOS QUE AFECTAN A LA TABLA ARActasC
        public bool CargarActa(ARActasC acta)
        {
            DAOARActasC Di = new DAOARActasC();
            return Di.set(acta);
        }

        public int CargarActa2(ARActasC acta)
        {
            DAOARActasC Di2 = new DAOARActasC();
            return Di2.set2(acta);
        }

        public ARActasC GetARActasCUltima(int id)
        {
            DAOARActasC DI = new DAOARActasC();
            return DI.setUltimo(id);
        }

        public void updateARActasc(ARActasC acta)
        {
            
            DAOARActasC DI = new DAOARActasC();
            //DI.update(acta);
        }
        public ARActasC getActasC(int id)
        {
           

            return DAOARActasC.get(id);
        }
        #endregion

        #region METODOS QUE AFECTAN A LA TABLA ARActasTemas
        public bool CargarTema(ARActasTemas tema)
        {
            DAOARActasTemas DI = new DAOARActasTemas();
            return DI.set(tema);
        }
        public ARActasTemas GetARActasTemaUltimo()
        {
            DAOARActasTemas DI = new DAOARActasTemas();
            return DI.setUltiomo();
        }
        public List<ARActasTemas> GetARActasTemas(int id)
        {
            DAOARActasTemas DI = new DAOARActasTemas();
            return DI.Listar(id);
        }
        public ARActasTemas GetActasTema(int id)
        {
            DAOARActasTemas DI = new DAOARActasTemas();
            return DI.Get(id);
        }

        public void updateARActasTemas(string desarrollo, string adjuntar, string nomTema, int oidARActasTemas, int oidARActasC, int OdiGNListaArchivos)
        {
            ARActasTemas aRActasTemas = new ARActasTemas
            {
                StrDesarrollo = desarrollo,
                StrAdjuntar = adjuntar,
                StrNomTema = nomTema,
                IntOidARActasTemas = oidARActasTemas,
                IntOidARActasC = oidARActasC,
                IntOidGNListaArchivos = OdiGNListaArchivos
            };
            DAOARActasTemas DI = new DAOARActasTemas();
            DI.update(aRActasTemas);
        }
        public void updateARActasTemas(ARActasTemas aRActasTemas)
        {
            
            DAOARActasTemas DI = new DAOARActasTemas();
            DI.update(aRActasTemas);
        }

        public void deleteTema(int id)
        {
            DAOARActasTemas DI = new DAOARActasTemas();
            DI.delete(id);
        }
        #endregion

        #region METODOS QUE AFECTAN A LA TABLA AReunionC
        public List<AReunionC> GetAReunionCs(int id)
        {
            DAOAReunionC Di = new DAOAReunionC();
            return Di.Listar(id);
        }

        public void setAReunionC(AReunionC data)
        {
            DAOAReunionC DI = new DAOAReunionC();
            DI.set(data);
        }

        public void updateComite(AReunionC comite)
        {
            DAOAReunionC DI = new DAOAReunionC();
            DI.update(comite);
        }
        

        public AReunionC GetAReunionC(int id)
        {
            DAOAReunionC DI = new DAOAReunionC();
            return DI.Get(id);
        }
        #endregion

        #region METODOS QUE AFECTAN LAS TABLAS DE LOS USUARIOS QUE PARTICIPAN EL ACTA
        
        public List<Usuario> getUSuariosAsistentes(int id)
        {
            return DAOUsuario.getUsuariosAsistentes(id);
        }

        public void deleteUsuarioMiembro(int id)
        {
            DAOAReunionD.getInstance().delete(id);
        }

        public void SetMiembro()
        {

        }


        #endregion

        #region METODOS QUE AFECTAN A LA TABLA ARActasCompromisos
        public void setActasCompromisos()
        {
            
        }
        #endregion

        #region METODOS QUE AFECTAN LA TABLA USUARIOS

        public List<UsuariosParticipantes> GetUsuariosParticipantes(int id)
        {
            return DAOAReunionD.GetUsuariosParticipantes(id);
        }
        public List<Usuario> GetUsuarios()
        {
            return DAOUsuario.getUsuarios();
        }

        public Usuario GetUsuario(int id)
        {
            return DAOUsuario.getInstance().GetUsuario(id);
        }

        #endregion

        #region METODOS QUE AFECTAN A LA TABLA DE UNIDADES FUNCIONALES

        public List<UnidadFuncional> GetUnidadFuncionales()
        {
            return DAOUnidadFuncional.GetInstance().listar();
        }

        #endregion

        #region METODOS QUE AFECTAN A LA TABLA ARAgenda

        public void SetARAgenda(ARAgenda data)
        {
            DAOARAgenda.Set(data);
        }

        public List<ARAgenda> listarAgenda(int id)
        {
            return DAOARAgenda.GetInstance().listar(id);
        }

        public void deleteAgenda(int id)
        {
            DAOARAgenda.GetInstance().delete(id);
        }

       
        #endregion
    }
}