using MauiAppMinhaLista.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhaLista.Views;

public partial class ListaProduto : ContentPage
{
    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();

    public ListaProduto()
    {
        InitializeComponent();
        BindingContext = this; // Adicionei para o databinding funcionar corretamente
        lst_produtos.ItemsSource = lista;
    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();
        lista = new ObservableCollection<Produto>(tmp); // Inicializa com todos os produtos.
        lst_produtos.ItemsSource = lista; // Atualiza a fonte de dados
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());
        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }
    }

    private async void txt_search_TextChanged(object sender, TextChangedEventArgs e)
    {
        string q = e.NewTextValue?.ToLower() ?? ""; // Adiciona tratamento para nulos

        // Usa LINQ para filtrar a lista EXISTENTE sem limpar a lista toda.
        lst_produtos.ItemsSource = lista.Where(p => p.Descricao.ToLower().Contains(q)).ToList();

        // Mostra mensagem se não há resultados
        if (lst_produtos.ItemsSource.Cast<object>().Count() == 0)
        {
            await DisplayAlert("Busca", "Nenhum produto encontrado.", "OK");
        }
    }

    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);
        string msg = $"O total é {soma:C}";
        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            MenuItem selecionado = sender as MenuItem;
            Produto p = selecionado.BindingContext as Produto;
            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não"
                );
            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
        catch (Exception ex)
        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }
    }
}