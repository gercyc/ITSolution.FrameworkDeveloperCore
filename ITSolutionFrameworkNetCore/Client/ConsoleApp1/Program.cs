using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using ITSolution.Framework.Core.Server.BaseClasses;
using ITSolution.Framework.Core.Server.BaseClasses.Repository;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors.Internal;

namespace ConsoleApp1
{
    class Program
    {
        static void Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            ParameterList pLst = new ParameterList();
            pLst.Add("@IdCliFor", Operator.Equals, 0);

            for (int i = 1; i < 10; i++)
            {
                pLst["@IdCliFor"].Value = i;
                using (IDataReader r = ConnectionFactory.Instance.ExecuteReader("SELECT IdCliFor FROM CLIFOR", pLst))
                {
                    while (r.Read())
                    {
                        Console.WriteLine(r.GetInt32(0));
                        Thread.Sleep((int) TimeSpan.FromSeconds(4).TotalMilliseconds);
                    }
                }
            }


            stopwatch.Stop();
            Console.WriteLine($"Consultas executadas em: {stopwatch.Elapsed.Seconds} segundos.");
            Console.ReadLine();
        }

        private static List<Task<DataTable>> TestTasks()
        {
            ConcurrentBag<DataTable> tableList = new ConcurrentBag<DataTable>();
            List<Task<DataTable>> tasks = new List<Task<DataTable>>();
            List<Task> tasksR = new List<Task>();
            for (int i = 0; i < 20; i++)
            {
                int index = i;
                Task<DataTable> task = new Task<DataTable>(
                    () => ConnectionFactory.Instance.ExecuteQuery($"CliFor_{index}",
                        "SELECT * FROM CLIFOR"));

                tasks.Add(task);
            }

            for (int i = 0; i < 2; i++)
            {
                int index = i;
                Task task = new Task(
                    () =>
                    {
                        using (IDataReader r = ConnectionFactory.Instance.ExecuteReader("SELECT * FROM CLIFOR"))
                        {
                            while (r.Read())
                            {
                                Console.Write(r.GetString(4));
                            }

                            Console.WriteLine("---");
                        }
                    });

                tasksR.Add(task);
            }

            tasksR.ForEach(
                t =>
                {
                    t.Start();
                    Thread.Sleep((int) TimeSpan.FromSeconds(3).TotalMilliseconds);
                    Console.WriteLine($"TaskId: {t.Id} | Started: {DateTime.Now} | Aguardando 3 segundos");
//                    Console.WriteLine($"Table > {t.Result.TableName} Rows > {t.Result.Rows.Count}");
                });

            Task.WaitAll(tasksR.ToArray());
            return tasks;
        }
    }
}