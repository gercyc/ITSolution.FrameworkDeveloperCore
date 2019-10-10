using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using ITSolution.Framework.Core.Server.BaseClasses;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;

namespace ConsoleApp1
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ExecuteLoop();
            stopwatch.Stop();
            Console.WriteLine($"Consultas executadas em: {stopwatch.Elapsed.Seconds} segundos.");
            Console.ReadLine();
        }

        private static void ExecuteLoop()
        {
            ParameterList pLst = new ParameterList();
            pLst.Add("IdCliFor", Operator.Equals, 0, Condition.And);
            pLst.Add("Reputacao", Operator.Equals, 2, Condition.None);

            for (int i = 1; i < 5; i++)
            {
                pLst["IdCliFor"].Value = i;
                using (IDataReader r = ConnectionFactory.Instance.ExecuteReader("SELECT COUNT(1) FROM CliFor", pLst))
                {
                    while (r.Read())
                    {
                        Console.WriteLine(r.GetInt32(0));
                        Thread.Sleep((int) TimeSpan.FromSeconds(4).TotalMilliseconds);
                    }
                }
            }
        }

        private async static Task TestTasks()
        {
            ConcurrentBag<DataTable> tableList = new ConcurrentBag<DataTable>();
            List<Task<DataTable>> tasks = new List<Task<DataTable>>();
            for (int i = 0; i < 5; i++)
            {
                int index = i;
                DataTable dt =
                    await Task.Run(() => ConnectionFactory.Instance.ExecuteQuery($"BscFilial_{index}",
                        "SELECT * FROM LF.BSC_FILIAL"));
                tableList.Add(dt);
                Thread.Sleep((int) TimeSpan.FromSeconds(8).TotalMilliseconds);
                Console.WriteLine($"TaskId: {0} | Started: {DateTime.Now} | Aguardando 3 segundos");
                Console.WriteLine($"Tables > {tableList.Count} Rows > {dt.Rows}");
            }


//            tasks.ForEach(
//                async t =>
//                {
//                    
//                    Thread.Sleep((int) TimeSpan.FromSeconds(3).TotalMilliseconds);
//                    Console.WriteLine($"TaskId: {t.Id} | Started: {DateTime.Now} | Aguardando 3 segundos");
//                    Console.WriteLine($"Table > {t.Result.TableName} Rows > {t.Result.Rows.Count}");
//                });
//            Task.WaitAll(tasks.ToArray());
        }
    }
}