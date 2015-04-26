namespace SimpleStock.Downloader
{
    class Program
    {
        static void Main()
        {
            var downloader = new Downloader();
            downloader.DownloadStock();
        }
    }
}
