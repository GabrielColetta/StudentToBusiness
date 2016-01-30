using S2B.Models.Domain;
using S2B.SQLite;
using S2B.ViewModels;
using System;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;

namespace S2B.Views
{
    public sealed partial class MaterialPage : Page
    {
        public MaterialPageViewModel ViewModel => this.DataContext as MaterialPageViewModel;
        public Armazenamento ArmazenamentoTodo = new Armazenamento();

        public MaterialPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Sobreescreve o metodo para recebero o argumento enviado pela ListPage.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            ArmazenamentoApresentar.DataContext = e.Parameter;
            ArmazenamentoTodo = e.Parameter as Armazenamento;
            using (var db = new Contexto())
            {
                this.materialList.ItemsSource = db.Materiais.Where(x => x.ArmazenamentoId == ArmazenamentoTodo.Id);
                this.removeList.ItemsSource = db.Materiais.Where(x => x.ArmazenamentoId == ArmazenamentoTodo.Id);
            }
        }

        /// <summary>
        /// Carrega a lista de Armazenamento salva no banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarregarPagina(object sender, RoutedEventArgs e)
        {
            using (var db = new Contexto())
            {
                this.materialList.ItemsSource = db.Materiais.Where(x => x.ArmazenamentoId == ArmazenamentoTodo.Id);
                this.removeList.ItemsSource = db.Materiais.Where(x => x.ArmazenamentoId == ArmazenamentoTodo.Id);
            }
        }

        /// <summary>
        /// Atualiza a pagina com os novos valores do banco de dados.
        /// </summary>
        private void AtualizarPagina()
        {
            using (var db = new Contexto())
            {
                this.materialList.ItemsSource = db.Materiais.Where(x => x.ArmazenamentoId == ArmazenamentoTodo.Id);
                this.removeList.ItemsSource = db.Materiais.Where(x => x.ArmazenamentoId == ArmazenamentoTodo.Id);
            }
        }

        /// <summary>
        /// Faz a conversão dos campos e envia os valores para adicionar um novo material no sistema.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AdicionarMaterial(object sender, RoutedEventArgs e)
        {
            var nomeMaterial = nomeAdd.Text.ToString();
            var quantidadeMaterial = Convert.ToInt32(quantidadeAdd.Text);
            var comentarioMaterial = comentarioAdd.Text.ToString();
            var validadeMaterial = DateTime.Now;

            ViewModel.ValidarCampos(ArmazenamentoTodo, nomeMaterial, quantidadeMaterial, comentarioMaterial, validadeMaterial);
            this.AtualizarPagina();
            this.VoltarPivotIicial.SelectedItem = this.PivotInicial;
        }

        /// <summary>
        /// Remove um Material da lista.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">O objeto Material a ser deletado.</param>
        private void RemoverMaterial(object sender, ItemClickEventArgs e)
        {
            var deletar = e.ClickedItem;
            ViewModel.RemoverBanco(deletar as Material);
            AtualizarPagina();
        }

        /// <summary>
        /// Faz a navegação até a OptionsMaterilPage.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">O objeto Material que será passado.</param>
        private void GoOptionsPage(object sender, ItemClickEventArgs e)
        {
            var valor = e.ClickedItem;
            ViewModel.GoOptionsMaterialPage(valor as Material);
        }

        /// <summary>
        /// Faz a validação do campo nome para que não haja nenhum carater inválido.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidarNome(object sender, TextChangedEventArgs e)
        {
            var nomeEscolhido = nomeAdd.Text.ToString();
            var letra = new char[nomeEscolhido.Length];
            var i = 0;

            foreach (var item in nomeEscolhido)
            {
                if ((item >= '0' && item <= '9') || (item >= 'A' && item <= 'Z')
                || (item >= 'a' && item <= 'z') || (item == '.') || (item == '_'))
                {
                    letra[i] = item;
                    i++;
                }
            }

            var devolveValor = new string(letra);
            nomeAdd.Text = devolveValor;
        }

        /// <summary>
        /// Valida o campo quantidade para que somente exista valores numerais.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ValidarQuantidade(object sender, TextChangedEventArgs e)
        {
            var quantidadeEscolhida = quantidadeAdd.Text.ToString();
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
            quantidadeAdd.Text = devolverNumeral;
        }

        /// <summary>
        /// Verifica a entrada de dados do usuário, validando para não conter nada além de numerais.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void VerificarValidade(object sender, TextChangedEventArgs e)
        {
            var dataEscolhida = ValidadeAdd.Text.ToString();
            var validade = new char[dataEscolhida.Length];
            var i = 0;

            foreach (var item in dataEscolhida)
            {
                if(item >= '0' && item <= '9')
                {
                    validade[i] = item;
                    i++;
                }
            }

            var devolverData = new string(validade);
            ValidadeAdd.Text = devolverData;
        }
    }
}
