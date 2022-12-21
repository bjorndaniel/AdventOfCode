var watch = new Stopwatch();
var totalTime = new Stopwatch();
totalTime.Start();

var day1File = $"{Helpers.DirectoryPath}Day1.txt";
watch.Start();
var day1Part1 = Day1.SolvePart1(day1File);
watch.Stop();
Console.WriteLine($"Day1 part 1: {day1Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day1Part2 = Day1.SolvePart2(day1File);
watch.Stop();
Console.WriteLine($"Day1 part 2: {day1Part2} in {watch.ElapsedMilliseconds}ms ");

var day2File = $"{Helpers.DirectoryPath}Day2.txt";
watch.Restart();
var day2Part1 = Day2.SolvePart1(day2File);
watch.Stop();
Console.WriteLine($"Day2 part 1: {day2Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day2Part2 = Day2.SolvePart2(day2File);
watch.Stop();
Console.WriteLine($"Day2 part 2: {day2Part2} in {watch.ElapsedMilliseconds}ms");

var day3File = $"{Helpers.DirectoryPath}Day3.txt";
watch.Restart();
var day3Part1 = Day3.SolvePart1(day3File);
watch.Stop();
Console.WriteLine($"Day3 part 1: {day3Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day3Part2 = Day3.SolvePart2(day3File);
watch.Stop();
Console.WriteLine($"Day3 part 2: {day3Part2} in {watch.ElapsedMilliseconds}ms");

var day4File = $"{Helpers.DirectoryPath}Day4.txt";
watch.Restart();
var day4Part1 = Day4.SolvePart1(day4File);
watch.Stop();
Console.WriteLine($"Day4 part 1: {day4Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day4Part2 = Day4.SolvePart2(day4File);
watch.Stop();
Console.WriteLine($"Day4 part 2: {day4Part2} in {watch.ElapsedMilliseconds}ms");

var day5File = $"{Helpers.DirectoryPath}Day5.txt";
watch.Restart();
var day5Part1 = Day5.SolvePart1(day5File);
watch.Stop();
Console.WriteLine($"Day5 part 1: {day5Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day5Part2 = Day5.SolvePart2(day5File);
watch.Stop();
Console.WriteLine($"Day5 part 2: {day5Part2} in {watch.ElapsedMilliseconds}ms");

var day6input = $"mnlnvlljqqccznnjtjljbllrtllwwpmmhjjbbzppnndmmsppdqqwvvstvssgmsggmlmttnvvfbbdsssnzzbssjrsjjpmpvmmcjjwsssndsslwsswtwnwrrslshhvzzsppffmpfmmfvfpfpsssqpqzpqqcjcjnjcnnzbzjzpzbpbnbwbcctvvhgvgsvvpwwvjjvqjjjdqqrmrmqmsqszqsqpsqslsddhbhcbhbchcvvjvjcjnccdbcdcrddldblbffhvffpvpzpvvmvfmmwhwqhqvhqhmmpdmmlbmbgmbbrqqpmqqcvcmvvcncllptltvtdtbbqzzcggjgsjjvjsvvgmgffqhqqgpptspsffvdvbbhqhzzllvvjbvvbpppggfpgptgtvvzdvdzdgzgccmmphpmhppldlnlpnnhghhrrgwrwssnllmpllbvbvqvtvhtvvmnvvpgvgfvggtztpthhcfhfqfhhnhtnhhljhjppqjjffgjggrwrjjhphzhtztggwswnwzzvbzzmmtrtqtjqttwlwmmmmnddmnddwvvcllgrgfgzznwnsswjjhwwspsbbvzzqvzvbvcvmmtltnlnfnfnwwsvwwpswppjhjdhhmbbblfbfwffwvvjgghwhzhjzzrttwhwjhjchjhggdrgdgmmsjsfstftvtmtctggcwgcgzccgzgsgrgmmjhhqzzrmrttgtgfgcffvsfvvslsvvpqvvnjnrrwdwcwcnnhllwpwdpwdpdqqtwtftdftddppncpnccllqqrffpssgvsvtvmvssrbrhbbzggtssdsvddqfdfjfhjhdjhhncnddfpdfdmmrddncnvcnvccgvvhzvzwztwzwtzwtwqttrlrvrddztzrzcccgmmqgqjgqjgjqqspqqpjppbggchcqcpqqgbgdbbspbbrbhrhzhqhrqhhhtbhhvshspsvsggjdjwjwvjvdvwddjggmrrbnrrztthlllhlclbclbbhpbhphjhccdwccdbbjrbjjmrjrhhnlnjllltwltlmmlqlnqntqnntsnsqqvtvwvgwvwnncgcdctddnttfjfqfttrhrjhjqqcnnsmnmgmqgmgbblcctntrntnccnvvmpmjjvfjfrrbpbttsbttvnttmnnjdnndnzntnrnwrwcctllvhvqhhddmzztppphghphzzglzlnnfccrfffvvhllpspwssstwstsvttcrtccfssbccdjdqdfqddrbrqbbtllmmsfmfcmmzwzpznpnttjgtgbbdtdvdwwpmphpprsrjrbrqbqwwljlslrlrhhpchpcprcrtcrcfcfssndsspddcjjjfmfqfggmssnhsnhnpncnfnmffdrdjdhjjrgjrjgjqgjqqlmlljffbcfcrrrzwwftwtrrpgpprqqmrmhmwhwmmcrrhqrqwqppwjwggpdpgpvgvzvttqlljhhbvbhblhhcsssvmsmppcvpvrvzvbvtbtssplpgptgtnthhvwhvwwfvvfwwmtwmwfmfgmgnmnllgsgmsgswwhqhhhzqhhfwwnttmfmrrfnnpbpssvbsshqhqvhqqbmbpmbmqqjtjqjvjtjjhtttpzphpqqwqfqttqqhfhbfhfwwcpcpssdvvzhzwwqddjzdjdldlggvnvlnlbljlqjjmcczbccznnlnslswlwplpttvrvllfwflftllhclldhlhddbvvpvzpvpmmrccvgvdvqqjcqqwvvnjjlbbjwjrjhhlzhlhttljlcjjsnsgngrnntzzbsbmbsbrbdrdppjrjlrrjljqlqhqqnqsgdvhpgdhmnslqtjclmcfzrmgmlfnjbzznfgfprvwprwdbcgfcclmspgnzpbshwjbqvhzhrhswjzbfvnmcjtfvqbwmjpvfvctpmwsspdbtvfhfdfzjdpqnvslgmdvrnflzwzcnzmvzsvznwhpwtjwnqdgrrttmmdwzbbnwtllpbffrgtpjjjwltqrcbqcttdwnfjpmhdsbbpqmstjqchgjvfrmrbgqrlstnbdnzzzbzbsmsnnsssswmqhcbswtjhmcgnwmcclhzjqjzqcpbzgdzjgqzpqbmvvhtcznfrhdndswfvfhtfpdpszpjqrlwfdscvcngftwqmfttjtjrlbgcwvcjwsstqmcblmjzsgtgrqnqqvhzhvsphjmbcpfcznlcqldcvhlsvggbjngmhspwwqhlwstslvwmmbwqdmrgdvvnlstmjllhzscrhzjtmnsjfbndnlmzqbzgdgbcqchnbvwsftjtznnbsnvsgzpdzdqznjsslrlfnccdhwsljhczggvmgqswjltmrqqmwtbzmtdzhpjcvmwsscsdzpfnwlcrrdgzqqdmgwdlzvvvjcqsgpcwvrdnrstpcmgfjnjffbfmgzjthhllzrlsjtnqfppltbrlnqnjvqlvtpqvsbfgmmlcdzhgmzzqjwtqtzmpwwddbqrqnfzzpsjglsjddsslwwlrttzfzplmwsswlnvrvwwcgddjwcmvsjjbfgcfjmthfbpmcwjptchhnsmzttjqnwzdljffghhqdcwzwgbvfsmwqdbtblphdgcmbhprtbccjbzqrpvjdbnsmlwfntvjgptnshzmddwbhgwsnfrjbpqqwlsfdpnmmnnwhdmhzvjcmddbdnjzfzvffbgdqgwbggprcrbzwhvtzzgbhhcscrlmfgztfswjbsnwsmdfwlntwjzvlwhvlrfzszllmflmrsrcfnncvszvgdmmnvgrqnjhljcnrrhpdhffwmrsqfnbcpfdmmgppwjbjrwdfmpcrbznrjnbmssszhnlbpgmlczhhcdtgjqcbqrvzcbpgrftfhzdqthhspwnqqswntlpcmmqtcszngpggqvfjnmnprhdfjsngwrncjcmqdmjhpdlfnshpdlnlfpcnprwjgdvwwbvhvsbrfjtqsqjnvcpfdsrnfwmrrbtcvcqzflhdlbpcthzthdjzsrvwgbhjvhbtrngthfrszlvrbtnscsqlblcwlngslspcrhqzzdlzcdbhqdhthlpmdrntbhnqtwtzwpndbgphpsllbvgqjtmszdvjpgttzcmbwgrgdwmsbfgvgbbcmsnhvmnsbcsthsdwdqtghpdclfbglbdjgnnnwhmzzvnhbmgfbmvqwvwqhdswgtzslspmbmznnwdmjbzbddhzchtdzdzgwtlmlpmwrqvghpfwhvfjrtvmjwgjjnwdwnpdcqjdmcctjfcrdgpvczvnhlrbfmqgnrhmdwsrmmpqhvwgqgbqccpznpjfldwpntnvzgdfzljmtqwvfnrdsjsqgbvzjsczwwjggqtrpvwgqggwwhqggtgqfjmzsmjvdhdwqggbgnftpqqqlsfpflwrdpjwnhfdpchcgntjshgtwnwrnpsvwmplvqcltbgrcpflpgzbqfclghfnwjchnbgjnplgldmphdplvjrnrtzcmlftprsnmrjmnffpqjlvqlztbwprjwprrmmgzhjgdnhbfdrwjtvsvnbqhtfhbqgdrvcwlwfdbbcthgvttpvrwrqmpmrmvgpjzwlpvbqcvgccpgfddjbwhrvgqmjqzwgghllrtrblcpbttmcrgjsftlqhjfvqnbhmhbhngwnfqtgdttstzvmstrqcpfjrdgtsdbqqccqvbhpwhnpmpqgntfqszndjrmfhlqjqjbqvjtlfmrnnzzrtqlzzhjfqmmsmzvzrcplmfjpcmpfmpzbsbrmbnbnjqwjcfwnnwwrwzvvrsvhvrwnhlmwqjdztqthcwnwrlbjdflfsbplbwfmzqqnpwvzjbcfdgztpwttlrvhlfzzsfltpqwcpnzlsqgvwnqfvgclrfvssfcfmfvvjsndrhqdbrqfggfhjbvdvvmgpglqzwgjdmqtscjpfhgsbshghtmftrrhznttpzrzcsmrrvzdjmwtmbcbpqbsdmqzqdzrncwzmptltvdphsltfrhbbrdzbnbsqdhfvrgvmbgfvwblsjvfphlpzfvsllwnqjmbhngzzslcdmdzfgrgscbzggrzmbmwlzbnpzcvsbsfgdpnwzljsf";
watch.Restart();
var day6Part1 = Day6.SolvePart1(day6input);
watch.Stop();
Console.WriteLine($"Day6 part 1: {day6Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day6Part2 = Day6.SolvePart2(day6input);
watch.Stop();
Console.WriteLine($"Day6 part 2: {day6Part2} in {watch.ElapsedMilliseconds}ms");

var day7File = $"{Helpers.DirectoryPath}Day7.txt";
watch.Restart();
var day7Part1 = Day7.SolvePart1(day7File);
watch.Stop();
Console.WriteLine($"Day7 part 1: {day7Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day7Part2 = Day7.SolvePart2(day7File);
watch.Stop();
Console.WriteLine($"Day7 part 2: {day7Part2} in {watch.ElapsedMilliseconds}ms");

var day8File = $"{Helpers.DirectoryPath}Day8.txt";
watch.Restart();
var day8Part1 = Day8.SolvePart1(day8File);
watch.Stop();
Console.WriteLine($"Day8 part 1: {day8Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day8Part2 = Day8.SolvePart2(day8File);
watch.Stop();
Console.WriteLine($"Day8 part 2: {day8Part2} in {watch.ElapsedMilliseconds}ms");

var day9File = $"{Helpers.DirectoryPath}Day9.txt";
watch.Restart();
var day9Part1 = Day9.SolvePart1(day9File);
watch.Stop();
Console.WriteLine($"Day9 part 1: {day9Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day9Part2 = Day9.SolvePart2(day9File);
watch.Stop();
Console.WriteLine($"Day9 part 2: {day9Part2.Count()} in {watch.ElapsedMilliseconds}ms");

var day10File = $"{Helpers.DirectoryPath}Day10.txt";
watch.Restart();
var day10Part1 = Day10.SolvePart1(day10File, 220);
watch.Stop();
Console.WriteLine($"Day10 part 1: {day10Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day10Part2 = Day10.SolvePart2(day10File, int.MaxValue);
watch.Stop();
Console.WriteLine($"Day10 part 2 in {watch.ElapsedMilliseconds}ms");
PrintDay10(day10Part2);

var day11File = $"{Helpers.DirectoryPath}Day11.txt";
watch.Restart();
var day11Part1 = Day11.SolvePart1(day11File, 20);
watch.Stop();
Console.WriteLine($"Day11 part 1: {day11Part1.result} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day11Part2 = Day11.SolvePart2(day11File, 10000);
watch.Stop();
Console.WriteLine($"Day11 part 2: {day11Part2.result} in {watch.ElapsedMilliseconds}ms");

var day12File = $"{Helpers.DirectoryPath}Day12.txt";
watch.Restart();
var day12Part1 = Day12.SolvePart1(day12File);
watch.Stop();
Console.WriteLine($"Day12 part 1: {day12Part1.result} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day12Part2 = Day12.SolvePart2(day12File);
watch.Stop();
Console.WriteLine($"Day12 part 2: {day12Part2} in {watch.ElapsedMilliseconds}ms");

var day13File = $"{Helpers.DirectoryPath}Day13.txt";
watch.Restart();
var day13Part1 = Day13.SolvePart1(day13File);
watch.Stop();
Console.WriteLine($"Day13 part 1: {day13Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day13Part2 = Day13.SolvePart2(day13File);
watch.Stop();
Console.WriteLine($"Day13 part 2: {day13Part2} in {watch.ElapsedMilliseconds}ms");

var day14File = $"{Helpers.DirectoryPath}Day14.txt";
watch.Restart();
var day14Part1 = Day14.SolvePart1(day14File);
watch.Stop();
Console.WriteLine($"Day14 part 1: {day14Part1.result} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day14Part2 = Day14.SolvePart2(day14File);
watch.Stop();
Console.WriteLine($"Day14 part 2: {day14Part2.result} in {watch.ElapsedMilliseconds}ms");

var day15File = $"{Helpers.DirectoryPath}Day15.txt";
watch.Restart();
var day15Part1 = Day15.SolvePart1(day15File, 2000000);
watch.Stop();
Console.WriteLine($"Day15 part 1: {day15Part1} in {watch.ElapsedMilliseconds}ms");
//watch.Restart();
//var day15Part2 = Day15.SolvePart2(day15File, 0, 4000000);
//watch.Stop();
//Console.WriteLine($"Day15 part 2: {day15Part2} in {watch.ElapsedMilliseconds}ms");

//var Day16File = $"{Helpers.DirectoryPath}Day16.txt";
//watch.Restart();
//var Day16Part1 = Day16.SolvePart1(Day16File, 2000000);
//watch.Stop();
//Console.WriteLine($"Day16 part 1: {Day16Part1} in {watch.ElapsedMilliseconds}ms");
//watch.Restart();
//var Day16Part2 = Day16.SolvePart2(Day16File, 0, 4000000);
//watch.Stop();
//Console.WriteLine($"Day16 part 2: {Day16Part2} in {watch.ElapsedMilliseconds}ms");

var day17File = $"{Helpers.DirectoryPath}Day17.txt";
watch.Restart();
var day17Part1 = Day17.SolvePart1(day17File, 2022, new Printer());
watch.Stop();
Console.WriteLine($"Day17 part 1: {day17Part1} in {watch.ElapsedMilliseconds}ms");
//watch.Restart();
//var day17Part2 = Day17.SolvePart1(day17File, 1000000000000, new Printer());
//watch.Stop();
//Console.WriteLine($"Day17 part 2: {day17Part2} in {watch.ElapsedMilliseconds}ms");

var day18File = $"{Helpers.DirectoryPath}Day18.txt";
watch.Restart();
var day18Part1 = Day18.SolvePart1(day18File);
watch.Stop();
Console.WriteLine($"Day18 part 1: {day18Part1.result} in {watch.ElapsedMilliseconds}ms");
//watch.Restart();
//var day18Part2 = Day18.SolvePart2(day18File);
//watch.Stop();
//Console.WriteLine($"Day15 part 2: {day18Part2} in {watch.ElapsedMilliseconds}ms");

//var day19File = $"{Helpers.DirectoryPath}day19.txt";
//watch.Restart();
//var day19Part1 = day19.SolvePart1(day19File);
//watch.Stop();
//Console.WriteLine($"day19 part 1: {day19Part1.result} in {watch.ElapsedMilliseconds}ms");
//watch.Restart();
//var day19Part2 = day19.SolvePart2(day19File);
//watch.Stop();
//Console.WriteLine($"Day15 part 2: {day19Part2} in {watch.ElapsedMilliseconds}ms");

var day20File = $"{Helpers.DirectoryPath}Day20.txt";
watch.Restart();
var day20Part1 = Day20.SolvePart1(day20File, new Printer());
watch.Stop();
Console.WriteLine($"Day20 part 1: {day20Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day20Part2 = Day20.SolvePart2(day20File, new Printer());
watch.Stop();
Console.WriteLine($"Day20 part 2: {day20Part2} in {watch.ElapsedMilliseconds}ms");

var day21File = $"{Helpers.DirectoryPath}Day21.txt";
watch.Restart();
var day21Part1 = Day21.SolvePart1(day21File);
watch.Stop();
Console.WriteLine($"Day21 part 1: {day21Part1} in {watch.ElapsedMilliseconds}ms");
watch.Restart();
var day21Part2 = Day21.SolvePart2(day21File, new Printer());
watch.Stop();
Console.WriteLine($"Day21 part 2: {day21Part2} in {watch.ElapsedMilliseconds}ms");

totalTime.Stop();
Console.WriteLine($"AoC 2022 total running time: {totalTime.Elapsed.TotalMilliseconds}ms");

void PrintDay10(char?[,] chars)
{
    for (int row = 0; row < chars.GetLength(0); row++)
    {
        var sb = new StringBuilder();
        for (int col = 0; col < chars.GetLength(1); col++)
        {
            sb.Append(chars[row, col] ?? '.');
        }
        Console.WriteLine(sb.ToString());
    }
    Console.WriteLine("");
}