using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MvcDeganitSite.Models
{
    /*
    public class RecipeInitializer
    {
    }
     * */
    
    public class RecipesInitializer :DropCreateDatabaseAlways<RecipesContext>
    {

        protected override void Seed(RecipesContext context)
        {
            var mainCategories = new List<MainCategory>
            {
                new MainCategory{Name="עוגות"},
                new MainCategory{Name="מרקים"},
                new MainCategory{Name="בשר"},
                new MainCategory{Name="פשטידות"},
                new MainCategory{Name="מוצלחים"},
                new MainCategory{Name="קאפקייקס"}
            };
            mainCategories.ForEach(m => context.MainCategories.Add(m));

            var recipes = new List<Recipe>
            {
                new Recipe {Name="קאפקייקס לבן על לבן", Description="עם חמאה", Url="http://morcake.blogspot.co.il/2010/08/blog-post_22.html", PictureName="קאפקייקס_לבן.jpg", MainCategoryID=6},
                new Recipe {Name="קאפקייקס שוקולד", Description="ל-16 קאפקייקס ללא חמאה", Url="http://www.litcake.co.il/tag/%D7%A7%D7%90%D7%A4%D7%A7%D7%99%D7%99%D7%A7%D7%A1/", PictureName="קאפקייקס_שוקולד.jpg", MainCategoryID=6},
                new Recipe {Name="קאפקייקס שוקולד חלבה ", Description="עם ממרח חלבה", Url="http://www.mako.co.il/food-recipes/recipes_column-cakes/Recipe-eb9c4f59d23ad21006.htm", PictureName="קאפקייקס_שוקולד_חלבה.jpg", MainCategoryID=6},
                new Recipe {Name="קאפקייקס טרמיסו", Description="עם חמאה", Url="http://www.tapuz.co.il/forums2008/viewmsg.aspx?forumid=1945&messageid=145407395", PictureName="קאפקייקס_טרמיסו.jpg", MainCategoryID=6},
                new Recipe {Name="עוגת מוס", Description="ללא קמח", Url="http://www.cookshare.co.il/modules.php?name=Recipes&op=viewrecipe&my=&orderid=&recipeid=1938&offset=9", PictureName="עוגת_מוס1.jpg", MainCategoryID=1},
                new Recipe {Name="עוגת שוקולד חמה", Description="קלה להכנה", Url="http://www.cookshare.co.il/modules.php?name=Recipes&op=viewrecipe&my=&orderid=&recipeid=728&offset=1", PictureName="עוגת_שוקולד_חמה.jpg", MainCategoryID=1},
                new Recipe {Name="עוגת טרמיסו", Description="ללא ביצים",Url="http://foodsdictionary.nana10.co.il/Recipes/1023",PictureName="טרמיסו_ללא_ביצים.jpg",MainCategoryID=1},
                new Recipe {Name="מוס שוקולד ושמנת", Description="קל להכנה",Url="http://www.xnet.co.il/food/articles/0,14567,L-3087304,00.html",PictureName="מוס_שוקולד.jpg",MainCategoryID=1},
                new Recipe {Name="מאפה חציל וגבינה בולגרית", Description="עם בצק פריך",Url="http://www.ynet.co.il/ency/1,7340,L-543090,00.html",PictureName="אין",MainCategoryID=4},
                new Recipe {Name="עוגת שוקולד גרמנית", Description="",Url="http://www.ynet.co.il/articles/0,7340,L-3656129,00.html",PictureName="עוגת_שוקולד_גרמנית.jpg",MainCategoryID=1},
                new Recipe {Name="גביעי מוס שוקולד", Description="בגביעי גלידה",Url="http://www.chef-lavan.co.il/item/6703",PictureName="גביעי_מוס_שוקולד.jpg",MainCategoryID=1},
                new Recipe {Name="עוגת ביסקויטים", Description="",Url="http://www.tapuz.co.il/blog/net/ViewEntry.aspx?entryId=1480671&skip=1",PictureName="עוגת_ביסקויטים.jpg",MainCategoryID=1},
                new Recipe {Name="עוגת יוגורט", Description="",Url="http://forums.tipo.co.il/apps/forum/forum_posts.asp?TID=808045&PN=1",PictureName="אין",MainCategoryID=1},
                new Recipe {Name="טרמיסו עם גבינת מסקרפונה", Description="",Url="http://www.tapuz.co.il/blog/net/ViewEntry.aspx?EntryId=1310914",PictureName="טרמיסו_ללא_ביצים1.jpg",MainCategoryID=1},
                new Recipe {Name="לזניה ירקות", Description="",Url="http://www.tnuva.co.il/Child_Nutrition/Child_Nutrition/Pages/Recipes/5.aspx",PictureName="אין",MainCategoryID=4},
                new Recipe {Name="לזניה מדהימה", Description="",Url="http://www.chef-lavan.co.il/item/8437",PictureName="לזניה_מדהימה.jpg",MainCategoryID=4},
                new Recipe {Name="בורקס בשר", Description="של אבי ממאסטר שף",Url="http://www.mako.co.il/food-cooking_magazine/avi-levy/Article-bcfd6e2948c3431006.htm",PictureName="בורקס_בשר.jpg",MainCategoryID=1},
                new Recipe {Name="עוגת שוקולד", Description="עם שמנת חמוצה",Url="http://justasty.com/cakes/chocolate-cakes/2011/01/27/%d7%a2%d7%95%d7%92%d7%aa-%d7%94%d7%a9%d7%95%d7%a7%d7%95%d7%9c%d7%93-%d7%a9%d7%9c-%d7%a8%d7%97%d7%9c%d7%99/",PictureName="עוגת_שוקולד.jpg",MainCategoryID=1},
                new Recipe {Name="עוגת שוקולד", Description="עם חמאה",Url="http://justasty.com/cakes/chocolate-cakes/2009/08/07/%D7%A2%D7%95%D7%92%D7%AA-%D7%A9%D7%95%D7%A7%D7%95%D7%9C%D7%93/",PictureName="עוגת_שוקולד_עם_חמאה.jpg",MainCategoryID=1}

            };
            recipes.ForEach(r => context.Recipes.Add(r));
            

            var navigationWords = new List<NavigationWord>
            {
                new NavigationWord{Name="אפייה"},new NavigationWord{Name="ללא אפייה"},new NavigationWord{Name="מוצלח"},
                new NavigationWord{Name="בצק פריך"},new NavigationWord{Name="מתכון קל"},new NavigationWord{Name="מתכון קשה"},
                new NavigationWord{Name="לפסח"},new NavigationWord{Name="פרוה"},new NavigationWord{Name="חלבי"},
                new NavigationWord{Name="שמנת"},new NavigationWord{Name="עם קצפת"},new NavigationWord{Name="בלי שמנת"}
              
            };
            navigationWords.ForEach(n => context.NavigationWords.Add(n));
           

            var users = new List<User>
            {
                new User{Name="דגנית",Password="1234",Recipes=recipes},
                new User{Name="עילי",Password="1234",Recipes=recipes},
                new User{Name="שי",Password="1234"}

            };



            users.ForEach(u => context.Users.Add(u));

            context.SaveChanges();



           
        }

        
    }
   
}