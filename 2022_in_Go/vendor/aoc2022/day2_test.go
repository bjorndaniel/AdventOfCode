package aoc2022

import "testing"


func TestParseInputD2(t *testing.T){
	rounds := (Day2).ParseInput(Day2{}, "../../puzzles/day2-test.txt", false)
	got := len(rounds)
	want := 3
	got1 := rounds[0].Player
	want1 := Paper
	got2 := rounds[1].Player
	want2 := Rock
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
	if int(got1) != want1 {
		t.Errorf("got %d want %d", got1, want1)
	}
	if got2 != want2 {
		t.Errorf("got %d want %d", got2, want2)
	}
}

func TestSolveTestPart1D2(t *testing.T){
	got := (Day2).SolvePart1(Day2{}, "../../puzzles/day2-test.txt")
	want := 15
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}

func TestSolveTestPart2D2(t *testing.T){
	got := (Day2).SolvePart2(Day2{}, "../../puzzles/day2-test.txt")
	want := 12
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}