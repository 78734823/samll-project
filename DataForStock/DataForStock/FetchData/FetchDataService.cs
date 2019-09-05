﻿using AngleSharp;
using AngleSharp.Html.Dom;
using HtmlAgilityPack;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataForStock.FetchData
{
    /// <summary>
    /// 获取数据
    /// </summary>
    public class FetchDataService
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        private static IBrowsingContext context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());

        public static async Task FetchDataAsync()
        {
            try
            {
                logger.Info($"爬虫程序开始运行");
                //2.打开连接
                var address = "http://data.eastmoney.com/report/stock.jshtml";
                var document = await  context.OpenAsync(address);
                logger.Info(document.QuerySelector("#stock_table").TextContent);

                //3.选择内容范围
                var cellSelector = "#stock_table table.table-model tbody tr";
                var cells = document.QuerySelectorAll(cellSelector);
                //4.获取内容
                var titles = cells.Select(m => m.TextContent);

                foreach (var title in titles)
                {
                    Console.WriteLine(title);
                    logger.Info(title);
                }
                logger.Info($"爬虫程序结束运行");
            }
            catch (Exception ex)
            {
                logger.Info($"程序异常：{ex.ToString()}");
            }
        }

        public static void Test2() {
            try
            {
                var address = "http://data.eastmoney.com/report/stock.jshtml";
                var web = new HtmlWeb();
                var doc = web.Load(address);
                var node = doc.DocumentNode.SelectSingleNode("//*[@id='stock_table']");
                logger.Info(node.InnerHtml);
            }
            catch (Exception ex) {
                logger.Info($"程序异常：{ex.ToString()}");
            }
        }

    }
}
