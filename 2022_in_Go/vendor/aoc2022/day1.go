package aoc2022

import (
	"bufio"
	"log"
	"os"
	"sort"
	"strconv"
)

type Day1 struct{
	
}

func (d1 Day1) SolvePart1(filepath string) int {
	backpacks := (Day1).ParseInput(Day1{}, filepath)
	max := 0
	for _, v := range backpacks {
		if v.CalorieCount(v.Calories) > max {
			max = v.CalorieCount(v.Calories)
		}
	}
	return max
}

func (d1 Day1) SolvePart2(filepath string) int{
	backpacks := (Day1).ParseInput(Day1{}, filepath)
	sort.Slice(backpacks, func(i, j int) bool {
		return backpacks[i].CalorieCount(backpacks[i].Calories) > backpacks[j].CalorieCount(backpacks[j].Calories)
	})
	slice := backpacks[0:3]
	sum := 0
	for _, v := range slice {
		sum += v.CalorieCount(v.Calories)
	}
	return sum
}

func (d1 Day1) ParseInput(filepath string) []Backpack {
	content, err:= os.Open(filepath)
	if err != nil {
		log.Fatal(err)
	}
	defer content.Close()
	backpacks := []Backpack{}
	scanner := bufio.NewScanner(content)
	current := Backpack{}
	for scanner.Scan(){
		if scanner.Text() == "" {
			backpacks = append(backpacks, current)
			current = Backpack{}
		} else {
			i, err := strconv.Atoi(scanner.Text())
			if err != nil {
				log.Fatal(err)
			}
			current.Calories = append(current.Calories, i)
		}
	}
	backpacks = append(backpacks, current)
	return backpacks
}

type Backpack struct {
	Calories []int
}

func (re Backpack) CalorieCount([]int) int {
	sum := 0
	for _, v := range re.Calories {
		sum += v
	}
	return sum
}

