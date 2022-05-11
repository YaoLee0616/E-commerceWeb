using Microsoft.EntityFrameworkCore;
using E_commerceWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace E_commerceWeb.Controllers
{
    public class WebController : Controller
    {
        private IGoodsRepository Goodsrepo; // 购物接口
        private ITypeRepository Typerepo; // 类别管理接口
        private IGoodsListRepository GoodsListrepo; // 商品列表管理接口
        private IOrderRepository Orderrepo; // 订单管理接口
        private IShoppingCartRepository ShoppingCartrepo; // 购物车管理接口


        /** 构造器 **/
        public WebController(IGoodsRepository Goodsr, ITypeRepository Typer, IGoodsListRepository GoodsListr,
            IOrderRepository Orderr, IShoppingCartRepository ShoppingCartr)
        {
            Goodsrepo = Goodsr;
            Typerepo = Typer;
            GoodsListrepo = GoodsListr;
            Orderrepo = Orderr;
            ShoppingCartrepo = ShoppingCartr;
        }

        /** 购物主页 **/
        public async Task<IActionResult> Index(string currentFilter, string searchString, int? pageNumber)
        {
            var goods = Goodsrepo.SelectAll();

            List<SelectListItem> types =
                (
                from c in Typerepo.SelectAll()
                orderby c.TypeName ascending
                select new SelectListItem
                {
                    Text = c.TypeName,
                    Value = c.TypeId.ToString()
                }
                ).ToList(); // 类别选择项
            ViewBag.Types = types;

            if (searchString != null) // 筛选时跳到第一页
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter; // 在筛选选过的数据中进行翻页
            }

            ViewBag.CurrentFilter = searchString; // 存储筛选状态

            if (!String.IsNullOrEmpty(searchString) && searchString != "-1")
            {
                types.Find(e => e.Value.Equals(searchString.ToString())).Selected = true; // 控制页面Select框选中状态
                goods = goods.Where(s => s.TypeId.ToString().Equals(searchString)); // 筛选
            }
            int pageSize = 10; // 每页数量
            ViewBag.ShoppingCartSum = ShoppingCartrepo.Sum(); // 购物车内商品数量
            return View(await PaginatedList<Goods>.CreateAsync(goods.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        public IActionResult CreateTestData() // 创建测试数据
        {
            Orderrepo.Clear();
            Typerepo.CreateTestData();
            Goodsrepo.CreateTestData();
            return RedirectToAction("Index");
        }

        /** 类别管理 **/
        public async Task<IActionResult> TypeManager(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var types = Typerepo.SelectAll(); // 获取全部类别数据

            ViewBag.CurrentSort = sortOrder; // 存储排序状态

            ViewBag.TypeIdSortParm = String.IsNullOrEmpty(sortOrder) ? "typeId_desc" : "";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString; // 存储筛选状态

            if (!String.IsNullOrEmpty(searchString))
            {
                types = types.Where(s => s.TypeName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "typeId_desc":
                    types = types.OrderByDescending(s => s.TypeId);
                    break;
                default:
                    types = types.OrderBy(s => s.TypeId);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Models.Type>.CreateAsync(types.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        /**商品管理**/
        public async Task<IActionResult> GoodsManager(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            var goods = Goodsrepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;

            ViewBag.GoodsIdSortParm = String.IsNullOrEmpty(sortOrder) ? "goodsId_desc" : "";
            ViewBag.GoodsNameSortParm = sortOrder == "GoodsName" ? "goodsName_desc" : "GoodsName";
            ViewBag.TypeIdSortParm = sortOrder == "TypeId" ? "typeId_desc" : "TypeId";
            ViewBag.GoodsPurchasePriceSortParm = sortOrder == "GoodsPurchasePrice" ? "goodsPurchasePrice_desc" : "GoodsPurchasePrice";
            ViewBag.GoodsRetailPriceSortParm = sortOrder == "GoodsRetailPrice" ? "goodsRetailPrice_desc" : "GoodsRetailPrice";

            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                goods = goods.Where(s => s.GoodsName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "goodsId_desc":
                    goods = goods.OrderByDescending(s => s.GoodsId);
                    break;
                case "GoodsName":
                    goods = goods.OrderBy(s => s.GoodsName);
                    break;
                case "goodsName_desc":
                    goods = goods.OrderByDescending(s => s.GoodsName);
                    break;
                case "TypeId":
                    goods = goods.OrderBy(s => s.TypeId);
                    break;
                case "typeId_desc":
                    goods = goods.OrderByDescending(s => s.TypeId);
                    break;

                case "GoodsPurchasePrice":
                    goods = goods.OrderBy(s => s.GoodsPurchasePrice);
                    break;
                case "goodsPurchasePrice_desc":
                    goods = goods.OrderByDescending(s => s.GoodsPurchasePrice);
                    break;

                case "GoodsRetailPrice":
                    goods = goods.OrderBy(s => s.GoodsRetailPrice);
                    break;
                case "goodsRetailPrice_desc":
                    goods = goods.OrderByDescending(s => s.GoodsRetailPrice);
                    break;

                default:
                    goods = goods.OrderBy(s => s.GoodsId);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Goods>.CreateAsync(goods.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        /**订单管理**/
        public async Task<IActionResult> OrderManager(string sortOrder, string currentFilter, string searchString, int? pageNumber)
        {
            if(GoodsLists!=null) GoodsLists.Clear();
            var orders = Orderrepo.SelectAll();

            ViewBag.CurrentSort = sortOrder;

            ViewBag.OrderIdSortParm = String.IsNullOrEmpty(sortOrder) ? "orderId_desc" : "";
            ViewBag.CustomerNameSortParm = sortOrder == "CustomerName" ? "customerName_desc" : "CustomerName";
            if (searchString != null)
            {
                pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            if (!String.IsNullOrEmpty(searchString))
            {
                orders = orders.Where(s => s.CustomerName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "orderId_desc":
                    orders = orders.OrderByDescending(s => s.OrderId);
                    break;
                case "CustomerName":
                    orders = orders.OrderBy(s => s.CustomerName);
                    break;
                case "customerName_desc":
                    orders = orders.OrderByDescending(s => s.CustomerName);
                    break;
                default:
                    orders = orders.OrderBy(s => s.OrderId);
                    break;
            }

            int pageSize = 8;
            return View(await PaginatedList<Order>.CreateAsync(orders.AsNoTracking(), pageNumber ?? 1, pageSize));
        }

        [HttpGet]
        public IActionResult UpdateGoods(int id)
        {
            List<SelectListItem> types =
                (
                from c in Typerepo.SelectAll()
                orderby c.TypeName ascending
                select new SelectListItem
                {
                    Text = c.TypeName,
                    Value = c.TypeId.ToString()
                }
                ).ToList();
            ViewBag.Types = types;
            return View(Goodsrepo.SelectGoodsById(id));
        }

        [HttpPost]
        public IActionResult UpdateGoods(Goods goods)
        {
            Goodsrepo.UpdateGoodsById(goods);
            return RedirectToAction("GoodsManager");
        }

        public IActionResult DeleteGoods(int id)
        {
            Goodsrepo.DeleteGoodsById(id);
            return RedirectToAction("GoodsManager");
        }

        [HttpGet]
        public IActionResult AddGoods()
        {
            List<SelectListItem> types =
               (
               from c in Typerepo.SelectAll()
               orderby c.TypeName ascending
               select new SelectListItem
               {
                   Text = c.TypeName,
                   Value = c.TypeId.ToString()
               }
               ).ToList();
            ViewBag.Types = types;
            return View("UpdateGoods");
        }

        [HttpPost]
        public IActionResult AddGoods(Goods goods)
        {
            Goodsrepo.AddGoods(goods);
            return RedirectToAction("GoodsManager");
        }

        [HttpGet]
        public IActionResult UpdateType(int id)
        {
            return View(Typerepo.SelectTypeById(id));
        }

        [HttpPost]
        public IActionResult UpdateType(Models.Type type)
        {
            Typerepo.UpdateTypeById(type);
            return RedirectToAction("TypeManager");
        }

        [HttpGet]
        public IActionResult AddType(int id)
        {
            List<SelectListItem> types =
                (
                from c in Typerepo.SelectAll()
                orderby c.TypeName ascending
                select new SelectListItem
                {
                    Text = c.TypeName,
                    Value = c.TypeId.ToString()
                }
                ).ToList();
            ViewBag.Types = types;
            return View("UpdateType");
        }

        [HttpPost]
        public IActionResult AddType(Models.Type type)
        {
            Typerepo.AddType(type);
            return RedirectToAction("TypeManager");
        }

        public IActionResult DeleteType(int id)
        {
            Typerepo.DeleteTypeById(id);
            return RedirectToAction("TypeManager");
        }

        /** 添加到购物车 **/
        public IActionResult AddShoppingCart(int id)
        {
            string name = Goodsrepo.SelectAll().First(e => e.GoodsId == id).GoodsName;
            ShoppingCartrepo.AddOrUpdate(id, name);
            return RedirectToAction("Index");
        }

        public IActionResult ShowShoppingCart()
        {
            return View(ShoppingCartrepo.SelectAll());
        }


        [HttpGet]
        public IActionResult SettleAccounts()
        {
            return View();
        }

        [HttpPost]
        public IActionResult SettleAccounts(Order order)
        {
            order.OrderStatus = 0;
            order.ShipmentsStatus = 0;
            int orderId = Orderrepo.Add(order);
            if (GoodsLists.Count()==0)
            {
                foreach (var item in ShoppingCartrepo.SelectAll())
                {
                    GoodsList goodsList = new GoodsList();
                    goodsList.GoodsId = item.GoodsId;
                    goodsList.GoodsCount = item.Count;
                    goodsList.OrderId = orderId;
                    GoodsListrepo.Add(goodsList);
                }
                ShoppingCartrepo.Clear();
            }
            else
            {
                foreach (var item in GoodsLists)
                {
                    item.OrderId = orderId;
                    GoodsListrepo.Add(item);
                }
                GoodsLists.Clear();
            }
            return RedirectToAction("OrderManager");
        }

        public IActionResult ShowGoodsLists(int id)
        {
            return View((Orderrepo.SelectAll().First(e => e.OrderId == id)).GoodsLists);
        }

        public IActionResult DeleteOrder(int id)
        {
            Orderrepo.DeleteOrderById(id);
            return RedirectToAction("OrderManager");
        }

        [HttpGet]
        public IActionResult UpdateOrder(int id)
        {
            return View(Orderrepo.SelectOrderById(id));
        }

        [HttpPost]
        public IActionResult UpdateOrder(Order order)
        {
            Orderrepo.UpdateOrderById(order);
            return RedirectToAction("OrderManager");
        }

  

        [HttpGet]
        public IActionResult AddOrder(int id)
        {
            List<SelectListItem> goods =
            (
            from g in Goodsrepo.SelectAll()
            orderby g.GoodsName ascending
            select new SelectListItem
            {
                Text = g.GoodsName,
                Value = g.GoodsId.ToString()
            }
            ).ToList();
 
            ViewBag.Goods = goods;
            return View("AddGoodsList");
        }
        
        [HttpPost]
        public IActionResult AddOrder(Order order)
        {

            Orderrepo.UpdateOrderById(order);
            return RedirectToAction("OrderManager");
        }

        private static  List<GoodsList> GoodsLists = new List<GoodsList> { };
        public IActionResult AddGoodsList(GoodsList goodsList)
        {
            List<SelectListItem> goods =
            (
            from g in Goodsrepo.SelectAll()
            orderby g.GoodsName ascending
            select new SelectListItem
            {
                Text = g.GoodsName,
                Value = g.GoodsId.ToString()
            }
            ).ToList();

            ViewBag.Goods = goods;
            GoodsLists.Add(goodsList);
            return View(GoodsLists);
        }

    }
}
