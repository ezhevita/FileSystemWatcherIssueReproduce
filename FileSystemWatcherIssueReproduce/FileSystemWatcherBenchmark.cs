using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace FileSystemWatcherIssueReproduce;

[SimpleJob(RuntimeMoniker.Net80)]
[SimpleJob(RuntimeMoniker.Net90)]
public class FileSystemWatcherBenchmark
{
	[Params(10)]
	[SuppressMessage("ReSharper", "UnusedAutoPropertyAccessor.Global", Justification = "Set implicitly by BenchmarkDotNet")]
	public int FileCount { get; set; }

	private string _testPath = null!;

	[GlobalSetup]
	public void Setup()
	{
		// replace this with custom path if needed
		_testPath = Environment.GetEnvironmentVariable("BASE_PATH") ?? throw new ArgumentNullException();
	}

	[Benchmark]
	public void Run()
	{
		for (var i = 0; i < FileCount; i++)
		{
			using var watcher = new FileSystemWatcher(Path.Join(_testPath));
			watcher.Filter = $"file{i:D2}.txt";
			watcher.EnableRaisingEvents = true;
		}
	}
}
