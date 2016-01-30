using S2B.SQLite;
using S2B.Models.Domain;
using System;

namespace S2B.ViewModels
{
    public class ListPageViewModel : Mvvm.ViewModelBase
    {
        public ListPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                Value = "Designtime value";
        }

        string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        /// <summary>
        /// Função para adicionar os valores de um nova classe Armazenamento no banco de dados.
        /// </summary>
        /// <param name="nome">É o nome da nova classe Armazenamento</param>
        /// <param name="categoria">É qual categoria o Armazenamento pertence</param>
        /// <param name="comentario">É o comentário sobre a nova lista a ser criada</param>
        public void AdicionarBanco(string nome, string categoria, string comentario)
        {
            using (var db = new Contexto())
            {
                db.Armazenamentos.Add(new Armazenamento { Nome = nome,
                                                          Categorias = categoria,
                                                          Comentario = comentario,
                                                          DataCriacao = DateTime.Now });
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Função para remover um objeto Armazenamento do banco de dados.
        /// </summary>
        /// <param name="deletar">É o objeto a ser deletado</param>
        public void RemoverBanco(Armazenamento deletar)
        {
            using(var db = new Contexto())
            {
                db.Armazenamentos.Remove(deletar);
                db.SaveChanges();
            }
        }

        /// <summary>
        /// Metodo para levar um objeto Armazenamento correspondente.
        /// a qual classe o Material vai pertencer na relação 1 para n.
        /// </summary>
        /// <param name="Armazenamento">O objeto a ser passado no argumento</param>
        public void GoToMaterialList(Armazenamento Armazenamento)
        {
            NavigationService.Navigate(typeof(Views.MaterialPage), Armazenamento);
        }
    }
}
