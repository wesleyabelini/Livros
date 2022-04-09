using System;
using System.Collections.Generic;

#nullable disable

namespace LivrosBot.Models
{
    public partial class Book
    {
        public int Id { get; set; }
        public string NomeLivro { get; set; }
        public string NomeAutor { get; set; }
        public string Descricao { get; set; }
        public string Categoria { get; set; }
        public string UrlCapa { get; set; }
        public string UrlBook { get; set; }
        public string Matrix { get; set; }
    }
}
