using S2B.SQLite;
using System;
using S2B.Models.Domain;

namespace S2B.ViewModels
{
    public class MaterialPageViewModel : Mvvm.ViewModelBase
    {
        public MaterialPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                Value = "Designtime value";
        }

        string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        /// <summary>
        /// Recebe os valores e cria um novo objeto no banco de dados do tipo Armazenamento.
        /// </summary>
        /// <param name="armazenamento">É o objeto ao qual o objeto Material pertence na relação de 1 para n</param>
        /// <param name="nome">É o nome do do Material.</param>
        /// <param name="quantidade">É quantos Materiais existe no deposito.</param>
        /// <param name="comentario">É um simples comentário.</param>
        /// <param name="validade">O  prazo mais proximo até a validade do material.</param>
        public void AdicionarBanco(Armazenamento armazenamento ,
                                   string nome, 
                                   int quantidade, 
                                   string comentario, 
                                   DateTime validade)
        {
            using(var db = new Contexto())
            {
                db.Materiais.Add(new Material {Nome = nome,
                                               Quantidade = quantidade,
                                               Comentario = comentario,
                                               Validade = validade,
                                               Armazenamentos = armazenamento,
                                               ArmazenamentoId = armazenamento.Id});
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Faz a validação de todos campos inseridos.
        /// </summary>
        /// <param name="armazenamento">É o objeto ao qual o objeto Material pertence na relação de 1 para n</param>
        /// <param name="nome">É o nome do do Material.</param>
        /// <param name="quantidade">É quantos Materiais existe no deposito.</param>
        /// <param name="comentario">É um simples comentário.</param>
        /// <param name="validade">O  prazo mais proximo até a validade do material.</param>
        public void ValidarCampos(Armazenamento armazenamento, 
                                  string nome, 
                                  int quantidade, 
                                  string comentario, 
                                  DateTime validade)
        {
            if (string.IsNullOrEmpty(nome))
            {
                nome = "Sem Nome";
            }

            if (quantidade < 0)
            {
                quantidade = 0;
            }
            else if (quantidade > 10000)
            {
                quantidade = 10000;
            }
            AdicionarBanco(armazenamento, nome, quantidade, comentario, validade);
        }

        /// <summary>
        /// Remove o objeto selecionado do banco de dados.
        /// </summary>
        /// <param name="deletar"></param>
        public void RemoverBanco(Material deletar)
        {
            using (var db = new Contexto())
            {
                db.Materiais.Remove(deletar);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Navegação até a página OptionsMaterialPage.
        /// </summary>
        /// <param name="valor">Os dados do objeto Material a ser apresentado na página.</param>
        public void GoOptionsMaterialPage(Material valor)
        {
            NavigationService.Navigate(typeof(Views.OptionsMaterialPage), valor);
        }
    }
}