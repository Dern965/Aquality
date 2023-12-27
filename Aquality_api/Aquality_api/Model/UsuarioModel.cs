namespace Aquality_api.Model
{
    public class UsuarioModel
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public string first_Name { get; set; }
        public string last_Name { get; set; }
        public int birth_Day { get; set; }
        public int birth_Month { get; set; }
        public int birth_Year { get; set; }
        public string idHistorial { get; set; }

        //public HistorialModel idHistorial { get; set; }
    }
}
