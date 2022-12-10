package aoc2022

import (
	"bufio"
	"log"
	"os"
	"strings"
)

type Day2 struct{}

func (d2 Day2) SolvePart1(filepath string) int {
	rounds := (Day2).ParseInput(Day2{}, filepath, false)
	sum := 0
	for _, v := range rounds {
		sum += (Day2).GetMatchResult(Day2{}, v)
	}
	return sum
}

func (d2 Day2) SolvePart2(filepath string) int {
	rounds := (Day2).ParseInput(Day2{}, filepath, true)
	sum := 0
	for _, v := range rounds {
		sum += (Day2).GetMatchResult(Day2{}, v)
	}
	return sum
}

func (d2 Day2) GetMatchResult(round Round) int {

	switch round.Opponent {
	case Rock:
		switch round.Player {
		case Rock:
			return int(Draw) + int(Rock)
		case Paper:
			return int(PlayerWin) + int(Paper)
		case Scissors:
			return int(Scissors)
		case PlayerWin:
			return int(PlayerWin) + int(Paper)
		case PlayerLose:
			return int(Scissors)
		case PlayerDraw:
			return int(Draw) + int(Rock)
		}
	case Paper:
		switch round.Player {
		case Rock:
			return int(Rock)
		case Paper:
			return int(Draw) + int(Paper)
		case Scissors:
			return int(PlayerWin) + int(Scissors)
		case PlayerWin:
			return int(PlayerWin) + int(Scissors)
		case PlayerLose:
			return int(Rock)
		case PlayerDraw:
			return int(Draw) + int(Paper)
		}
	case Scissors:
		switch round.Player {
		case Rock:
			return int(PlayerWin) + int(Rock)
		case Paper:
			return int(Paper)
		case Scissors:
			return int(Draw) + int(Scissors)
		case PlayerWin:
			return int(PlayerWin) + int(Rock)
		case PlayerLose:
			return int(Paper)
		case PlayerDraw:
			return int(Draw) + int(Scissors)
		}
	}
	return 0
}

func (d2 Day2) ParseInput(filepath string, round2 bool) []Round {
	content, err := os.Open(filepath)
	if err != nil {
		log.Fatal(err)
	}
	defer content.Close()
	rounds := []Round{}
	scanner := bufio.NewScanner(content)
	for scanner.Scan() {
		p := strings.Split(scanner.Text(), " ")
		rounds = append(rounds, (Day2).CreateRPS(Day2{}, p[0], p[1], round2))
	}
	return rounds
}

func (d2 Day2) CreateRPS(opponent string, player string, round2 bool) Round {
	o := (Day2).CreateOpponent(Day2{}, opponent)
	p := (Day2).CreatePlayer(Day2{}, player, round2)
	return Round{Opponent: o, Player: p}
}

func (d2 Day2) CreatePlayer(player string, round2 bool) RPS {
	switch player {
	case "X":
		if round2 {
			return PlayerLose
		}
		return Rock
	case "Y":
		if round2 {
			return PlayerDraw
		}
		return Paper
	case "Z":
		if round2 {
			return PlayerWin
		}
		return Scissors
	}
	return NA
}

func (d2 Day2) CreateOpponent(opponent string) RPS {
	switch opponent {
	case "A":
		return Rock
	case "B":
		return Paper
	case "C":
		return Scissors
	}
	return NA
}

type RPS int64

const (
	Rock       RPS = 1
	Paper          = 2
	Scissors       = 3
	PlayerDraw     = 4
	PlayerLose     = 5
	PlayerWin      = 6
	NA
)

type RoundResult int64

const (
	Loss RoundResult = 0
	Draw             = 3
	Win              = 6
)

type Round struct {
	Opponent RPS
	Player   RPS
}
