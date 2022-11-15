using System;
using System.ComponentModel;
using System.Threading.Tasks;
using Xamarin.Forms;
using XF_CrudFirebase.Services;

namespace XF_CrudFirebase
{
    public partial class MainPage : ContentPage
    {
       
        FirebaseService fbService = new FirebaseService();
        public MainPage()
        {
            InitializeComponent();
        }
        protected async override void OnAppearing()
        {
            base.OnAppearing();
            var contatos = await fbService.GetContatos();
            listaContatos.ItemsSource = contatos;
        }
        private async void BtnIncluir_Clicked(object sender, System.EventArgs e)
        {
            await fbService.AddContato(Convert.ToInt32(txtId.Text), txtNome.Text, txtEmail.Text);

            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            await DisplayAlert("Success", "Contato incluído com sucesso", "OK");
            var contatos = await fbService.GetContatos();
            listaContatos.ItemsSource = contatos;
        }
        private async void BtnExibir_Clicked(object sender, System.EventArgs e)
        {
            var contato = await fbService.GetContato(Convert.ToInt32(txtId.Text));
            if (contato != null)
            {
                txtId.Text = contato.ContatoId.ToString();
                txtNome.Text = contato.Nome;
                txtEmail.Text = contato.Email;
            }
            else
            {
                await DisplayAlert("Success", "Não existe contato com esse ID", "OK");
            }
        }
        private async void BtnDeletar_Clicked(object sender, System.EventArgs e)
        {
            await fbService.DeleteContato(Convert.ToInt32(txtId.Text));
            txtId.Text = string.Empty;
            await DisplayAlert("Success", "Contato deletado com sucesso", "OK");
            await ExibeContatos();
        }
        private async void BtnAtualizar_Clicked(object sender, System.EventArgs e)
        {
            await fbService.UpdateContato(Convert.ToInt32(txtId.Text), txtNome.Text, txtEmail.Text);
            txtId.Text = string.Empty;
            txtNome.Text = string.Empty;
            txtEmail.Text = string.Empty;
            await DisplayAlert("Success", "Contato atualizado com sucesso", "OK");
            await ExibeContatos();
        }
        private async Task ExibeContatos()
        {
            var contatos = await fbService.GetContatos();
            listaContatos.ItemsSource = contatos;
        }
    }

}
