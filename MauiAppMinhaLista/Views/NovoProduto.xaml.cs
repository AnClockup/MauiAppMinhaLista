using MauiAppMinhaLista.Models;

namespace MauiAppMinhaLista.Views;

public partial class NovoProduto : ContentPage
{
    public NovoProduto()
    {
        InitializeComponent();
        // Preenche o Picker com as categorias:
        categoriaPicker.ItemsSource = new List<string> { "Alimentos", "Higiene", "Limpeza" };
    }
    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            string selectedCategory = categoriaPicker.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(selectedCategory))
            {
                await DisplayAlert("Erro", "Selecione uma categoria.", "OK");
                return;
            }

            Produto p = new Produto
            {
                Descricao = txt_descricao.Text,
                Quantidade = Convert.ToDouble(txt_quantidade.Text),
                Preco = Convert.ToDouble(txt_preco.Text),
                Categoria = selectedCategory // <-- Atribui a categoria selecionada
            };

            await App.Db.Insert(p);
            await DisplayAlert("Sucesso!", "Registro Inserido", "Ok");
            await Navigation.PopAsync(); // Volta para a lista após inserir

        }
        catch (Exception ex)

        {
            await DisplayAlert("Ops", ex.Message, "OK");
        }

    }
}
