﻿var directoryPath = "C:/OneDrive/Code/AdventOfCodeInputs/2021/Puzzles/";
var watch = new Stopwatch();
var totalTime = new Stopwatch();
totalTime.Start();
watch.Start();
var day1File = $"{directoryPath}day1.json";
var day1Part1 = Day1.CountIncreases(day1File);
Console.WriteLine($"Day1 part 1: {day1Part1}");
var day1Part2 = Day1.CountIncreasesBy3(day1File);
Console.WriteLine($"Day1 part 2: {day1Part2}");
watch.Stop();
Console.WriteLine($"Day one time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day2File = $"{directoryPath}day2.txt";
var day2Part1 = Day2.Navigate(day2File);
Console.WriteLine($"Day2 part 1: {day2Part1}");
var day2Part2 = Day2.Aim(day2File);
Console.WriteLine($"Day2 part 2: {day2Part2}");
watch.Stop();
Console.WriteLine($"Day2 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day3File = $"{directoryPath}day3.txt";
var day3Part1 = Day3.CalculatePowerConsumption(day3File);
Console.WriteLine($"Day3 part 1: {day3Part1}");
var day3Part2 = Day3.CalculateLifeSupportRating(day3File);
Console.WriteLine($"Day3 part 2: {day3Part2}");
watch.Stop();
Console.WriteLine($"Day3 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day4File = $"{directoryPath}day4.txt";
var day4Part1 = Day4.PlayBingo(day4File);
Console.WriteLine($"Day4 part 1: {day4Part1}");
var day4Part2 = Day4.LastBoardToWin(day4File);
Console.WriteLine($"Day4 part 2: {day4Part2}");
watch.Stop();
Console.WriteLine($"Day4 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day5File = $"{directoryPath}day5.txt";
var day5Part1 = Day5.GetOverlappingPoints(day5File);
Console.WriteLine($"Day5 part 1: {day5Part1}");
var day5Part2 = Day5.GetOverlappingPoints(day5File, true);
Console.WriteLine($"Day5 part 2: {day5Part2}");
watch.Stop();
Console.WriteLine($"Day5 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day6File = $"{directoryPath}day6.txt";
var day6Part1 = Day6.CountLanternfish(day6File, 80);
Console.WriteLine($"Day6 part 1: {day6Part1}");
var day6Part2 = Day6.CountLanternfish(day6File, 256);
Console.WriteLine($"Day6 part 2: {day6Part2}");
watch.Stop();
Console.WriteLine($"Day6 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day7File = $"{directoryPath}day7.txt";
var day7Part1 = Day7.CalculateFuelConsumption(day7File);
Console.WriteLine($"Day7 part 1: {day7Part1}");
var day7Part2 = Day7.CalculateFuelConsumption2(day7File);
Console.WriteLine($"Day7 part 2: {day7Part2}");
watch.Stop();
Console.WriteLine($"Day7 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day8File = $"{directoryPath}day8.txt";
var day8Part1 = Day8.CountDigits(day8File);
Console.WriteLine($"Day8 part 1: {day8Part1}");
var day8Part2 = Day8.GetTotal(day8File);
Console.WriteLine($"Day8 part 2: {day8Part2}");
watch.Stop();
Console.WriteLine($"Day8 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day9File = $"{directoryPath}day9.txt";
var day9Part1 = Day9.CalculateLowpoint(day9File);
Console.WriteLine($"Day9 part 1: {day9Part1}");
var day9Part2 = Day9.CalculateLargestBasins(day9File);
Console.WriteLine($"Day9 part 2: {day9Part2}");
watch.Stop();
Console.WriteLine($"Day9 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day10File = $"{directoryPath}day10.txt";
var day10Part1 = Day10.CalculateErrors(day10File);
Console.WriteLine($"Day10 part 1: {day10Part1}");
var day10Part2 = Day10.CalculateCompletions(day10File);
Console.WriteLine($"Day10 part 2: {day10Part2}");
watch.Stop();
Console.WriteLine($"Day10 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day11File = $"{directoryPath}day11.txt";
var day11Part1 = Day11.CalculateFlashes(day11File);
Console.WriteLine($"Day11 part 1: {day11Part1}");
var day11Part2 = Day11.CalculateSynchronized(day11File);
Console.WriteLine($"Day11 part 2: {day11Part2}");
watch.Stop();
Console.WriteLine($"Day11 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day12File = $"{directoryPath}day12.txt";
var day12Part1 = Day12.FindAllPaths(day12File);
Console.WriteLine($"Day12 part 1: {day12Part1}");
var day12Part2 = Day12.FindAllPaths(day12File, true);
Console.WriteLine($"Day12 part 2: {day12Part2}");
watch.Stop();
Console.WriteLine($"Day12 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day13File = $"{directoryPath}day13.txt";
var (day13Part1, _) = Day13.CountDots(day13File);
Console.WriteLine($"Day13 part 1: {day13Part1}");
Day13.GetCode(day13File);
Console.WriteLine($"Day13 part 2:");
watch.Stop();
Console.WriteLine($"Day13 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day14File = $"{directoryPath}day14.txt";
var day14Part1 = Day14.CalculateElements(day14File);
Console.WriteLine($"Day14 part 1: {day14Part1}");
var result = Day14.CalculateElements(day14File, 40);
Console.WriteLine($"Day14 part 2: {result}");
watch.Stop();
Console.WriteLine($"Day14 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day15File = $"{directoryPath}day15.txt";
var day15Part1 = Day15.CalculateRisk(day15File);
Console.WriteLine($"Day15 part 1: {day15Part1}");
var day15Part2 = Day15.CalculateRisk2(day15File);
Console.WriteLine($"Day15 part 2: {day15Part2}");
watch.Stop();
Console.WriteLine($"Day15 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day16File = $"{directoryPath}day16.txt";
var day16Part1 = Day16.GetMessageVersionSum(day16File);
Console.WriteLine($"Day16 part 1: {day16Part1}");
var day16Part2 = Day16.EvaluteExpression(day16File);
Console.WriteLine($"Day16 part 2: {day16Part2}");
watch.Stop();
Console.WriteLine($"Day16 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day17Part1 = Day17.GetHighestY("target area: x=277..318, y=-92..-53");
Console.WriteLine($"Day17 part 1: {day17Part1}");
var day17Part2 = Day17.FindAllTrajectories("target area: x=277..318, y=-92..-53");
Console.WriteLine($"Day17 part 2: {day17Part2}");
watch.Stop();
Console.WriteLine($"Day17 time: {watch.Elapsed.TotalMilliseconds}ms");
watch.Restart();
var day18File = $"{directoryPath}day18.txt";
var day18Part1 = Day18.CompleteHomework(day18File);
Console.WriteLine($"Day18 part 1: {day18Part1}");
var day18Part2 = Day18.GetLargestMagnitued(day18File);
Console.WriteLine($"Day18 part 2: {day18Part2}");
watch.Stop();
Console.WriteLine($"Day18 time: {watch.Elapsed.TotalMilliseconds}ms");
//watch.Restart();
//var day19File = $"{directoryPath}day19.txt";
//var day19Part1 = Day19.CountBeacons(day19File);
//Console.WriteLine($"Day19 part 1: {day19Part1.origin.Beacons.Count()}");
//Console.WriteLine($"Day19 part 2: {day19Part1.manhattan}");
//watch.Stop();
//Console.WriteLine($"Day19 time: {watch.Elapsed.TotalMilliseconds}ms");
//watch.Restart();
//var day20File = $"{directoryPath}day20.txt";
//var day20Part1 = Day20.RunImageTransform(day20File);
//Console.WriteLine($"Day20 part 1: {day20Part1.Count(_ => _.Value == 1)}");
//var day20Part2 = Day20.RunImageTransform(day20File, 50);
//Console.WriteLine($"Day20 part 2: {day20Part2.Count(_ => _.Value == 1)}");
//watch.Stop();
//Console.WriteLine($"Day20 time: {watch.Elapsed.TotalMilliseconds}ms");
//watch.Restart();
//var day21Part1 = Day21.Play(5, 9);
//Console.WriteLine($"Day21 part 1: {(day21Part1.dieRolls * day21Part1.losingScore)}");
//var day21Part2 = Day21.Play2(5, 9);
//Console.WriteLine($"Day21 part 2: {day21Part2}");
//watch.Stop();
//Console.WriteLine($"Day21 time: {watch.Elapsed.TotalMilliseconds}ms");
//watch.Restart();
//var day22File = $"{directoryPath}day22.txt";
//var day22Part1 = Day22.Initialize(day22File);
//Console.WriteLine($"Day22 part 1: {day22Part1}");
//var day22Part2 = Day22.Reboot(day22File);
//Console.WriteLine($"Day22 part 2: {day22Part2}");
//watch.Stop();
//Console.WriteLine($"Day22 time: {watch.Elapsed.TotalMilliseconds}ms");
//watch.Restart();
//var day23File = $"{directoryPath}day23.txt";
//var day23Part1 = Day23.Initialize(day23File);
////Console.WriteLine($"Day23 part 1: {day23Part1}");
//var day23Part2 = Day23.Reboot(day23File);
//Console.WriteLine($"Day23 part 2: {day23Part2}");
//watch.Stop();
//Console.WriteLine($"Day23 time: {watch.Elapsed.TotalMilliseconds}ms");
//watch.Restart();
//var day24File = $"{directoryPath}day24.txt";
//var day24Part1 = Day24.FindLargestModelNumber(day24File);
//Console.WriteLine($"Day24 part 1: {day24Part1}");
//var day25File = $"{directoryPath}day25.txt";
//var day25Part1 = Day25.CountMoves(day25File);
//Console.WriteLine($"Day25 part 1: {day25Part1}");
//totalTime.Stop();
//Console.WriteLine($"AoC 2021 total running time: {totalTime.Elapsed.TotalMilliseconds}ms");