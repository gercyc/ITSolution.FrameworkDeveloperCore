using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data;
using System.Reflection;
using System.Text;

namespace ITSolution.Framework.Core.BaseClasses.Tools
{
    public static class DataUtil
    {

        /// <summary>
        ///Converte uma lista tipada para um DataTable por reflexão.
        ///
        /// A class POCO não pode conter propriedas de outras classes.
        /// 
        /// Exemplo: 
        ///     CentroCusto
        ///         string nome;
        ///         ....
        ///         //considerando uma classe com chave estrangeira
        ///         Lancamento lancamento;
        ///          public virtual ICollection<Lancamento> Lancamentos;
        ///     Caso haja o metódo irá falhas
        ///     
        /// </summary>
        /// <typeparam colName="T"></typeparam>
        /// <param colName="lista"></param>
        /// <returns></returns>
        public static DataTable ConvertToDataTable<T>(List<T> lista)
        {

            if (lista == null)
            {
                Console.WriteLine("Lista não informada na conversão para DataTable");
                return new DataTable();
            }
            DataTable dataTable = new DataTable(typeof(T).Name);

            try
            {
                //Get all the properties
                PropertyInfo[] Props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

                foreach (PropertyInfo prop in Props)
                {
                    //Setting column names as Property names
                    try
                    {
                        dataTable.Columns.Add(prop.Name, prop.PropertyType);
                    }
                    catch (NotSupportedException)
                    {
                        dataTable.Columns.Add(prop.Name);
                    }

                }

                foreach (T item in lista)
                {
                    try
                    {
                        var values = new object[Props.Length];

                        for (int i = 0; i < Props.Length; i++)
                        {
                            //inserting property values to datatable rows
                            if (Props[i].PropertyType.BaseType == typeof(System.Enum))
                                values[i] = Convert.ToInt32(Props[i].GetValue(item, null));
                            else
                                values[i] = Props[i].GetValue(item, null);
                        }

                        dataTable.Rows.Add(values);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Falha na conversão do paramentro da lista em dados\n"
                            + ex.StackTrace + "\n\n" + ex.Message);

                        throw ex;
                    }


                }
            }
            catch (NullReferenceException ex)
            {
                Console.WriteLine("Falha ao gerar DataTable através da lista\n" + ex.StackTrace + "\n" + ex.Message, "Falha");

            }

            //put first breakpoint here and check datatable
            return dataTable;
        }
    }
}
