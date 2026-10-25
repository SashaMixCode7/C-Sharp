using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using WebApplication_Store.Models;

namespace WebApplication_Store.Services
{
    public static class ListHelper
    {
        public static HtmlString CreateImage(this IHtmlHelper html, IEnumerable<Goods> goods)
        {
            string code = "";

            foreach (var pud in goods)
            {
                code += $@"
            <div class='imge'>
                <img src='/Files/{pud.Image_link}' alt='{pud.Name}' class='imge'>
                <div class='info-block'>
                    <div class='tittle_prod'>{pud.Name}</div>
                    <div class='price_prod'>{pud.Price} ₽</div>
                    <div class='descrip_prod'>{pud.Description}</div>
                </div>
                <div class='btn_1' id='btn_card{pud.Id}' onclick='add_Goods({pud.Id})'> # </div>
            </div>";
            }
            return new HtmlString(code);
        }

           
        
    }
}
