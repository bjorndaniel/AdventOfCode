package aoc2022

import "testing"


func TestParseInputD3(t *testing.T){
	rucksacks := (Day3).ParseInput(Day3{}, "../../puzzles/day3-test.txt")
	got := rucksacks[0].Compartments()[0]
	want := "vJrwpWtwJgWr"
	got1 := rucksacks[0].Compartments()[1]
	want1 := "hcsFMMfFFhFp"
	got2 := rucksacks[2].Compartments()[0]
	want2 := "PmmdzqPrV"
	if got != want {
		t.Errorf("got %s want %s", got, want)
	}
	if got1 != want1 {
		t.Errorf("got %s want %s", got1, want1)
	}
	if got2 != want2 {
		t.Errorf("got %s want %s", got2, want2)
	}

}

func TestGetCharacter(t *testing.T) {
	got := GetCharValue('a')
	want := 1
	got1 := GetCharValue('A')
	want1 := 27
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
	if got1 != want1 {
		t.Errorf("got %d want %d", got1, want1)
	}
	got2 := GetCharValue('L')
	want2 := 38
	if got2 != want2 {
		t.Errorf("got %d want %d", got2, want2)
	}
}

func TestGetChunks(t *testing.T) {
	input := (Day3).ParseInput(Day3{}, "../../puzzles/day3-test.txt")
	want:= 2
	got := len(GetChunks(input, 3))
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}

func TestSolveTestPart1D3(t *testing.T){
	got := (Day3).SolvePart1(Day3{}, "../../puzzles/day3-test.txt")
	want := 157
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}

func TestSolveTestPart2D3(t *testing.T){
	got := (Day3).SolvePart2(Day3{}, "../../puzzles/day3-test.txt")
	want := 70
	if got != want {
		t.Errorf("got %d want %d", got, want)
	}
}