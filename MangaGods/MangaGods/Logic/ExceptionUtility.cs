using System;
using System.IO;
using System.Web;

namespace MangaGods.Logic
{
    //Maneja el registro de la excepciones de la app
    public sealed class ExceptionUtility
    {
        // Todos los metodos son státicos, asi que puede ser privado
        private ExceptionUtility()
        { }

        /// <summary>
        /// Guarda en el log de errores cuando se generan excepciones
        /// </summary>
        /// <param name="exc"></param>
        /// <param name="source"></param>
        public static void LogException(Exception exc, string source)
        {
            try
            {
                // Guarda el log en la carpeta App_Data
                //var logFile = "App_Data/ErrorLog.txt";
                //logFile = HttpContext.Current.Server.MapPath(logFile);
                var logFile = Path.Combine(HttpContext.Current.Server.MapPath("~"), "App_Data/ErrorLog.txt");
                // Abre  el archivo y registra el error capturado
                using (var sw = new StreamWriter(logFile, true))
                {
                    sw.WriteLine($"********** {DateTime.Now} **********");
                    if (exc.InnerException != null)
                    {
                        sw.Write("Inner Exception Type: ");
                        sw.WriteLine(exc.InnerException.GetType().ToString());
                        sw.Write("Inner Exception: ");
                        sw.WriteLine(exc.InnerException.Message);
                        sw.Write("Inner Source: ");
                        sw.WriteLine(exc.InnerException.Source);
                        if (exc.InnerException.StackTrace != null)
                        {
                            sw.WriteLine("Inner Stack Trace: ");
                            sw.WriteLine(exc.InnerException.StackTrace);
                        }
                    }
                    sw.Write("Exception Type: ");
                    sw.WriteLine(exc.GetType().ToString());
                    sw.WriteLine("Exception: " + exc.Message);
                    sw.WriteLine("Source: " + source);
                    sw.WriteLine("Stack Trace: ");
                    if (exc.StackTrace != null)
                    {
                        sw.WriteLine(exc.StackTrace);
                        sw.WriteLine();
                    }
                    sw.Close();
                }
            }
            catch (IOException)
            {
                throw new NullReferenceException(
                    HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorArchivoNoEncontrado")?.ToString());
            }
            catch (ObjectDisposedException)
            {
                throw new NullReferenceException(
                    HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorDespachoArchivo")?.ToString());
            }
            catch (Exception)
            {
                throw new Exception(
                    HttpContext.GetGlobalResourceObject("RecursosMangaGods", "ErrorEscrituraLog")?.ToString());
            }
        }
    }
}