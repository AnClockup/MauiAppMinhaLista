﻿using SQLite;

namespace MauiAppMinhaLista.Models
{
    public class Produto
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao { get; set; }
        public double Quantidade { get; set; }
        public double Preco { get; set; }

        public double Total { get => Quantidade * Preco; }

        public string Categoria { get; set; } // Adiciona o campo Categoria
    }
}
