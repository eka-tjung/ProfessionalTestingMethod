using System;
using System.IO;
using System.Net;
using System.Text;

namespace CLAssistant
{
    public partial class MainWindow
    {
        void ShowProgress(int progress)
        {
            ResultsTextbox.AppendText(".");
            if (0 < progress)
            {
                ResultsTextbox.AppendText(progress + "\n\r");
            }
        }

        string GetWebPage(string inputURL)
        {
            HttpWebRequest request = WebRequest.Create(inputURL) as HttpWebRequest;
            request.KeepAlive = false;

            HttpWebResponse response = null;
            Stream responseStream = null;

            string result = string.Empty;
            try
            {
                response = request.GetResponse() as HttpWebResponse;
                responseStream = response.GetResponseStream();
                int bufferCount = 0;
                byte[] byteBuffer = new byte[1024];
                int size = Convert.ToInt32(response.ContentLength);
                int downloaded = 0;
                int progress = 0;

                do
                {
                    if (worker.CancellationPending)
                    {
                        break;
                    }

                    bufferCount = responseStream.Read(byteBuffer, 0, byteBuffer.Length);
                    if (0 < bufferCount)
                    {
                        string line = Encoding.ASCII.GetString(byteBuffer, 0, bufferCount);
                        result += line;
                        if (0 < size)
                        {
                            downloaded += byteBuffer.Length;
                            progress = Convert.ToInt32(((decimal)downloaded / (decimal)size) * 100);
                        }
                        worker.ReportProgress(progress);
                    }
                } while (0 < bufferCount);
            }
            //catch (IOException ioEx)
            //{

            //}
            finally
            {
                response.Close();
                responseStream.Close();
            }
            return result;
        }
    }
}
