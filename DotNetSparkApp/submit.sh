#!/bin/bash -xe

~/bin/spark-2.4.1-bin-hadoop2.7/bin/spark-submit \
    --class org.apache.spark.deploy.dotnet.DotnetRunner \
    --master local[4] bin/Debug/netcoreapp3.0/microsoft-spark-2.4.x-0.5.0.jar \
    dotnet bin/Debug/netcoreapp3.0/DotNetSparkApp.dll