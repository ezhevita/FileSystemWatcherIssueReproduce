using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace FileSystemWatcherIssueReproduce;

[SimpleJob(RuntimeMoniker.Net90)]
#pragma warning disable CA1515 // Consider making public types internal -- needed for BenchmarkDotNet
public class FileSystemWatcherBenchmark
#pragma warning restore CA1515 // Consider making public types internal
{
	private string _testPath = null!;

	[GlobalSetup]
	public void Setup()
	{
		_testPath = Path.Combine(Path.GetTempPath(), "test");
		if (!Directory.Exists(_testPath))
		{
			Directory.CreateDirectory(_testPath);
		}
	}

	[GlobalCleanup]
	public void Cleanup()
	{
		Directory.Delete(_testPath, false);
	}

	[Benchmark]
	public void Run()
	{
		using var watcher = new FileSystemWatcher(Path.Join(_testPath));
		watcher.EnableRaisingEvents = true;
	}
}
