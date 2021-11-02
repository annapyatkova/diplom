using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.EntityFrameworkCore;

namespace Craft
{
    
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DBCraftefoldContext db;
        /**/
        List<OrderModif> Mod = new List<OrderModif>(1);
        List<OrderModif> UpMod = new List<OrderModif>(1);
        List<PaintCol> Col = new List<PaintCol>(1);
        List<PaintCol> UpCol = new List<PaintCol>(1);
        List<PaintProd> Prod = new List<PaintProd>(1);
        List<PaintProd> UpProd = new List<PaintProd>(1);
        public MainWindow()
        {
            InitializeComponent();

            try
            {
                db = new DBCraftefoldContext();
                db.Orders.Load();
                db.Products.Load();
                db.Categories.Load();
                db.Shops.Load();
                db.Stages.Load();
                db.Sizes.Load();
                db.Dispatch.Load();
                db.Modifications.Load();
                db.Painting.Load();
                db.Colors.Load();
                db.Photos.Load();
                db.Orderphotos.Load();
                db.Productphotos.Load();
                db.Ordermodifications.Load();
                db.Paintcolors.Load();
                db.Paintingproduct.Load();
                UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }

            /*
             
            try
            {
            }  
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }

            */


            /*
            То что мне открылось

            //данные из одной таблицы
            //ordersGrid.ItemsSource = db.Orders.Local.ToBindingList();


            //Первый способ
            var users = from o in db.Orders
                        join p in db.Products on o.IdProduct equals p.IdProduct
                        select new { IdOrder = o.IdOrder, Name = p.Name, Orderdate = o.Orderdate };

            
            ordersGrid.ItemsSource = users.ToList();


            //Второй способ
            var users = db.Orders.Join(db.Products, // второй набор
                o => o.IdProduct, // свойство-селектор объекта из первого набора
                p => p.IdProduct, // свойство-селектор объекта из второго набора
                (o, p) => new // результат
                {
                    IdOrder = o.IdOrder,
                    Name = p.Name,
                    Orderdate = o.Orderdate
                });
            ordersGrid.ItemsSource = users.ToList();*/

            /*var orders = from o in db.Orders 
                         join p in db.Products on o.IdProduct equals p.IdProduct
                         join c in db.Categories on p.IdCategory equals c.IdCategory
                         //orderby o.Orderdate descending /* не уверена в этой строке, но прога работает пока таблица не заполнена 
                         select new { IdOrder = o.IdOrder, Name = p.Name, Orderdate = o.Orderdate, IdStage = o.IdStage, Nameofcategory = c.Nameofcategory };
            ordersGrid.ItemsSource = orders.ToList();*/





            /*var category = from c in db.Categories
                           select c.Nameofcategory;
            categoryList.ItemsSource = category.ToList();*/

            /*  <Window.Resources>
                    <local:Categories x:Key="Categorie" />
                </Window.Resources>

                DataContext="{StaticResource Categorie}">
                <ComboBox Text="{Binding Path=Nameofcategory, Mode=TwoWay, UpdateSourceTrigger=PropertyChanged}"/>
            */









            /*string t = categoryList.Text;
            var products = from c in db.Categories
                           where c.Nameofcategory == t
                           select c.IdCategory;
            productsList.ItemsSource = t;*/


            this.Closing += MainWindow_Closing;
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            db.Dispose();
        }



        class OrderModif
        {
            public OrderModif(string mod)
            {
                this.mod = mod;
            }
            public string mod { get; set; }
        }
        class PaintCol
        {
            public PaintCol(string col)
            {
                this.col = col;
            }
            public string col { get; set; }
        }

        class PaintProd
        {
            public PaintProd(string prod)
            {
                this.prod = prod;
            }
            public string prod { get; set; }
        }

        ////////
        ////////
        //////// Заполняем комбобоксы и датагриды данными
        ////////
        ///////


        /*заполняет таблицы все*/
        private void UpdateData()
        {
            //ordersGrid.Items.SortDescriptions.Clear();
            try
            {
                /*первая вкладка*/
                /*заполянем таблицу заказов и таблицу с категориями в форме добавления заказа*/
                Orde();
            Stagecheck();
            Categorie();
            SizeList();
            ShopList();
            StageList();
            Orderphoto();
            OrdermodList();

            /*вторая вкладка*/
            /*заполняем дата гриды*/
            ProductGrid();
            CategoriyList();
            PaintprodList();
            Productphoto();
            CategoryGrid();
            ShopsGrid();
            StagesGrid();
            SizesGrid();
            ModGrid();
            PaintGrid();
            PaintcolorList();
            ColorGrid();
            ProductPhotoGrid();

            /*третья вкладка*/
            /*заполняем дата гриды*/
            DispatchGrid();
            OrderList();
            DatapaintGrid();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }

        }

        /*заполняет таблицу заказов на первой вкладке*/
        private void Orde()
        {
            
            /*вытаскивает нужные значения из базы*/
            var orders = from o in db.Orders
                         join sh in db.Shops on o.IdShop equals sh.IdShop
                         join sz in db.Sizes on o.IdSize equals sz.IdSize
                         join pt in db.Painting on o.IdPainting equals pt.IdPainting
                         join p in db.Products on o.IdProduct equals p.IdProduct
                         join c in db.Categories on p.IdCategory equals c.IdCategory
                         join s in db.Stages on o.IdStage equals s.IdStage
                         orderby o.IdOrder 
                         select new { IdOrder = o.IdOrder, Name = p.Name, Orderdate = o.Orderdate.ToShortDateString(), Stagename = s.Stagename, Nameofcategory = c.Nameofcategory, Paint = pt.Paintingname, Size = sz.Size, Shop = sh.Nameofshop, Cost = o.Cost, Deadline = o.Deadline, Specification = o.Specification};
            /*передает в таблицу заказов*/
            
            ordersGrid.ItemsSource = orders.ToList();

            
        }

        /*заполняет выпадающий список этапов в таблице заказов*/
        private void Stagecheck()
        {
            

            var stage = (from s in db.Stages
                         where s.IdStage != 6 
                         select s.Stagename).ToList();
            Resources["checkstageList"] = stage;
        }

        

        /*считывает изменение этапа заказа*/
        private void CheckStagesList_Selected(object sender, EventArgs e)
        {

            /*считывает выбранный элемент*/
            ComboBox cmbx = sender as ComboBox;
            string nameofstage = cmbx.SelectedValue.ToString();

                /*находим айди этапа */
                var idstage = (from s in db.Stages
                               where s.Stagename == nameofstage
                               select s.IdStage).FirstOrDefault();

                /*считаем сколько строк в таблице*/
                if (ordersGrid.SelectedItems.Count > 0)
                {
                    /*пока в таблице есть данные*/
                    for (int i = 0; i < ordersGrid.SelectedItems.Count; i++)
                    {
                        /*считываем первый столбец выбранной строки, в этом случае это айдишник заказа*/
                        var id = new DataGridCellInfo(ordersGrid.SelectedItems[i], ordersGrid.Columns[0]);
                        /*делаем эту штуку текстом*/
                        var content = id.Column.GetCellContent(id.Item) as TextBlock;
                        /*если ячейка не пустая*/
                        int j;
                        if (content != null)
                        {
                            /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                            bool success = int.TryParse(content.Text.Trim(), out j);
                            if (success)
                            {

                                /*меняем этап заказа на выбранный*/
                                var order = (from o in db.Orders
                                             where o.IdOrder == j
                                             select o).FirstOrDefault();
                                if (order != null)
                                {
                                    order.IdStage = idstage;

                                    db.SaveChanges();
                                    ordersGrid.CommitEdit(DataGridEditingUnit.Row, true);
                                    UpdateData();
                                }
                            }
                        }
                    }
                }
             

        }

        /*заполняет комбобокс с категориями в форме добавления заказа*/
        private void Categorie()
        {
            var category = from c in db.Categories
                           select c.Nameofcategory;
            categoryList.ItemsSource = category.ToList();
        }

        /*вытаскивает название категории из комбобокса в добавлении заказа и заполянет следующий комбобокс подходящими товарами*/
        private void CategoryList_Selected(object sender, SelectionChangedEventArgs e)
        {
            /*считывает название категории из комбобокса*/
            var id = db.Orders.Select(u => u.IdOrder).FirstOrDefault();
            if (id != 0)
                id = db.Orders.Max(u => u.IdOrder);
            string nameofcategory = categoryList.SelectedValue.ToString();
            /*находим товары по категории и передаем в комбобокс*/
            var name = from c in db.Categories
                       join p in db.Products on c.IdCategory equals p.IdCategory
                       where c.Nameofcategory == nameofcategory
                       select p.Name;
            productsList.ItemsSource = name.ToList();

            
        }

        

        /*вытаскивает название изделия из комбобокса в форме добавления заказа и заполняет комбобокс с покраской*/
        private void ProductsList_Selected(object sender, SelectionChangedEventArgs e)
        {
            if (productsList.SelectedValue != null) {
                string name = productsList.SelectedValue.ToString();
                var paint = from pp in db.Paintingproduct
                            join p in db.Products on pp.IdProduct equals p.IdProduct
                            join pc in db.Painting on pp.IdPainting equals pc.IdPainting
                            where p.Name == name
                            select pc.Paintingname;
                paintingList.ItemsSource = paint.ToList(); 
            }
        }


        /*заполняет комбобокс с размерами в форме добавления заказа*/
        private void SizeList()
        {
            var size = from c in db.Sizes
                       select c.Size;
            sizeList.ItemsSource = size.ToList();
        }



        /*заполняет комбобокс с магазинаим в форме добавления заказа*/
        private void ShopList()
        {
            var shop = from c in db.Shops
                       select c.Nameofshop;
            shopList.ItemsSource = shop.ToList();
        }

        

        /*заполняет комбобокс с этапами в форме добавления заказа*/
        private void StageList()
        {
            var stage = from c in db.Stages
                        where c.IdStage != 6
                        select c.Stagename;
            stageList.ItemsSource = stage.ToList();
        }

        /*попытка реализовать добавление фото в заказ*/
        private void Orderphoto()
        {
            var id = db.Orderphotos.Select(u => u.IdOrder).FirstOrDefault();
            if (id != 0)
                id = db.Orderphotos.Max(u => u.IdOrder);
            id++;
            
            orderphotosGrid.ItemsSource = (from c in db.Orderphotos
                                          where c.IdOrder == id
                                          select c).ToList();
        }

        /*заполняет комбобокс с модификациями в форме добавления заказа*/
        private void OrdermodList()
        {
            var mod = from c in db.Modifications
                           select c.Modificationname;
            ordermodList.ItemsSource = mod.ToList();

        }



        /*открытие подробной информации о заказе*/
        private void Popupmod_Opening(object sender, EventArgs e)
        {
            

            /*считаем сколько строк в таблице*/
            if (ordersGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < ordersGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник заказа*/
                    var id = new DataGridCellInfo(ordersGrid.SelectedItems[i], ordersGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим список модификаций по данному заказу и пихаем его в ресурсы*/
                            var mod = (from o in db.Ordermodifications
                                      join m in db.Modifications on o.IdModification equals m.IdModification
                                      where o.IdOrder == j
                                      select new { Modificationname = m.Modificationname }).ToList();
                            Resources["allmodList"] = mod;

                            /*находим список фотографий по данному заказу и пихаем его в ресурсы*/
                            var photo = (from o in db.Orderphotos
                                       where o.IdOrder == j
                                       select new { Title = o.Title, Orderphoto = o.Orderphoto }).ToList();
                            Resources["allorderphotoList"] = photo;

                        }
                    }
                }
            }

        }

        /*заполняет всплывающее окно редактирования заказа*/
        private void UpPopupmod_Opening(object sender, EventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (ordersGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < ordersGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник заказа*/
                    var id = new DataGridCellInfo(ordersGrid.SelectedItems[i], ordersGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {

                           /* var category = (from o in db.Orders
                                         join p in db.Products on o.IdProduct equals p.IdProduct
                                         join c in db.Categories on p.IdCategory equals c.IdCategory
                                         where o.IdOrder == j
                                         select c.Nameofcategory).FirstOrDefault();
                            upCategoryList.Text = category.ToString();*/

                            var product = (from o in db.Orders
                                           join p in db.Products on o.IdProduct equals p.IdProduct
                                           where o.IdOrder == j
                                           select p.Name).FirstOrDefault().ToString();
                            /*upProductList.Text = product.ToString();*/

                            
                            var paint = (from o in db.Orders
                                           join p in db.Painting on o.IdPainting equals p.IdPainting
                                           where o.IdOrder == j
                                           select p.Paintingname).FirstOrDefault().ToString();
                            upPaintingList.SelectedValue = null;
                            upPaintingList.SelectedValue = paint;

                            /*находим список модификаций по данному заказу и пихаем его в ресурсы*/
                            var painting = (from pp in db.Paintingproduct
                                            join p in db.Products on pp.IdProduct equals p.IdProduct
                                            join pc in db.Painting on pp.IdPainting equals pc.IdPainting
                                            where p.Name == product
                                            select pc.Paintingname).ToList();
                            Resources["upPaintingList"] = painting;

                            var Size = (from o in db.Orders
                                       join s in db.Sizes on o.IdSize equals s.IdSize
                                       where o.IdOrder == j
                                       select s.Size).FirstOrDefault();
                            upSizeList.SelectedValue = Size.ToString();

                            var sizeList = from c in db.Sizes
                                       select c.Size;
                            Resources["upSizeList"] = sizeList.ToList();

                            /*var shop = (from o in db.Orders
                                       join s in db.Shops on o.IdShop equals s.IdShop
                                       where o.IdOrder == j
                                       select s.Nameofshop).FirstOrDefault();
                            upShopList.Text = shop.ToString();*/

                            var cost = (from o in db.Orders
                                        where o.IdOrder == j
                                        select o.Cost).FirstOrDefault();
                            upCost.Text = cost.ToString();

                            var deadline = (from o in db.Orders
                                        where o.IdOrder == j
                                        select o.Deadline).FirstOrDefault();
                            upDeadline.Text = deadline.ToString();

                            var specific= (from o in db.Orders
                                            where o.IdOrder == j
                                            select o.Specification).FirstOrDefault();
                            upSpecific.Text = specific.ToString();

                            /*находим список модификаций по данному заказу и пихаем его в ресурсы*/
                            var mod = (from o in db.Ordermodifications
                                       join m in db.Modifications on o.IdModification equals m.IdModification
                                       where o.IdOrder == j
                                       select m.Modificationname).ToList();

                            UpMod = null;
                            UpMod = new List<OrderModif>(1);
                            foreach (var m in mod)
                            {
                                UpMod.Add(new OrderModif(m.ToString()));
                            }
                            upOrdermodGrid.ItemsSource = null;
                            upOrdermodGrid.ItemsSource = UpMod;



                            var addmod = (from c in db.Modifications
                                      select c.Modificationname).ToList();
                            upOrdermodList.ItemsSource = addmod;


                            /*
                            var id = db.Orderphotos.Select(u => u.IdOrder).FirstOrDefault();
                            if (id != 0)
                                id = db.Orderphotos.Max(u => u.IdOrder);
                            id++;

                            orderphotosGrid.ItemsSource = (from c in db.Orderphotos
                                                           where c.IdOrder == id
                                                           select c).ToList();
                            */
                            /*находим список фотографий по данному заказу и пихаем его в ресурсы*/
                            var photo = (from o in db.Orderphotos
                                         where o.IdOrder == j
                                         select o).ToList();
                            upOrderphotosGrid.ItemsSource = photo;


                        }
                    }
                }
            }
        }



        /*открытие подробной информации о товаре */
        private void ProductPopupmod_Opening(object sender, EventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (productGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < productGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник */
                    var id = new DataGridCellInfo(productGrid.SelectedItems[i], productGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим список  и пихаем его в ресурсы*/
                            var paint = (from o in db.Paintingproduct
                                       join m in db.Painting on o.IdPainting equals m.IdPainting
                                         where o.IdProduct == j
                                       select new { Paintingname = m.Paintingname }).ToList();
                            Resources["allpaintprodList"] = paint;

                            /*находим список фотографий по данному и пихаем его в ресурсы*/
                            var photo = (from o in db.Productphotos
                                         join m in db.Photos on o.IdProductphoto equals m.IdProductphoto
                                         where o.IdProduct == j
                                         select new { Title = m.Title, Productphoto = m.Productphoto }).ToList();
                            Resources["allproductphotoList"] = photo;

                        }
                    }
                }
            }
        }

        /*заполняет всплывающее окно редактирования товара*/
        private void UpProductPopupmod_Opening(object sender, EventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (productGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < productGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник товара*/
                    var id = new DataGridCellInfo(productGrid.SelectedItems[i], productGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*заполяем все поля*/
                            
                            var price = (from o in db.Products
                                        where o.IdProduct == j
                                        select o.Price).FirstOrDefault();
                            upPrice.Text = price.ToString();

                            var description = (from o in db.Products
                                            where o.IdProduct == j
                                            select o.Description).FirstOrDefault();
                            upDescription.Text = description.ToString();



                            /*находим список покрасок по данному товару и пихаем его в ресурсы*/
                            var paint = (from o in db.Paintingproduct
                                       join m in db.Painting on o.IdPainting equals m.IdPainting
                                       where o.IdProduct == j
                                       select m.Paintingname).ToList();

                            UpProd = null;
                            UpProd = new List<PaintProd>(1);
                            foreach (var m in paint)
                            {
                                UpProd.Add(new PaintProd(m.ToString()));
                            }
                            upPaintprodGrid.ItemsSource = null;
                            upPaintprodGrid.ItemsSource = UpProd;

                            var addpaint = (from c in db.Painting
                                          select c.Paintingname).ToList();
                            upPaintprodList.ItemsSource = addpaint;


                           

                            /*находим список фотографий по данному товару и пихаем его в ресурсы*/
                            var photo = (from o in db.Productphotos
                                         join m in db.Photos on o.IdProductphoto equals m.IdProductphoto
                                         where o.IdProduct == j
                                         select m).ToList();
                            upProductphotosGrid.ItemsSource = photo;
                        }
                    }
                }
            }
        }



        /*открытие подробной информации о покраске*/
        private void PaintingPopupmod_Opening(object sender, EventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (paintGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < paintGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник */
                    var id = new DataGridCellInfo(paintGrid.SelectedItems[i], paintGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим список  и пихаем его в ресурсы*/
                            var color = (from o in db.Paintcolors
                                         join m in db.Colors on o.IdColor equals m.IdColor
                                         where o.IdPainting == j
                                         select new { Colorname = m.Colorname }).ToList();
                            Resources["allpaintcolorList"] = color;

                        }
                    }
                }
            }
        }

        /*заполняет всплывающее окно редактирования покраски*/
        private void UpPaitningPopupmod_Opening(object sender, EventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (paintGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < paintGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник покраски*/
                    var id = new DataGridCellInfo(paintGrid.SelectedItems[i], paintGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*заполяем все поля*/


                            /*находим список цветов по данной покраске */
                            var color = (from o in db.Paintcolors
                                         join m in db.Colors on o.IdColor equals m.IdColor
                                         where o.IdPainting == j
                                         select m.Colorname).ToList();

                            UpCol = null;
                            UpCol = new List<PaintCol>(1);
                            foreach (var m in color)
                            {
                                UpCol.Add(new PaintCol(m.ToString()));
                            }
                            upPaintGrid.ItemsSource = null;
                            upPaintGrid.ItemsSource = UpCol;

                            var addcolor = (from c in db.Colors
                                            select c.Colorname).ToList();
                            upPaintList.ItemsSource = addcolor;

                        }
                    }
                }
            }
        }



        /*заполняет таблицу изделий на второй вкладке*/
        private void ProductGrid()
        {
            /*вытаскивает нужные значения из базы*/
            var products = from p in db.Products
                         join c in db.Categories on p.IdCategory equals c.IdCategory
                         select new { IdProduct = p.IdProduct, Description = p.Description, Price = p.Price, Name = p.Name, Nameofcategory = c.Nameofcategory };
            /*передает в таблицу изделий*/
            productGrid.ItemsSource = products.ToList();
        }

        /*заполняет комбобокс с категориями в форме добавления изделия во второй вкладке*/
        private void CategoriyList()
        {
            var category = from c in db.Categories
                           select c.Nameofcategory;
            categoryProductList.ItemsSource = category.ToList();
        }

        /*заполняет комбобокс с покрасками в форме добавления заказа*/
        private void PaintprodList()
        {
            var prod = from c in db.Painting
                       select c.Paintingname;
            paintprodList.ItemsSource = prod.ToList();
        }

        /*добавление фото в изделие*/
        private void Productphoto()
        {
            /*ищем максимальный айди фотографии*/
            var id = db.Photos.Select(u => u.IdProductphoto).FirstOrDefault();
            if (id != 0)
                id = db.Photos.Max(u => u.IdProductphoto);
            id++;

            productphotosGrid.ItemsSource = (from c in db.Photos
                                             where c.IdProductphoto == id
                                             select c).ToList();
        }

        /*выводит категории в дата грид на второй вкладке*/
        private void CategoryGrid()
        {
            categoryGrid.ItemsSource = db.Categories.Local.ToBindingList();
        }

        /*выводит магазины в дата грид на второй вкладке*/
        private void ShopsGrid()
        {
            shopsGrid.ItemsSource = db.Shops.Local.ToBindingList();
        }

        /*выводит этапы в дата грид на второй вкладке*/
        private void StagesGrid()
        {
            stagesGrid.ItemsSource = db.Stages.Local.ToBindingList();

            
        }

        /*выводит размеры в дата грид на второй вкладке*/
        private void SizesGrid()
        {
            sizesGrid.ItemsSource = db.Sizes.Local.ToBindingList();
        }

        /*выводит модификации в дата грид на второй вкладке*/
        private void ModGrid()
        {
            modGrid.ItemsSource = db.Modifications.Local.ToBindingList();
        }

        /*выводит покраску в дата грид на второй вкладке*/
        private void PaintGrid()
        {
            paintGrid.ItemsSource = db.Painting.Local.ToBindingList();
        }

        /*заполняет комбобокс с цветами в форме добавления покраски*/
        private void PaintcolorList()
        {
            var color = from c in db.Colors
                      select c.Colorname;
            paintcolorList.ItemsSource = color.ToList();
        }

        /*выводит цвет в дата грид на второй вкладке*/
        private void ColorGrid()
        {
            colorGrid.ItemsSource = db.Colors.Local.ToBindingList();
        }

        /*выводит фотогорафии изделий в дата грид на второй вкладке*/
        private void ProductPhotoGrid()
        {
            productphotoGrid.ItemsSource = db.Photos.Local.ToBindingList();
        }



        

        /*выводит отправки в дата грид на третьей вкладке*/
        private void DispatchGrid()
        {
            dispatchGrid.ItemsSource = db.Dispatch.Local.ToBindingList();
        }

        /*заполняет комбобокс с заказами в форме добавления отправки в третьей вкладке*/
        private void OrderList()
        {
            var order = from o in db.Orders
                        where o.IdStage != 6
                        select o.IdOrder;
            numberorderdispatch.ItemsSource = order.ToList();

            /*dispatchGrid.ItemsSource = (from d in db.Dispatch
                                        join o in db.Orders on d.IdOrder equals o.IdOrder
                                        where o.IdStage != 6
                                        select d).ToList();*/
        }

        /*заполняет таблицу статистики в третьей вкладке*/
        private void DatapaintGrid()
        {
            /*SELECT Products.name,  COUNT(Orders.id_stage) as "vsego"
            FROM Orders 
            JOIN Products ON Orders.id_product = Products.id_product
            GROUP BY Products.name;*/
            /*считает все заказы по наименованиям*/
            var dataall = from o in db.Orders
                          join p in db.Products on o.IdProduct equals p.IdProduct
                          group p by p.Name into d
                          select new { Dataname = d.Key, Dataall = d.Count() };

            var datastage = from o in db.Orders
                          join p in db.Stages on o.IdStage equals p.IdStage
                          group p by p.Stagename into d
                          select new { Dataname = d.Key, Datastage = d.Count() };


            var datanotstart = from o in db.Orders
                            join p in db.Products on o.IdProduct equals p.IdProduct
                            where o.IdStage == 1
                            group p by p.Name into d
                            select new { Dataname = d.Key, Datanotstart = d.Count() };


            var dataotlito = from o in db.Orders
                               join p in db.Products on o.IdProduct equals p.IdProduct
                               where o.IdStage == 2
                               group p by p.Name into d
                               select new { Dataname = d.Key, Dataotlito = d.Count() };
            /*SELECT Products.name,  COUNT(Orders.id_stage) as "v pokraske"
            FROM Orders 
            JOIN Products ON Orders.id_product = Products.id_product
            WHERE Orders.id_stage = 3
            GROUP BY Products.name;*/
            /*считает заказы в покраске по наименованиям*/
            var datapaint = from o in db.Orders
                            join p in db.Products on o.IdProduct equals p.IdProduct
                            where o.IdStage == 3
                            group p by p.Name into d
                            select new { Dataname = d.Key, Datapaint = d.Count() };

            var dataallpaint = from o in db.Orders
                            join p in db.Products on o.IdProduct equals p.IdProduct
                            where o.IdStage == 4
                            group p by p.Name into d
                            select new { Dataname = d.Key, Dataallpaint = d.Count() };

            /*
            SELECT Products.name,  COUNT(Orders.id_stage) as "upakovano"
            FROM Orders 
            JOIN Products ON Orders.id_product = Products.id_product
            WHERE Orders.id_stage = 5
            GROUP BY Products.name;*/
            /*считает упакованные заказы по наименованиям*/
            var datapacket = from o in db.Orders
                            join p in db.Products on o.IdProduct equals p.IdProduct
                            where o.IdStage == 5
                            group p by p.Name into d
                            select new { Dataname = d.Key, Datapacket = d.Count() };
            var datadispatch = from o in db.Orders
                             join p in db.Products on o.IdProduct equals p.IdProduct
                             where o.IdStage == 6
                             group p by p.Name into d
                             select new { Dataname = d.Key, Datadispatch = d.Count() };




            /*передает в таблицу статистики*/
            dataallGrid.ItemsSource = dataall.ToList();
            datastageGrid.ItemsSource = datastage.ToList();
            datanotstartGrid.ItemsSource = datanotstart.ToList();
            dataotlitoGrid.ItemsSource = dataotlito.ToList();
            datapaintGrid.ItemsSource = datapaint.ToList();
            dataallpaintGrid.ItemsSource = dataallpaint.ToList();
            datapacketGrid.ItemsSource = datapacket.ToList();
            datadispatchGrid.ItemsSource = datadispatch.ToList();

        }




        ////////
        ////////
        //////// Добавление в базу новых объектов с кнопки
        ////////
        ///////

        

        /*добавляет в базу новый заказ на первой вкладке и еще раз вызывает функцию, заполняющую таблицу заказов*/
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            /*Orders order = new Orders { IdCategory = i, Nameofcategory = FruitTextBox.Text };
            db.Orders.Add(order);
            db.SaveChanges();
            Categorie();
            i++;*/
            /*находим в таблице максимальный айдишник заказа и увеличиваем на 1, чтобы добавить в таблицу новый заказ*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Orders.Select(u => u.IdOrder).FirstOrDefault();
            if (id != 0)
                id = db.Orders.Max(u => u.IdOrder);
            id++;

            /*вытаскиваем из формы добавления заказа заполненные поля и ищем в базе их айдишники*/
            if (productsList.SelectedValue != null && paintingList.SelectedValue != null && stageList.SelectedValue != null && shopList.SelectedValue != null && sizeList.SelectedValue != null)
            {
                try
                {
                    string name = productsList.SelectedValue.ToString();
                    var idname = from c in db.Products
                             where c.Name == name
                             select c.IdProduct;
                    string paint = paintingList.SelectedValue.ToString();
                    var idpaint = from c in db.Painting
                              where c.Paintingname == paint
                              select c.IdPainting;
                    string stage = stageList.SelectedValue.ToString();
                    var idstage = from c in db.Stages
                              where c.Stagename == stage
                              select c.IdStage;
                    string shop = shopList.SelectedValue.ToString();
                    var idshop = from c in db.Shops
                             where c.Nameofshop == shop
                             select c.IdShop;
                    string size = sizeList.SelectedValue.ToString();
                    var idsize = from c in db.Sizes
                             where c.Size == size
                             select c.IdSize;
                
                    if (0 < date.Text.Length)
                    {
                        if (0 < deadline.Text.Length)
                        {
                            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу заказы и сохраняем изменения*/

                            Orders order = new Orders { IdOrder = id, IdProduct = idname.FirstOrDefault(), IdPainting = idpaint.FirstOrDefault(), IdStage = idstage.FirstOrDefault(), IdShop = idshop.FirstOrDefault(), IdSize = idsize.FirstOrDefault(), Orderdate = DateTime.Parse(date.Text), Deadline = DateTime.Parse(deadline.Text), Cost = Decimal.Parse(cost.Text), Specification = specific.Text };
                            db.Orders.Add(order);
                            db.SaveChanges();

                            /*модификации*/
                            if (ordermodGrid.Items.Count > 0)
                            {
                                /*для каждой строки таблицы*/
                                for (int i = 0; i < (ordermodGrid.Items.Count); i++)
                                {
                                    /*считываем название и переделываем в текстблок*/
                                    var mod = new DataGridCellInfo(ordermodGrid.Items[i], ordermodGrid.Columns[0]);
                                    var modcontent = mod.Column.GetCellContent(mod.Item) as TextBlock;
                                    var idmod = from m in db.Modifications
                                                where m.Modificationname == modcontent.Text
                                                select m.IdModification;
                                    /*добавляем фотку в таблицу фоток*/
                                    Ordermodifications ordermods = new Ordermodifications { IdOrder = id, IdModification = idmod.FirstOrDefault() };

                                    db.Ordermodifications.Add(ordermods);
                                    db.SaveChanges();

                                }
                            }

                            /*фотки*/
                            /*считаем сколько их*/
                            if (orderphotosGrid.Items.Count > 0)
                            {
                                /*считаем максимальный айди фотографии заказов*/
                                var idphoto = db.Orderphotos.Select(u => u.IdOrderphoto).FirstOrDefault();
                                if (idphoto != 0)
                                    idphoto = db.Orderphotos.Max(u => u.IdOrderphoto);
                                idphoto++;
                                /*для каждой строки таблицы*/
                                for (int i = 0; i < (orderphotosGrid.Items.Count) - 1; i++)
                                {
                                    /*считываем название и ссылку и переделываем в текстблок*/
                                    var title = new DataGridCellInfo(orderphotosGrid.Items[i], orderphotosGrid.Columns[0]);
                                    var orderphoto = new DataGridCellInfo(orderphotosGrid.Items[i], orderphotosGrid.Columns[1]);
                                    var titlecontent = title.Column.GetCellContent(title.Item) as TextBlock;
                                    var photocontent = orderphoto.Column.GetCellContent(orderphoto.Item) as TextBlock;
                                    /*добавляем фотку в таблицу фоток*/
                                    Orderphotos orderphotos = new Orderphotos { IdOrderphoto = idphoto, IdOrder = id, Title = titlecontent.Text.ToString(), Orderphoto = photocontent.Text.ToString() };
                                    idphoto++;
                                    db.Orderphotos.Add(orderphotos);
                                    db.SaveChanges();

                                }
                                UpdateData();
                            }



                            /*заново заполняем таблицу заказов*/
                            UpdateData();
                            Mod = null;
                            Mod = new List<OrderModif>(1);
                            ordermodGrid.ItemsSource = null;
                        }
                        else
                        {
                            MessageBox.Show("Срок должен быть заполнен!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("Дата заказа должна быть заполнена!");
                    }

                }
                catch
                {
                    MessageBox.Show("Пожалуйста, заполните поле стоимость! В поле стоимость используйте только цифры. Разделительный символ - запятая.");
                }
            }
            else 
            {
                MessageBox.Show("Выберите категорию, продукт, покраску, размер, этап и магазин!");
            }
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }


        /*редактирует заказ на первой вкладке*/
        private void UpButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (ordersGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < ordersGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник заказа*/
                    var id = new DataGridCellInfo(ordersGrid.SelectedItems[i], ordersGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*вытаскиваем из формы добавления заказа заполненные поля и ищем в базе их айдишники*/
                            string paint = upPaintingList.SelectedValue.ToString();
                            var idpaint = (from c in db.Painting
                                where c.Paintingname == paint
                                select c.IdPainting).FirstOrDefault();
            
                            string size = upSizeList.SelectedValue.ToString();
                            var idsize = (from c in db.Sizes
                                where c.Size == size
                                select c.IdSize).FirstOrDefault();

                            var deadline = DateTime.Parse(upDeadline.Text);

                            var cost = Decimal.Parse(upCost.Text);

                            var specification = upSpecific.Text;

                            /*находим нужный объект по айди и  обновляем*/
                            Orders order = db.Orders.FirstOrDefault(c => c.IdOrder == j);
                            if (order != null)
                            {
                                order.IdPainting = idpaint;
                                order.IdSize = idsize;
                                order.Deadline = deadline;
                                order.Cost = cost;
                                order.Specification = specification;
                            }
                            db.SaveChanges();

                            ordersGrid.CommitEdit(DataGridEditingUnit.Row, true);
                            UpdateData();

                            /*модификации*/
                            if (upOrdermodGrid.Items.Count > 0)
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var ordmod = (from om in db.Ordermodifications
                                              where om.IdOrder == j
                                              select om).ToList();
                                foreach (Ordermodifications o in ordmod)
                                {
                                    db.Ordermodifications.Remove(o);
                                    db.SaveChanges();
                                }
                                
                                /*для каждой строки таблицы*/
                                for (int k = 0; k < (upOrdermodGrid.Items.Count); k++)
                                {
                                    /*считываем название и переделываем в текстблок*/
                                    var mod = new DataGridCellInfo(upOrdermodGrid.Items[k], upOrdermodGrid.Columns[0]);
                                    var modcontent = mod.Column.GetCellContent(mod.Item) as TextBlock;
                                    var idmod = from m in db.Modifications
                                                where m.Modificationname == modcontent.Text
                                                select m.IdModification;

                                    
                                    

                                    /*добавляем в заказ модификацию*/
                                    Ordermodifications ordermods = new Ordermodifications { IdOrder = j, IdModification = idmod.FirstOrDefault() };

                                    db.Ordermodifications.Add(ordermods);
                                    db.SaveChanges();

                                }
                            }
                            else
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var ordmod = (from om in db.Ordermodifications
                                              where om.IdOrder == j
                                              select om).ToList();
                                foreach (Ordermodifications o in ordmod)
                                {
                                    db.Ordermodifications.Remove(o);
                                    db.SaveChanges();
                                }
                            }

                            /*фотки*/
                            /*считаем сколько их*/
                            if (upOrderphotosGrid.Items.Count > 0)
                            {
                                

                                /*находим нужный объект по айди и удаляем*/
                                var ordphoto = (from op in db.Orderphotos
                                                where op.IdOrder == j
                                              select op).ToList();
                                foreach (Orderphotos p in ordphoto)
                                {
                                    db.Orderphotos.Remove(p);
                                    db.SaveChanges();
                                }

                                //считаем максимальный айди фотографии заказов
                                var idphoto = db.Orderphotos.Select(u => u.IdOrderphoto).FirstOrDefault();
                                if (idphoto != 0)
                                    idphoto = db.Orderphotos.Max(u => u.IdOrderphoto);
                                idphoto++;

                                /*для каждой строки таблицы*/
                                for (int n = 0; n < (upOrderphotosGrid.Items.Count) - 1; n++)
                                {
                                    /*считываем название и ссылку и переделываем в текстблок*/
                                    var title = new DataGridCellInfo(upOrderphotosGrid.Items[n], upOrderphotosGrid.Columns[0]);
                                    var orderphoto = new DataGridCellInfo(upOrderphotosGrid.Items[n], upOrderphotosGrid.Columns[1]);
                                    var titlecontent = title.Column.GetCellContent(title.Item) as TextBlock;
                                    var photocontent = orderphoto.Column.GetCellContent(orderphoto.Item) as TextBlock;
                                    /*добавляем фотку в таблицу фоток*/
                                    Orderphotos orderphotos = new Orderphotos { IdOrderphoto = idphoto, IdOrder = j, Title = titlecontent.Text.ToString(), Orderphoto = photocontent.Text.ToString() };
                                    idphoto++;
                                    db.Orderphotos.Add(orderphotos);
                                    db.SaveChanges();

                                }
                                UpdateData();
                            }
                            else
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var ordphoto = (from op in db.Orderphotos
                                                where op.IdOrder == j
                                                select op).ToList();
                                foreach (Orderphotos p in ordphoto)
                                {
                                    db.Orderphotos.Remove(p);
                                    db.SaveChanges();
                                }
                            }

                            /*заново заполняем таблицу заказов*/
                            UpdateData();
                            
                        }
                    }
                }
            }
            /*UpMod = null;
            UpMod = new List<OrderModif>(1);*/
        }

        /*добавляет в заказ модификацию*/
        private void Buttonordermod_Click(object sender, RoutedEventArgs e)
        {
            if (ordermodList.SelectedValue != null)
            {
                string mod = ordermodList.SelectedValue.ToString();
            
            Mod.Add(new OrderModif(mod));
            ordermodGrid.ItemsSource = null;
            ordermodGrid.ItemsSource = Mod;
            }
            else
                {
                    MessageBox.Show("Выберите модификацию");
                }
        }
            


        /*добавляет в редактирование заказа модификацию*/
        private void UpButtonordermod_Click(object sender, RoutedEventArgs e)
        {
            if (upOrdermodList.SelectedValue != null)
            {
                string mod = upOrdermodList.SelectedValue.ToString();

            UpMod.Add(new OrderModif(mod));
            upOrdermodGrid.ItemsSource = null;
            upOrdermodGrid.ItemsSource = UpMod;
            }
            else
            {
                MessageBox.Show("Выберите модификацию");
            }
        }
        


        /*добавляет в базу новое изделие на второй вкладке*/
        private void newProductButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник изделия и увеличиваем на 1, чтобы добавить в таблицу новое изделие*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Products.Select(u => u.IdProduct).FirstOrDefault();
            if (id != 0)
                id = db.Products.Max(u => u.IdProduct);
            id++;
            /*вытаскиваем из формы добавления изделия заполненные поля и ищем в базе их айдишники*/

            if (categoryProductList.SelectedValue != null)
            {

                /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
                try
                {
                
                    string category = categoryProductList.SelectedValue.ToString();
                    var idcategory = from c in db.Categories
                                     where c.Nameofcategory == category
                                     select c.IdCategory;
                    if (0 < newProduct.Text.Length && newProduct.Text.Length < 33)
                    {
                        Products product = new Products { IdProduct = id, IdCategory = idcategory.FirstOrDefault(), Name = newProduct.Text, Price = Decimal.Parse(Price.Text), Description = newDescription.Text };
                        db.Products.Add(product);

                            /*фотки*/
                            /*считаем сколько их*/
                            if (productphotosGrid.Items.Count > 0)
                            {
                                /*считаем максимальный айди фотографии изделий*/
                                var idphoto = db.Photos.Select(u => u.IdProductphoto).FirstOrDefault();
                                if (idphoto != 0)
                                    idphoto = db.Photos.Max(u => u.IdProductphoto);
                                idphoto++;
                                /*для каждой строки таблицы*/
                                for (int i = 0; i < (productphotosGrid.Items.Count) - 1; i++)
                                {
                                    /*считываем название и ссылку и переделываем в текстблок*/
                                    var title = new DataGridCellInfo(productphotosGrid.Items[i], productphotosGrid.Columns[0]);
                                    var orderphoto = new DataGridCellInfo(productphotosGrid.Items[i], productphotosGrid.Columns[1]);
                                    var titlecontent = title.Column.GetCellContent(title.Item) as TextBlock;
                                    var photocontent = orderphoto.Column.GetCellContent(orderphoto.Item) as TextBlock;
                                    /*добавляем фотку в таблицу фоток*/
                                    Photos photos = new Photos { IdProductphoto = idphoto, Title = titlecontent.Text.ToString(), Productphoto = photocontent.Text.ToString() };
                                    db.Photos.Add(photos);
                                    db.SaveChanges();
                                    Productphotos productphoto = new Productphotos { IdProductphoto = idphoto, IdProduct = id };
                                    idphoto++;
                                    db.Productphotos.Add(productphoto);
                                    db.SaveChanges();

                                }
                                UpdateData();
                            }

                            /*покраски*/
                            if (paintprodGrid.Items.Count > 0)
                            {
                                /*для каждой строки таблицы*/
                                for (int i = 0; i < (paintprodGrid.Items.Count); i++)
                                {

                                    /*считываем название и переделываем в текстблок*/
                                    var prod = new DataGridCellInfo(paintprodGrid.Items[i], paintprodGrid.Columns[0]);
                                    var prodcontent = prod.Column.GetCellContent(prod.Item) as TextBlock;
                                    var idprod = from m in db.Painting
                                                 where m.Paintingname == prodcontent.Text
                                                 select m.IdPainting;
                                    /*добавляем покраску в изделия и покраски*/
                                    Paintingproduct paintproducts = new Paintingproduct { IdProduct = id, IdPainting = idprod.FirstOrDefault() };

                                    db.Paintingproduct.Add(paintproducts);
                                    db.SaveChanges();

                                }
                            }

                            /*заново заполняем таблицу изделий*/
                            UpdateData();
                            Prod = null;
                            Prod = new List<PaintProd>(1);
                            paintprodGrid.ItemsSource = null;
                        }
                    else
                    {
                        MessageBox.Show("В названии должно быть не меньше 1 символа и не больше 32!");
                    }
                }
                catch
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля! В поле цена используйте только цифры. Разделительный символ - запятая.");

                }
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("Выберите категорию");
            }

            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в изделие покраску*/
        private void Buttonpaintprod_Click(object sender, RoutedEventArgs e)
        {
            if (paintprodList.SelectedValue != null)
            {
                string prod = paintprodList.SelectedValue.ToString();

            Prod.Add(new PaintProd(prod));
            paintprodGrid.ItemsSource = null;
            paintprodGrid.ItemsSource = Prod;
            }
            else
            {
                MessageBox.Show("Выберите покраску");
            }
        }

        /*добавляет в редактирование товара покраску*/
        private void UpButtonPaintprod_Click(object sender, RoutedEventArgs e)
        {
            if (upPaintprodList.SelectedValue != null)
            {
                string paint = upPaintprodList.SelectedValue.ToString();

            UpProd.Add(new PaintProd(paint));
            upPaintprodGrid.ItemsSource = null;
            upPaintprodGrid.ItemsSource = UpProd;
            }
            else
            {
                MessageBox.Show("Выберите покраску");
            }
        }

        /*редактирует товар на второй вкладке*/
        private void UpProductButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (productGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < productGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник товара*/
                    var id = new DataGridCellInfo(productGrid.SelectedItems[i], productGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*вытаскиваем из формы  заполненные поля и ищем в базе их айдишники*/
                            
                            var price = Decimal.Parse(upPrice.Text);

                            var description = upDescription.Text;

                            /*находим нужный объект по айди и  обновляем*/
                            Products product = db.Products.FirstOrDefault(c => c.IdProduct == j);
                            if (product != null)
                            {
                                product.Price = price;
                                product.Description = description;
                            }
                            db.SaveChanges();
                            UpdateData();

                            /*покраски*/
                            if (upPaintprodGrid.Items.Count > 0)
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var paintprod = (from om in db.Paintingproduct
                                              where om.IdProduct == j
                                              select om).ToList();
                                foreach (Paintingproduct o in paintprod)
                                {
                                    db.Paintingproduct.Remove(o);
                                    db.SaveChanges();
                                }

                                /*для каждой строки таблицы*/
                                for (int k = 0; k < (upPaintprodGrid.Items.Count); k++)
                                {
                                    /*считываем название и переделываем в текстблок*/
                                    var paint = new DataGridCellInfo(upPaintprodGrid.Items[k], upPaintprodGrid.Columns[0]);
                                    var paintcontent = paint.Column.GetCellContent(paint.Item) as TextBlock;
                                    var idpaint = from m in db.Painting
                                                where m.Paintingname == paintcontent.Text
                                                select m.IdPainting;


                                    /*добавляем в товар покраску*/
                                    Paintingproduct paintprods = new Paintingproduct { IdProduct = j, IdPainting = idpaint.FirstOrDefault() };

                                    db.Paintingproduct.Add(paintprods);
                                    db.SaveChanges();

                                }
                            }
                            else
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var paintprod = (from om in db.Paintingproduct
                                                 where om.IdProduct == j
                                                 select om).ToList();
                                foreach (Paintingproduct o in paintprod)
                                {
                                    db.Paintingproduct.Remove(o);
                                    db.SaveChanges();
                                }
                            }

                            /*фотки*/
                            /*считаем сколько их*/
                            if (upProductphotosGrid.Items.Count > 0)
                            {


                                /*находим нужный объект по айди и удаляем*/
                                var prodphoto = (from op in db.Productphotos
                                                where op.IdProduct == j
                                                select op).ToList();
                                foreach (Productphotos p in prodphoto)
                                {
                                    db.Productphotos.Remove(p);
                                    db.SaveChanges();
                                }

                                //считаем максимальный айди фотографии товаров
                                var idphoto = db.Photos.Select(u => u.IdProductphoto).FirstOrDefault();
                                if (idphoto != 0)
                                    idphoto = db.Photos.Max(u => u.IdProductphoto);
                                idphoto++;

                                /*для каждой строки таблицы*/
                                for (int n = 0; n < (upProductphotosGrid.Items.Count) - 1; n++)
                                {
                                    /*считываем название и ссылку и переделываем в текстблок*/
                                    var title = new DataGridCellInfo(upProductphotosGrid.Items[n], upProductphotosGrid.Columns[0]);
                                    var productphoto = new DataGridCellInfo(upProductphotosGrid.Items[n], upProductphotosGrid.Columns[1]);
                                    var titlecontent = title.Column.GetCellContent(title.Item) as TextBlock;
                                    var photocontent = productphoto.Column.GetCellContent(productphoto.Item) as TextBlock;
                                    /*добавляем фотку в таблицу фоток*/
                                    Photos photos  = new Photos { IdProductphoto = idphoto, Title = titlecontent.Text.ToString(), Productphoto = photocontent.Text.ToString() };
                                    db.Photos.Add(photos);
                                    db.SaveChanges();
                                    Productphotos productphotos = new Productphotos { IdProductphoto = idphoto, IdProduct = j };
                                    idphoto++;
                                    db.Productphotos.Add(productphotos);
                                    db.SaveChanges();
                                }
                                UpdateData();

                            }
                            else
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var prodphoto = (from op in db.Productphotos
                                                 where op.IdProduct == j
                                                 select op).ToList();
                                foreach (Productphotos p in prodphoto)
                                {
                                    db.Productphotos.Remove(p);
                                    db.SaveChanges();
                                }
                            }

                            /*заново заполняем таблицу заказов*/
                            UpdateData();

                        }
                    }
                }
            }
        }



        /*добавляет в базу новую категорию со второй вкладки*/
        private void Buttoncategory_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник категории и увеличиваем на 1, чтобы добавить в таблицу новую*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Categories.Select(u => u.IdCategory).FirstOrDefault();
            if (id != 0)
                id = db.Categories.Max(u => u.IdCategory);
            id++;

                /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
                try
                {

                    if (0 < newcategory.Text.Length && newcategory.Text.Length < 33)
                    {
                        /*вытаскиваем из формы добавления категории заполненное поле*/
                        Categories category = new Categories { IdCategory = id, Nameofcategory = newcategory.Text };
                        db.Categories.Add(category);
                    }
                    else
                    {
                        MessageBox.Show("В названии должно быть не меньше 1 символа и не больше 32!");
                    }
                }
                catch
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

                }
                db.SaveChanges();
            
            /*заново заполняем таблицу категорий*/
            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в базу новый магазин со второй вкладки*/
        private void Buttonshops_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник магазина и увеличиваем на 1, чтобы добавить в таблицу новый*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Shops.Select(u => u.IdShop).FirstOrDefault();
            if (id != 0)
                id = db.Shops.Max(u => u.IdShop);
            id++;


            
                /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
                try
                {
                    if (0 < namenewshops.Text.Length && namenewshops.Text.Length < 33)
                    {
                        if (0 < addressnewshops.Text.Length && addressnewshops.Text.Length < 33)
                        {
                            /*вытаскиваем из формы добавления магазина заполненные поля*/
                            Shops shop = new Shops { IdShop = id, Nameofshop = namenewshops.Text, Shopaddress = addressnewshops.Text };
                            db.Shops.Add(shop);
                        }
                        else
                        {
                            MessageBox.Show("В адресе должно быть не меньше 1 символа и не больше 128!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("В названии должно быть не меньше 1 символа и не больше 32!");
                    }
                }
                catch
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

                }
                db.SaveChanges();
            /*заново заполняем таблицу магазинов*/
            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в базу новый этап со второй вкладки*/
        private void Buttonstages_Click(object sender, RoutedEventArgs e)
        {
            try
            {
           
            /*находим в таблице максимальный айдишник этап и увеличиваем на 1, чтобы добавить в таблицу новый*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Stages.Select(u => u.IdStage).FirstOrDefault();
            if (id != 0)
                id = db.Stages.Max(u => u.IdStage);
            id++;

            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
            try
            {

                if (0 < namenewstages.Text.Length && namenewstages.Text.Length < 33)
                {
                    /*вытаскиваем из формы добавления этапа заполненные поля*/
                    Stages stage = new Stages { IdStage = id, Stagename = namenewstages.Text };
                    db.Stages.Add(stage);
                }
                else
                {
                    MessageBox.Show("В названии этапа должно быть не меньше 1 символа и не больше 32!");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

            }
            db.SaveChanges();

            
            /*заново заполняем таблицу этапов*/
            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в базу новый размер со второй вкладки*/
        private void Buttonsizes_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник размера и увеличиваем на 1, чтобы добавить в таблицу новый*/

            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Sizes.Select(u => u.IdSize).FirstOrDefault();
            if (id != 0)
                id = db.Sizes.Max(u => u.IdSize);
            id++;

            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
            try
            {

                if (0 < namenewsizes.Text.Length && namenewsizes.Text.Length < 9)
                {
                    /*вытаскиваем из формы добавления размера заполненные поля*/
                    Sizes size = new Sizes { IdSize = id, Size = namenewsizes.Text };
                    db.Sizes.Add(size);
                }
                else
                {
                    MessageBox.Show("В размере должно быть не меньше 1 символа и не больше 8!");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

            }
            db.SaveChanges();

            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в базу новую модификацию со второй вкладки*/
        private void Buttonmod_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник модификации и увеличиваем на 1, чтобы добавить в таблицу новую*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Modifications.Select(u => u.IdModification).FirstOrDefault();
            if (id != 0)
                id = db.Modifications.Max(u => u.IdModification);
            id++;

            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
            try
            {

                if (0 < namenewmod.Text.Length && namenewmod.Text.Length < 33)
                {
                    /*вытаскиваем из формы добавления размера заполненные поля*/
                    Modifications mod = new Modifications { IdModification = id, Modificationname = namenewmod.Text };
                    db.Modifications.Add(mod);
                }
                else
                {
                    MessageBox.Show("В названии модификации должно быть не меньше 1 символа и не больше 32!");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

            }

            
            db.SaveChanges();
            /*заново заполняем таблицы модификаций*/
            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }



        /*добавляет в базу новую покраску со второй вкладки*/
        private void Buttonpaint_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник покраски и увеличиваем на 1, чтобы добавить в таблицу новую*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Painting.Select(u => u.IdPainting).FirstOrDefault();
            if (id != 0)
                id = db.Painting.Max(u => u.IdPainting);
            id++;

            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
            try
            {

                if (0 < namenewpaint.Text.Length && namenewpaint.Text.Length < 65)
                {
                    /*вытаскиваем из формы добавления покраски заполненные поля*/
                    Painting paint = new Painting { IdPainting = id, Paintingname = namenewpaint.Text };
                    db.Painting.Add(paint);
                }
                else
                {
                    MessageBox.Show("В названии покраски должно быть не меньше 1 символа и не больше 64!");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

            }

            db.SaveChanges();

            /*цвета*/
            if (paincolorGrid.Items.Count > 0)
            {
                /*для каждой строки таблицы*/
                for (int i = 0; i < (paincolorGrid.Items.Count) ; i++)
                {

                    /*считываем название и переделываем в текстблок*/
                    var col = new DataGridCellInfo(paincolorGrid.Items[i], paincolorGrid.Columns[0]);
                    var colcontent = col.Column.GetCellContent(col.Item) as TextBlock;
                    var idcol = from m in db.Colors
                                where m.Colorname == colcontent.Text
                                select m.IdColor;
                    /*добавляем цвет в цвета и покраски*/
                    Paintcolors paintcolors = new Paintcolors { IdPainting = id, IdColor = idcol.FirstOrDefault(), Comment = "." };

                    db.Paintcolors.Add(paintcolors);
                    db.SaveChanges();

                }
            }

            /*заново заполняем таблицы покраски*/
            UpdateData();
            Col = null;
            Col = new List<PaintCol>(1);
            paincolorGrid.ItemsSource = null;
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в покраску цвет*/
        private void Buttonpaintcolor_Click(object sender, RoutedEventArgs e)
        {

            if (paintcolorList.SelectedValue != null)
            {
                string col = paintcolorList.SelectedValue.ToString();

                Col.Add(new PaintCol(col));
                paincolorGrid.ItemsSource = null;
                paincolorGrid.ItemsSource = Col;
            }
            else
            {
                MessageBox.Show("Выберите цвет");
            }

            
        }
        
        /*добавляет в редактирование покраски цвет*/
        private void UpButtonPaint_Click(object sender, RoutedEventArgs e)
        {
            if (upPaintList.SelectedValue != null)
            {
                string color = upPaintList.SelectedValue.ToString();

                UpCol.Add(new PaintCol(color));
                upPaintGrid.ItemsSource = null;
                upPaintGrid.ItemsSource = UpCol;
            }
            else
            {
                MessageBox.Show("Выберите цвет");
            }
        }

        /*редактирует покраску на второй вкладке*/
        private void UpPaintingButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*считаем сколько строк в таблице*/
            if (paintGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < paintGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник покраски*/
                    var id = new DataGridCellInfo(paintGrid.SelectedItems[i], paintGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            
                            /*цвета*/
                            if (upPaintGrid.Items.Count > 0)
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var paintcol = (from om in db.Paintcolors
                                                 where om.IdPainting == j
                                                 select om).ToList();
                                foreach (Paintcolors o in paintcol)
                                {
                                    db.Paintcolors.Remove(o);
                                    db.SaveChanges();
                                }

                                /*для каждой строки таблицы*/
                                for (int k = 0; k < (upPaintGrid.Items.Count); k++)
                                {
                                    /*считываем название и переделываем в текстблок*/
                                    var color = new DataGridCellInfo(upPaintGrid.Items[k], upPaintGrid.Columns[0]);
                                    var colorcontent = color.Column.GetCellContent(color.Item) as TextBlock;
                                    var idcolor = from m in db.Colors
                                                  where m.Colorname == colorcontent.Text
                                                  select m.IdColor;


                                    /*добавляем в покраску цвет*/
                                    Paintcolors paintcols = new Paintcolors { IdPainting = j, IdColor = idcolor.FirstOrDefault(), Comment = "." };

                                    db.Paintcolors.Add(paintcols);
                                    db.SaveChanges();

                                }
                            }
                            else
                            {
                                /*находим нужный объект по айди и удаляем*/
                                var paintcol = (from om in db.Paintcolors
                                                where om.IdPainting == j
                                                select om).ToList();
                                foreach (Paintcolors o in paintcol)
                                {
                                    db.Paintcolors.Remove(o);
                                    db.SaveChanges();
                                }
                            }

                            

                            /*заново заполняем таблицу заказов*/
                            UpdateData();

                        }
                    }
                }
            }
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }



        /*добавляет в базу новый цвет со второй вкладки*/
        private void Buttoncolor_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник цвета и увеличиваем на 1, чтобы добавить в таблицу новый*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Colors.Select(u => u.IdColor).FirstOrDefault();
            if (id != 0)
                id = db.Colors.Max(u => u.IdColor);
            id++;

            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
            try
            {
                if (0 < namenewcolor.Text.Length && namenewcolor.Text.Length < 33)
                {
                    if (0 < numbernewcolor.Text.Length && numbernewcolor.Text.Length < 17)
                    {
                        /*вытаскиваем из формы добавления цвета заполненные поля*/
                        Colors color = new Colors { IdColor = id, Colorname = namenewcolor.Text, Colornumber = numbernewcolor.Text };
                        db.Colors.Add(color);
                    }
                    else
                    {
                        MessageBox.Show("В номере цвета должно быть не меньше 1 символа и не больше 16!");
                    }
                }
                else
                {
                    MessageBox.Show("В названии цвета должно быть не меньше 1 символа и не больше 32!");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля! ");

            }

            
            db.SaveChanges();
            /*заново заполняем таблицу цветов*/
            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }

        /*добавляет в базу новую фотографию изделия со второй вкладки*/
        private void Buttonproductphoto_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник фотографии и увеличиваем на 1, чтобы добавить в таблицу новую*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Photos.Select(u => u.IdProductphoto).FirstOrDefault();
            if (id != 0)
                id = db.Photos.Max(u => u.IdProductphoto);
            id++;

            /*создаем новый объект с введенными данными и добавляем его в базу в таблицу изделия и сохраняем изменения*/
            try
            {
                if (0 < namenewphoto.Text.Length && namenewphoto.Text.Length < 33)
                {
                    
                        /*вытаскиваем из формы добавления фотографии заполненные поля*/
                        Photos photo = new Photos { IdProductphoto = id, Productphoto = addressnewphoto.Text, Title = namenewphoto.Text };
                        db.Photos.Add(photo);
                    
                }
                else
                {
                    MessageBox.Show("В названии фотографии должно быть не меньше 1 символа и не больше 32!");
                }
            }
            catch
            {
                MessageBox.Show("Пожалуйста, заполните все обязательные поля!");

            }

            
            db.SaveChanges();
            /*заново заполняем таблицу фотографий*/
            UpdateData();
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }



        /*добавляет в базу новую отправку с третьей вкладки*/
        private void Buttondispatch_Click(object sender, RoutedEventArgs e)
        {
            try
            {
            
            /*находим в таблице максимальный айдишник отправки и увеличиваем на 1, чтобы добавить в таблицу новую*/
            /*если там ничего нет, то просто берем айдишник единичку*/
            var id = db.Dispatch.Select(u => u.IdDispatch).FirstOrDefault();
            if (id != 0)
                id = db.Dispatch.Max(u => u.IdDispatch);
            id++;

            if (numberorderdispatch.SelectedValue != null)
            {
                try
                {
                    /*вытаскиваем из формы добавления отправки заполненные поля*/
                    var idorder = Int32.Parse(numberorderdispatch.Text);

                    if (0 < tracknumberdispatch.Text.Length && tracknumberdispatch.Text.Length < 33 )
                    {
                        if (0 < datedispatch.Text.Length)
                        {
                            if (0 < addressdispatch.Text.Length && addressdispatch.Text.Length < 129)
                            {
                                Dispatch dispatch = new Dispatch { IdDispatch = id, IdOrder = idorder, Tracknumber = tracknumberdispatch.Text, Date = DateTime.Parse(datedispatch.Text), Address = addressdispatch.Text };
                                db.Dispatch.Add(dispatch);
                                db.SaveChanges();

                                /*меняем статус заказа на отправлено*/
                                var order = (from o in db.Orders
                                             where o.IdOrder == idorder
                                             select o).FirstOrDefault();
                                if (order != null)
                                {
                                    order.IdStage = 6;
                                }
                            }
                            else
                            {
                                MessageBox.Show("В адресе должно быть не меньше 1 символа и не больше 128!");
                            }
                        }
                        else
                        {
                            MessageBox.Show("Дата должна быть заполнена!");
                        }
                    }
                    else
                    {
                        MessageBox.Show("В трек-номере должно быть не меньше 1 символа и не больше 32!");
                    }

                }
                catch
                {
                    MessageBox.Show("Пожалуйста, заполните все обязательные поля! В поле цена используйте только цифры. Разделительный символ - запятая.");

                }
                db.SaveChanges();
                /*заново заполняем таблицу отправок*/
                UpdateData();
            }
            else
            {
                MessageBox.Show("Выберите заказ!");
            }
            }
            catch (Npgsql.PostgresException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
            catch (System.InvalidOperationException)
            {
                MessageBox.Show("Отсутствует соединение с базой данных!");
            }
        }




        ////////
        ////////
        //////// Удаление из базы выбранных объектов с кнопки
        ////////
        ///////

        /*удаление заказа с первой вкладки*/
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (ordersGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < ordersGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник заказа*/
                    var id = new DataGridCellInfo(ordersGrid.SelectedItems[i], ordersGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            
                            /*находим все фотки заказа и удаляем*/
                            var orderphoto = (db.Orderphotos.Where(p => p.IdOrder == j)).ToList();
                            foreach (Orderphotos o in orderphoto)
                            {
                                db.Orderphotos.Remove(o);
                            }

                            /*находим все модификации заказа и удаляем*/
                            var ordermod = (db.Ordermodifications.Where(p => p.IdOrder == j)).ToList();
                            foreach (Ordermodifications o in ordermod)
                            {
                                db.Ordermodifications.Remove(o);
                            }

                            /*находим нужный заказ по айди и удаляем*/
                            Orders order = db.Orders.FirstOrDefault(c => c.IdOrder == j);
                            if (order != null)
                            {

                                
                                try
                                {
                                    db.Orders.Remove(order);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляет модификацию из нового заказа*/
        private void DeleteOrdermodButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк выбрано в таблице*/
            if (ordermodGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть выбранные данные*/
                for (int i = 0; i < ordermodGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки*/
                    var item = new DataGridCellInfo(ordermodGrid.SelectedItems[i], ordermodGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = item.Column.GetCellContent(item.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    if (content != null)
                    {
                        OrderModif res = Mod.FirstOrDefault(x => x.mod == content.Text);
                        if (res != null)
                        {
                            Mod.Remove(res);
                            ordermodGrid.ItemsSource = null;
                            ordermodGrid.ItemsSource = Mod;
                        }
                    }
                }
            }
        }

        /*удаляет модификацию из редактирования заказа*/
        private void UpDeleteOrdermodButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк выбрано в таблице*/
            if (upOrdermodGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть выбранные данные*/
                for (int i = 0; i < upOrdermodGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки*/
                    var item = new DataGridCellInfo(upOrdermodGrid.SelectedItems[i], upOrdermodGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = item.Column.GetCellContent(item.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    if (content != null)
                    {
                        OrderModif res = UpMod.FirstOrDefault(x => x.mod == content.Text);
                        if (res != null)
                        {
                            UpMod.Remove(res);
                            upOrdermodGrid.ItemsSource = null;
                            upOrdermodGrid.ItemsSource = UpMod;
                        }
                    }
                }
            }
        }



        /*удаление изделия со второй вкладки*/
        private void DeleteProductButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (productGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < productGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник изделия*/
                    var id = new DataGridCellInfo(productGrid.SelectedItems[i], productGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим все покраски изделия и удаляем*/
                            var painting = (db.Paintingproduct.Where(p => p.IdProduct == j)).ToList();
                            foreach (Paintingproduct o in painting)
                            {
                                db.Paintingproduct.Remove(o);
                            }

                            /*находим все фотки изделия и удаляем*/
                            var photos = (db.Productphotos.Where(p => p.IdProduct == j)).ToList();
                            foreach (Productphotos c in photos)
                            {
                                db.Productphotos.Remove(c);
                            }

                            /*находим нужное изделие по айди и удаляем*/
                            Products product = db.Products.FirstOrDefault(c => c.IdProduct == j);
                            if (product != null)
                            {
                                try
                                {
                                    db.Products.Remove(product);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляет покраску из нового изделия*/
        private void DeletePaintprodButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк выбрано в таблице*/
            if (paintprodGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть выбранные данные*/
                for (int i = 0; i < paintprodGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки*/
                    var item = new DataGridCellInfo(paintprodGrid.SelectedItems[i], paintprodGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = item.Column.GetCellContent(item.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    if (content != null)
                    {
                        PaintProd res = Prod.FirstOrDefault(x => x.prod == content.Text);
                        if (res != null)
                        {
                            Prod.Remove(res);
                            paintprodGrid.ItemsSource = null;
                            paintprodGrid.ItemsSource = Prod;
                        }
                    }
                }
            }
        }

        /*удаляет покраску из редактирования изделия*/
        private void UpDeletePaintprodButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк выбрано в таблице*/
            if (upPaintprodGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть выбранные данные*/
                for (int i = 0; i < upPaintprodGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки*/
                    var item = new DataGridCellInfo(upPaintprodGrid.SelectedItems[i], upPaintprodGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = item.Column.GetCellContent(item.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    if (content != null)
                    {
                        PaintProd res = UpProd.FirstOrDefault(x => x.prod == content.Text);
                        if (res != null)
                        {
                            UpProd.Remove(res);
                            upPaintprodGrid.ItemsSource = null;
                            upPaintprodGrid.ItemsSource = UpProd;
                        }
                    }
                }
            }
        }



        /*удаляем строку из таблицы категорий во второй вкладке*/
        private void DeleteCategoryButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (categoryGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < categoryGrid.SelectedItems.Count; i++)
                {
                    /*куски кода, которые частично работали*/
                    /*var ci = new DataGridCellInfo(categoryGrid.SelectedItems[i], categoryGrid.Columns[1]);
                    var content = ci.Column.GetCellContent(ci.Item) as TextBlock;
                    MessageBox.Show(content.Text);*/

                    /*Categories category = categoryGrid.SelectedItems[i] as Categories;
                    if (category != null)
                    {
                        db.Categories.Remove(category);
                    }*/

                    /*считываем первый столбец выбранной строки, в этом случае это айдишник категории*/
                    var id = new DataGridCellInfo(categoryGrid.SelectedItems[i], categoryGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Categories category = db.Categories.FirstOrDefault(c => c.IdCategory == j);
                            if (category != null)
                            {
                                try
                                {
                                    db.Categories.Remove(category);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }

                        /*Categories category = db.Categories.Include(c => c.Nameofcategory).Where(c => c.IdCategory == content);
                        db.Categories.Remove(category);*/
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляем строку из таблицы магазинов во второй вкладке*/
        private void DeleteShopsButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (shopsGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < shopsGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник магазина*/
                    var id = new DataGridCellInfo(shopsGrid.SelectedItems[i], shopsGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Shops shop = db.Shops.FirstOrDefault(c => c.IdShop == j);
                            if (shop != null)
                            {
                                
                                try
                                {
                                    db.Shops.Remove(shop);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляем строку из таблицы этапов во второй вкладке*/
        private void DeleteStagesButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (stagesGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < stagesGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник этапа*/
                    var id = new DataGridCellInfo(stagesGrid.SelectedItems[i], stagesGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Stages stage = db.Stages.FirstOrDefault(c => c.IdStage == j);
                            if (stage != null)
                            {
                                
                                try
                                {
                                    db.Stages.Remove(stage);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляем строку из таблицы размеров во второй вкладке*/
        private void DeleteSizesButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (sizesGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < sizesGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник размера*/
                    var id = new DataGridCellInfo(sizesGrid.SelectedItems[i], sizesGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Sizes size = db.Sizes.FirstOrDefault(c => c.IdSize == j);
                            if (size != null)
                            {
                                
                                try
                                {
                                    db.Sizes.Remove(size);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляем строку из таблицы модификаций во второй вкладке*/
        private void DeleteModButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (modGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < modGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник модификации*/
                    var id = new DataGridCellInfo(modGrid.SelectedItems[i], modGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Modifications mod = db.Modifications.FirstOrDefault(c => c.IdModification == j);
                            if (mod != null)
                            {
                                
                                try
                                {
                                    db.Modifications.Remove(mod);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }



        /*удаляем строку из таблицы покрасок во второй вкладке*/
        private void DeletePaintButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (paintGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < paintGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник покраски*/
                    var id = new DataGridCellInfo(paintGrid.SelectedItems[i], paintGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим все цвета и покраски и удаляем*/
                            var paintcolor = (db.Paintcolors.Where(p => p.IdPainting == j)).ToList();
                            foreach (Paintcolors c in paintcolor)
                            {
                                db.Paintcolors.Remove(c);
                            }

                            /*находим нужный объект по айди и удаляем*/
                            Painting paint = db.Painting.FirstOrDefault(c => c.IdPainting == j);
                            if (paint != null)
                            {
                                
                                try
                                {
                                    db.Painting.Remove(paint);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляет цвет из новой покраски*/
        private void DeletePaintcolorButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк выбрано в таблице*/
            if (paincolorGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть выбранные данные*/
                for (int i = 0; i < paincolorGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки*/
                    var item = new DataGridCellInfo(paincolorGrid.SelectedItems[i], paincolorGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = item.Column.GetCellContent(item.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    if (content != null)
                    {
                        PaintCol res = Col.FirstOrDefault(x => x.col == content.Text);
                        if (res != null)
                        {
                            Col.Remove(res);
                            paincolorGrid.ItemsSource = null;
                            paincolorGrid.ItemsSource = Col;
                        }
                    }
                }
            }
        }

        /*удаляет цвет из редактирования покраски*/
        private void UpDeletePaintButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк выбрано в таблице*/
            if (upPaintGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть выбранные данные*/
                for (int i = 0; i < upPaintGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки*/
                    var item = new DataGridCellInfo(upPaintGrid.SelectedItems[i], upPaintGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = item.Column.GetCellContent(item.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    if (content != null)
                    {
                        PaintCol res = UpCol.FirstOrDefault(x => x.col == content.Text);
                        if (res != null)
                        {
                            UpCol.Remove(res);
                            upPaintGrid.ItemsSource = null;
                            upPaintGrid.ItemsSource = UpCol;
                        }
                    }
                }
            }
        }



        /*удаляем строку из таблицы цветов во второй вкладке*/
        private void DeleteColorButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (colorGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < colorGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник цвета*/
                    var id = new DataGridCellInfo(colorGrid.SelectedItems[i], colorGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Colors color = db.Colors.FirstOrDefault(c => c.IdColor == j);
                            if (color != null)
                            {
                                
                                try
                                {
                                    db.Colors.Remove(color);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляем строку из таблицы фотографий изделий во второй вкладке*/
        private void DeleteProductPhotoButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (productphotoGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < productphotoGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник фотографии*/
                    var id = new DataGridCellInfo(productphotoGrid.SelectedItems[i], productphotoGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Photos photo = db.Photos.FirstOrDefault(c => c.IdProductphoto == j);
                            if (photo != null)
                            {
                                
                                try
                                {
                                    db.Photos.Remove(photo);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        /*удаляем строку из таблицы отправок в третьей вкладке*/
        private void DeleteDispatchButton_Click(object sender, RoutedEventArgs e)
        {
            /*считаем сколько строк в таблице*/
            if (dispatchGrid.SelectedItems.Count > 0)
            {
                /*пока в таблице есть данные*/
                for (int i = 0; i < dispatchGrid.SelectedItems.Count; i++)
                {
                    /*считываем первый столбец выбранной строки, в этом случае это айдишник отправки*/
                    var id = new DataGridCellInfo(dispatchGrid.SelectedItems[i], dispatchGrid.Columns[0]);
                    /*делаем эту штуку текстом*/
                    var content = id.Column.GetCellContent(id.Item) as TextBlock;
                    /*если ячейка не пустая*/
                    int j;
                    if (content != null)
                    {
                        /*если в ячейке только числа, получим это число в переменную джей - это и есть нужный айди*/
                        bool success = int.TryParse(content.Text.Trim(), out j);
                        if (success)
                        {
                            /*находим нужный объект по айди и удаляем*/
                            Dispatch dispatch = db.Dispatch.FirstOrDefault(c => c.IdDispatch == j);
                            if (dispatch != null)
                            {
                                
                                try
                                {
                                    db.Dispatch.Remove(dispatch);
                                    db.SaveChanges();
                                }
                                catch
                                {
                                    MessageBox.Show("Этот объект используется");
                                }
                            }
                        }
                    }
                }
            }
            //db.SaveChanges();
            UpdateData();
        }

        private void checkstagesList_DropDownOpened(object sender, EventArgs e)
        {
            ComboBox cmbx = sender as ComboBox;
            if (cmbx.SelectedValue == null)
            {
                MessageBox.Show("Этот заказ уже отправлен!");
            }
        }
    }
}
