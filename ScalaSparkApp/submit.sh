#!/bin/bash -xe

cp /home/anderson.souza/bin/apps/ScalaSparkApp/target/ScalaSparkApp.jar ScalaSparkApp.jar

~/bin/spark-2.4.1-bin-hadoop2.7/bin/spark-submit \
  --master local[4] \
  --name "Scala-Word-Count" \
  --class "br.com.demo.Executor" \
  "ScalaSparkApp.jar"
