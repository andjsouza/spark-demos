package br.com.demo

import org.apache.spark.sql.SparkSession
import org.apache.spark.sql.functions.{explode, length, regexp_replace, split}

object Executor extends App with Serializable {

  val spark =  SparkSession.builder
    .config("spark.sql.catalogImplementation", "in-memory")
    .master("local[*]")
    .appName("Scala-Word-Count")
    .getOrCreate

  import spark.implicits._

  // Create initial DataFrame
  val df = spark.read.text("file:/home/anderson.souza/bin/lorem_action.txt")
  df.printSchema()

  // Count words
  val words = df
    .select(split($"value", " ").alias("words"))
    .select(explode($"words").alias("words"))
    .select(regexp_replace($"words", "\\.|,", "").alias("words"))
    .filter(length($"words") > 5).alias("words")
    .groupBy($"words")
    .count()
    .orderBy($"count".desc)

  // Show results
  words.printSchema
  words.show

}
