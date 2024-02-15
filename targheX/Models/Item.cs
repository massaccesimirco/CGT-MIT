using System.ComponentModel.DataAnnotations;

namespace targheX.Models
{
    public class Item
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        // Giacenza iniziale dell'anno
        public int Giacenza { get; set; }


        // Movimenti per ogni mese
        [Display(Name = "Gennaio carico")]
        public int GennaioCarico { get; set; }
        [Display(Name = "Gennaio scarico")]
        public int GennaioScarico { get; set; }
        [Display(Name = "Febbraio carico")]
        public int FebbraioCarico { get; set; }
        [Display(Name = "Febbraio scarico")]
        public int FebbraioScarico { get; set; }
        [Display(Name = "Marzo carico")]
        public int MarzoCarico { get; set; }
        [Display(Name = "Marzo scarico")]
        public int MarzoScarico { get; set; }
        [Display(Name = "Aprile carico")]
        public int AprileCarico { get; set; }
        [Display(Name = "Aprile scarico")]
        public int AprileScarico { get; set; }
        [Display(Name = "Maggio carico")]
        public int MaggioCarico { get; set; }
        [Display(Name = "Maggio scarico")]
        public int MaggioScarico { get; set; }
        [Display(Name = "Giugno carico")]
        public int GiugnoCarico { get; set; }
        [Display(Name = "Giugno scarico")]
        public int GiugnoScarico { get; set; }
        [Display(Name = "Luglio carico")]
        public int LuglioCarico { get; set; }
        [Display(Name = "Luglio scarico")]
        public int LuglioScarico { get; set; }
        [Display(Name = "Agosto carico")]
        public int AgostoCarico { get; set; }
        [Display(Name = "Agosto scarico")]
        public int AgostoScarico { get; set; }
        [Display(Name = "Settembre carico")]
        public int SettembreCarico { get; set; }
        [Display(Name = "Settembre scarico")]
        public int SettembreScarico { get; set; }
        [Display(Name = "Ottobre carico")]
        public int OttobreCarico { get; set; }
        [Display(Name = "Ottobre scarico")]
        public int OttobreScarico { get; set; }
        [Display(Name = "Novembre carico")]
        public int NovembreCarico { get; set; }
        [Display(Name = "Novembre scarico")]
        public int NovembreScarico { get; set; }
        [Display(Name = "Dicembre carico")]
        public int DicembreCarico { get; set; }
        [Display(Name = "Dicembre scarico")]
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
