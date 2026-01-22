using System;
using System.Collections.Generic;
using System.Text;
using Telerik.XamarinForms.Common.DataAnnotations;
using Common.Models;

namespace LvsScan.Portable.Models
{
    public class InventoryArticlePageViewCls
    {
        [DisplayOptions(Header = "ArtikelId")]
        [ReadOnly]
        public int ArtikelId
        {
            get { return InventoryArticle.Artikel.Id; }
        }
        [DisplayOptions(Header = "LvsNr")]
        [ReadOnly]
        public int LvsNr
        {
            get { return InventoryArticle.Artikel.LVS_ID; }
        }

        [DisplayOptions(Header = "Produktionsnummer")]
        [ReadOnly]
        public string Produktionsnummer
        {
            get { return InventoryArticle.Artikel.Produktionsnummer; }
        }

        [DisplayOptions(Header = "Werksnummer")]
        [ReadOnly]
        public string Werksnummer
        {
            get { return InventoryArticle.Artikel.Werksnummer; }
        }

        [DisplayOptions(Header = "Brutto KG")]
        [Ignore]
        public decimal Brutto
        {
            get { return InventoryArticle.Artikel.Brutto; }
        }

        [DisplayOptions(Header = "Werk")]
        [Ignore]
        public string Werk
        {
            get { return InventoryArticle.Artikel.Werk; }
        }
        [DisplayOptions(Header = "Halle")]
        [Ignore]
        public string Halle
        {
            get { return InventoryArticle.Artikel.Halle; }
        }

        [DisplayOptions(Header = "Reihe")]
        [Ignore]
        public string Reihe
        {
            get { return InventoryArticle.Artikel.Reihe; }
        }

        [DisplayOptions(Header = "Ebene")]
        [Ignore]
        public string Ebene
        {
            get { return InventoryArticle.Artikel.Ebene; }
        }

        [DisplayOptions(Header = "Platz")]
        [Ignore]
        public string Platz
        {
            get { return InventoryArticle.Artikel.Platz; }
        }

        [Ignore]
        public InventoryArticles InventoryArticle { get; set; }

        public InventoryArticlePageViewCls Copy()
        {
            return (InventoryArticlePageViewCls)MemberwiseClone();
        }
    }
}
