package main

import (
	"aoc2022"
	"fmt"
)

func main() {
	day1Part1 := (aoc2022.Day1).SolvePart1(aoc2022.Day1{}, "./puzzles/day1.txt")
	fmt.Println(day1Part1)
	day1Part2 := (aoc2022.Day1).SolvePart2(aoc2022.Day1{}, "./puzzles/day1.txt")
	fmt.Println(day1Part2)
	day2part1 := (aoc2022.Day2).SolvePart1(aoc2022.Day2{}, "./puzzles/day2.txt")
	fmt.Println(day2part1)
	day2part2 := (aoc2022.Day2).SolvePart2(aoc2022.Day2{}, "./puzzles/day2.txt")
	fmt.Println(day2part2)
	day3part1 := (aoc2022.Day3).SolvePart1(aoc2022.Day3{}, "./puzzles/day3.txt")
	fmt.Println(day3part1)
	day3part2 := (aoc2022.Day3).SolvePart2(aoc2022.Day3{}, "./puzzles/day3.txt")
	fmt.Println(day3part2)
}
