using Amazon.DynamoDBv2.Model;

namespace TextTheWeather.Core.Entities.DynamoDb;

public class DynamoDbScanResult
{
	public List<Dictionary<string, AttributeValue>> Items { get; set; }
	public int Count { get; set; }
	public int ScannedCount { get; set; }
}