using S2B.SQLite;
using S2B.ViewModels;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using S2B.Models.Domain;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace S2B.Views
{
    public sealed partial class OptionsMaterialPage : Page
    {
        public OptionsMaterialPageViewModel ViewModel => this.DataContext as OptionsMaterialPageViewModel;
        public Material materialInteiro = new Material();

        public OptionsMaterialPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Carrega a página na inicialização dela.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarregarPagina(object sender, RoutedEventArgs e)
        {
            using (var db = new Contexto())
            {
                apresentarObjeto.DataContext = db.Materiais.Where(x => x == materialInteiro);
            }
        }

        /// <summary>
        /// Atualiza a página com novos dados do banco de dados.
        /// </summary>
        private void AtualizarPagina()
        {
            using(var db = new Contexto())
            {
                apresentarObjeto.DataContext = db.Materiais.Where(x => x == materialInteiro);
            }
        }

        /// <summary>
        /// Metodo que pega o argumento passado na hora da navegação.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            apresentarObjeto.DataContext = e.Parameter;
            materialInteiro = e.Parameter as Material;
        }

        /// <summary>
        /// Modifica um objeto Material selecionado pelo usuário.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ModificarMaterial(object sender, RoutedEventArgs e)
        {
            var quantidade = Convert.ToInt32(novaQuantidade.Text);
            var comentario = novoComentario.Text.ToString();
            var validade = DateTime.Now;

            ViewModel.ValidarModificacao(materialInteiro ,quantidade, comentario, validade);
            AtualizarPagina();
            this.VoltarPivotInicial.SelectedItem = this.PivotInicial;
        }

        /// <summary>
        /// Valida a entrada de dados na variavel Quantidade e retorna os valores filtrados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidarQuantidade(object sender, TextChangedEventArgs e)
        {
            var quantidadeEscolhida = novaQuantidade.Text.ToString();
            var numeral = new char[quantidadeEscolhida.Length];
            var i = 0;

            foreach (var item in quantidadeEscolhida)
            {
                if (item >= '0' && item <= '9')
                {
                    numeral[i] = item;
                    i++;
                }
            }

            var devolverNumeral = new string(numeral);
            novaQuantidade.Text = devolverNumeral;
        }
    }
}
