package aoc2022

import (
	"bufio"
	"log"
	"os"
	"unicode"
)

type Day3 struct{}

func (d3 Day3) SolvePart1(filepath string) int {
	rucksacks := (Day3).ParseInput(Day3{}, filepath)
	sum := 0
	for _, v := range rucksacks {
		i := Intersection(v.Compartments()[0], v.Compartments()[1])	
		if len(i) > 0 {
			for _, v := range i {
				sum += GetCharValue(v)
			}
		}	
	}
	return sum
}

func (d3 Day3) SolvePart2(filepath string) int {
	rucksacks := (Day3).ParseInput(Day3{}, filepath)
	chunks := GetChunks(rucksacks, 3)
	sum := 0
	for _, v := range chunks {
		b := GetBadge(v)
		sum += GetCharValue(b)
	}
	return sum
}


func (d3 Day3) ParseInput (filepath string) []Rucksack {
	content, err := os.Open(filepath)
	if err != nil {
		log.Fatal(err)
	}
	defer content.Close()
	rucksacks := []Rucksack{}
	scanner := bufio.NewScanner(content)
	for scanner.Scan() {
		rucksacks = append(rucksacks, Rucksack{scanner.Text()})
	}
	return rucksacks
}

type Rucksack struct {
	Contents string
}

func (r Rucksack) Compartments() []string {
	length := len(r.Contents)
	cLength := length / 2
	return []string{r.Contents[0:cLength], r.Contents[cLength:length]}
}

//From https://nilpointer.net/programming-language/golang/intersection-of-two-slice-in-golang/
func Intersection(slice1, slice2 string) (inter []rune) {
    hash := make(map[rune]bool)
    for _, e := range slice1 {
        hash[e] = true
    }
    for _, e := range slice2 {
        if hash[e] {
            inter = append(inter, e)
        }
    }
    inter = RemoveDups(inter)
    return
}

//From https://nilpointer.net/programming-language/golang/intersection-of-two-slice-in-golang/
func RemoveDups(elements []rune) (nodups []rune) {
    encountered := make(map[rune]bool)
    for _, element := range elements {
        if !encountered[element] {
            nodups = append(nodups, element)
            encountered[element] = true
        }
    }
    return
}

func GetCharValue(char rune) int {
	if unicode.IsUpper(char) {
		return int(char) - 38
	}
	return int(char) - 96
}

func GetChunks(target []Rucksack, chunkSize int) [][]Rucksack {
	var chunks [][]Rucksack
	for i := 0; i < len(target); i += chunkSize {
		end := i + chunkSize
		if end > len(target) {
			end = len(target)
		}
		chunks = append(chunks, target[i:end])
	}
	return chunks
}

func GetBadge(rucksacks []Rucksack) rune {
	first := Intersection(rucksacks[0].Contents, rucksacks[1].Contents)
	second := Intersection(string(first), rucksacks[2].Contents)
	return second[0]
}