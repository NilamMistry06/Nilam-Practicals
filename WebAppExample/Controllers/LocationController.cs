using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using NuGet.Packaging;
using System;
using System.Text.Json.Nodes;
namespace WebAppExample.Controllers
{
    public class LocationController : Controller
    {
        private static readonly HttpClient client = new HttpClient();
        IConfiguration configuration;
        public LocationController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            var model = new List<string> { "Item1", "Item2", "Item3" };
            return View(model);
        }
        public IActionResult FindLocation()
        {
            return View();
            //try
            //{
            //    string url = string.Format(configuration["locationUrl"], ip);
            //    string json = getResponse(url);
            //    if (IsValidJsonTest(json))
            //    {
            //        var obj = JsonObject.Parse(json);
            //        if (obj != null)
            //        {
            //            return Ok("City: " + (string)obj["city"]);
            //        }
            //    }
            //    return BadRequest(json);

            //}
            //catch (Exception ex)
            //{
            //    return BadRequest(ex.Message);
            //}
        }
        [HttpPost("GetLocation")]
        [ValidateAntiForgeryToken]
        public IActionResult GetLocation(string ip)
        {
            try
            {
                string url = string.Format(configuration["locationUrl"], ip);
                string json = getResponse(url);
                if (IsValidJsonTest(json))
                {
                    var obj = JsonObject.Parse(json);
                    if (obj != null)
                    {
                        return Ok("City: " + (string)obj["city"]);
                    }
                }
                return Ok(json);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPost("GetSubString")]
        [ValidateAntiForgeryToken]
        public IActionResult GetSubString(string str)
        {
            try
            {



                return Ok();

            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        //public static async Task getResponse(string url)
        //{
        //    HttpClient client = new HttpClient();
        //    HttpResponseMessage response = await client.GetAsync(url);
        //}
        private string getResponse(string url)
        {
            HttpResponseMessage response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();
            //var request = System.Net.WebRequest.Create(url);

            //using (WebResponse wrs = request.GetResponse())
            //{
            //    using (Stream stream = wrs.GetResponseStream())
            //    {
            //        using (StreamReader reader = new StreamReader(stream))
            //        {
            //            return reader.ReadToEnd();
            //        }
            //    }
            //}
            var result = JsonObject.Parse(response.Content.ReadAsStringAsync().Result);
            if (result != null)
            {
                var error = result["error"];
                if (error != null)
                    return "Error Code: " + error["code"] + " - " + error["info"];
            }
            return response.Content.ReadAsStringAsync().Result;
        }
        public bool IsValidJsonTest(string jsonString)
        {
            try
            {
                JObject.Parse(jsonString);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        //Strongly Typed View
        public ActionResult ProductDetails()
        {
            GetProduct();
            var product = new Product { Id = 1, Name = "Laptop", Price = 999.99M };
            return View(product);
        }

        //Dynamic View
        public ActionResult DynamicView()
        {
            ViewBag.Message = "Your application description page.";
            bool test1 = hasDuplicate(new int[]{ 1,2,3,4});
            var aa = GroupAnagrams(new string[] { "stop", "pots", "reed", "", "tops", "deer", "opts", "" });
            //bool palindrome = IsPalindrome("")
            int[] t = { 3, 2, 3 };
            int[] test = TwoSum(t,5);
            int[] A = { 3, 8, 9, 7, 6 };
            ShiftArray(A, 3);
            FindBinaryGap(529);
            return View();
        }
        public bool hasDuplicate(int[] nums)
        {
            if (nums.Count() == 0) return false;



            //int X = 1, Y = 5, D = 2;
            //int count1 = 0;
            //while (X <= Y)
            //{
            //    count1++;
            //    X = X + D;
            //}

            int[] count = new int[4];
            for (int i = 0; i < nums.Count(); i++)
            {
                if (!count.Contains(i))
                {
                    count[i] = nums[i];
                }
                else
                {
                    return true;
                }
            }
            return false;
        }
        public int FindBinaryGap(int N)
        {
            //long test = 1000010001;
            //2^9 + 2^4 + 2^0
            string binary = Convert.ToString(N, 2);
            var splitVal = binary.Trim('0').Split(new[] { '1' });
            var val = splitVal.Max(x => x.Length);
            return val;

            //or

            //561892
            //1162
            string binary2 = Convert.ToString(N, 2);
            var splitVal2 = binary.Trim('0').Split(new[] { '1' });
            Array.Sort(splitVal, (a, b) => b.CompareTo(a));
            int val2 = splitVal[0].Length;
            return val2;

            //if (binary[binary.Count() - 1] == '0')
            //{
            //    if (splitVal.Length > 1) val = splitVal[1].Length;
            //    else return 0;
            //}

        }

        //Shift or rotate array elements
        public int[] ShiftArray(int[] A, int K)
        {
            int last;
            for(int i=1; i<=K; i++)
            {
                last = A[A.Count() - 1];
                A = A.Take(A.Count() - 1).ToArray();
                A = A.Prepend(last).ToArray();
            }

            return A;
        }

        public int[] TwoSum(int[] nums, int target)
        {
            LinkedList<int> arrayVals = new LinkedList<int>(new int[]{ 0,1,2});
            for (int i = 0; i < 3; i++)
            {
                LinkedListNode<int> last = arrayVals.Last;
                arrayVals.RemoveLast();
                arrayVals.AddFirst(last);
            }
            int[] rotatedArray = new int[arrayVals.Count];
            rotatedArray.CopyTo(arrayVals.ToArrayre(), 0);



            if (nums.Count() == 0) return nums;
            for (int i = 0; i < nums.Count(); i++)
            {
                if (nums[i] + nums[i + 1] == target)
                {
                    int[] result = { i, i + 1 };
                    return result;
                }
            }
            return null;
        }

        public List<List<string>> GroupAnagrams(string[] strs)
        {
            List<string> inputArr = strs.ToList();
            List<List<string>> anagramsList = new List<List<string>>();
            List<string> anagramsSubList;
            int i = 0;
            while (i < inputArr.Count)
            {
                anagramsSubList = new List<string>();
                anagramsSubList.Add(inputArr[i]);
                for (int j = i + 1; j < inputArr.Count; j++)
                {
                    if (IsAnagrams(inputArr[i], inputArr[j]))
                    {
                        anagramsSubList.Add(inputArr[j]);
                    }
                }
                anagramsList.Add(anagramsSubList);
                inputArr.RemoveAll(item => anagramsSubList.Contains(item));
            }
            return anagramsList;
        }
        public bool IsPalindrome(string s)
        {
            s = s.ToLower().Replace(" ", "");
            for (int i = 0; i <= (s.Count() - 1) / 2; i++)
            {
                //for(int j=s.Count();j<=i;j--){
                if (s[i] != s[s.Count() - 1 - i]) return false;

                //}
            }
            return true;
        }
        public bool IsAnagrams(string i, string j)
        {
            return i.OrderBy(b => b).SequenceEqual(j.OrderBy(c => c));
        }

        //public int[] MergeSort(int[] arr)
        //{
        //    if (arr.Length == 0 || arr.Length == 1) return arr;
        //    int mid = (arr.Length - 1) / 2;
        //    int[] firstArr = arr[0..mid];
        //    int[] secondArr = arr[(mid+1)..];
        //    firstArr = MergeSort(firstArr);
        //    secondArr = MergeSort(secondArr);

        //    return null;
        //}

        //Razor Views
        public ActionResult RazorViews()
        {
            ViewBag.Message = "Your contact page.";
            return View();
        }

        public delegate int PrintDelegate(int number);

        public IActionResult DelegateExample()
        {
            // Create an instance of the Printer class
            Printer printer = new Printer();

            // Instantiate the delegate and assign the PrintNumber method
            PrintDelegate printDelegate = new PrintDelegate(printer.PrintNumber);

            // Invoke the delegate
            ViewBag.PrintNumber = printDelegate(5);

            // Reassign the delegate to point to the PrintSquare method
            printDelegate = new PrintDelegate(printer.PrintSquare);

            // Invoke the delegate again           
            ViewBag.PrintSquare = printDelegate(5);


            //multicast delegate
            printDelegate = new PrintDelegate(printer.PrintNumber);
            printDelegate += printer.PrintSquare;
            ViewBag.MultiCastDelegate = printDelegate(5);


            // Anonymous method assigned to a delegate
            printDelegate = delegate (int number)
            {
                return number;
            };
            ViewBag.AnonymousMethod = printDelegate(5);


            // Lambda expression assigned to a delegate
            printDelegate = (int number) =>
            {
                return number;
            };
            ViewBag.LambdaExpression = printDelegate(5);

            return View();
        }
        public ActionResult AddProduct()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                var category = new Category { Name = "Electronics" };
                context.Categories.Add(category);

                var product = new Product { Name = "Laptop", Price = 999.99M, Category = category };
                context.Products.Add(product);

                context.SaveChanges();
            }
            return View();
        }
        public ActionResult GetProduct()
        {
            var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
            optionsBuilder.UseSqlServer("DefaultConnection");
            using (var context = new AppDbContext(optionsBuilder.Options))
            {
                // var products = context.Products.Include(d=>d.Category).ToList();
                var products = context.Products.ToList();
                foreach (var product in products)
                {
                    Console.WriteLine($"{product.Name} - {product.Category.Name}");
                }
            }
            return View();
        }

    }
}
