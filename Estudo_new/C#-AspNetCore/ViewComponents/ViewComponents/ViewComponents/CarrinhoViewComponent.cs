using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ViewComponents.ViewComponents
{
    [ViewComponent(Name = "Carrinho")]
    //para cada componente é necessario ter uma pasta 
    public class CarrinhoViewComponent : ViewComponent
    {
        public int ItensCarrinho { get; set; }
        public CarrinhoViewComponent()
        {
            ItensCarrinho = 3;
        }

        
        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View(ItensCarrinho);
        }
    }
}
