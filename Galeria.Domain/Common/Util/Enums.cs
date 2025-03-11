namespace Galeria.Domain.Common.Util
{
    public class Enums
    {
        public enum Sexo
        {
            NO_ESPECIFICADO,
            MASCULINO,
            FEMENINO,
            OTRO
        }

        public enum  DiasSemana 
        {
            LUNES, MARTES, MIERCOLES, JUEVES, VIERNES, SABADO, DOMINGO
        }

        public enum EstatusPago
        {
            RECHAZADO, APROBADO, NO_PAGADO, PAGO_CANCELADO
        }
    }
}
