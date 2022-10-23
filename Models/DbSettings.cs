namespace PruebaTecnicaMasiv.Models
{
    public class DbSettings : IDbSettings
    {
        public string Server { get; set; }
        public string Database { get; set; }
    }
    public interface IDbSettings
    {
        string Server { get; set; }
        string Database { get; set; }
    }
}
