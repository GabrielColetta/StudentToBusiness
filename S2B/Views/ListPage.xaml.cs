using S2B.SQLite;
using S2B.ViewModels;
using System.Linq;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using S2B.Models.Domain;

namespace S2B.Views
{
    public sealed partial class ListPage : Page
    {
        public ListPageViewModel ViewModel => this.DataContext as ListPageViewModel;

        public ListPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Vai passar uma lista de todos valores do objeto Armazenamento existe dentro do banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CarregarPagina(object sender, RoutedEventArgs e)
        {
            using (var db = new Contexto())
            {
                ArmazenamentosList.ItemsSource = db.Armazenamentos.ToList();
                RemoverList.ItemsSource = db.Armazenamentos.ToList();
            }
        }

        /// <summary>
        /// Funcao para atualizar os dados das listas, caso contrario poderia haver problemas com o entityframework.
        /// </summary>
        private void AtualizarPagina()
        {
            using (var db = new Contexto())
            {
                ArmazenamentosList.ItemsSource = db.Armazenamentos.ToList();
                RemoverList.ItemsSource = db.Armazenamentos.ToList();
            }
        }

        /// <summary>
        /// Faz a conversão de todos objetos para enviar para salvar no banco de dados.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CriarArmazenamento(object sender, RoutedEventArgs e)
        {
            var nomeArmazenamento = (string)nomeAdd.Text;
            var enumArmazenamento = enumCategoria.SelectedValue.ToString();
            var comentarioArmazenamento = comentarioAdd.Text.ToString();
            if (comentarioArmazenamento == null)
            {
                comentarioArmazenamento = "Sem comentarios";
            }
            ViewModel.AdicionarBanco(nomeArmazenamento, enumArmazenamento, comentarioArmazenamento);
            AtualizarPagina();
            this.VoltarPivotInicial.SelectedItem = this.PivotInicial;
        }

        /// <summary>
        /// Função que faz a navegação até a página MaterialList
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">é o objeto que será passado como argumento para MaterialList.</param>
        private void GoMaterialList(object sender, ItemClickEventArgs e)
        {
            var valor = e.ClickedItem;
            ViewModel.GoToMaterialList(valor as Armazenamento);
        }

        /// <summary>
        /// Função para remover do sistema uma lista de armazenamento salva.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Qual é objeto a ser deletado do sistema.</param>
        private void RemoverArmazenamento(object sender, ItemClickEventArgs e)
        {
            var deletar = e.ClickedItem;
            ViewModel.RemoverBanco(deletar as Armazenamento);
            AtualizarPagina();
        }

        /// <summary>
        /// Função para fazer a validação dos caracteres que pode conter o nome.
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
    }
}

