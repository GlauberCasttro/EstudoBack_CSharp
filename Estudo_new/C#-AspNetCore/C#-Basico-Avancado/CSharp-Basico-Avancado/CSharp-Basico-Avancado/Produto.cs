using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSharp_Basico_Avancado
{
    public class Produto
    {
        public string Nome { get; set; }
        public double Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public Produto()
        {

        }
        public Produto(string nome)
        {
            Nome = nome;
        }

        public Produto(string nome, double preco, int estoque) : this(nome)
        {
            Preco = preco;
            QuantidadeEstoque = estoque;
        }
       

        public double ValorTotalEstoque()
        {
            var total = QuantidadeEstoque > 0 ? (Preco * QuantidadeEstoque) : 0;
            return total;
        }

        public void AdicionarProdutos(int quantidade)
        {
            if(quantidade> 0) QuantidadeEstoque += quantidade;
        }

        public void RemoverProdutos(int quantidadeRemover)
        {
            if(quantidadeRemover > 0 
                && QuantidadeEstoque > 0 
                && QuantidadeEstoque >= quantidadeRemover)
            QuantidadeEstoque -= quantidadeRemover;
        }

        public override string ToString()
        {
            StringBuilder b = new StringBuilder();
            b.Append("Nome do produto: "+ Nome);
            b.AppendLine();
            b.Append("Preco: "+ Preco.ToString("F2", CultureInfo.InvariantCulture));
            b.AppendLine();
            b.Append("Quantidade em estoque: "+ QuantidadeEstoque);
            return b.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is Produto produto &&
                   Nome == produto.Nome &&
                   Preco == produto.Preco &&
                   QuantidadeEstoque == produto.QuantidadeEstoque;
        }

        public override int GetHashCode()
        {
            int hashCode = -1409227857;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Nome);
            hashCode = hashCode * -1521134295 + Preco.GetHashCode();
            hashCode = hashCode * -1521134295 + QuantidadeEstoque.GetHashCode();
            return hashCode;
        }
    }
}
