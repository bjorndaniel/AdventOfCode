namespace Advent2021;
public class Day6
{
   
    public static long CountLanternfish(string filename, int days)
    {
        var school = GetFishSchool(filename);
        while(days > 0)
        {
            long newFish = 0;
            if(school.Zero > 0)
            {
                newFish = school.Zero;
                school.Zero = 0;
            }
            school.Decrement();
            school.Eight = newFish;
            school.Six += newFish;
            days--;
        }
        return school.Count;    
    }

    private static SchoolOfFish GetFishSchool(string filename)
    {
        var school = new SchoolOfFish();
        var input = File.ReadAllText(filename);
        var days = input.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(_ => int.Parse(_));
        foreach (var day in days)
        {
            switch (day)
            {
                case 0:
                    school.Zero += 1;
                    break;
                case 1:
                    school.One += 1; 
                    break;
                case 2:
                    school.Two += 1;
                    break;
                case 3:
                    school.Three += 1;
                    break;
                case 4:
                    school.Four += 1;
                    break;
                case 5:
                    school.Five += 1;
                    break;
                case 6:
                    school.Six += 1;
                    break;
                case 7:
                    school.Seven += 1;
                    break;
                case 8:
                    school.Eight += 1;
                    break;

            }
        }
        return school;
    }

    public class SchoolOfFish
    {
        public long Zero { get; set; }
        public long One { get; set; }
        public long Two { get; set; }
        public long Three { get; set; }
        public long Four { get; set; }
        public long Five { get; set; }
        public long Six { get; set; }
        public long Seven { get; set; }
        public long Eight { get; set; }
        public long Count =>
            Zero + One + Two + Three + Four + Five + Six + Seven + Eight;
        public void Decrement()
        {
            Zero = One;
            One = Two;
            Two = Three;
            Three = Four;   
            Four = Five;
            Five = Six;
            Six = Seven;
            Seven = Eight;
            Eight = 0;
        }
    }
}
