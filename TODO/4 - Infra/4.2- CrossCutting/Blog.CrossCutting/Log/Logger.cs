using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Todo.CrossCutting.Log
{
    public class Logger : ILogger
    {
        private readonly LoggerOptions _logOptions;

        public Logger(LoggerOptions logOptions)
        {
            _logOptions = logOptions;
        }
        public void writeLog(Exception excecao)
        {
            string caminhoArquivoLog = _logOptions.TargetPath + DateTime.Now.ToString("ddMMyyyyhhmmssff") + "_" + Guid.NewGuid() + ".txt";

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog))
            {
                streamWriter.WriteLine(excecao.Message);
                streamWriter.WriteLine(excecao.StackTrace);
                streamWriter.Close();
            }
        }
    }
}
