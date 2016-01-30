using S2B.SQLite;
using S2B.Models.Domain;
using System;
using System.Linq;

namespace S2B.ViewModels
{
    public class OptionsMaterialPageViewModel : Mvvm.ViewModelBase
    {
        public OptionsMaterialPageViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled)
                Value = "Designtime value";
        }

        string _Value = string.Empty;
        public string Value { get { return _Value; } set { Set(ref _Value, value); } }

        /// <summary>
        /// Valida os campos para modificar o objeto no banco de dados.
        /// </summary>
        /// <param name="materialModificar">O Material que se deseja modificar.</param>
        /// <param name="quantidade">O novo dado Quantidade a ser adicionado.</param>
        /// <param name="comentario">O novo dado Comentário a ser adicionado.</param>
        /// <param name="validade">O novo dado Validade a ser adicionado.</param>
        public void ValidarModificacao(Material materialModificar, int quantidade, string comentario, DateTime validade)
        {
            var teste = Convert.ToString(quantidade);
            if (string.IsNullOrEmpty(teste))
            {
                quantidade = materialModificar.Quantidade;
            } else if(quantidade < 0)
            {
                quantidade = 0;
            } else if (quantidade > 10000)
            {
                quantidade = 10000;
            }

            if(string.IsNullOrEmpty(comentario))
            {
                comentario = materialModificar.Comentario;
            }

            validade = DateTime.Now;
            DBModificarMaterial(materialModificar, quantidade, comentario, validade);
        }

        /// <summary>
        /// Faz a modificação do banco de dados.
        /// </summary>
        /// <param name="materialModificar">O Material que se deseja modificar.</param>
        /// <param name="quantidade">O novo dado Quantidade a ser adicionado.</param>
        /// <param name="comentario">O novo dado Comentário a ser adicionado.</param>
        /// <param name="validade">O novo dado Validade a ser adicionado.</param>
        public void DBModificarMaterial(Material materialModificar, int quantidade, string comentario, DateTime validade)
        {
            Material valorModificar;
            using (var db = new Contexto())
            {
                valorModificar = db.Materiais.Where(x => x.Id == materialModificar.Id).FirstOrDefault<Material>();
            }

            if(valorModificar != null)
            {
                valorModificar.Quantidade = quantidade;
                valorModificar.Comentario = comentario;
                valorModificar.Validade = validade;
            }

            using (var dbContext = new Contexto())
            {
                dbContext.Entry(valorModificar).State = Microsoft.Data.Entity.EntityState.Modified;
                dbContext.SaveChanges();
            }
        }
    }
}
