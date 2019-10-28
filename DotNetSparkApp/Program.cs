using System;
using Microsoft.Spark.Sql;
using static Microsoft.Spark.Sql.Functions;

namespace DotNetSparkApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Create a Spark session
            var spark = SparkSession
                .Builder()
                .AppName("DotNet-Word-Count")
                .GetOrCreate();

            // Create initial DataFrame
            var df = spark.Read().Text("file:/home/anderson.souza/bin/lorem_action.txt");
            df.PrintSchema();

            // Count words
            var words = df
                .Select(Functions.Split(Functions.Col("value"), " ").Alias("words"))
                .Select(Functions.Explode(Functions.Col("words")).Alias("words"))
                .Select(Functions.RegexpReplace(Functions.Col("words"), "\\.|,", "").Alias("words"))
                .Filter(Functions.Length(Functions.Col("words")) > 5).Alias("words")
                .GroupBy("words")
                .Count()
                .OrderBy(Functions.Col("count").Desc());

            // Show results
            words.PrintSchema();
            words.Show();
        }      
    }
}