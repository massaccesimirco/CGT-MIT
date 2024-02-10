using System.ComponentModel.DataAnnotations;

namespace targheX.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Name { get; set; }

        // Giacenza iniziale dell'anno
        public int Giacenza { get; set; }
        

        // Movimenti per ogni mese
        public int GennaioCarico { get; set; }
        public int GennaioScarico { get; set; }
        public int FebbraioCarico { get; set; }
        public int FebbraioScarico { get; set; }
        public int MarzoCarico { get; set; }
        public int MarzoScarico { get; set; }
        public int AprileCarico { get; set; }
        public int AprileScarico { get; set; }
        public int MaggioCarico { get; set; }
        public int MaggioScarico { get; set; }
        public int GiugnoCarico { get; set; }
        public int GiugnoScarico { get; set; }
        public int LuglioCarico { get; set; }
        public int LuglioScarico { get; set; }
        public int AgostoCarico { get; set; }
        public int AgostoScarico { get; set; }
        public int SettembreCarico { get; set; }
        public int SettembreScarico { get; set; }
        public int OttobreCarico { get; set; }
        public int OttobreScarico { get; set; }
        public int NovembreCarico { get; set; }
        public int NovembreScarico { get; set; }
        public int DicembreCarico { get; set; }
        public int DicembreScarico { get; set; }

        //Rimanenza a fine anno
        public int Rimanenza { get; set; }

        // Totale di carico
        public int TotaleCarico { get; set; }

        // Totale di scarico
        public int TotaleScarico { get; set; }

        // Totale
        public int Totale { get; set; }


    }
}
