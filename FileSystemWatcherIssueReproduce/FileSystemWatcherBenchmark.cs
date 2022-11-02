using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace FileSystemWatcherIssueReproduce;

[SimpleJob(RuntimeMoniker.Net60)]
[SimpleJob(RuntimeMoniker.Net70)]
public class FileSystemWatcherBenchmark
{
	[Params(10)]
	public int FileCount { get; set; }

	[Params(4)]
	public int ThreadCount { get; set; }

	public string TestPath { get; set; } = null!;

	[GlobalSetup]
	public void Setup()
	{
		TestPath = Environment.GetEnvironmentVariable("BASE_PATH")!; // replace this with custom path if needed
	}

	[Benchmark]
	public void RunSingleThreaded()
	{
		for (var i = 0; i < FileCount; i++)
		{
			using var watcher = new FileSystemWatcher(Path.Join(TestPath)) {Filter = $"file{i:D2}.txt"};
			watcher.EnableRaisingEvents = true;
		}
	}

	[Benchmark]
	public void RunMultiThreaded()
	{
		var threads = new Thread[ThreadCount];
		for (var i = 0; i < threads.Length; i++)
		{
			threads[i] = new Thread(RunSingleThreaded);
		}

		foreach (var thread in threads)
		{
			thread.Start();
		}

		foreach (var thread in threads)
		{
			thread.Join();
		}
	}
}
