using System;
using System.IO;

namespace LiveStreamMusic.Business.Utils
{
    public static class BusinessFile
    {
        public static void MoverTemporales(string folderOrigen, string folderDestino, string archivo)
        {
            try
            {
                if (File.Exists(folderDestino + archivo))
                    File.Delete(folderDestino + archivo);
                File.Move(folderOrigen + archivo, folderDestino + archivo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void CreaRepositorio(string correo)
        {
            try
            {
                Directory.CreateDirectory(BusinessVariables.Directorios.Repositorio + correo);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static void LimpiarTemporales(string archivo)
        {
            try
            {
                File.Delete(BusinessVariables.Directorios.RepositorioTemporal + archivo);

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
