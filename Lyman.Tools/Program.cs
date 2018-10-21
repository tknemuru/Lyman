using System;
using Lyman.Di;

namespace Lyman.Tools
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                DiProvider.GetContainer().GetInstance<TilesImageTrimmer>().Execute();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.StackTrace);
                Console.ReadKey();
            }
        }
    }
}
