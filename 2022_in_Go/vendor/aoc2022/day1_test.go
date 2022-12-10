package aoc2022

import "testing"

func TestCalorieCount(t *testing.T) {
	backpack := Backpack{Calories: []int{1, 2, 3, 4, 5}}
	got:= backpack.CalorieCount(backpack.Calories)
	want:= 15

	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}

func TestParseInput(t *testing.T){
	backpacks := (Day1).ParseInput(Day1{}, "../../puzzles/day1-test.txt")
	got := len(backpacks)
	want := 5

	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}

func TestSolveTest(t *testing.T){
	got := (Day1).SolvePart1(Day1{}, "../../puzzles/day1-test.txt")
	want := 24000

	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}