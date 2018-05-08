using System.Configuration;

namespace LiveStreamMusic.Business.Utils
{
    public class BusinessVariables
    {
        public static class Directorios
        {
            public static string Repositorio = ConfigurationManager.AppSettings["Repositorio"];
            public static string RepositorioTemporal = ConfigurationManager.AppSettings["RepositorioTemporal"];
        }
        public static class ComboBoxCatalogo
        {
            public static int IndexSeleccione = 0;
            public static int IndexTodos = 1;
            public static int ValueSeleccione = 0;
            public static int ValueTodos = -1;
            public static string DescripcionSeleccione = "==SELECCIONE==";
            public static string DescripcionTodos = "==TODOS==";
            public static bool Habilitado = false;
        }

        public static class EnumeradoresStreaming
        {
            public enum EnumTipoLink
            {
                Confirmacion = 1,
                Reset = 2
            }

            public enum EnumTipoCorreo
            {
                AltaUsuario = 1,
                RecuperarCuenta = 2
            }

            public enum EnumTipoIncidencia
            {
                GeneroIncorrecto = 1,
                ArtistaIncorrecto = 2,
                AlbumIncorrecto = 3,
                CancionConMalAudio = 4,
                CancionConNombreIncorrecto = 5,
                CancionConMalFormato = 6
            }
        }
    }
}