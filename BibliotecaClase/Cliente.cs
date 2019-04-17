using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BibliotecaClase
{
    public enum ActividadEmpresa
    {
        Agropecuaria, Mineria, Manufactura, Comercio, Hoteleria,
        Alimentos, Transporte, Servicios
    }

    public enum TipoEmpresa
    {
        SPA, EIRL, Limitada, Sociedad_Anonima
    }
    public class Cliente
    {
        private String _rut;

        public String Rut
        {
            get { return _rut; }
            set
            {
                if (value != null && value.Length>=9 || value.Length<=10)
                {
                    //validar rut
                    if (value.Length==9)
                    {
                        _rut = 0 + _rut;
                    }
                                        
                    _rut = _rut.ToUpper();
                    _rut = _rut.Replace("-", "");


                    _rut = value;

                    

                }
                else
                {
                    throw new ArgumentException("ERROR: Campo Rut no puede estar Vacío");
                }
            }
        }

        private String _razonSocial;

        public String RazonSocial
        {
            get { return _razonSocial; }
            set
            {
                if (value != null)
                {
                    _razonSocial = value;
                }
                else
                {
                    throw new ArgumentException("ERROR: Campo RazónSocial no puede estar Vacío");
                }

            }
        }

        private String _nombreContacto;

        public String NombreContacto
        {
            get { return _nombreContacto; }
            set
            {
                if (value != null)
                {
                    _nombreContacto = value;
                }
                else
                {
                    throw new ArgumentException("ERROR: Campo Nombre Contrato no puede estar Vacío");
                }

            }
        }

        private String _mailContacto;

        public String Mail
        {
            get { return _mailContacto; }
            set
            {
                if (value != null)
                {
                    _mailContacto = value;
                }
                else
                {
                    throw new ArgumentException("ERROR: Campo Email no puede estar Vacío");
                }
            }
        }

        private String _direccion;

        public String Direccion
        {
            get { return _direccion; }
            set
            {
                if (value != null)
                {
                    _direccion = value;
                }
                else
                {
                    throw new ArgumentException("ERROR: Campo Dirección no puede estar Vacío");
                }
            }
        }

        private int _telefono;

        public int Telefono
        {
            get { return _telefono; }
            set
            {
                if (value != 0)
                {
                    _telefono = value;
                }
                else
                {
                    throw new ArgumentException("ERROR: Campo Teléfono no puede estar Vacío");
                }

            }
        }

        private TipoEmpresa _emp;

        public TipoEmpresa Empresa
        {
            get { return _emp; }
            set { _emp = value; }
        }

        private ActividadEmpresa _act;

        public ActividadEmpresa Actividad
        {
            get { return _act; }
            set { _act = value; }
        }

        public Cliente()
        {

        }

        public Cliente(string rut, string razonSocial, string nombreContacto, string mail, string direccion, int telefono, TipoEmpresa empresa, ActividadEmpresa actividad)
        {
            Rut = rut;
            RazonSocial = razonSocial;
            NombreContacto = nombreContacto;
            Mail = mail;
            Direccion = direccion;
            Telefono = telefono;
            Actividad = actividad;
            Empresa = empresa;
        }

       
        
        
        
    }
}