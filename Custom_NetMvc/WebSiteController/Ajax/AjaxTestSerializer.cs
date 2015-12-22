﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WebSiteModel;
using MyMVC;

namespace WebSiteController
{
	public class AjaxTestSerializer
	{
		// 假设只有一个客户端用于测试。（很不规范的写法，请勿模仿）
		private static TestSerializerModel s_lastTestSerializerModel;

		private TestSerializerModel GetTestSerializerModel()
		{
			Random rand = new Random();
			s_lastTestSerializerModel = new TestSerializerModel {
				StringVal = "Fish Li",
				DtValue = DateTime.Now,
				IntVal = rand.Next(10, 100),
				Money = 1000M * (decimal)rand.NextDouble(),
				Guid = Guid.NewGuid()
			};
			return s_lastTestSerializerModel;
		}

		[Action]
		public object GetXml()
		{
			TestSerializerModel model = GetTestSerializerModel();
			return new XmlResult(model);
		}


		[Action]
		public object GetJson()
		{
			TestSerializerModel model = GetTestSerializerModel();
			return new JsonResult(model);
		}


		[Action]
		public string Test1(TestSerializerModel model)
		{
			return model.ToString();
		}

		[Action]
		public string Test2(string stringVal, int intVal, DateTime dtValue, decimal money, Guid guid)
		{
			TestSerializerModel model = TestSerializerModel.Create(stringVal, intVal, dtValue, money, guid);
			return model.ToString();
		}


		[Action]
		public int Test1_AutoCheck(TestSerializerModel model)
		{
			if( System.Web.HttpContext.Current.Request.ContentType.StartsWith("application/json") ) 
				model.DtValue = model.DtValue.ToLocalTime();

			string xml1 = model.ToString();
			string xml2 = s_lastTestSerializerModel.ToString();
			return (xml1 == xml2 ? 1 : 0);
		}

		[Action]
		public int Test2_AutoCheck(string stringVal, int intVal, DateTime dtValue, decimal money, Guid guid)
		{
			if( System.Web.HttpContext.Current.Request.ContentType.StartsWith("application/json") )
				dtValue = dtValue.ToLocalTime();

			TestSerializerModel model = TestSerializerModel.Create(stringVal, intVal, dtValue, money, guid);

			string xml1 = model.ToString();
			string xml2 = s_lastTestSerializerModel.ToString();
			return (xml1 == xml2 ? 1 : 0);
		}






















		// 假设只有一个客户端用于测试。（很不规范的写法，请勿模仿）
		private static TestSerializerModel2 s_lastTestSerializerModel2;



		private TestSerializerModel2 GetTestSerializerModel2()
		{
			Random rand = new Random();

			TestSerializerModel2 model = new TestSerializerModel2 { StringVal = "Fish" };

			if( rand.Next(1, 100) > 50 )
				model.DtValue = DateTime.Now;

			if( rand.Next(1, 100) > 50 )
				model.IntVal = rand.Next(10, 100);

			if( rand.Next(1, 100) > 50 )
				model.Money = 1000M * (decimal)rand.NextDouble();

			if( rand.Next(1, 100) > 50 )
				model.Guid = Guid.NewGuid();

			s_lastTestSerializerModel2 = model;
			return model;
		}

		[Action]
		public object GetXml2()
		{
			TestSerializerModel2 model = GetTestSerializerModel2();
			return new XmlResult(model);
		}


		[Action]
		public object GetJson2()
		{
			TestSerializerModel2 model = GetTestSerializerModel2();
			return new JsonResult(model);
		}

		[Action]
		public string Test3(TestSerializerModel2 model)
		{
			return model.ToString();
		}

		[Action]
		public string Test4(string stringVal, int? intVal, DateTime? dtValue, decimal? money, Guid? guid)
		{
			TestSerializerModel2 model = TestSerializerModel2.Create(stringVal, intVal, dtValue, money, guid);
			return model.ToString();
		}



		[Action]
		public int Test3_AutoCheck(TestSerializerModel2 model)
		{
			if( model.DtValue.HasValue && System.Web.HttpContext.Current.Request.ContentType.StartsWith("application/json") )
				model.DtValue = model.DtValue.Value.ToLocalTime();

			string xml1 = model.ToString();
			string xml2 = s_lastTestSerializerModel2.ToString();
			return (xml1 == xml2 ? 1 : 0);
		}

		[Action]
		public int Test4_AutoCheck(string stringVal, int? intVal, DateTime? dtValue, decimal? money, Guid? guid)
		{
			if( dtValue.HasValue && System.Web.HttpContext.Current.Request.ContentType.StartsWith("application/json") )
				dtValue = dtValue.Value.ToLocalTime();

			TestSerializerModel2 model = TestSerializerModel2.Create(stringVal, intVal, dtValue, money, guid);

			string xml1 = model.ToString();
			string xml2 = s_lastTestSerializerModel2.ToString();
			return (xml1 == xml2 ? 1 : 0);
		}

















		// 假设只有一个客户端用于测试。（很不规范的写法，请勿模仿）
		private static TestSerializerModel3 s_lastTestSerializerModel3;


		private TestSerializerModel3 GetTestSerializerModel3()
		{
			Random rand = new Random();
			TestSerializerModel3 model = new TestSerializerModel3 { StringVal = "Fish Li" };

			if( rand.Next(1, 100) > 50 ) {
				int index = rand.Next(1, WebSiteDB.MyNorthwind.Categories.Count - 1);
				model.Category = WebSiteDB.MyNorthwind.Categories.Skip(index).Take(1).First();
			}

			if( rand.Next(1, 100) > 50 ) {
				int index = rand.Next(1, WebSiteDB.MyNorthwind.Products.Count - 1);
				model.Product = WebSiteDB.MyNorthwind.Products.Skip(index).Take(1).First();
			}

			if( rand.Next(1, 100) > 50 ) {
				int index = rand.Next(1, WebSiteDB.MyNorthwind.Customers.Count - 1);
				model.Customer = WebSiteDB.MyNorthwind.Customers.Skip(index).Take(1).First();
			}
			
			s_lastTestSerializerModel3 = model;
			return model;
		}

		[Action]
		public object GetXml5()
		{
			TestSerializerModel3 model = GetTestSerializerModel3();
			return new XmlResult(model);
		}


		[Action]
		public object GetJson5()
		{
			TestSerializerModel3 model = GetTestSerializerModel3();
			return new JsonResult(model);
		}


		[Action]
		public int Test5(TestSerializerModel3 model)
		{
			string xml1 = XmlHelper.XmlSerialize(model, Encoding.UTF8);
			string xml2 = XmlHelper.XmlSerialize(s_lastTestSerializerModel3, Encoding.UTF8);
			return (xml1 == xml2 ? 1 : 0);
		}

		[Action]
		public int Test6(Category category, Product product, Customer customer, string stringVal)
		{
			TestSerializerModel3 model = new TestSerializerModel3 {
				StringVal = stringVal,
				Category = category,
				Customer = customer,
				Product = product
			};

			string xml1 = XmlHelper.XmlSerialize(model, Encoding.UTF8);
			string xml2 = XmlHelper.XmlSerialize(s_lastTestSerializerModel3, Encoding.UTF8);
			return (xml1 == xml2 ? 1 : 0);
		}






	}
}
