namespace AspNetCore_QuartzExample.Models
{
    public static class Common
    {
        public static void Logs(string message, string filenName)
        {
            var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "MyLogs");

            if (!Directory.Exists(path)) // Belirtilen dizinin var olup olmadığını kontrol ediyor.
            {
                Directory.CreateDirectory(path); // Eğer belirtilen dizin yoksa oluşturuyor.
            }

            path = Path.Combine(path, filenName);

            using FileStream stream = new FileStream(path, FileMode.Create);
            using TextWriter textWriter = new StreamWriter(stream);

            textWriter.WriteLine(message);
        }
    }
}
